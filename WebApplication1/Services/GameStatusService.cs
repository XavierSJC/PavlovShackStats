using PavlovShackStats.Models;
using PavlovVR_Rcon;
using PavlovVR_Rcon.Models.Replies;
using WebApplication1.Classes;
using WebApplication1.Models;
using WebApplication1.Models.Mod.io;

namespace WebApplication1.Services
{
    public class GameStatusService : IGameStatusService
    {
        private readonly ILogger<GameStatusService> _logger;
        private static int _updateMatchIntervalMs = 3000;
        private static int _updateMapsIntervalMs = 21600000;
        private static int _delayNextCommandIntervalMs = 333;
        private PavlovRcon _Rcon;
        private LiveMatch _liveMatch;
        private IList<object> _mapList;
        private IModIoService _modIoService;

        public GameStatusService(ILogger<GameStatusService> logger, IModIoService modIoService, string gameServerAddress, int rconPort, string rconPassword)
        {
            _logger = logger;
            _modIoService = modIoService;
            _Rcon = new(gameServerAddress, rconPort, rconPassword);
            _liveMatch = new LiveMatch();
            _mapList = new List<object>();
            _ = WatchMatchInfo();
            _ = WatchMapListInfo();
        }

        private async Task WatchMatchInfo()
        {
            _logger.LogInformation("Starting monitoring matches of Pavlov Server");
            while (true)
            {
                if (await IsConnected())
                {
                    try
                    {
                        Classes.ServerInfoReply serverInfoReply = await new ServerInfoCommand().ExecuteCommand(_Rcon);
                        await Task.Delay(_delayNextCommandIntervalMs);
                        Classes.InspectAllReply inspectAllReply = await new InspectAllCommand().ExecuteCommand(_Rcon);
                       
                        _liveMatch = GetLiveMatchInfo(serverInfoReply.ServerInfo, inspectAllReply.InspectList);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning("Issue to fetch match information: {errorMessage}", ex.Message);
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
                _logger.LogWarning("Error to connect with RCON, please check the IP/Port/Password data: {errorMessage}", ex.Message);
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

        private void AddMapDetailsToList(MapList map)
        {
            _mapList.Add(new
            {
                name = map.MapId,
                mode = map.GameMode
            });
        }

        private void GetMapDetails(MapList[] mapList, bool isModIoAvailable)
        {
            foreach (var map in mapList)
            {
                if (map.MapId.ToLower().StartsWith("ugc") && isModIoAvailable)
                {
                    Mod mapInfo = _modIoService.GetModDetailsByResourceId(int.Parse(map.MapId.Substring(3)));
                    if (mapInfo != null)
                    {
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
                        _logger.LogWarning("Mod.io service failed to fetch details of map {mapId}.", map.MapId);
                        AddMapDetailsToList(map);
                    }
                }
                else
                {
                    _logger.LogInformation("Mod.io service unvailable.");
                    AddMapDetailsToList(map);
                }
            }
        }

        public async Task WatchMapListInfo()
        {
            _logger.LogInformation("Starting monitoring map list of Pavlov Server");
            while (true)
            {
                if (await IsConnected())
                {
                    try
                    {
                        var mapListReply = await new MapListCommand().ExecuteCommand(_Rcon);
                        _mapList.Clear();
                        bool usingModIo = _modIoService.IsConfigured();
                        GetMapDetails(mapListReply.MapList, usingModIo);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning("Exception when fetch map list details: {exceptionMessage}", ex.Message);
                    }
                }

                if (_mapList.Count <= 0)
                {
                    _logger.LogWarning("Was not possible fetch info about maps of the server. Trying again in 11 seconds");
                    await Task.Delay(11000);
                }
                else
                {
                    _logger.LogInformation("Maps information fetched. Waiting for the next update");
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
