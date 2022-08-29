namespace Sat.Recruitment.Domain.Common
{
    public class NonSuccessfulResult : Result
    {
        // Retuns a Result object with the IsSucess set as false and a given error message.
        public NonSuccessfulResult(string errorText)
        {
            IsSuccess = false;
            Errors = errorText;
        }
    }
}