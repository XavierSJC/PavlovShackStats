using PavlovVR_Rcon.Models.Commands;

namespace WebApplication1.Classes
{
    public class InspectAllCommand : BaseCommand<InspectAllReply>
    {
        public InspectAllCommand() : base("InspectAll")
        {
        }
    }
}
