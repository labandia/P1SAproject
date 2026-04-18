using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FanTraceableSystem.Services
{
    public class AuthHelper
    {
        public static bool RequiredPassword(int section)
        {
            // Map section Password 
            var passwordMap = new Dictionary<int, string>
            {
                {1, "Molding" },
                {2, "Press" },
                {3, "Rotor" },
                {4, "Winding" },
                {5, "Circuit" },
                {6, "Oilproof" },
                {7, "Harness" }
            };

            // if section not defined 
            if (!passwordMap.ContainsKey(section))
            {
                MessageBox.Show("Invalid Section");
                return false;
            }

            while (true)
            {
                using (var dialog = new EnterPassword())
                {
                    var result = dialog.ShowDialog();

                    // Cancel 
                    if(result != DialogResult.OK)
                        return false;

                    string correctpassword = passwordMap[section];


                    // ✅ Match password for that section
                    if (dialog.EnteredPassword == correctpassword)
                        return true;

                    // Wrong retry 
                    MessageBox.Show("Wrong password. Try again");
                }
            }

        }
    }
}
