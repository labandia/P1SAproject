using EmailSender.Repository;
using System.Threading;

namespace EmailSender
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                // Check and Send Email Processing 
                EmailRepository.EmailSentProcess();
                Thread.Sleep(5000); //Wait for 5 seconds
            }
        }
    }
}
