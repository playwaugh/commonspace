using Microsoft.AspNetCore.Mvc;
using Commonspace.Data;
using Commonspace.Models;
using Commonspace.Services;
using Microsoft.EntityFrameworkCore;

namespace Commonspace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpaceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SpaceQuery _spaceQuery;
        private readonly CreateSpaceService _spaceService;

        public SpaceController(ApplicationDbContext context, SpaceQuery spaceQuery, CreateSpaceService spaceService)
        {
            _context = context;
            _spaceQuery = spaceQuery;
            _spaceService = spaceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSpaces(string query)
        {
            var spaces = await _spaceQuery.GetSpacesByQuery(query).ToListAsync();
            return Ok(spaces);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpace(Space space)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdSpace = await _spaceService.CreateSpace(space);

            return CreatedAtAction(nameof(GetSpaces), new { id = createdSpace.SpaceId }, createdSpace);
        }
    }
}
