using PavlovShackStats.Data;
using PavlovShackStats.Models;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class PavlovShackStatsService : IPavlovShackStatsService
    {
        private readonly PavlovShackStatsContext _dbContext;

        public PavlovShackStatsService(PavlovShackStatsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public object GetPlayersStats()
        {
            var PlayerStats = _dbContext.MatchersPlayerStats
                .GroupBy(p => p.Player)
                .Select(g => new
                {
                    Player = g.Select(p => p.Player.Name),
                    kill = g.Sum(p => p.Kill),
                    Death = g.Sum(p => p.Death),
                    Assist = g.Sum(p => p.Asssistant)
                });

            return PlayerStats;
        }

        public void InsertNewStats(PavlovStats Stats, string Filename)
        {
            if (MatchAlreadyExist(Filename))
            {
                throw new Exception("This match already exist in DB");
            }
            var match = new PavlovShackStats.Models.Match();            
            match.Map = GetMap(Stats.MapLabel);
            match.GameMode = GetGameMode(Stats.GameMode);          
            match.PlayerCount = Stats.PlayerCount;
            match.Team0Score = Stats.Team0Score;
            match.Team1Score = Stats.Team1Score;
            match.IsTeamMatch = Stats.BTeams;
            match.FinishedTime = ConvertFilenameToDateTime(Filename);

            match.MatchPlayerStats = new Collection<MatchPlayerStats>();
            foreach (var player in Stats.AllStats)
            {
                var matchPlayerStats = new MatchPlayerStats
                {
                    Player = GetPlayer(player.PlayerName),
                    TeamId = player.TeamId
                };
                foreach (var stats in player.Stats)
                {
                    switch (stats.StatType)
                    {
                        case "Kill":
                            matchPlayerStats.Kill = stats.Amount;
                            break;
                        case "Death":
                            matchPlayerStats.Death = stats.Amount;
                            break;
                        case "Assist":
                            matchPlayerStats.Asssistant = stats.Amount;
                            break;
                        case "Headshot":
                            matchPlayerStats.Headshot = stats.Amount;
                            break;
                        case "BombDefused":
                            matchPlayerStats.BombDefused = stats.Amount;
                            break;
                        case "BombPlanted":
                            matchPlayerStats.BombPlanted = stats.Amount;
                            break;
                        case "TeamKill":
                            matchPlayerStats.TeamKill = stats.Amount;
                            break;
                    }
                }
                match.MatchPlayerStats.Add(matchPlayerStats);
            }
            _dbContext.Matchers.Add(match);
            _dbContext.SaveChanges();
        }

        private DateTime ConvertFilenameToDateTime(string filename)
        {
            string strMatchTime = filename.Substring(filename.IndexOf('-') + 1);
            strMatchTime = Path.GetFileNameWithoutExtension(strMatchTime);
            return DateTime.ParseExact(strMatchTime,
                "yyyy.MM.dd-HH.mm.ss",
                System.Globalization.CultureInfo.InvariantCulture);
        }

        private Map GetMap(string name)
        {
            return _dbContext.Maps.Where(m => m.Name == name).FirstOrDefault() ?? new Map { Name = name };
        }

        private GameMode GetGameMode(string name)
        {
            return _dbContext.GameModes.Where(gm => gm.Name == name).FirstOrDefault() ?? new GameMode { Name = name };
        }

        private Player GetPlayer(string name)
        {
            return _dbContext.Players.Where(p => p.Name == name).FirstOrDefault() ?? new Player { Name = name };
        }

        private bool MatchAlreadyExist(string FinishedTime)
        {
            var finishedTime = ConvertFilenameToDateTime(FinishedTime);
            var match = _dbContext.Matchers.Where(m => m.FinishedTime.Equals(finishedTime)).FirstOrDefault();
            if (match != null)
            {
                return true;
            }
            return false;
        }
    }
}
