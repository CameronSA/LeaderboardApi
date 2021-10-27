using MABApi.Interfaces;
using MABApi.Objects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MABApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeaderboardController : ControllerBase
    {
        private readonly ILogger<LeaderboardController> _logger;

        private readonly IDatabaseContext _context;
        public LeaderboardController(ILogger<LeaderboardController> logger, IDatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeaderboardItem>>> Get(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    var leaderboard = await _context.GetLeaderboard();
                    return leaderboard.ToArray();
                }

                if (int.TryParse(id, out int idInt))
                {
                    var leaderboardItem = await _context.GetLeaderboardItem(idInt);
                    return new LeaderboardItem[] { leaderboardItem };
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Post(LeaderboardItem leaderboardItem)
        {
            try
            {
                await _context.CreateLeaderboardItem(leaderboardItem);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            return Ok("Leaderboard item created");
        }

        [HttpPut]
        public async Task<ActionResult> Put(LeaderboardItem leaderboardItem)
        {
            try
            {
                await _context.EditLeaderboardItem(leaderboardItem);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            return Ok("Leaderboard item edited");
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(LeaderboardItem leaderboardItem)
        {
            try
            {
                await _context.DeleteLeaderboardItem(leaderboardItem);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            return Ok("Leaderboard item deleted");
        }
    }
}