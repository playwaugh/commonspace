using Xunit;
using Microsoft.AspNetCore.Mvc;
using Commonspace.Data;
using Commonspace.Models;
using Commonspace.Services;
using Microsoft.EntityFrameworkCore;

namespace Commonspace.Tests.Services
{
    public class CreateSpaceServiceTests
    {
        private ApplicationDbContext _context;
        private CreateSpaceService _service;

        public CreateSpaceServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
            _service = new CreateSpaceService(_context);
        }

        [Fact]
        public async Task CreateSpace_ReturnsSpace()
        {
            var space = new Space
            {
                Name = "Test Space",
                Description = "This is a test space",
                Address = "Test Location",
                Capacity = 10,
                User = new User { UserName = "TestUser", Email = "testuser@gmail.com" }
            };

            var result = await _service.CreateSpace(space);

            Assert.Equal(space, result);
        }
    }
}
