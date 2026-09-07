using ProgramPartListWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Interfaces
{
    public interface IEmailService
    {
        // Send immediately via SMTP
        bool SendEmailAsync(
            string toEmail,
            string subject,
            string body,
            string cc = null,
            string bcc = null);

        // Queue email into database table
        Task<bool> QueueEmailAsync(SentEmailModel model);

        // Send using predefined template
        string EmailMangerBody(
          string fullName, string reg, string DepartmentName, string link, string emailType = "default");

        string CreateAEmailBody(string Fullname, string messageContent);
        string RegistrationEmailBody(string Fullname, string reg, string link);
    }
}
