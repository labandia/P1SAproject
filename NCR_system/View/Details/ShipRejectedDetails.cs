using NCR_system.Interface;
using NCR_system.Models;
using NCR_system.Utilities;
using System.Runtime.Remoting.Contexts;
using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Media.Media3D;

namespace NCR_system.View.Details
{
    public partial class ShipRejectedDetails : Form
    {
        //private readonly IShipRejected _ship;

        public ShipRejectedDetails(RejectShipmentModel reg)
        {
            InitializeComponent();

            RegNoText.Text = reg.RegNo;
            //DateissuedText.Value = reg.DateIssued;
            Issuedbox.Text = reg.IssueGroup;
            sectionbox.SelectedIndex = reg.SectionID;
            ModelText.Text = reg.ModelNo;
            QuanText.Text = reg.Quantity.ToString();
            ContentText.Text = reg.Contents;
                //DateCloseReg = DateRegText.Text,
                //StatsText.SelectedIndex = (_proc == 0) ? 1 : StatsText.SelectedIndex,
                //UploadImage = ImageUpload
        }
    }
}
