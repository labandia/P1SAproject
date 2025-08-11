
namespace EmailSender.Repository
{
    public class EmailModel
    {
        public int EmailID { get; set; }
        public string Employee_ID { get; set; }
        public string FullName { get; set; }
        public string SentDate { get; set; }    
        public string Sender { get; set; }
        public string Recipient { get; set; }   
        public string BCC { get; set; } 
        public string Subject { get; set; } 
        public string Body { get; set; }
        public string AttachmentPath { get; set; }
    }
}
