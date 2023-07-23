namespace PavlovShackStats.Models
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string Name { get; set; }

        public ICollection<MatchPlayerStats> MatchPlayerStats { get; set; }
    }
}
