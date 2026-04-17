using FanTraceableSystem.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FanTraceableSystem
{
    public partial class Form1 : Form
    {
        private readonly ITraceable _trac;
        private readonly ISummary _summary;

        public Form1(ITraceable traceable, ISummary sum)
        {
            InitializeComponent();
            _trac = traceable;
            _summary = sum; 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var openinput = new TraceableHistory(_summary);
            openinput.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {   
            //var openinput = new FanTraceabilityAutoSearch(_trac);
            //openinput.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SelectionSection(1);
        }

        public void SelectionSection(int section)
        {
            var openinput = new FanTraceabilityAutoSearch(_trac, section);
            openinput.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SelectionSection(2);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SelectionSection(3);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SelectionSection(4);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SelectionSection(5);
        }
    }
}
