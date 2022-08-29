using System;
using Sat.Recruitment.Domain.Enums;

namespace Sat.Recruitment.Domain.Entities.Users
{
    /// <summary>
    /// Factory class which returns a new user given a type and some UsersCreationParameters.
    /// </summary>
    public class UsersFactory
    {
        public static User NewUser(User parameters)
        {
            if(Enum.TryParse(parameters.UserType, out UserType myUserType))
                return NewUser(myUserType, parameters);
            else
                throw new NotSupportedException(parameters.UserType);
        }

        public static User NewUser(string type, User parameters)
        {
            if(Enum.TryParse(type, out UserType myUserType))
                return NewUser(myUserType, parameters);
            else
                throw new NotSupportedException(type);
        }

        public static User NewUser(UserType type, User parameters)
        {
            return type switch
            {
                UserType.Normal => new UserNormal(parameters),
                UserType.SuperUser => new UserSuperUser(parameters),
                UserType.Premium => new UserPremium(parameters),
                _ => throw new NotSupportedException(parameters.UserType),
            };
        }
    }
}