using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace EmailSender.Services
{
    public sealed class EmailServices
    {
        public static bool SendMailOutlook(string emailTo, string subject, string body)
        {
            try
            {
                Outlook.Application outlookApp = new Outlook.Application();
                Outlook.MailItem mailItem = (Outlook.MailItem)outlookApp.CreateItem(Outlook.OlItemType.olMailItem);

                mailItem.To = emailTo;
                mailItem.Subject = subject;
                mailItem.HTMLBody = body;
                mailItem.SentOnBehalfOfName = "jaye.labandia@sanyodenki.com";

                mailItem.Send();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public static bool SendMailOutlookV2(string fromSender, string emailTo, string subject, string body)
        {
            try
            {
                Outlook.Application outlookApp = new Outlook.Application();
                Outlook.MailItem mailItem = (Outlook.MailItem)outlookApp.CreateItem(Outlook.OlItemType.olMailItem);

                mailItem.To = emailTo;
                mailItem.Subject = subject;
                mailItem.HTMLBody = body;
                mailItem.SentOnBehalfOfName = fromSender;

                mailItem.Send();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }


        public static string CreateAEmailBody(string Fullname, string Content)
        {
            return $@" <html>
                        <body style='margin:0; padding:0; 
                                font-family:Segoe UI, sans-serif; 
                                background-color:#f4f4f4; display: flex; justify-content: center; 
                                align-items: center; flex-direction: column;'>
        
                            <div class='Emailcard' style='border-radius: .2em; margin:30px 0 0; box-shadow: rgba(0, 0, 0, 0.16) 0px 1px 4px;' >
                                <div class='EmailHeader' style='padding: 1.2em 2em; width: 600px; border-radius: .2em .2em 0 0;
                                background-image: radial-gradient(circle at 33% 41%, rgba(250, 250, 250,0.03) 0%, rgba(250, 250, 250,0.03) 50%,rgba(37, 37, 37,0.03) 50%, rgba(37, 37, 37,0.03) 100%),radial-gradient(circle at 76% 49%, rgba(28, 28, 28,0.03) 0%, rgba(28, 28, 28,0.03) 50%,rgba(187, 187, 187,0.03) 50%, rgba(187, 187, 187,0.03) 100%),radial-gradient(circle at 41% 99%, rgba(247, 247, 247,0.03) 0%, rgba(247, 247, 247,0.03) 50%,rgba(120, 120, 120,0.03) 50%, rgba(120, 120, 120,0.03) 100%),radial-gradient(circle at 66% 27%, rgba(17, 17, 17,0.03) 0%, rgba(17, 17, 17,0.03) 50%,rgba(156, 156, 156,0.03) 50%, rgba(156, 156, 156,0.03) 100%),linear-gradient(0deg, rgb(18, 81, 88),rgb(39, 101, 99));'>
                                    <h4 style='color: #fff; margin: 0; font-weight: 500;'>Sending Email</h4>
                                </div>

                                <div class='EmailBody' style='padding: 1.7em 2em 2em;'>
                                    <h3 style='margin: 0;'>Good Day {Fullname}</h3>

                                     <p style='margin:0; margin-top: 1em;'>{Content}</p>

                                     <p style='margin:2.5em 0 .5em;'>Best regards,</p>
                                      <strong>P1SA process Control</strong>
                                </div>
                            </div>

                            <footer style='text-align: center; margin-top: .5em;'>
                                 <p style='font-size:14px; color:#222; font-weight: 400; font-family:Segoe UI; margin: 0; margin-top: 1em;'> ⚠️ This is a auto system-generated email. Do not reply in this email.</p>
                            </footer>
                        </body>
                    </html>
                    ";
        }


        public static string CreateAEmailBodyV2(string Fullname, string messageContent)
        {
            return $@"
                    <html>
                        <body style='margin:0; padding:0; font-family:Segoe UI, sans-serif; background-color:#f4f4f4;'>
                            <table width='100%' cellpadding='0' cellspacing='0' border='0' align='center'>
                                <tr>
                                    <td align='center' style='padding:30px 0;'>
                                        <!-- Email Card -->
                                        <table width='600' cellpadding='0' cellspacing='0' border='0' style='background-color:#ffffff; border-radius:4px; box-shadow:0 1px 4px rgba(0,0,0,0.16); overflow:hidden;'>
                                            <!-- Header -->
                                            <tr>
                                                <td style='padding:20px; background:rgb(18,81,88);'>
                                                    <img src=""data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAACXBIWXMAAACxAAAAsQHGLUmNAAAAGXRFWHRTb2Z0d2FyZQB3d3cuaW5rc2NhcGUub3Jnm+48GgAAApFJREFUSIm9lj1oFFEQx3/zdk8JGBAshETB+IEBERvtLdW7y+4maJpUYrDSIoogGk0glmIriVZaGXO7t3unjWBnZ2MjCJqIyYGdEsGPu3tjkUuyCbnkLoL/7s28/+/NeywzK2wiL/L211U8Ec0qHEBlHwCi8wJzqlJ2RItFv/ilGUM2CuaSXLfU3DsKFwF3syIAK8KMo9yIgmhuywOyxb4+seYp0LkFeL0W1dihshfH6aBZAw/9K2JNuA04QKdYE+YLwdV0cOUG+dA/q5AAzjbgaVlR6U/6w+LKAbkk103Nfc/2Kt9Ii8bY3tiLKy6A1txxScNVFhD93RZSZSei3Y1Vp60748CwND7FWVaf5i0qj0r94cN2+PnIu2xVLgmcbITqjmiPqan4rH33khr7Lht5E+efnd+xFfj069NuLvTvWtEPAqVUyqmreEbg7HpT2S++McY+/pmp3g8KwZ5m8KAQ7Nn1bfcDtc6Tshe/3mDLOQMc3sic9CWzHdXMzT9wKx/nj63P554PHKmKjv6qZm6XB2Y+NanhkAH2Nqtw+sL0j1PvTlyn5vr50F+56blCcAanPthRzVx7dWH6ezM/0OXSpF0sa2xszAL3sqE/lA39UQEV9HMSRBOb+ZZlgEorG8tB9NQxdtIYO5UE0ZNWPEDFBT4BR1vZHXvx1xbBy/pogBdtmlqXStk4okWgngof/Adg2lt3jI2XelEhmEL0UipZAX61CU+3ChrdYHhpmGSqY9TcQVb7UVfb1YumV4vGqd+Fxjwo5UsLAoOsfartyqqxQ7EXV1YOAEiC6KWojAD2X+DASHqq/d+RCVD24tgY2yswCdRaqVqEaWvN8fVw2KJNrPltUekB9jVS8yI628pvy18JMQONQA1gfAAAAABJRU5ErkJggg=="" width=""30"" height=""30"" alt=""Icon""/>
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

    }
}
