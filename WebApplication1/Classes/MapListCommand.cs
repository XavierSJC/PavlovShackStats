using PavlovVR_Rcon.Models.Commands;

namespace WebApplication1.Classes
{
    public class MapListCommand : BaseCommand<MapListReply>
    {
        public MapListCommand() : base("MapList")
        {
        }
    }
}
