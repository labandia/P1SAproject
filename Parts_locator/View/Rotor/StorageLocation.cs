using System;
using System.Data;
using System.Windows.Forms;

namespace Parts_locator.Modals
{
    public partial class StorageLocation : Form
    {
        private readonly GlobalDb db;
        public string setPallet { get; set; }
        public string part;
        public int pal;


        public StorageLocation(string part, int pal)
        {
            InitializeComponent();
            db = new GlobalDb();
            this.part = part;
            this.pal = pal;
        }

        private void StorageLocation_Load(object sender, EventArgs e)
        {
            letter.Text = setPallet;

            string strsql = "SELECT l.PartNumber, pr.ModelName, pa.PalletName, l.Quantity, l.PalletID " +
                            "FROM Part_ProductPalateLocation l " +
                            "INNER JOIN Part_Products pr ON pr.PartNumber = l.PartNumber " +
                            "INNER JOIN Part_Pallets pa ON pa.PalletID = l.PalletID " +
                            "WHERE l.PalletID = "+ pal +"";
            DataTable td = db.GetData(strsql);
            Storagetablelist.DataSource = td;

        }

        private void Btnback_Click(object sender, EventArgs e)
        {
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Storagetablelist.DataSource = null;
            Visible = false;
        }

        private void Storagetablelist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string part = Storagetablelist.Rows[e.RowIndex].Cells[0].Value.ToString();
                int pallet = Convert.ToInt32(Storagetablelist.Rows[e.RowIndex].Cells[4].Value.ToString());
                ProductDetails sm = new ProductDetails(part, pallet);
                this.Hide();
                sm.ShowDialog();
                
            }
        }
    }
}
