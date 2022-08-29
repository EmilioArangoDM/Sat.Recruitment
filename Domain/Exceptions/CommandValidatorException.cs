using System;

namespace Sat.Recruitment.Domain.Exceptions
{
    public class CommandValidatorException : Exception
    {
        public CommandValidatorException(string message) : base(message) { }
    }
}