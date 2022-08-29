using Sat.Recruitment.Application.Users.Commands.CreateUser;
using Sat.Recruitment.Domain.Entities.Users;
using Xunit;

namespace Sat.Recruitment.Test.Domain.UnitTests.Users
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UserTests
    {
        [Fact]
        public void CheckGiftsInNormalUserUSD10()
        {
            //Setup
            var userCommand = new CreateUserCommand()
            {
                UserType = "Normal",
                Money = 10
            };

            //Action
            User user = UsersFactory.NewUser(userCommand);

            //Assert
            Assert.Equal(10, user.Money);
        }

        [Fact]
        public void CheckGiftsInNormalUserUSD50()
        {
            //Setup
            var userCommand = new CreateUserCommand()
            {
                UserType = "Normal",
                Money = 50
            };

            //Action
            User user = UsersFactory.NewUser(userCommand);

            //Assert
            Assert.Equal(90, user.Money); //TODO make sure this is right. It seems it should be 54.
        }

        [Fact]
        public void CheckGiftsInNormalUserUSD100()
        {
            //Setup
            var userCommand = new CreateUserCommand()
            {
                UserType = "Normal",
                Money = 100
            };

            //Action
            User user = UsersFactory.NewUser(userCommand);

            //Assert
            Assert.Equal(100, user.Money);
        }

        [Fact]
        public void CheckGiftsInNormalUserUSD200()
        {
            //Setup
            var userCommand = new CreateUserCommand()
            {
                UserType = "Normal",
                Money = 200
            };

            //Action
            User user = UsersFactory.NewUser(userCommand);

            //Assert
            Assert.Equal(224, user.Money);
        }

        [Fact]
        public void CheckGiftsInSuperlUserUSD100()
        {
            //Setup
            var userCommand = new CreateUserCommand()
            {
                UserType = "SuperUser",
                Money = 100
            };

            //Action
            User user = UsersFactory.NewUser(userCommand);

            //Assert
            Assert.Equal(100, user.Money);
        }

        [Fact]
        public void CheckGiftsInSuperlUserUSD200()
        {
            //Setup
            var userCommand = new CreateUserCommand()
            {
                UserType = "SuperUser",
                Money = 200
            };

            //Action
            User user = UsersFactory.NewUser(userCommand);

            //Assert
            Assert.Equal(240, user.Money);
        }

        [Fact]
        public void CheckGiftsInPremiumUserUSD100()
        {
            //Setup
            var userCommand = new CreateUserCommand()
            {
                UserType = "Premium",
                Money = 100
            };

            //Action
            User user = UsersFactory.NewUser(userCommand);

            //Assert
            Assert.Equal(100, user.Money);
        }

        [Fact]
        public void CheckGiftsInPremiumUserUSD150()
        {
            //Setup
            var userCommand = new CreateUserCommand()
            {
                UserType = "Premium",
                Money = 150
            };

            //Action
            User user = UsersFactory.NewUser(userCommand);

            //Assert
            Assert.Equal(450, user.Money);
        }

        [Fact]
        public void CheckUserTypeInNormalUserCreation()
        {
            //Setup
            var userParams = new CreateUserCommand()
            {
                UserType = "Normal",
                Money = 0
            };

            //Action
            User normalUser = UsersFactory.NewUser(userParams);

            //Assert
            Assert.Equal("Normal", normalUser.UserType);
        }

        [Fact]
        public void CheckUserTypeInSuperUserUserCreation()
        {
            //Setup
            var userParams = new CreateUserCommand()
            {
                UserType = "SuperUser",
                Money = 0
            };

            //Action
            User superUser = UsersFactory.NewUser(userParams);

            //Assert
            Assert.Equal("SuperUser", superUser.UserType);
        }

        [Fact]
        public void CheckUserTypeInPremiumUserCreation()
        {
            //Setup
            var userParams = new CreateUserCommand()
            {
                UserType = "Premium",
                Money = 0
            };

            //Action
            User premiumUser = UsersFactory.NewUser(userParams);

            //Assert
            Assert.Equal("Premium", premiumUser.UserType);
        }
    }
}
