using System.Security.Policy;
using System;

namespace Sat.Recruitment.Domain.Entities.Users
{
    public class UserNormal : User
    {
        public UserNormal(User usersCreationParameters) : base(usersCreationParameters)
        {
            decimal gift = 0;

            if (Money > 100)
            {
                //If new user is normal and has more than USD100
                var percentage = Convert.ToDecimal(0.12);
                gift = Money * percentage;
            }
            else if (Money < 100 && Money > 10)
            {
                //If new user is normal and has less than than USD100 but more than USD10
                var percentage = Convert.ToDecimal(0.8); //TODO make sure this is right. It seems it should be 0.08.
                gift = Money * percentage;
            }

            Money += gift;
        }
    }
}
