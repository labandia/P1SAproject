using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZebraPrinterLabel
{
    public partial class ZebraPrinter : Form
    {
        public ZebraPrinter()
        {
            InitializeComponent();
        }


        public static string GenerateZplLabel(string topText, string bottomText, string qty)
        {
            return $@"^XA
            ^CI28
            ^PW559
            ^LL208

            ^FO30,10
            ^A0N,30,30
            ^FD*{topText}*^FS

            ^FO30,40
            ^BY2
            ^BCN,60,Y,N,N
            ^FD*{topText}*^FS

            ^FO430,10
            ^BQN,2,6
            ^FDLA,{topText}^FS

            ^FO30,120
            ^A0N,25,25
            ^FDSMT WH *{bottomText}*^FS

            ^FO30,145
            ^BY2
            ^BCN,50,Y,N,N
            ^FD*{bottomText}*^FS

            ^FO430,130
            ^A0N,20,20
            ^FDQTY.:^FS

            ^FO430,150
            ^A0N,40,40
            ^FD*{qty}*^FS

            ^XZ";
        }

        private void Printbtn_Click(object sender, EventArgs e)
        {
            string zplCode = GenerateZplLabel(
                "SDP00715310-02 410210268",
                "RCC3A001001001",
                "50"
            );
            bool printed = ZebraProcess.SendZplToPrinter("Zebra ZD421", zplCode);
            MessageBox.Show(printed ? "Printed successfully!" : "Print failed.");
        }

        private void SearchAmbassador(object sender, KeyEventArgs e)
        {

        }
    }
}
