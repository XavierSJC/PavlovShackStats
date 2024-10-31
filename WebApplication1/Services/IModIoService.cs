using WebApplication1.Models.Mod.io;

namespace WebApplication1.Services
{
    public interface IModIoService
    {
        Mod GetModDetailsByResourceId(int resourceId);
        bool IsServiceConfigured();
    }
}
