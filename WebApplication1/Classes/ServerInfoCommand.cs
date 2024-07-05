using PavlovVR_Rcon.Models.Commands;

namespace WebApplication1.Classes
{
    public class ServerInfoCommand : BaseCommand<ServerInfoReply>
    {
        public ServerInfoCommand() : base("ServerInfo")
        {
        }
    }
}
