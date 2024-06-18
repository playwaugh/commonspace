using Microsoft.AspNetCore.Mvc;
using Commonspace.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Commonspace.Controllers
{
    public class SpaceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SpaceController(ApplicationDbContext context)
        {
            _context = context;
        }

       [HttpGet]
       public async Task<IActionResult> GetSpaces()
       {
           var spaces = await _context.Spaces.ToListAsync();
           return Ok(spaces);
       }
    }
}
