namespace WebApplication1.Models
{
    public class PlayerStats
    {
        public string UniqueId { get; set; }
        public string PlayerName { get; set; }
        public int TeamId { get; set; }
        public List<Stat> Stats { get; set; }
    }

    public class Stat
    {
        public string StatType { get; set; }
        public int Amount { get; set; }
    }

    public class PavlovStats
    {
        public List<PlayerStats> AllStats { get; set; }
        public string MapLabel { get; set; }
        public string GameMode { get; set; }
        public int PlayerCount { get; set; }
        public bool BTeams { get; set; }
        public int Team0Score { get; set; }
        public int Team1Score { get; set; }
    }
}
