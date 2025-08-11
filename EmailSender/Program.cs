using EmailSender.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EmailSender
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                try
                {
                    // Check and Send Email Processing 
                    await EmailRepository.EmailSentProcess();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                Thread.Sleep(5000); //Wait for 5 seconds
            }
        }
    }
}
