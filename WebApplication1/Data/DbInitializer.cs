namespace PavlovShackStats.Data
{
    public class DbInitializer
    {
        public static void Initialize(PavlovShackStatsContext context)
        {
            context.Database.EnsureCreated();


        }
    }
}
