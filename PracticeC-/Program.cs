
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace PracticeC_
{
    internal class Program
    {
        private static readonly string key = "1234567890123456"; // Must be 16/24/32 bytes
        private static readonly string iv = "6543210987654321";    // Must be 16 bytes

        static void Main(string[] args)
        {
            //samplemethod(i: 1);

            string strReelID = "SDP00287327-01 505113353";

            //Console.WriteLine("Number of String : " +  strReelID.Length);




            //string filePath = @"C:\Users\jaye-labandia\Downloads\MSD_MasterList.pdf";

            //// 1. Create Document (doc is NOT null now)
            //Document doc = new Document(PageSize.A4, 20, 20, 20, 20);

            //// 2. Bind writer
            //PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));

            //// 3. Open doc before writing anything
            //doc.Open();

            //// 4. Add content
            //Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);
            //Paragraph title = new Paragraph("MASTER LIST OF MOISTURE SENSITIVE DEVICES", titleFont);
            //title.Alignment = Element.ALIGN_CENTER;
            //doc.Add(title);

            //doc.Add(new Paragraph("\n")); // spacer

            //PdfPTable table = new PdfPTable(3);
            //table.WidthPercentage = 100;

            //// header
            //Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
            //table.AddCell(new PdfPCell(new Phrase("ID", headerFont)));
            //table.AddCell(new PdfPCell(new Phrase("Part Number", headerFont)));
            //table.AddCell(new PdfPCell(new Phrase("Floor Life", headerFont)));

            //// dummy data
            //Font rowFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            //for (int i = 1; i <= 5; i++)
            //{
            //    table.AddCell(new Phrase(i.ToString(), rowFont));
            //    table.AddCell(new Phrase("PN-" + i, rowFont));
            //    table.AddCell(new Phrase("168 hrs", rowFont));
            //}

            //doc.Add(table);

            //// 5. Close doc
            //doc.Close();
            //writer.Close();

            //Console.WriteLine("PDF created: " + filePath);




            //TimeSpan interval = new TimeSpan(5, 6, 22);
            //Console.WriteLine(interval.ToString());

            //DateTime launchDate = new DateTime(2020, 3, 15, 9, 0, 0);
            //DateTime now = DateTime.Now;

            //TimeSpan ts = launchDate - now;
            //samplehash();
            //Console.WriteLine("TimeSpan: {0}", ts.ToString());
            //string machineName = Environment.MachineName.ToLower();
            //Console.WriteLine($"Original: {machineName}");
            //Console.WriteLine(VerifyPassword("ErcXN5O+LcsWG5/cMOMWOg==:cZs22bq4HkpRKxiubSnaRQ0U4NIhfn7xUHi3De8cXWw=", "sdp1234a*"));
            samplehash();

            //PingComputer("172.29.1.121");

            //Console.WriteLine("Generate ID : " + GenerateID(""));


            //ConvertstringtoBase64();

            Console.ReadKey();
        }

        public static string GenerateID(string prefix)
        {
            string datePart = DateTime.Now.ToString("MMddyy");
            string timePart = DateTime.Now.ToString("mmss");
            return $"{datePart}{timePart}";
        }


        public static void ConnectionString()
        {
            //string original = "server=DESKTOP-FC0UP1P;User ID=PCsystem;password=p1saprocess;database=P1sa;";
            //string original = "SoloCoding";
            string original = "KurokoNoBasket";
            string encrypted = Encrypt(original);
            string decrypted = Decrypt(encrypted);



            //Console.WriteLine($"Original: {original}");
            //Console.WriteLine($"Encrypted: {encrypted}");
            //Console.WriteLine($"Decrypted: {decrypted}");
            Console.ReadKey();
        }

        public static void ConvertstringtoBase64()
        {
            //string connectionString = "server=DESKTOP-FC0UP1P;User ID=PCsystem;password=p1saprocess;database=Programlist";
            //string connectionString = "SoloCoding";
            //string connectionString = "server=DESKTOP-FC0UP1P;User ID=PCsystem;password=p1saprocess;database=P1sa";
            //string connectionString = "server=SDP0706ES;User ID=p1sa;password=p1sa1234a*;database=PMACS_TEST;Pooling=True;Min Pool Size=50;Max Pool Size=100;";
            //string connectionString = "server=172.29.3.139;User ID=p1sa;password=p1sa1234a*;database=PRODCONF;Pooling=True;Min Pool Size=50;Max Pool Size=100;";
            //string connectionString = "Data Source=DESKTOP-FC0UP1P;Initial Catalog=Prodcon;Persist Security Info=True;User ID=PCsystem;Password=p1saprocess;";

            //string connectionString = "server=172.29.3.139;User ID=p1sa;password=p1sa1234a*;database=PMACS_LIVE;Pooling=True;Min Pool Size=50;Max Pool Size=100;";
            //string connectionString = "server=SDP0706ES; User ID = p1sa; password=p1sa1234a*; database=PMACS_LIVE; Pooling=True; Min Pool Size=50; Max Pool Size=100;";
            //string connectionString = "server=DESKTOP-FC0UP1P;User ID=PCsystem;password=p1saprocess;database=P1sa;Pooling=True;Min Pool Size=50;Max Pool Size=100;";
            //string connectionString = "server=DESKTOP-FC0UP1P\\SQLEXPRESS;database=P1sa;Integrated Security=True;";
            //string base64ConnectionString = Convert.ToBase64String(Encoding.UTF8.GetBytes(connectionString));

            // Print the encoded connection string
            //Console.WriteLine($"Base64 Encoded Connection String: {base64ConnectionString}");
            // Print the encoded connection string
            string a = DecodeBase64ToString("c2VydmVyPVNEUDA3MDZFUzsgVXNlciBJRCA9IHAxc2E7IHBhc3N3b3JkPXAxc2ExMjM0YSo7IGRhdGFiYXNlPVBNQUNTX0xJVkU7IFBvb2xpbmc9VHJ1ZTsgTWluIFBvb2wgU2l6ZT01MDsgTWF4IFBvb2wgU2l6ZT0xMDA7");
            Console.WriteLine($"Base64 Decrypted {a}");
            Console.ReadKey();
        }

        public static string Encrypt(string plainText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = Encoding.UTF8.GetBytes(iv);

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }

                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public static string Decrypt(string cipherText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = Encoding.UTF8.GetBytes(iv);

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                byte[] buffer = Convert.FromBase64String(cipherText);

                using (MemoryStream msDecrypt = new MemoryStream(buffer))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        public static string DecodeBase64ToString(string base64Encoded)
        {
            byte[] data = Convert.FromBase64String(base64Encoded);
            return Encoding.UTF8.GetString(data);
        }


        public static void sendingEmail()
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("labandiajaye@gmail.com", "ibbm xcse xppo jple"),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };

                MailMessage message = new MailMessage
                {
                    From = new MailAddress("labandiajaye@gmail.com"),
                    Subject = "Test Email",
                    Body = "<h1>This is a test email. sdsdsdsd</h1>",
                    IsBodyHtml = true
                };

                message.To.Add("backendf@gmail.com");

                client.Send(message);
                Console.WriteLine("Email Sent Successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public static void OutlooksendingEmail()
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.office365.com");
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                System.Net.NetworkCredential cred = new System.Net.NetworkCredential("Backendf@gmail.com", "2468");
                client.EnableSsl = true;
                client.Credentials = cred;

                MailMessage message = new MailMessage("backendf@gmail.com", "labandiajaye@gmail.com");
                message.Subject ="Test Mail";
                message.Body = "<h1>This is a mail Body </h1>";
                message.IsBodyHtml = true;
                client.Send(message);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static void samplehash()
        {
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            byte[] hash = KeyDerivation.Pbkdf2(
                password: "abc12345",
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32);

            Console.WriteLine($"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}");
        }

        public static string Hashpassword(string pass)
        {
            // Generate a 128-bit salt using a cryptographically strong random number generator
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            // Hash the password with the salt using PBKDF2 and HMACSHA256
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: pass,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32));

            // Return the salt and the hash separated by a colon
            return $"{Convert.ToBase64String(salt)}:{hashed}";
        }

        //public static bool VerifyPassword(string hash, string password)
        //{
        //    // Split the stored hash into salt and hash components
        //    var parts = hash.Split(':');
        //    byte[] salt = Convert.FromBase64String(parts[0]);
        //    string storedHash = parts[1];

        //    // Hash the password with the stored salt
        //    string testHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
        //        password: password,
        //        salt: salt,
        //        prf: KeyDerivationPrf.HMACSHA256,
        //        iterationCount: 10000,
        //        numBytesRequested: 32));


        //    // Compare the stored hash with the newly computed hash
        //    return storedHash == testHash;
        //}


        public async static void OutlooksendingEmail2()
        {
            string fromAddress = "jaye.labandia@sanyodenki.com";
            string toAddress = "p1sa-processcontrol@sanyodenki.com";
            string subject = "Test Email";
            string bodyContent = "This is a test email sent from C#.";

            string smtpHost = "smtp.office365.com";
            int smtpPort = 587;
            string smtpUsername = "jaye.labandia@sanyodenki.com";
            string smtpPassword = "sanyo2024@11";

            try
            {
                using (var mail = new MailMessage())
                {
                    mail.From = new MailAddress(fromAddress);

                    mail.To.Add(toAddress);

                    mail.Subject = subject;
                    mail.Body = bodyContent;

                    using (var smtp = new SmtpClient(smtpHost, smtpPort))
                    {
                        smtp.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                        smtp.EnableSsl = true;

                        await smtp.SendMailAsync(mail);
                        Console.WriteLine("Email sent successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cant send Email ");
                Console.WriteLine(ex.ToString());
            }
        }



        //public static async Task SendEmailUsingGraph()
        //{
        //    var clientId = "YOUR-CLIENT-ID";
        //    var tenantId = "YOUR-TENANT-ID";
        //    var clientSecret = "YOUR-CLIENT-SECRET";

        //    var scopes = new[] { "https://graph.microsoft.com/.default" };

        //    var confidentialClient = ConfidentialClientApplicationBuilder
        //        .Create(clientId)
        //        .WithTenantId(tenantId)
        //        .WithClientSecret(clientSecret)
        //        .Build();

        //    var authResult = await confidentialClient.AcquireTokenForClient(scopes).ExecuteAsync();

        //    var graphClient = new GraphServiceClient(new DelegateAuthenticationProvider(requestMessage =>
        //    {
        //        requestMessage.Headers.Authorization =
        //            new AuthenticationHeaderValue("Bearer", authResult.AccessToken);
        //        return Task.CompletedTask;
        //    }));

        //    var message = new Message
        //    {
        //        Subject = "Test Email via Microsoft Graph",
        //        Body = new ItemBody
        //        {
        //            ContentType = BodyType.Text,
        //            Content = "This is a test email sent using Microsoft Graph API."
        //        },
        //        ToRecipients = new List<Recipient>
        //{
        //    new Recipient
        //    {
        //        EmailAddress = new EmailAddress
        //        {
        //            Address = "p1sa-processcontrol@sanyodenki.com"
        //        }
        //    }
        //}
        //    };

        //    await graphClient.Users["jaye.labandia@sanyodenki.com"].SendMail(message, null).Request().PostAsync();

        //    Console.WriteLine("Email sent successfully via Graph API.");
        //}

        public class CDItem
        {
            public string Code { get; set; }
            public int Value { get; set; }
        }

        public static void SamplejsonDeserialize()
        {
            var cdList = new List<CDItem>
            {
                new CDItem { Code = "CD1", Value = 1 },
                new CDItem { Code = "CD2", Value = 1 },
                new CDItem { Code = "CD3", Value = 1 },
                new CDItem { Code = "CD4", Value = 1 },
                new CDItem { Code = "CD5", Value = 1 },
                new CDItem { Code = "CD6", Value = 1 },
                new CDItem { Code = "CD7", Value = 1 },
                new CDItem { Code = "CD8", Value = 1 },
                new CDItem { Code = "CD9", Value = 1 }
            };

            string jsonString = JsonConvert.SerializeObject(cdList, Newtonsoft.Json.Formatting.Indented);
            Console.WriteLine(jsonString);
        }



        public static void samplemethod(string name = "", int i = 0)
        {

        }

        public static void PingComputer(string ipAddress)
        {
            Ping pingSender = new Ping();

            try
            {
                PingReply reply = pingSender.Send(ipAddress, 1000); // 1000 ms timeout
                if (reply.Status == IPStatus.Success)
                {
                    Console.WriteLine($"Ping to {ipAddress} successful:");
                    Console.WriteLine($"  Address: {reply.Address}");
                    Console.WriteLine($"  RoundTrip time: {reply.RoundtripTime}ms");
                    Console.WriteLine($"  TTL (Time to live): {reply.Options.Ttl}");
                    Console.WriteLine($"  Buffer size: {reply.Buffer.Length}");
                }
                else
                {
                    Console.WriteLine($"Ping failed: {reply.Status}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public static void Samplelog()
        {

            //// Get the Downloads folder path
            //string downloadsPath = Path.Combine(
            //    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            //    "Downloads"
            //);

            //// Full path to the log file
            //string logFilePath = Path.Combine(downloadsPath, "tasklog.txt");

            //Console.WriteLine("Logging to: " + logFilePath);

            //while (true)
            //{
            //    using (StreamWriter sw = new StreamWriter(logFilePath, true)) // 'true' = append
            //    {
            //        sw.WriteLine($"{DateTime.Now} - This is Running on Task");
            //    }

            //    Console.WriteLine("Wrote to file.");
            //    Thread.Sleep(5000); // Wait for 5 seconds
            //}
        }
    }
}
