using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Attendance_Monitoring.View
{
    public partial class ChangeTimeIn : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Attendance _attend;
        private readonly int _RecordID;

        public ChangeTimeIn(int RecordID, Attendance attend, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _attend = attend;
            _serviceProvider = serviceProvider;
            _RecordID = RecordID;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string formattedDate = dateTimePicker1.Value
                    .ToString("yyyy-MM-dd HH:mm:ss.fff");
            MessageBox.Show($@"ID : {_RecordID}");
            MessageBox.Show($@"Successfully Changed Time In! {formattedDate}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
