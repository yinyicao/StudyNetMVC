using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyNetMVC.Entity
{
    public class User
    {
        public User()
        {
        }

        public User(int id, string username, string email, string password, string phoneNumber, string createDate)
        {
            Id = id;
            Username = username;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
            CreateDate = createDate;
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string CreateDate { get; set; }
    }
}
