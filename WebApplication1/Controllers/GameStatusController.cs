using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameStatusController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IGameStatusService _GameStatusService;

        public GameStatusController(ILogger<GameStatusController> logger, IGameStatusService gameStatus)
        {
            _logger = logger;
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
                _logger.LogWarning("Failed to fetch IsOnline information: {errorMessage}", ex.Message);
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
                _logger.LogWarning("Failed to fetch ServerInfo information: {errorMessage}", ex.Message);
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
                _logger.LogWarning("Failed to fetch MapList information: {errorMessage}", ex.Message);
                return NotFound(ex.Message);
            }
        }
    }
}
