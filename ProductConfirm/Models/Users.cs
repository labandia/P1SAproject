using ProductConfirm.Global;
using System.ComponentModel;
using System.Data;
using System.Threading.Tasks;

namespace ProductConfirm.Models
{
    public class Users
    {
        public int Account_ID { get; set; }
        public string Fullname { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int role_type { get; set; }



        public async Task<DataTable> LoginCredentials(string username)
        {
             Dataconnect con = new Dataconnect();

             string strsql = "SELECT Account_ID, username, password, role_type, Date_created, Project, Fname, Lname  " +
                         "FROM Project_account WHERE username = '"+ username + "'";

             DataTable dt = await con.GetData(strsql);

             return dt;
        }

    }


    public class RegisterModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public string Fname { get; set; }
        public string lname { get; set; }
        public int role_type { get; set; }
        public int Project_ID { get; set; }
    }
}
