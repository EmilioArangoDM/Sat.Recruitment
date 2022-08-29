using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Application.Users.Commands.CreateUser;
using System.Linq;
using Xunit;

namespace Sat.Recruitment.Test.Application.IntegrationTests.Users.Commands
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class CreateUserTests
    {
        private static readonly CustomWebApplicationFactory _factory = new CustomWebApplicationFactory();
        private static readonly IServiceScopeFactory _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();

        private readonly UsersController userController = new UsersController(_factory.dbService);


        [Fact]
        public void CheckDuplicatedUserByEmail()
        {
            //Setup
            var userCommand = CreateUserCommand("RepeatedEmail", "Agustina@gmail.com", "RepeatedEmail", "+000 1122354215", "Normal", "124");

            //Action
            var result = userController.CreateUser(userCommand).Result;

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("The user currently exists", result.Errors);
        }

        [Fact]
        public void CheckDuplicatedUserByPhone()
        {
            //Setup
            var userCommand = CreateUserCommand("RepeatedPhone", "RepeatedPhone@gmail.com", "RepeatedPhone", "+534645213542", "Normal", "124");

            //Action
            var result = userController.CreateUser(userCommand).Result;

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("The user currently exists", result.Errors);
        }

        [Fact]
        public void CheckDuplicatedUserByNameAndAddress()
        {
            //Setup
            var userCommand = CreateUserCommand("Agustina", "RepeatedNameAndAddress@gmail.com", "Garay y Otra Calle", "+000 1122354215", "Normal", "124");

            //Action
            var result = userController.CreateUser(userCommand).Result;

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("The user currently exists", result.Errors);
        }

        [Fact]
        public void CheckUserFieldNameFailValidation()
        {
            //Setup
            var userCommand = CreateUserCommand(null, "email@gmail.com", "Street", "+000 1213288215", "Normal", "9");

            //Action
            var result = userController.CreateUser(userCommand).Result;

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("The name is required", result.Errors);
        }

        [Fact]
        public void CheckUserFieldEmailFailValidation()
        {
            //Setup
            var userCommand = CreateUserCommand("No name user", null, "Street", "+000 1213288215", "Normal", "9");

            //Action
            var result = userController.CreateUser(userCommand).Result;

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("The email is required", result.Errors);
        }

        [Fact]
        public void CheckUserFieldAddressFailValidation()
        {
            //Setup
            var userCommand = CreateUserCommand("No name user", "email@gmail.com", null, "+000 1213288215", "Normal", "9");

            //Action
            var result = userController.CreateUser(userCommand).Result;

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("The address is required", result.Errors);
        }

        [Fact]
        public void CheckUserFieldPhoneFailValidation()
        {
            //Setup
            var userCommand = CreateUserCommand("No name user", "email@gmail.com", "Street", null, "Normal", "9");

            //Action
            var result = userController.CreateUser(userCommand).Result;

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("The phone is required", result.Errors);
        }

        [Fact]
        public void CheckUserFieldTypeFailValidation()
        {
            //Setup
            var userCommand = CreateUserCommand("No name user", "email@gmail.com", "Street", "+000 1213288215", null, "9");

            //Action
            var result = userController.CreateUser(userCommand).Result;

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Internal error: Exception of type 'System.NotSupportedException' was thrown.", result.Errors);
        }

        [Fact]
        public void CheckUserFieldMoneyFailValidation()
        {
            //Setup
            var userCommand = CreateUserCommand("No name user", "email@gmail.com", "Street", "+000 1213288215", "Normal", "NonParseableMoney");

            //Action
            var result = userController.CreateUser(userCommand).Result;

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("The money value is not parseable", result.Errors);
        }

        [Fact]
        public void CheckUserFieldsMultiFailValidation()
        {
            //Setup
            var userCommand = CreateUserCommand(null, null, null, null, "Normal", "NonParseableMoney");

            //Action
            var result = userController.CreateUser(userCommand).Result;

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("The name is required", result.Errors);
            Assert.Contains("The email is required", result.Errors);
            Assert.Contains("The address is required", result.Errors);
            Assert.Contains("The phone is required", result.Errors);
            Assert.Contains("The money value is not parseable", result.Errors);
        }

        [Fact]
        public void CheckSuccessfulUserCreation()
        {
            //Setup
            int nUsersBefore = _factory.dbService.Users.Count;
            var userController = new UsersController(_factory.dbService);
            var userCommand = CreateUserCommand("Roberto", "Roberto.random@gmail.com", "Metacarpos Street", "+000 1122354444", "Normal", "250");

            //Action
            var result = userController.CreateUser(userCommand).Result;

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(nUsersBefore + 1, _factory.dbService.Users.Count);

            //Rollback db
            if (_factory.dbService.Users.Count > nUsersBefore)
            {
                _factory.dbService.Users.RemoveAt(nUsersBefore);
                _factory.dbService.SaveChangesAsync().Wait();
            }
        }

        [Fact]
        public void CheckSuccessfulUserEmailNormalization()
        {
            //Setup
            int nUsersBefore = _factory.dbService.Users.Count;
            var userCommand = CreateUserCommand("Heráclito", "first.second+third.fourth@gmail.com", "Glorious Street", "+000 4444112235", "Normal", "250");

            //Action
            var result = userController.CreateUser(userCommand).Result;
            var newEmail = _factory.dbService.Users.Last().Email;

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("firstsecond@gmail.com", newEmail);

            //Rollback db
            if (_factory.dbService.Users.Count > nUsersBefore)
            {
                _factory.dbService.Users.RemoveAt(nUsersBefore);
                _factory.dbService.SaveChangesAsync().Wait();
            }
        }



        public CreateUserCommand CreateUserCommand(string name, string email, string address, string phone, string userType, string money)
        {
            //Wrap the request in a command
            decimal dMoney = (!decimal.TryParse(money, out dMoney)) ? decimal.MinValue : dMoney;
            CreateUserCommand command = new CreateUserCommand()
            {
                Name = name,
                Email = email,
                Phone = phone,
                Address = address,
                UserType = userType,
                Money = dMoney
            };

            return command;
        }
    }
}
