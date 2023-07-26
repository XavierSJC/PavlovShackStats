using PavlovShackStats.Models;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IPavlovShackStatsService
    {
        void InsertNewStats(PavlovStats Stats, string Filename);
        object GetPlayersStats();
    }
}
