namespace Sat.Recruitment.Domain.Entities.Users
{
    public class UserPremium : User
    {
        public UserPremium(User usersCreationParameters) : base(usersCreationParameters)
        {
            //Add huge gift to users with more than USD100.
            if (Money > 100)
            {
                var gift = Money * 2;
                Money += gift;
            }
        }
    }
}
