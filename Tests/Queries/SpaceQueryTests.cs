using System.Collections.Generic;
using System.Linq;
using Xunit;
using Commonspace.Models;
using Commonspace.Data;
using Microsoft.EntityFrameworkCore;

namespace Commonspace.Tests.Queries
{
    public class SpaceQueryTests
    {
        [Fact]
        public void GetSpacesByQuery_ReturnsCorrectSpaces()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Spaces.AddRange(
                    new Space { Name = "Space1", Address = "Address1", Description = "Description1", User = new User { UserName = "User1", Email = "User1@example.com" } },
                    new Space { Name = "Space2", Address = "Address2", Description = "Description2", User = new User { UserName = "User2", Email = "User2@example.com" } },
                    new Space { Name = "QuerySpace", Address = "QueryAddress", Description = "DescriptionQuery", User = new User { UserName = "UserQuery", Email = "UserQuery@example.com" } }
                );
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var spaceQuery = new SpaceQuery(context);

                var result = spaceQuery.GetSpacesByQuery("Query").ToList();

                Assert.Single(result);
                Assert.Equal("QuerySpace", result[0].Name);
                Assert.Equal("QueryAddress", result[0].Address);
            }
        }
    }
}
