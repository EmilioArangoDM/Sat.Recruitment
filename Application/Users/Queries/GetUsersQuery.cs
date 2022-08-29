using Sat.Recruitment.Application.Common.Interfaces;
using Sat.Recruitment.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Users.Queries
{
    public class GetUsersQuery
    {
        private readonly IApplicationDbContext _context;

        public GetUsersQuery(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> Execute(Func<User, bool> query)
        {
            return _context.Users.Where(query);
        }
    }
}
