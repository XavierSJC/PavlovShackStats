using PavlovVR_Rcon.Models.Replies;

namespace WebApplication1.Classes
{
    public class MapListReply : BaseReply
    {
        public MapList[] MapList { get; set; } = Array.Empty<MapList>();
    }
}
