using PavlovVR_Rcon.Models.Replies;

namespace WebApplication1.Classes
{
    public class InspectAllReply : BaseReply
    {
        public PlayerDetailShack[] InspectList { get; init; } = Array.Empty<PlayerDetailShack>();
    }
}
