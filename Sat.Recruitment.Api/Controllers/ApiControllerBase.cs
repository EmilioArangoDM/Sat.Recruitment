using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Application.Common.Interfaces;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiControllerBase : ControllerBase
    {
        private readonly IApplicationDbContext _context = null;

        protected IApplicationDbContext Context
        {
            get { return _context; }
        }

        public ApiControllerBase(IApplicationDbContext context) : base() { _context = context; }
    }
}
