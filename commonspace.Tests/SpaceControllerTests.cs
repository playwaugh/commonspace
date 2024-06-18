using Xunit;
using Microsoft.AspNetCore.Mvc;
using Commonspace.Data;
using Commonspace.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Commonspace.Controllers;

namespace Commonspace.Tests
{
    public class SpaceControllerTests
    {
        private ApplicationDbContext _context;
        private SpaceQuery _spaceQuery;
        private SpaceController _controller;

        public SpaceControllerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
            _spaceQuery = new SpaceQuery(_context);
            _controller = new SpaceController(_context, _spaceQuery);
        }

        [Fact]
        public async Task GetSpaces_ReturnsOkResult()
        {
            var query = "test";

            var result = await _controller.GetSpaces(query);

            Assert.IsType<OkObjectResult>(result);
        }
    }
}
