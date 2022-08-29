using Application.Users.Queries;
using Sat.Recruitment.Application.Common.Interfaces;
using Sat.Recruitment.Domain.Common;
using Sat.Recruitment.Domain.Entities.Users;
using Sat.Recruitment.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace Sat.Recruitment.Application.Users.Commands.CreateUser
{
    //TODO extract every hardcoded message to a proper message manager.
    //TODO Use FluentValidator?

    public class CreateUserCommandValidator : ICommandValidator<CreateUserCommand, User>
    {
        private readonly IApplicationDbContext _context;

        public CreateUserCommandValidator(IApplicationDbContext context)
        {
            _context = context;
        }


        public Result Validate(CreateUserCommand instance, out User validatedInstance)
        {
            validatedInstance = null;

            try
            {
                //CreateUserCommand validation
                ValidateIssuesOnCreateUserInputs(instance);
                NormalizeEmail(instance);
                //TODO The phone should be also normalized.


                //User validation
                User output = UsersFactory.NewUser(instance);
                AvoidDuplicatedUser(output);


                //Success!
                validatedInstance = output;
                return new Result() { IsSuccess = true };
            }
            catch (CommandValidatorException e)
            {
                return new NonSuccessfulResult(e.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception: {0}{1}  {2}", e.Message, Environment.NewLine, e.ToString());
                return new NonSuccessfulResult("Internal error: " + e.Message);
            }
        }




        private void AvoidDuplicatedUser(User newUser)
        {
            var duplicatedUsers = new GetUsersQuery(_context).
                Execute((_) => { return IsSameUser(_, newUser); });

            if (duplicatedUsers.Any())
                throw new CommandValidatorException("The user currently exists");
        }

        private static bool IsSameUser(User u1, User u2)
        {
            return u1.Email == u2.Email || u1.Phone == u2.Phone || (u1.Name == u2.Name && u1.Address == u2.Address);
        }


        private void NormalizeEmail(CreateUserCommand instance)
        {
            instance.Email = Regex.Replace(instance.Email, @"\.(?=.*?@)|\+(.*?[^@]*)", string.Empty);
        }


        private void ValidateIssuesOnCreateUserInputs(CreateUserCommand instance)
        {
            List<string> errorsList = new List<string>();

            //Validate if Name is null
            if (instance.Name == null)
                errorsList.Add("The name is required");

            //Validate if Email is null
            if (instance.Email == null)
                errorsList.Add("The email is required");

            //Validate if Address is null
            if (instance.Address == null)
                errorsList.Add("The address is required");

            //Validate if Phone is null
            if (instance.Phone == null)
                errorsList.Add("The phone is required");

            //Validate if Money is not parseable
            if (instance.Money == decimal.MinValue)
                errorsList.Add("The money value is not parseable");

            if (errorsList.Count > 0)
                throw new CommandValidatorException(String.Join(Environment.NewLine, errorsList));
        }
    }
}
