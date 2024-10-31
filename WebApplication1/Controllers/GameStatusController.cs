using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameStatusController : ControllerBase
    {
        private readonly IGameStatusService _GameStatusService;

        public GameStatusController(IGameStatusService gameStatus)
        {
            _GameStatusService = gameStatus;
        }

        [HttpGet("IsOnline")]
        public ActionResult IsOnline()
        {
            try
            {
                return Ok(_GameStatusService.IsRunning());
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }            
        }

        [HttpGet("ServerInfo")]
        public ActionResult GetServerInfo()
        {
            try
            {
                return Ok(_GameStatusService.GetLiveMatchInfo());
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("MapList")]
        public ActionResult GetMapList()
        {
            try
            {
                return Ok(_GameStatusService.GetListMaps());
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
