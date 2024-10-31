namespace WebApplication1.Models.Mod.io
{
    public class Mod
    {
        public int id { get; set; }
        public int status { get; set; }
        public int visible { get; set; }
        public SubmittedBy submitted_by { get; set; }
        public Logo logo { get; set; }
        public string name { get; set; }
        public string summary { get; set; }
        public string profile_url { get; set; }
        public IEnumerable<Tag> tags { get; set; }
    }
}
