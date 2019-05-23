using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyNetMVC.WEB.Models
{
    public class DataModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string CreateDate { get; set; }
    }
}