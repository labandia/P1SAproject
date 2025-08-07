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
    public partial class Printer_History : Form
    {
        private readonly IMasterlist _master;

        public Printer_History(IMasterlist master)
        {
            InitializeComponent();
            _master=master;
        }

        private async void Printer_History_Load(object sender, EventArgs e)
        {
            var data =  await _master.GetPrintHistoryData();
            HistoryGrid.DataSource = data;
        }
    }
}
