using FanTraceableSystem.Interface;
using FanTraceableSystem.Services;
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
        private readonly ISubassy _sub;
        private readonly ISummary _summary;

        public Form1(ITraceable traceable, ISummary sum, ISubassy sub)
        {
            InitializeComponent();
            _trac = traceable;
            _summary = sum;
            _sub = sub; 
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
            if (!AuthHelper.RequiredPassword(1))
                return;
            SelectionSection(1);
           
        }

      

        private void button5_Click(object sender, EventArgs e)
        {
            if (!AuthHelper.RequiredPassword(2))
                return;
            SelectionSection(2);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!AuthHelper.RequiredPassword(3))
                return;
            SelectionSection(3);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (!AuthHelper.RequiredPassword(4))
                return;
            SelectionSection(4);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (!AuthHelper.RequiredPassword(5))
                return;
            SelectionSection(5);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (!AuthHelper.RequiredPassword(6))
                return;
            SelectionSection(6);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (!AuthHelper.RequiredPassword(7))
                return;
            SelectionSection(7);
        }


        public void SelectionSection(int section)
        {
            var openinput = new FanTraceabilityAutoSearch(_trac, _sub, section);

            openinput.Owner = this;   // set current form as parent
            openinput.Show();

            this.Hide(); // or Close() depending on your flow
        }
    }
}
