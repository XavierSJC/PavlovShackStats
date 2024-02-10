namespace WebApplication1.Models
{
    public class LiveMatch
    {
        public int ScoreTeam0 { get; set; }
        public int ScoreTeam1 { get; set; }
        public IList<LivePlayer> Team0 { get; set; } = new List<LivePlayer>();
        public IList<LivePlayer> Team1 { get; set; } = new List<LivePlayer>();
    }
}
