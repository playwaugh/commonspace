using Microsoft.AspNetCore.Mvc;
using Commonspace.Data;
using Commonspace.Models;
using Microsoft.EntityFrameworkCore;

namespace Commonspace.Services
{
    public class CreateSpaceService(ApplicationDbContext context)
  {
        private readonly ApplicationDbContext _context = context;

    public async Task<Space> CreateSpace(Space space)
        {
            _context.Spaces.Add(space);
            await _context.SaveChangesAsync();
            return space;
        }
  }
}
