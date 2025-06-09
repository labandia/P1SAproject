using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security;
using System.Web;

namespace ProgramPartListWeb.Services
{
    public class EmailServices
    {
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