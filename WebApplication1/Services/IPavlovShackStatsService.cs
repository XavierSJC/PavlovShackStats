using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IPavlovShackStatsService
    {
        void InsertNewStats(PavlovStats Stats, string Filename);
        object GetPlayersStats();
        object GetPlayerStats(string playerName);
        object GetPlayerStats(int playerId);
        object GetPlayerMatches(string playerName);
        object GetPlayerMatches(int playerId);
        object GetPlayersStats(DateTime sinceDate, DateTime untilDate, string playerName, string gameMode, int count);
        IQueryable<object> GetMatches();
        object ListPlayers();
        object GetMatch(int matchId);
        object GetMatch(DateTime matchDate);
        object GetMatchDetails(int matchId);
        object GetGameModeList();
    }
}
