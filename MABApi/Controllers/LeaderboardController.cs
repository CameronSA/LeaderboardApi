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
                    return await _context.Leaderboard.ToListAsync();
                }

                if (int.TryParse(id, out int idInt))
                {
                    LeaderboardItem item = await _context.Leaderboard.SingleAsync(x => x.ID == idInt);
                    return new LeaderboardItem[] { item };
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
                await _context.Leaderboard.AddAsync(leaderboardItem);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            return NotFound();
        }

        [HttpPut]
        public async Task<ActionResult> Put(LeaderboardItem leaderboardItem)
        {
            try
            {
                _context.Leaderboard.Update(leaderboardItem);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            return NotFound();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(LeaderboardItem leaderboardItem)
        {
            try
            {
                _context.Leaderboard.Remove(leaderboardItem);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            return NotFound();
        }
    }
}