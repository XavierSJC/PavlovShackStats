using PavlovVR_Rcon.Models.Replies;

namespace WebApplication1.Classes
{
    public class ServerInfoReply : BaseReply
    {
        public ServerInfoShack ServerInfo { get; init; } = new ServerInfoShack();
    }
}
