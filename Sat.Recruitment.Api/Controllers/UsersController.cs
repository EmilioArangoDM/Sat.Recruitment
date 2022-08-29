using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Application.Common.Interfaces;
using Sat.Recruitment.Application.Users.Commands.CreateUser;
using Sat.Recruitment.Domain.Common;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    public class UsersController : ApiControllerBase
    {
        public UsersController(IApplicationDbContext _context) : base(_context) { }

        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser(CreateUserCommand command)
        {
            return await new CreateUserCommandHandler(Context).Handle(command);
        }
    }
}
