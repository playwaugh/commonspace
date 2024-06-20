using Xunit;
using Microsoft.AspNetCore.Mvc;
using Commonspace.Data;
using Commonspace.Models;
using Commonspace.Services;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Commonspace.Controllers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Commonspace.Tests.Controllers
{
    public class SpaceControllerTests
    {
        private ApplicationDbContext _context;
        private SpaceQuery _spaceQuery;
        private CreateSpaceService _spaceService;
        private SpaceController _controller;
        private User user;

        public SpaceControllerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
            _spaceQuery = new SpaceQuery(_context);
            _spaceService = new CreateSpaceService(_context);
            _controller = new SpaceController(_context, _spaceQuery, _spaceService);
            user = new User{};
        }

        [Fact]
        public async Task GetSpaces_ReturnsOkResult()
        {
            var query = "test";

            var result = await _controller.GetSpaces(query);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetSpace_ReturnsNotFoundResult_WhenSpaceDoesNotExist()
        {
            var id = 257;

            var result = await _controller.GetSpace(id);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetSpace_ReturnsOkResult_WhenSpaceExists()
        {
            var user = new User{};

            var space = new Space
            {
                SpaceId = 128,
                Name = "testspace",
                Address = "testaddress",
                Description = "testdesc",
                User = user
            };

            _context.Spaces.Add(space);
            await _controller.GetSpace(space.SpaceId);

            var result = await _controller.GetSpace(space.SpaceId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnSpace = Assert.IsType<Space>(okResult.Value);
            Assert.Equal(space.SpaceId, returnSpace.SpaceId);
        }

        [Fact]
        public async Task CreateSpace_ReturnsBadRequestResult_WhenModelStateIsInvalid()
        {
            _controller.ModelState.AddModelError("error", "some error");
            Space space = new Space
            {
                Name = "testspace",
                Address = "testaddress",
                Description = "testdesc",
                User = user
            };

            var result = await _controller.CreateSpace(space);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateSpace_ReturnsCreatedAtActionResult_WhenModelStateIsValid()
        {
            Space space = new Space
            {
                Name = "testspace",
                Address = "testaddress",
                Description = "testdesc",
                User = user
            };

            var result = await _controller.CreateSpace(space);

            Assert.IsType<CreatedAtActionResult>(result);
        }
    }
}
