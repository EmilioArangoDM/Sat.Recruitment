using CsvHelper.Configuration;
using Sat.Recruitment.Application.Users.Commands.CreateUser;

namespace Sat.Recruitment.Infrastructure.Files.Mappers
{    
    public class UserRecordMapCSV : ClassMap<CreateUserCommand>
    {
        public UserRecordMapCSV()
        {
            Map(member => member.Name).Index(0);
            Map(member => member.Email).Index(1);
            Map(member => member.Phone).Index(2);
            Map(member => member.Address).Index(3);
            Map(member => member.UserType).Index(4);
            Map(member => member.Money).Index(5);
        }
    }
}
