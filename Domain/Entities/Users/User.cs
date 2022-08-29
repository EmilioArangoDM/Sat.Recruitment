namespace Sat.Recruitment.Domain.Entities.Users
{
    public abstract class User
    {
        public virtual string Name { get; set; }

        public virtual string Email { get; set; }

        public virtual string Phone { get; set; }

        public virtual string Address { get; set; }

        public virtual string UserType { get; protected set; }

        public virtual decimal Money { get; set; }


        //Copy constructor to be used by the subclasses
        protected User(User usersCreationParameters)
        {
            Name = usersCreationParameters.Name;
            Email = usersCreationParameters.Email;
            Phone = usersCreationParameters.Phone;
            Address = usersCreationParameters.Address;
            UserType = usersCreationParameters.UserType;
            Money = usersCreationParameters.Money;
        }

        protected User() { }
    }
}