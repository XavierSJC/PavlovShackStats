﻿using PavlovVR_Rcon;
using WebApplication1.Classes;
using WebApplication1.Models;
using WebApplication1.Models.Mod.io;

namespace WebApplication1.Services
{
    public class GameStatusService : IGameStatusService
    {
        private static int _updateMatchIntervalMs = 3000;
        private static int _updateMapsIntervalMs = 21600000;
        private static int _delayNextCommandIntervalMs = 333;
        private PavlovRcon _Rcon;
        private LiveMatch _liveMatch;
        private IList<object> _mapList;
        private IModIoService _modIoService;
        public GameStatusService(IModIoService modIoService, string gameServerAddress, int rconPort, string rconPassword)
        {
            _modIoService = modIoService;
            _Rcon = new(gameServerAddress, rconPort, rconPassword);
            _liveMatch = new LiveMatch();
            _mapList = new List<object>();
            _ = WatchMatchInfo();
            _ = WatchMapListInfo();
        }

        private async Task WatchMatchInfo()
        {
            while (true)
            {

                if (await IsConnected())
                {
                    try
                    {
                        ServerInfoReply serverInfoReply = await new ServerInfoCommand().ExecuteCommand(_Rcon);
                        await Task.Delay(_delayNextCommandIntervalMs);
                        InspectAllReply inspectAllReply = await new InspectAllCommand().ExecuteCommand(_Rcon);
                       
                        _liveMatch = GetLiveMatchInfo(serverInfoReply.ServerInfo, inspectAllReply.InspectList);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }

                }
                else
                {
                    _liveMatch = new LiveMatch();
                }
                await Task.Delay(_updateMatchIntervalMs);
            }
        }

        private async Task<bool> IsConnected()
        {
            if (_Rcon.Connected) return true;

            try
            {
                await _Rcon.Connect(CancellationToken.None);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

            return true;
        }

        private LiveMatch GetLiveMatchInfo(ServerInfoShack serverInfo, PlayerDetailShack[] playersDetail)
        {
            var result = new LiveMatch()
            {
                MapLabel = serverInfo.MapLabel,
                MapName = serverInfo.MapName,
                GameModeName = serverInfo.GameModeName,
                PlayerCount = serverInfo.PlayerCount,
                RoundState = serverInfo.RoundState,
                Round = serverInfo.Round ?? 0,
                ScoreTeam0 = serverInfo.Team0Score ?? 0,
                ScoreTeam1 = serverInfo.Team1Score ?? 0
            };

            foreach (var player in playersDetail)
            {
                if (player == null) continue;
                if (player.TeamId == 0)
                {
                    result.Team0.Add(new LivePlayer() { 
                        PlayerName =  player.PlayerName, 
                        Cash = player.Cash,
                        KDA = player.KDA,
                        Dead = player.Dead 
                        });
                }
                else
                {
                    result.Team1.Add(new LivePlayer() { 
                        PlayerName =  player.PlayerName, 
                        Cash = player.Cash,
                        KDA = player.KDA,
                        Dead = player.Dead 
                        });
                }
            }

            return result;
        }

        public object GetLiveMatchInfo()
        {
            return _liveMatch;
        }

        public object IsRunning()
        {
            return _Rcon.Connected;
        }

        public async Task WatchMapListInfo()
        {
            while (true)
            {
                if (await IsConnected())
                {
                    var mapListReply = await new MapListCommand().ExecuteCommand(_Rcon);
                    _mapList.Clear();
                    foreach (var map in mapListReply.MapList)
                    {
                        if (map.MapId.ToLower().StartsWith("ugc") && _modIoService.IsServiceConfigured())
                        {
                            Mod mapInfo = _modIoService.GetModDetailsByResourceId(int.Parse(map.MapId.Substring(3)));
                            _mapList.Add(new
                            {
                                id = mapInfo.id,
                                name = mapInfo.name,
                                mode = map.GameMode,
                                status = mapInfo.status,
                                visible = mapInfo.visible,
                                submitted_by = mapInfo.submitted_by,
                                logo = mapInfo.logo,
                                summary = mapInfo.summary,
                                profile_url = mapInfo.profile_url,
                                tags = mapInfo.tags
                            });
                        }
                        else
                        {
                            _mapList.Add(new 
                            {
                                name = map.MapId,
                                mode = map.GameMode
                            });
                        }
                    }
                }

                if (_mapList.Count <= 0)
                {
                    await Task.Delay(11000);
                }
                else
                {
                    await Task.Delay(_updateMapsIntervalMs);
                }
            }
        }

        public object GetListMaps()
        {
            return _mapList;
        }
    }
}
