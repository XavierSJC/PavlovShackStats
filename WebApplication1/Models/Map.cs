namespace PavlovShackStats.Models
{
    public class Map
    {
        public int MapId { get; set; }
        public string Name { get; set; }
        public ICollection<Match> Match { get; set; }
    }
}
