using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Diagnostics;

namespace ProgramPartListWeb.Utilities
{
    public class EmailService
    {
        public static bool SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                var fromAddress = new MailAddress("p1sa-processcontrol@sanyodenki.com", "AI Ssample");
                var toAddress = new MailAddress(toEmail);
                string fromPassword = "p1sapc0525*";

                var smtp = new SmtpClient
                {
                    Host = "smtp.office365.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                    Timeout = 20000
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                {
                    smtp.Send(message);
                }

                return true;
            }
            catch (Exception ex)
            {
                // Log the error (optional)
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
    }
}