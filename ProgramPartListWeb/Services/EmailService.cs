using System;
using System.Net.Mail;
using System.Net;
using System.Diagnostics;
using System.Configuration;
using ProgramPartListWeb.Models;
using ProgramPartListWeb.Helper;
using System.Threading.Tasks;

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
                    mail.From = new MailAddress("jaye.labandia@sanyodenki.com");
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



        public static  Task<bool> SendEmailViaSqlDatabase(SentEmailModel em)
        {
            string strsql = $@"INSERT INTO P1SA_EmailSend(Subject, Sender, Recipient, Body)
                               VALUES(@Subject, @Sender, @Recipient, @Body)";
            return  SqlDataAccess.UpdateInsertQuery(strsql, em);
        }

        public static string CreateAEmailBody(string Fullname, string messageContent)
        {
            return $@"
                    <html>
                        <body style='margin:0; padding:0; font-family:Segoe UI, sans-serif; background-color:#f4f4f4;'>
                            <table width='100%' cellpadding='0' cellspacing='0' border='0' align='center'>
                                <tr>
                                    <td align='center' style='padding:30px 0;'>
                                        <!-- Email Card -->
                                        <table width='600' cellpadding='0' cellspacing='0' border='0' style='background-color:#ffffff; border-radius:4px; border: 1px solid #a09e9e; overflow:hidden;'>
                                            <!-- Header -->
                                             <!-- Header -->
                                            <tr>
                                                <td style='padding:20px; background:rgb(18,81,88);'>
                                                    <table cellpadding='0' cellspacing='0' border='0' width='100%'>
                                                        <tr>
                                                            <td valign='middle' style='padding-right:10px;' width='40'>
                                                                <img src='data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAACXBIWXMAAACxAAAAsQHGLUmNAAAAGXRFWHRTb2Z0d2FyZQB3d3cuaW5rc2NhcGUub3Jnm+48GgAAApFJREFUSIm9lj1oFFEQx3/zdk8JGBAshETB+IEBERvtLdW7y+4maJpUYrDSIoogGk0glmIriVZaGXO7t3unjWBnZ2MjCJqIyYGdEsGPu3tjkUuyCbnkLoL/7s28/+/NeywzK2wiL/L211U8Ec0qHEBlHwCi8wJzqlJ2RItFv/ilGUM2CuaSXLfU3DsKFwF3syIAK8KMo9yIgmhuywOyxb4+seYp0LkFeL0W1dihshfH6aBZAw/9K2JNuA04QKdYE+YLwdV0cOUG+dA/q5AAzjbgaVlR6U/6w+LKAbkk103Nfc/2Kt9Ii8bY3tiLKy6A1txxScNVFhD93RZSZSei3Y1Vp60748CwND7FWVaf5i0qj0r94cN2+PnIu2xVLgmcbITqjmiPqan4rH33khr7Lht5E+efnd+xFfj069NuLvTvWtEPAqVUyqmreEbg7HpT2S++McY+/pmp3g8KwZ5m8KAQ7Nn1bfcDtc6Tshe/3mDLOQMc3sic9CWzHdXMzT9wKx/nj63P554PHKmKjv6qZm6XB2Y+NanhkAH2Nqtw+sL0j1PvTlyn5vr50F+56blCcAanPthRzVx7dWH6ezM/0OXSpF0sa2xszAL3sqE/lA39UQEV9HMSRBOb+ZZlgEorG8tB9NQxdtIYO5UE0ZNWPEDFBT4BR1vZHXvx1xbBy/pogBdtmlqXStk4okWgngof/Adg2lt3jI2XelEhmEL0UipZAX61CU+3ChrdYHhpmGSqY9TcQVb7UVfb1YumV4vGqd+Fxjwo5UsLAoOsfartyqqxQ7EXV1YOAEiC6KWojAD2X+DASHqq/d+RCVD24tgY2yswCdRaqVqEaWvN8fVw2KJNrPltUekB9jVS8yI628pvy18JMQONQA1gfAAAAABJRU5ErkJggg==' width='30' height='30' alt='Icon' style='display:block;'/>
                                                            </td>
                                                            <td valign='middle'>
                                                                <h4 style='color:#fff; font-weight:500; margin:0; font-size:16px;'>Production Patrol Email Notification</h4>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>

                                            <!-- Body -->
                                            <tr>
                                                <td style='padding:25px;'>
                                                    <h3 style='margin:0; font-weight:600; color:#333;'>Good Day {Fullname}</h3>
                                                    <p style='margin:16px 0; color:#444; line-height:1.5;'>{messageContent}</p>

                                                    <!-- <p style='margin:0 0 5px;'>If you have any questions, call local# 224/225 for Programmer</p> -->
                                                    <!-- <p style='margin:30px 0 5px; color:#333;'>Best regards,</p>
                                                    <strong style='color:#000;'>P1SA Process Control</strong> -->

                                                    <p style='font-size:14px; color:#444; margin-top:.5em;'>
                                                       For any questions or concerns, please contact the P1SA Programmer at local 224 or 225.
                                                    </p>
                                                </td>
                                            </tr>
                                        </table>

                                        <!-- Footer -->
                                        <table width='600' cellpadding='0' cellspacing='0' border='0' style='margin-top:10px;'>
                                            <tr>
                                                <td align='center' style='padding:10px;'>
                                                    <p style='font-size:14px; color:#444; margin:0;'>
                                                        ⚠️ This is an auto system-generated email. Do not reply to this email.
                                                    </p>
                            
                                                </td>
                                            </tr>
                                        </table>

                                    </td>
                                </tr>
                            </table>
                        </body>
                        </html>";
        }

        public static string RegistrationEmailBody(string Fullname, string reg, string link)
        {
            // Create the link with the registration number
    

            // Create the email body with HTML formatting
            string emailBody = $@"
                <html>
                <head>
                    <style>
                        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
                        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
                        .header {{ background-color: #f8f9fa; padding: 15px; border-radius: 5px; }}
                        .content {{ padding: 20px 0; }}
                        .button {{ display: inline-block; padding: 12px 24px; background-color: #007bff; color: white; text-decoration: none; border-radius: 4px; margin: 10px 0; }}
                        .footer {{ margin-top: 20px; padding-top: 20px; border-top: 1px solid #eee; font-size: 12px; color: #666; }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>
                            <h2>Notification</h2>
                        </div>
                        <div class='content'>
                            <p>Dear {Fullname},</p>
                            <p>asdasdad</p>
                            <p>Please click the link below to proceed:</p>
                            <a href='{link}' class='button'>Click Here to Process</a>
                            <p>Or copy and paste this URL in your browser:<br>
                            <a href='{link}'>{link}</a></p>
                        </div>
                        <div class='footer'>
                            <p>This is an automated message. Please do not reply to this email.</p>
                        </div>
                    </div>
                </body>
                </html>";

            return emailBody;
        }
    }

}