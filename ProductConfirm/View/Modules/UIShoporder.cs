using ProductConfirm.Data;
using ProductConfirm.Modals;
using ProductConfirm.Models;
using ProductConfirm.View.Modals;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductConfirm.Modules
{
    public partial class UIShoporder : UserControl
    {
        private const int PageSize = 20;
        private int CurrentPageIndex = 1;
        private int TotalPages = 0;
        private int TotalRows = 0;


        public static UIShoporder instanceform;
        public DataGridView shopordergrid { get { return Shoptables; } }
        public DataGridView measuregrid { get { return Measuretable; } }
        public Button confirmbutton { get { return button1; } }
        public Button checkbutton { get { return Checkbtn; } }
        public Button Disablebutton { get { return Disablebtn; } }


        public Label confirm;
        public RichTextBox remarks;


        public Label ModelText { get { return ModelnameText; } }
        public Label PartText { get { return Partnumtext; } }   
        public Label ShopText { get { return shopdisplay; } }
        public Label StandarMach { get { return Standardtext; } }
        public Label Changein { get { return chengeinput; } }
        public RichTextBox RemarksText { get { return changeremarks; } }

        public int shopID;
        public int RotorID;
        public string partnumshop;
        public string shoporderstr;
        public int prodConfirm = 0;
        public string shopstats;

        public int totalrowsconfirm;
        public int totalrowsdone;


        public List<ShopOrderModel> shop { get; private set; } = new List<ShopOrderModel>();


        private readonly IProductRepositoryV2 _prod;

        public UIShoporder(IProductRepositoryV2 prod)
        {
            InitializeComponent();
            _prod = prod;
            instanceform = this;
            confirm = chengeinput;
            remarks = changeremarks;  
        }

       

        private void Addbtn_Click(object sender, EventArgs e)
        {
            Add_shoporder add = new Add_shoporder(this);
            add.ShowDialog();
        }
     
        public async Task displayshopordertable()
        {
            int TotalrowCount = await _prod.GetShoporderTotalList();
            TotalRows = TotalrowCount;
            TotalPages = (int)Math.Ceiling((double)TotalRows / PageSize);
            lblTotalPages.Text = TotalPages.ToString();
            lblCurrentPage.Text = CurrentPageIndex.ToString();  

            //Shoptables.DataSource = await Products.GetShoporderlist(CurrentPageIndex, PageSize);
            shop = await _prod.GetShoporderlist(CurrentPageIndex, PageSize);
            Shoptables.DataSource = shop;
        }

        public async void displaymeasurementTable(string shop, int ID)
        {
            DataTable sample = await Shopordersdata.getShoporderDetails(shop, shopID);
            int sum = sample.AsEnumerable()
                 .Where(row => row.Field<int>("Status") == 0 || row.Field<int>("Status") == 1)
                 .Sum(row => row.Field<int>("Status"));


            totalrowsconfirm = sample.Rows.Count;
            totalrowsdone = sum;

            Measuretable.DataSource = await Shopordersdata.getShoporderDetails(shop, ID);
        }


       



        private async  void Shoptables_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && !(shopordergrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn))
            {

                string confirm = Convert.ToString(shopordergrid.Rows[e.RowIndex].Cells["ConfirmBy"].Value) ?? "";

                shopID = Convert.ToInt32(shopordergrid.Rows[e.RowIndex].Cells["ShoporderID"].Value);
                RotorID = Convert.ToInt32(shopordergrid.Rows[e.RowIndex].Cells["RotorProductID"].Value);
                shopstats = shopordergrid.Rows[e.RowIndex].Cells["Shopstats"].Value.ToString();

               
                // TEXT DISPLAY ONLY
                ModelnameText.Text = "Model name: " + shopordergrid.Rows[e.RowIndex].Cells["ProductType"].Value.ToString();
                Partnumtext.Text = "Part number :" + shopordergrid.Rows[e.RowIndex].Cells["RotorAssy"].Value.ToString();
                shopdisplay.Text = "Shop order : " +  shopordergrid.Rows[e.RowIndex].Cells["Shoporder"].Value.ToString();
                Standardtext.Text = "Standard Machine Pressure : " + shopordergrid.Rows[e.RowIndex].Cells["MachinePressureMinMax"].Value.ToString();

                var changeinput = shopordergrid.Rows[e.RowIndex].Cells["ConfirmBy"].Value;
                var remarksinput = shopordergrid.Rows[e.RowIndex].Cells["Remarks"].Value;

                chengeinput.Text = changeinput != null ? changeinput.ToString() : string.Empty;
                changeremarks.Text = remarksinput != null ? remarksinput.ToString() : string.Empty;
              
                // STORE THE SHOPORDER VALUE AND PARTNUMBER
                string getshoporder = shopordergrid.Rows[e.RowIndex].Cells["Shoporder"].Value.ToString();
                shoporderstr = getshoporder;
                partnumshop = shopordergrid.Rows[e.RowIndex].Cells["RotorAssy"].Value.ToString();
                button1.Enabled = (confirm != "") ? false : true;
                Checkbtn.Visible = (confirm != "") ? true : false;
                prodConfirm = (confirm != "") ? 1 : 0;

                

                // DiSPLY THE PRODUCT MEASUREMENT
                var m = await _prod.GetProductsTools(getshoporder, shopID);


                //DataTable sample = await Shopordersdata.getShoporderDetails(getshoporder, shopID);
                int sum = m.Where(row => Convert.ToInt32(row.Status) == 0 || Convert.ToInt32(row.Status) == 1)
                         .Sum(row => Convert.ToInt32(row.Status));

                totalrowsconfirm = m.Count();
                totalrowsdone = sum;

                //MessageBox.Show("Total all rows count: " + sample.Rows.Count);
                //MessageBox.Show("Total Confirm: " + sum);

                Measuretable.DataSource = await Shopordersdata.getShoporderDetails(getshoporder, shopID);
                Measuretable.Columns["ShopProdID"].Visible = false;

            }




        }

      


        // CHANGES THE DATA COLUMN DISPLAY
        private void Measuretable_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Check if we are formatting the correct column (e.g., column index 1)
            if (Measuretable.Columns[e.ColumnIndex].Name == "Status")
            {
                
                if (e.Value.ToString() == "0")
                {
                    e.Value = "For Confirmation";  // Change 0 to "For Confirmation"
                    e.FormattingApplied = true;    // Indicate that formatting has been applied
                   
                }
                else if (e.Value.ToString() == "1")
                {
                    e.Value = "Done Checking";  // Change 1 to "Completed"
                    e.FormattingApplied = true;  // Indicate that formatting has been applied
                }
            }
        }

   
        private void Shoptables_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked cell is a button (assuming it's a DataGridViewButtonColumn)
            if (shopordergrid.Columns[e.ColumnIndex] is DataGridViewImageColumn  && e.RowIndex >= 0)
            {
             
                Edit_shoporder ed = new Edit_shoporder(this);
                ed.ID = Convert.ToInt32(Shoptables.Rows[e.RowIndex].Cells["ShoporderID"].Value.ToString());
                ed.Shoptext.Text = Shoptables.Rows[e.RowIndex].Cells["Shoporder"].Value.ToString();
                ed.PartText.Text = Shoptables.Rows[e.RowIndex].Cells["RotorAssy"].Value.ToString();
                ed.Line_text.Text = Shoptables.Rows[e.RowIndex].Cells["Line"].Value.ToString();           
                ed.EnterText.Text = Shoptables.Rows[e.RowIndex].Cells["Inputby"].Value.ToString();
           
                ed.ShowDialog();
            }
        }

        private void Measuretable_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Debug.WriteLine("Good");

            if (e.RowIndex >= 0)
            {
                string checktool = measuregrid.Rows[e.RowIndex].Cells[0].Value.ToString();
                Debug.WriteLine("SELECT Equip : " + checktool);
                int measureID = (measuregrid.Rows[e.RowIndex].Cells[2].Value == null || string.IsNullOrEmpty(measuregrid.Rows[e.RowIndex].Cells[2].Value.ToString())) ? 0 : Convert.ToInt32(measuregrid.Rows[e.RowIndex].Cells[2].Value.ToString());
                switch (checktool)
                {
                    case "Caulking Dent ":
                        CaulkDent cd = new CaulkDent(this, _prod);
                        cd.ID = shopID;
                        cd.RotorID = RotorID;
                        cd.measureID = measureID;
                        cd.itemID = 1;
                        cd.partnum = partnumshop;
                        cd.shopordersec = shoporderstr;
                        cd.StatusID.Text = shopstats;
                        cd.ShowDialog();
                        break;
                    case "Shaft length":
                        ShaftLength sl = new ShaftLength(this);
                        sl.ID = shopID;
                        sl.RotorID = RotorID;
                        sl.measureID = measureID;
                        sl.itemID = 2;
                        sl.partnum = partnumshop;
                        sl.shopordersec = shoporderstr;
                        sl.StatusID.Text = shopstats;
                        sl.ShowDialog();
                        break;
                    case "Surface Edge Alignment":
                        SurfaceEdge se = new SurfaceEdge(this);
                        se.ID = shopID;
                        se.RotorID = RotorID;
                        se.measureID = measureID;
                        se.itemID = 3;
                        se.partnum = partnumshop;
                        se.shopordersec = shoporderstr;
                        se.StatusID.Text = shopstats;
                        se.ShowDialog();
                        break;
                    case "Shaft Pulling Force":
                        ShaftPull sp = new ShaftPull(this);
                        sp.ID = shopID;
                        sp.RotorID = RotorID;
                        sp.measureID = measureID;
                        sp.itemID = 4;
                        sp.partnum = partnumshop;
                        sp.shopordersec = shoporderstr;
                        sp.StatusID.Text = shopstats;
                        sp.ShowDialog();
                        break;
                    case "Bush Pulling Force":
                        BushPull bp = new BushPull(this);
                        bp.ID = shopID;
                        bp.RotorID = RotorID;
                        bp.measureID = measureID;
                        bp.itemID = 5;
                        bp.partnum = partnumshop;
                        bp.shopordersec = shoporderstr;
                        bp.StatusID.Text = shopstats;
                        bp.ShowDialog();
                        break;
                    case "Magnet Height ":
                        MagnetHeight mh = new MagnetHeight(this);
                        mh.ID = shopID;
                        mh.RotorID = RotorID;
                        mh.measureID = measureID;
                        mh.itemID = 6;
                        mh.partnum = partnumshop;
                        mh.shopordersec = shoporderstr;
                        mh.StatusID.Text = shopstats;
                        mh.ShowDialog();
                        break;

                }
            }

          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //int sum = table.AsEnumerable()
            //        .Where(row => row.Field<int>("Status") == 0 || row.Field<int>("Status") == 1)
            //        .Sum(row => row.Field<int>("Status"));


            if (prodConfirm == 0)
            {
                Confirmdialog c = new Confirmdialog(shopID, totalrowsconfirm, totalrowsdone);
                c.ShowDialog();
            }
           
        }

        private void Disablebtn_Click(object sender, EventArgs e)
        {

        }

        private async void BtnNext_Click(object sender, EventArgs e)
        {
            if(CurrentPageIndex < TotalPages)
            {
                CurrentPageIndex++;
                await displayshopordertable();
                lblCurrentPage.Text = CurrentPageIndex.ToString();
            }
        }

        private async void BtnPrev_Click(object sender, EventArgs e)
        {
            if (CurrentPageIndex > 1)
            {
                CurrentPageIndex--;
                await displayshopordertable();
                lblCurrentPage.Text = CurrentPageIndex.ToString();
            }
        }

        private async void BtnFirst_Click(object sender, EventArgs e)
        {
            CurrentPageIndex = 1;
            await displayshopordertable();
            lblCurrentPage.Text = CurrentPageIndex.ToString();
        }

        private async void BtnLast_Click(object sender, EventArgs e)
        {
            CurrentPageIndex = TotalPages;
            await displayshopordertable();
            lblCurrentPage.Text = CurrentPageIndex.ToString();
        }
    }
}
