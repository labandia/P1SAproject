using Microsoft.Office.Interop.Excel;
using ProductConfirm.Data;
using ProductConfirm.DataAccess;
using ProductConfirm.Global;
using ProductConfirm.Modals;
using ProductConfirm.Models;
using ProductConfirm.View.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ProductConfirm.Modules
{
    public partial class Masterlistpage : UserControl
    {
        public ProductRepositoryV2 _prod;
        private readonly IProductRepositoryV2 _prod2;

        public DataGridView mastergrid { get { return Masterlistable; } }
        //public DataGridView equipmentgrid { get { return Equipmentable; } }


        public Masterlistpage(IProductRepositoryV2 prod)
        {
            InitializeComponent();
            _prod2 = prod;
        }

      
        public async void DisplayMastelist()
        {
           var products = await _prod2.GetAllProducts();
           Masterlistable.DataSource = products.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddProduct ad = new AddProduct(this, _prod2);
            ad.Show();
        }

        private void Masterlistable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure the row index is valid (prevents header clicks)
            if (e.RowIndex >= 0)
            {
                // Check if the clicked column is the first image column
                if (mastergrid.Columns[e.ColumnIndex] is DataGridViewImageColumn && e.ColumnIndex == 0)
                {
                    EditProducts p = new EditProducts(this);
                    p.TextID.Text =  Masterlistable.Rows[e.RowIndex].Cells["RotorProductID"].Value.ToString();
                    p.PartText.Text = Masterlistable.Rows[e.RowIndex].Cells["RotorAssy"].Value.ToString();
                    p.ModelText.Text = Masterlistable.Rows[e.RowIndex].Cells["ProductType"].Value.ToString();

                    //-------------- MACHINE PRESSURE -------------------- //
                    string machpress = Masterlistable.Rows[e.RowIndex].Cells["MachinePressureMinMax"].Value.ToString(); 
                    string[] machsplit = machpress.Split('-'); 
                    p.MinText.Text =  machsplit[0].Trim();
                    p.MaxText.Text = machsplit[1].Trim();

                    //-------------- CAULKING DENT ----------------------- // 
                    string caulkall = Masterlistable.Rows[e.RowIndex].Cells["CaulkingDentMinMax"].Value.ToString();
                    //string[] caulksplit = caulkall.Split('–');
                    //p.Caulkmin.Text =  caulksplit[0].Trim();
                    //p.Caulkmax.Text = caulksplit[1].Trim();

                    //p.Shaftmin.Text =  Masterlistable.Rows[e.RowIndex].Cells["ShaftLengthMin"].Value.ToString();
                    //p.Shaftmax.Text = Masterlistable.Rows[e.RowIndex].Cells["ShaftLengthMax"].Value.ToString();
                    //p.SEAmin.Text =  Masterlistable.Rows[e.RowIndex].Cells["SEA_Min"].Value.ToString();
                    //p.SEAmax.Text = Masterlistable.Rows[e.RowIndex].Cells["SEA_Max"].Value.ToString();
                    //p.ShaftPullmin.Text =  Masterlistable.Rows[e.RowIndex].Cells["ShaftPullingForce"].Value.ToString();
                    //p.ShaftPullmax.Text = Masterlistable.Rows[e.RowIndex].Cells["BushPullingForce"].Value.ToString();
                    //p.Magnetmin.Text =  Masterlistable.Rows[e.RowIndex].Cells["MagnetHeightMin"].Value.ToString();
                    //p.Magnetmax.Text = Masterlistable.Rows[e.RowIndex].Cells["MagnetHeightMax"].Value.ToString();
                    p.ShowDialog();

                    // Optionally, prevent row selection
                    mastergrid.ClearSelection();
                }
                // Check if the clicked column is the second image column
                //else if (mastergrid.Columns[e.ColumnIndex] is DataGridViewImageColumn && e.ColumnIndex == 1)
                //{
                //    // Perform action for the second image
                //    MessageBox.Show("Second image clicked!");

                //    // Optionally, prevent row selection
                //    mastergrid.ClearSelection();
                //}
            }
        }

        private async void searchbox_TextChanged(object sender, EventArgs e)
        {
            Dataconnect db = new Dataconnect();



            string strsql = "SELECT " +
                                "r.RotorProductID, r.RotorAssy, r.ProductType, " +
                                "r.MachinePressureMinMax, r.RecommendedPressureSetting, " +
                                "CASE WHEN p.CaulkingDentMin IS NOT NULL AND p.CaulkingDentMax IS NOT NULL " +
                               "THEN CONCAT(CAST(p.CaulkingDentMin AS VARCHAR(10)), ' - ', CAST(p.CaulkingDentMax AS VARCHAR(10))) " +
                               "ELSE '-' END AS CaulkingDentMinMax, " +
                               "p.CaulkingDentMax as CaulkingDentTarget, " +
                               "CASE WHEN p.ShaftLengthMin IS NOT NULL AND p.ShaftLengthMax IS NOT NULL " +
                               "THEN CONCAT(CAST(p.ShaftLengthMin AS VARCHAR(10)), ' - ', CAST(p.ShaftLengthMax AS VARCHAR(10))) " +
                               "ELSE '-' END AS ShaftLengthMinMax, " +

                               "CASE WHEN p.SEA_Min IS NOT NULL AND p.SEA_Max IS NOT NULL " +
                               "THEN CONCAT(CAST(p.SEA_Min AS VARCHAR(10)), ' - ', CAST(p.SEA_Max AS VARCHAR(10)))  " +
                               "ELSE '-' END AS SEA_MinMax, " +

                               "CASE WHEN p.ShaftPullingForce IS NOT NULL THEN " +
                               "CONCAT(CAST(p.ShaftPullingForce AS VARCHAR(10)), ' Kgf or more ') " +
                               "ELSE '-' END AS ShaftPullingForce, " +

                              "CASE WHEN p.BushPullingForce IS NOT NULL THEN " +
                                   "CONCAT(CAST(p.BushPullingForce AS VARCHAR(10)), ' Kgf or more ') " +
                              "ELSE '-'  END AS BushPullingForce " +
                              "FROM ProdCon_RotorProduct r " +
                              "INNER JOIN  ProdCon_RotorProductInfo p " +
                                "ON r.RotorProductID = p.RotorProductID " +
                              "WHERE  r.RotorAssy LIKE '%" + searchbox.Text + "%' " +
                              "ORDER BY p.RotorProductID ASC";

            Masterlistable.DataSource = await db.GetData(strsql);
        }
    }
}
