using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carstore.Model
{
    public class UserRoleModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }

        public UserRoleModel(int id, string name, string role, string email)
        {
            Id = id;
            Name = name;
            Role = role;
            string[] splittedEmail = email.Split('@');
            if (splittedEmail.Length != 2) throw new ArgumentException();
            string emailName = splittedEmail[0];
            if (emailName.Length < 3)
            {
                Email = email;
            }
            else
            {
                Email = $"{emailName.Substring(0, 2)}...{emailName.Last()}@{splittedEmail[1]}";
            }
        }

        public UserRoleModel(User user)
        {
            Id = user.Id;
            Name = $"{user.Firstname} {user.Lastname}";
            Role = Properties.Resources.ResourceManager.GetString(user.UserType.Name);
            string[] splittedEmail = user.Email.Split('@');
            if (splittedEmail.Length != 2) throw new ArgumentException();
            string emailName = splittedEmail[0];
            if (emailName.Length < 3)
            {
                Email = user.Email;
            } else
            {
                Email = $"{emailName.Substring(0, 2)}...{emailName.Last()}";
            }
        }

    }
}
