using PavlovShackStats.Data;
using PavlovShackStats.Models;
using System.Collections.ObjectModel;
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
            var query =
                from playerStats in _dbContext.MatchersPlayerStats
                join player in _dbContext.Players on playerStats.PlayerId equals player.PlayerId
                group playerStats by new { player.Name } into g
                select new
                {
                    PlayerName = g.Key.Name,
                    Kills = g.Sum(_ => _.Kill),
                    Death = g.Sum(_ => _.Death),
                    Assist = g.Sum(_ => _.Asssistant),
                    HeadShot = g.Sum(_ => _.Headshot),
                    BombDefused = g.Sum(_ => _.BombDefused),
                    BombPlanted = g.Sum(_ => _.BombPlanted),
                    TeamKill = g.Sum(_ => _.TeamKill)
                };

            return query;
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

        public object GetPlayerStats(string playerName)
        {
            var query =
                from playerStats in _dbContext.MatchersPlayerStats
                join player in _dbContext.Players on playerStats.PlayerId equals player.PlayerId
                where player.Name == playerName
                group playerStats by new { player.Name } into g
                select new
                {
                    PlayerName = g.Key.Name,
                    Kills = g.Sum(_ => _.Kill),
                    Death = g.Sum(_ => _.Death),
                    Assist = g.Sum(_ => _.Asssistant),
                    HeadShot = g.Sum(_ => _.Headshot),
                    BombDefused = g.Sum(_ => _.BombDefused),
                    BombPlanted = g.Sum(_ => _.BombPlanted),
                    TeamKill = g.Sum(_ => _.TeamKill)
                };

            return query;
        }

        public object GetPlayerStats(int playerId)
        {
            var query =
                from playerStats in _dbContext.MatchersPlayerStats
                join player in _dbContext.Players on playerStats.PlayerId equals player.PlayerId
                where player.PlayerId == playerId
                group playerStats by new { player.Name } into g
                select new
                {
                    PlayerName = g.Key.Name,
                    Kills = g.Sum(_ => _.Kill),
                    Death = g.Sum(_ => _.Death),
                    Assist = g.Sum(_ => _.Asssistant),
                    HeadShot = g.Sum(_ => _.Headshot),
                    BombDefused = g.Sum(_ => _.BombDefused),
                    BombPlanted = g.Sum(_ => _.BombPlanted),
                    TeamKill = g.Sum(_ => _.TeamKill)
                };

            return query;
        }

        public object GetPlayerMatches(string playerName)
        {
            var query =
                from match in _dbContext.Matchers
                join map in _dbContext.Maps on match.MapId equals map.MapId
                join gameMode in _dbContext.GameModes on match.GameModeId equals gameMode.GameModeId
                join playerStats in _dbContext.MatchersPlayerStats on match.MatchId equals playerStats.MatchId
                join player in _dbContext.Players on playerStats.PlayerId equals player.PlayerId
                where player.Name == playerName
                select new
                {
                    match.MatchId,
                    PlayerName = player.Name,
                    MapName = map.Name,
                    GameMode = gameMode.Name,
                    match.Team0Score,
                    match.Team1Score,
                    playerStats.TeamId,
                    PlayersMatch = match.PlayerCount,
                    match.FinishedTime,
                };

            return query;
        }

        public object GetPlayerMatches(int playerId)
        {
            var query =
                from match in _dbContext.Matchers
                join map in _dbContext.Maps on match.MapId equals map.MapId
                join gameMode in _dbContext.GameModes on match.GameModeId equals gameMode.GameModeId
                join playerStats in _dbContext.MatchersPlayerStats on match.MatchId equals playerStats.MatchId
                join player in _dbContext.Players on playerStats.PlayerId equals player.PlayerId
                where player.PlayerId == playerId
                select new
                {
                    match.MatchId,
                    PlayerName = player.Name,
                    MapName = map.Name,
                    GameMode = gameMode.Name,
                    match.Team0Score,
                    match.Team1Score,
                    playerStats.TeamId,
                    PlayersMatch = match.PlayerCount,
                    match.FinishedTime,
                };

            return query;
        }

        public IQueryable<object> GetMatches()
        {
            var query =
                from match in _dbContext.Matchers
                join map in _dbContext.Maps on match.MapId equals map.MapId
                join gameMode in _dbContext.GameModes on match.GameModeId equals gameMode.GameModeId
                orderby match.FinishedTime descending
                select new
                {
                    match.MatchId,
                    MapName = map.Name,
                    GameMode = gameMode.Name,
                    match.Team0Score,
                    match.Team1Score,
                    PlayersMatch = match.PlayerCount,
                    match.FinishedTime,
                };

            return query;
        }

        public object ListPlayers()
        {
            return from player in _dbContext.Players
                   select new {  player.PlayerId, player.Name };
        }

        public object GetMatch(int matchId)
        {
            var query =
                from match in _dbContext.Matchers
                join map in _dbContext.Maps on match.MapId equals map.MapId
                join gameMode in _dbContext.GameModes on match.GameModeId equals gameMode.GameModeId
                where match.MatchId == matchId
                select new
                {
                    match.MatchId,
                    MapName = map.Name,
                    GameMode = gameMode.Name,
                    match.Team0Score,
                    match.Team1Score,
                    PlayersMatch = match.PlayerCount,
                    match.FinishedTime,
                };

            return query;
        }

        public object GetMatch(DateTime matchDate)
        {
            var query =
                from match in _dbContext.Matchers
                join map in _dbContext.Maps on match.MapId equals map.MapId
                join gameMode in _dbContext.GameModes on match.GameModeId equals gameMode.GameModeId
                where match.FinishedTime == matchDate
                select new
                {
                    match.MatchId,
                    MapName = map.Name,
                    GameMode = gameMode.Name,
                    match.Team0Score,
                    match.Team1Score,
                    PlayersMatch = match.PlayerCount,
                    match.FinishedTime,
                };

            return query;
        }

        public object GetMatchDetails(int matchId)
        {
            return new
            {
                Team0 = GetTeamMatchDetails(matchId, 0),
                Team1 = GetTeamMatchDetails(matchId, 1)
            };
        }

        private object GetTeamMatchDetails(int matchId, int teamId)
        {
            var query =
                from playerStats in _dbContext.MatchersPlayerStats
                join player in _dbContext.Players on playerStats.PlayerId equals player.PlayerId
                where playerStats.TeamId == teamId
                   && playerStats.MatchId == matchId
                select new
                {
                    player.Name,
                    playerStats.Kill,
                    playerStats.Death,
                    playerStats.Asssistant,
                    playerStats.Headshot,
                    playerStats.BombDefused,
                    playerStats.BombPlanted,
                    playerStats.TeamKill    
                };

            return query.ToList();
        }
    }
}
