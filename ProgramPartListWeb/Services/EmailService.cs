using System;
using System.Net.Mail;
using System.Net;
using System.Diagnostics;
using System.Configuration;

namespace ProgramPartListWeb.Utilities
{
    public class EmailService
    {
        public static bool SendEmail(string toEmail, string subject, string body)
        {
            Debug.WriteLine($@"Email : ${toEmail} Subject: ${subject}");

            try
            {
                var fromAddress = new MailAddress(ConfigurationManager.AppSettings["SMTPEmail"], "AI Ssample");
                var toAddress = new MailAddress(toEmail);
                string fromPassword = ConfigurationManager.AppSettings["SMTPPassword"];

                var smtp = new SmtpClient
                {
                    Host = ConfigurationManager.AppSettings["SMTPHost"],
                    Port = int.Parse(ConfigurationManager.AppSettings["SMTPPort"]),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                    Timeout = 100000
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
                    smtp.Credentials = new NetworkCredential("p1sa-processcontrol@sanyodenki.com", "p1sapc0725*");
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