using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramPartListWeb.Models
{
    public class LoginRequest
    {
        public string username { get; set; }
        public string password { get; set; }
        public int proj { get; set; } = 1;
    }


    public class UsersModel
    {
        public int User_ID { get; set; }
        public string Employee_ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public int Role_ID { get; set; }
    }

    public class Users
    {
        public int User_ID { get; set; }
        public string Employee_ID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }


    public class UserAccounts
    {
        public int User_ID { get; set; }
        public int Project_ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Role_ID { get; set; }
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

    public class RegisterModel
    {
        public int Project_ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Employee_ID { get; set; }
        public string FullName { get; set; }
        public string ProfileImage { get; set; }
        public int Role_ID { get; set; }
        public string Email { get; set; }
    }
}