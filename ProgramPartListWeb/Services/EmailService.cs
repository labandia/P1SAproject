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

        public static void SendEmailOutlook(string stremail, string sub, string body)
        {
            Debug.WriteLine("HERE");

            try
            {
                using (SmtpClient smtp = new SmtpClient("smtp.office365.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("p1sa-processcontrol@sanyodenki.com", "p1sapc0325*");
                    smtp.EnableSsl = true;

                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("p1sa-processcontrol@sanyodenki.com");
                    mail.To.Add(stremail);
                    mail.Subject = sub;
                    mail.Body = body;
                    mail.IsBodyHtml = true;

                    smtp.Send(mail);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Email could not be sent: " + ex.Message);
            }
        }
    }

}