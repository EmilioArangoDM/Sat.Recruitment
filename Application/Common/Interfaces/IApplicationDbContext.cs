using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Sat.Recruitment.Domain.Entities.Users;

namespace Sat.Recruitment.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        //TODO ideally, this must be a DbSet from Entity Framework Core
        public List<User> Users { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
