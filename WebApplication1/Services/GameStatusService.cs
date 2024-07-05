using PavlovVR_Rcon;
using WebApplication1.Classes;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class GameStatusService : IGameStatusService
    {
        private static int _updateMatchIntervalMs = 5000;
        private static int _delayNextCommandIntervalMs = 333;
        private PavlovRcon _Rcon;
        private LiveMatch _liveMatch;

        public GameStatusService(string gameServerAddress, int rconPort, string rconPassword)
        {
            _Rcon = new(gameServerAddress, rconPort, rconPassword);
            _liveMatch = new LiveMatch();
            _ = WatchMatchInfo();
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
    }
}
