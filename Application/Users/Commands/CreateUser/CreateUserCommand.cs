using Sat.Recruitment.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : User
    {
        [Required(ErrorMessage = "The {0} is required")]
        public override string Name { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        [EmailAddress]
        public override string Email { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        public override string Phone { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        public override string Address { get; set; }

        //Shadow UserType field to allow set its value when instancing a new object.
        [Required(ErrorMessage = "The {0} is required")]
        new public string UserType
        {
            get { return base.UserType; }
            set { base.UserType = value; }
        }
    }
}
