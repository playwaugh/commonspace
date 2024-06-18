using Microsoft.AspNetCore.Mvc;
using Commonspace.Data;
using Commonspace.Models;
using Microsoft.EntityFrameworkCore;

namespace Commonspace.Controllers
{
  public class SpaceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SpaceQuery _spaceQuery;

        public SpaceController(ApplicationDbContext context, SpaceQuery spaceQuery)
        {
            _context = context;
            _spaceQuery = spaceQuery;
        }

       [HttpGet]
       public async Task<IActionResult> GetSpaces(string query)
       {
           var spaces = await _spaceQuery.GetSpacesByQuery(query).ToListAsync();
           return Ok(spaces);
       }
    }
}
