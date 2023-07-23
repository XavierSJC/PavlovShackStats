using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PavlovShackStats.Models
{
    public class Match
    {
        public int MatchId { get; set; }
        public int MapId { get; set; }
        public int GameModeId { get; set; }
        public int PlayerCount { get; set; }
        public int Team0Score { get; set; }
        public int Team1Score { get; set; }
        public bool IsTeamMatch { get; set; }
        public DateTime FinishedTime { get; set; }
        public Map Map { get; set; }
        public GameMode GameMode { get; set; }
        public ICollection<MatchPlayerStats> MatchPlayerStats { get; set; }
    }
}
