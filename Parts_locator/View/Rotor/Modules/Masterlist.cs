using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Parts_locator.Modules
{
    public partial class Masterlist : UserControl
    {

        public DataGridView Mastergridview { get { return MasterlistTable; } }
        public Label ResultData { get { return Result; } }
        public Label TotalQuan { get { return GtotalText; } }

        public Masterlist()
        {
            InitializeComponent();

        }


        private void Partnumtext_TextChanged(object sender, EventArgs e)
        {
            GlobalDb connect = new GlobalDb();
            string query;
            query = "SELECT l.PartNumber, pr.ModelName, pa.PalletName, l.Quantity " +
                    "FROM Part_ProductPalateLocation l " +
                    "INNER JOIN Part_Products pr ON pr.PartNumber = l.PartNumber " +
                    "INNER JOIN Part_Pallets pa ON pa.PalletID = l.PalletID " +
                    "WHERE pr.PartNumber LIKE '%"+ Partnumtext.Text +"%' " +
                    "ORDER BY pa.PalletName ASC";
           
            MasterlistTable.DataSource = connect.GetData(query);
        }

        //IMPORTING DATA FROM THE EXCEL FILE TO DATABASE
        private void Exportbtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();

            f.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";
            
            if(f.ShowDialog() == DialogResult.OK)
            {
                var filepath = f.FileName;

            }
            
           

        }

        private void Masterlist_Load(object sender, EventArgs e)
        {

        }

        private void MasterlistTable_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Check if the current column index matches your target column
            if (e.ColumnIndex == 1) // Replace 2 with your actual column index
            {
                // Apply bold font to the cell in the specified column
                e.CellStyle.Font = new Font(MasterlistTable.DefaultCellStyle.Font, FontStyle.Bold);
            }
        }
    }
}
