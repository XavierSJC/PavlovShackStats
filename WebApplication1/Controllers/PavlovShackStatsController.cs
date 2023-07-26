using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PavlovShackStatsController : ControllerBase
    {
        private readonly IPavlovShackStatsService _PavlovShackStats;

        public PavlovShackStatsController(IPavlovShackStatsService pavlovShackStats)
        {
            _PavlovShackStats = pavlovShackStats;
        }

        [HttpPost("{filename}")]
        public ActionResult Post([FromBody] PavlovStats pavlovStats, string filename)
        {
            try
            {
                _PavlovShackStats.InsertNewStats(pavlovStats, filename);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Created("/", null);
        }

        [HttpGet("PlayersStats")]
        public ActionResult GetPlayerStats() 
        {

            try
            {
                return Ok(_PavlovShackStats.GetPlayersStats());
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
