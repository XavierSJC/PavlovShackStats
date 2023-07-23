namespace PavlovShackStats.Models
{
    public class GameMode
    {
        public int GameModeId { get; set; }
        public string Name { get; set; }
        public ICollection<Match> Match { get; set; }
    }
}
