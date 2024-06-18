using Microsoft.EntityFrameworkCore;
using Commonspace.Models;
using ApplicationDbContext = Commonspace.Data.ApplicationDbContext;

public interface ISpaceQuery
{
    IQueryable<Space> GetSpacesByQuery(string query);
}

public class SpaceQuery : ISpaceQuery
{
    private readonly ApplicationDbContext _context;

    public SpaceQuery(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<Space> GetSpacesByQuery(string query)
    {
        return _context.Spaces
            .Where(s => s.Name.Contains(query) || s.Address.Contains(query));
    }
}
