using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetalMaskMonitoring.Subforms
{
    public partial class Brushing : UserControl
    {
        private readonly int _ID;

        public Brushing(int ID)
        {
            InitializeComponent();
            _ID = ID;
        }

        private void Brushing_Load(object sender, EventArgs e)
        {

        }
    }
}
