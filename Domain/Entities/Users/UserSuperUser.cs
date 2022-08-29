using System.Security.Policy;
using System;

namespace Sat.Recruitment.Domain.Entities.Users
{
    public class UserSuperUser : User
    {
        public UserSuperUser(User usersCreationParameters) : base(usersCreationParameters)
        {
            //Add gift to users with more than USD100.
            if (Money > 100)
            {
                var percentage = Convert.ToDecimal(0.20);
                var gift = Money * percentage;
                Money += gift;
            }
        }
    }
}
