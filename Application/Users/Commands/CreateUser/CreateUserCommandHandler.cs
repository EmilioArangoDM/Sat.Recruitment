using Sat.Recruitment.Application.Common.Interfaces;
using Sat.Recruitment.Domain.Common;
using Sat.Recruitment.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler
    {
        private readonly IApplicationDbContext _context;

        public CreateUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(CreateUserCommand request)
        {
            try
            {
                var validator = new CreateUserCommandValidator(_context);
                Result result = validator.Validate(request, out User userToCreate);

                if (result.IsSuccess)
                {
                    _context.Users.Add(userToCreate);
                    await _context.SaveChangesAsync();
                    result.Message = "User Created";
                }

                return result;
            }
            catch
            {
                return new NonSuccessfulResult("Error handling the CreateUserCommand");
            }
        }
    }
}
