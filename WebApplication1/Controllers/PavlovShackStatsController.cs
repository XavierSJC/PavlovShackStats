﻿using Microsoft.AspNetCore.Mvc;
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
        public ActionResult Post([FromBody]PavlovStats pavlovStats, string filename)
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
        public ActionResult GetPlayersStats(
            [FromQuery] DateTime sinceDate,
            [FromQuery] DateTime untilDate,
            [FromQuery] DayOfWeek[] daysOfWeek,
            [FromQuery] string playerName = "",
            [FromQuery] string gameMode = "",
            [FromQuery] int count = int.MaxValue
        ) 
        {
            try
            {
                if (daysOfWeek.Count() < 1)
                {
                    daysOfWeek = new DayOfWeek[7] { 
                        DayOfWeek.Sunday, 
                        DayOfWeek.Monday,
                        DayOfWeek.Tuesday,
                        DayOfWeek.Wednesday,
                        DayOfWeek.Thursday,
                        DayOfWeek.Friday,
                        DayOfWeek.Saturday};
                }
                return Ok(_PavlovShackStats.GetPlayersStats(sinceDate, untilDate, daysOfWeek, playerName, gameMode, count));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("PlayerStats")]
        public ActionResult GetPlayerStats([FromQuery]string playerName = "", [FromQuery]int playerId = 0)
        {
            ActionResult result;

            try
            {
                if (string.IsNullOrEmpty(playerName))
                {
                    result = Ok(_PavlovShackStats.GetPlayerStats(playerId));
                }
                else
                {
                    result = Ok(_PavlovShackStats.GetPlayerStats(playerName));
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return result;
        }

        [HttpGet("Players")]
        public ActionResult GetPlayer()
        {
            try
            {
                return Ok(_PavlovShackStats.ListPlayers());
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("PlayerMatches")]
        public ActionResult GetPlayer([FromQuery]string playerName = "", [FromQuery]int playerId = 0)
        {
            ActionResult result;
            try
            {
                if(string.IsNullOrEmpty(playerName))
                {
                    result = Ok(_PavlovShackStats.GetPlayerMatches(playerId));
                }
                else
                {
                    result = Ok(_PavlovShackStats.GetPlayerMatches(playerName));
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return result;
        }

        [HttpGet("GetMatches")]
        public ActionResult GetMatches([FromQuery]int count)
        {
            try
            {
                if (count <= 0)
                {
                    return Ok(_PavlovShackStats.GetMatches());
                }
                else
                {
                    return Ok(_PavlovShackStats.GetMatches().Take(count));
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("GetMatch")]
        public ActionResult GetMatch([FromQuery]int matchId, [FromQuery]DateTime finishedTime)
        {
            ActionResult result;
            try
            {
                if (matchId <= 0)
                {
                    result = Ok(_PavlovShackStats.GetMatch(finishedTime));
                }
                else
                {
                    result = Ok(_PavlovShackStats.GetMatch(matchId));
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return result;
        }

        [HttpGet("TeamsMatch")]
        public ActionResult GetTeamsMatch([FromQuery]int matchId)
        {
            try
            {
                return Ok(_PavlovShackStats.GetMatchDetails(matchId));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("GameModeList")]
        public ActionResult GetGameModeList()
        {
            try
            {
                return Ok(_PavlovShackStats.GetGameModeList());
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
