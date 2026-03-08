using System.Web;

namespace SanyoDenki.Models
{
    public class SignatureModel
    {
        public HttpPostedFileBase SignatureImage { get; set; }  
        public int UserID { get; set; }
    }
}