using Application.Users.Queries;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Xunit;

namespace Sat.Recruitment.Test.Application.IntegrationTests.Users.Queries
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class GetUsersTests
    {
        private static readonly CustomWebApplicationFactory _factory = new CustomWebApplicationFactory();
        private static readonly IServiceScopeFactory _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();

        [Fact]
        public void TestGetUsersQuery()
        {
            //Setup
            GetUsersQuery query = new GetUsersQuery(_factory.dbService);

            //Action
            var result = query.Execute((_) => { return true; }).ToList();

            //Assert
            Assert.Equal(_factory.dbService.Users.Count, result.Count);
        }
    }
}
