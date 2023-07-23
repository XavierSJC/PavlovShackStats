using Microsoft.EntityFrameworkCore;

namespace PavlovShackStats.Models
{
    [PrimaryKey(nameof(MatchId), nameof(PlayerId))]
    public class MatchPlayerStats
    {
        public int MatchId { get; set; }
        public int PlayerId { get; set; }
        public int TeamId { get; set; }
        public int Kill { get; set; }
        public int Death { get; set; }
        public int Asssistant { get; set; }
        public int Headshot { get; set; }
        public int BombDefused { get; set; }
        public int BombPlanted { get; set; }
        public int TeamKill { get; set; }
        public Match Match { get; set; }
        public Player Player { get; set; }

    }
}
