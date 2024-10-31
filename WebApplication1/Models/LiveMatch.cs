namespace WebApplication1.Models
{
    public class LiveMatch
    {
        public string? MapLabel { get; set; }
        public string? MapName { get; set; }
        public string? GameModeName { get; set; }
        public string? PlayerCount { get; set; }
        public string? RoundState { get; set; }
        public int Round { get; set; }
        public int ScoreTeam0 { get; set; }
        public int ScoreTeam1 { get; set; }
        public IList<LivePlayer> Team0 { get; set; } = new List<LivePlayer>();
        public IList<LivePlayer> Team1 { get; set; } = new List<LivePlayer>();
    }
}
