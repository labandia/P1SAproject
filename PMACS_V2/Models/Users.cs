
using System.Collections.Generic;

namespace PMACS_V2.Models
{
    public class Users
    {
        private int _Account_ID;
        private string _Fullname;
        private string _Password;
        private int _Roles_ID;
        private string _Position;

        public int Account_ID
        {
            get => _Account_ID;
            set => _Account_ID = value;
        }
        public string Fullname
        {
            get => _Fullname;
            set => _Fullname = value;
        }
        public string Password
        {
            get => _Password;
            set => _Password = value;
        }
        public int Roles_ID
        {
            get => _Roles_ID;
            set => _Roles_ID = value;
        }
        public string Position
        {
            get => _Position;
            set => _Position = value;
        }
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

    public class SentEmailModel
    {
        public string Subject { get; set; }
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public string BCC { get; set; }
        public string Body { get; set; }

    }


    public class RequestData<T>
    {
        public List<T> Items { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }
    }
}