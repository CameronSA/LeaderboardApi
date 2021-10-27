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
    public class PlayerController : ControllerBase
    {
        private readonly ILogger<PlayerController> _logger;

        private readonly IDatabaseContext _context;
        public PlayerController(ILogger<PlayerController> logger, IDatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> Get(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    var players = await _context.GetPlayers();
                    return players.ToArray();
                }

                if (int.TryParse(id, out int idInt))
                {
                    Player player = await _context.GetPlayer(idInt);
                    return new Player[] { player };
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Player player)
        {
            try
            {
                await _context.CreatePlayer(player);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            return Ok("Player created");
        }

        [HttpPut]
        public async Task<ActionResult> Put(Player player)
        {
            try
            {
                await _context.EditPlayer(player);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            return Ok("Player edited");
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(Player player)
        {
            try
            {
                await _context.DeletePlayer(player);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            return Ok("Player deleted");
        }
    }
}
