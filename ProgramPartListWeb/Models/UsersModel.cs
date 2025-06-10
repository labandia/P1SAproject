using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramPartListWeb.Models
{
    public class UsersModel
    {
        public int User_ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }    
        public int Role_ID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Status { get; set; }
        public int Project_ID { get; set; }
    }

    public class AuthModel
    {
        public int User_ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Role_ID { get; set; }
        public string Fullname { get; set; }
        //public string Last_Name { get; set; }
    }

    public class AuthModelV2
    {
        public int User_ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string ProfileImage { get; set; }
        public int Role_ID { get; set; }
        public string Role_Name { get; set; }

    }


}