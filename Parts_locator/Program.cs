using Parts_locator.DataAccess;
using Parts_locator.Models;
using Parts_locator.View.Moldingbush;
using Parts_locator.View.Rotor;
using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Parts_locator
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        private static PrivateFontCollection privateFonts = new PrivateFontCollection();

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Initialize your repositories.
            //IProductRepository ip = new SQLMainlayoutRespository();


            // Initialize your views.
            //IMainlayoutView mainview = new Mainlayout();

            // Set presenters to the views (if necessary).
           // MainlayoutPresentor mainpresentor = new MainlayoutPresentor(mainview, ip);


            // Set presenters to the views (if necessary).
            


            Application.Run(new Startup());
        }
    }   

    
}   
