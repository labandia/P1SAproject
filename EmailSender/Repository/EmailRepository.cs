using EmailSender.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSender.Repository
{
    public sealed class EmailRepository
    {
        public async static void EmailSentProcess()
        {
            Console.WriteLine("Starting email process...");
            string strsql = $@"SELECT 
	                            e.EmailID, e.Employee_ID, emp.FullName, e.Sender, e.Recipient, 
	                            e.SentDate, e.Recipient, e.Subject, e.Body, e.AttachmentPath
                            FROM P1SA_EmailSend e
                            LEFT JOIN Employee_tbl emp ON emp.Employee_ID = e.Employee_ID
                            WHERE e.IsSent = 0";

            var EmailsList = await SqlDataAccess.GetData<EmailModel>(strsql);

            if (EmailsList == null || EmailsList.Count == 0)
            {
                Console.WriteLine("No pending emails found.");
                return;
            }

            string updatequery = "UPDATE P1SA_EmailSend SET IsSent = 1 WHERE EmailID =@EmailID";


            var tasks = EmailsList.Select(async item =>
            {
                Console.WriteLine($"Sending email: From {item.Sender} To {item.Recipient}");

                bool sent = EmailServices.SendMailOutlookV2(item.Sender, item.Recipient, item.Subject, item.Body);

                if (!sent)
                {
                    Console.WriteLine($"Failed to send email to {item.Recipient}");
                    return;
                }
                Console.WriteLine("Email sent successfully. Updating database...");

                bool updated = await SqlDataAccess.UpdateInsertQuery(updatequery, new { EmailID = item.EmailID });

                Console.WriteLine(updated
                            ? $"Database updated for EmailID {item.EmailID}"
                            : $"Database update failed for EmailID {item.EmailID}");
            });

            await Task.WhenAll(tasks);
            Console.WriteLine("Email process completed.");

            //foreach (var item in EmailsList)
            //{
            //    string updatequery = "UPDATE P1SA_EmailSend SET IsSent = 1 WHERE EmailID =@EmailID";
            //    Console.WriteLine($"Email Sender {item.Sender} - Recepient : {item.Recipient}");
            //    // Send and Checks the Emails Sent
            //    if (EmailServices.SendMailOutlookV2(item.Sender, item.Recipient, item.Subject, item.Body))
            //    {
            //        Console.WriteLine("Emails Data is Sent to Outlook ");

            //        bool IsSent = await SqlDataAccess.UpdateInsertQuery(updatequery, new { EmailID = item.EmailID });
            //        if (IsSent)
            //        {
            //            // SENDING EMAIL DIRECTLY TO OUTLOOK 
            //            Console.WriteLine("Emails Table is Updated ...");
            //        }
            //        else
            //        {
            //            Console.WriteLine("Emails Tables Not Updating");
            //        }
            //    }
            //    else
            //    {
            //        Console.WriteLine("Emails Data is not Sending . . .");
            //    }
            //}
        }
    }
}
