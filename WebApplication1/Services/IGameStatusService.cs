namespace WebApplication1.Services
{
    public interface IGameStatusService
    {
        object IsRunning();
        object GetLiveMatchInfo();
        object GetListMaps();
    }
}
