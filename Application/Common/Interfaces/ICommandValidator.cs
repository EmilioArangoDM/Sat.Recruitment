using Sat.Recruitment.Domain.Common;
using System.Threading.Tasks;

namespace Sat.Recruitment.Application.Common.Interfaces
{
    public interface ICommandValidator<in T1, T2>
    {
        Result Validate(T1 instance, out T2 validatedInstance);
    }
}
