using BarcodeStandard;
using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZebraPrinterLabel.Services;
using ZebraPrinterLabel.View;
using ZXing;
using ZXing.Common;

namespace ZebraPrinterLabel
{
    public partial class ZebraPrinter : Form
    {
        private readonly IMasterlist _master;
        private List<LabelData> _labelsToPrint = new List<LabelData>();
        private List<FinalLabelData> _labelPrint = new List<FinalLabelData>();
        public string reelID;
        public string Warehouse;
        public int LocationQty;

        public int Todayprint;
        public string AmbassadorText
        {
            get => Ambassador.Text;
            set => Ambassador.Text = value;
        }

        public ZebraPrinter(IMasterlist master)
        {
            InitializeComponent();
            _master=master;
        }
        private async void Printbtn_Click(object sender, EventArgs e)
        {
            try
            {
                var labelData = _labelPrint.Select(item => (item.SDP, item.Warehouse, item.Quantity)).ToList();

                // Generate a ZPL code for the print page
                string zplCode = ZebraProcess.GenerateMultiLabelZpl(labelData);
                // Send to the printer
                bool printed = ZebraProcess.SendZplToPrinter("ZDesigner ZD421-203dpi ZPL", zplCode);

                if (printed)
                {
                    int updateTotal = Todayprint + Convert.ToInt32(PrintCount.Value);
                    // Get the Ambasssador Partnumber 
                    string ambassadorText = Ambassador.Text.Trim();

                    // Save to the History Logs
                    foreach (var item in _labelPrint)
                    {
                        await _master.AddnewHistorylist(ambassadorText, item.SDP);
                    }

                    // Update state
                    Todayprint = updateTotal;
                    FileServices.UpdateCountToday(updateTotal);

                    MessageBox.Show("Print successfully!");
                    ResetForms();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           

        }
        private void ZebraPrinter_Load(object sender, EventArgs e)
        {
            CountToday ct = FileServices.GetReelIDCount();
            Todayprint = (ct != null) ? ct.Count : 0;

            if (timer1 == null)
            {
                timer1 = new Timer
                {
                    Interval = 3000 // 3 seconds
                };
                timer1.Tick += timer1_Tick;
            }
            timer1.Start();

            if (timer2 == null)
            {
                timer2 = new Timer
                {
                    Interval = 3000
                };
                timer2.Tick += timer2_Tick;
            }
            timer2.Start();
        }
        private void DrawLabelPreview(List<(string partNum, string barcodeText, string qty)> labelDataList)
        {
            int labelHeight = 140;  // Height of one label row
            int totalHeight = labelHeight * labelDataList.Count;

            Bitmap labelImage = new Bitmap(panelPreview.Width, totalHeight);
            using (Graphics g = Graphics.FromImage(labelImage))
            {
                g.Clear(Color.White);

                Font fontBold = new Font("Arial", 10, FontStyle.Bold);
                Font fontRegular = new Font("Arial", 9);

                for (int i = 0; i < labelDataList.Count; i++)
                {
                    var (partNum, barcodeText, qty) = labelDataList[i];

                    int offsetY = i * labelHeight;

                    // Draw border for each label
                    g.DrawRectangle(Pens.Black, 0, offsetY, labelImage.Width - 1, labelHeight - 1);

                    // Text
                    g.DrawString(partNum, fontRegular, Brushes.Black, new PointF(10, offsetY + 15));
                    // Barcode for SDP
                    var SDPBarcodeWriter = new BarcodeWriter
                    {
                        Format = BarcodeFormat.CODE_128,
                        Options = new EncodingOptions
                        {
                            Height = 30,
                            Width = 200,
                            Margin = 0,
                            PureBarcode = true
                        }
                    };
                    Bitmap SDPbarcodeImage = SDPBarcodeWriter.Write(partNum);
                    g.DrawImage(SDPbarcodeImage, new PointF(15, offsetY + 35));



                    g.DrawString("SMT WH : " + barcodeText + "", fontRegular, Brushes.Black, new PointF(10, offsetY + 75));

                    //Barcode Warehouse
                    var WarehousebarcodeWriter = new BarcodeWriter
                    {
                        Format = BarcodeFormat.CODE_128,
                        Options = new EncodingOptions
                        {
                            Height = 30,
                            Width = 200,
                            Margin = 0,
                            PureBarcode = true
                        }
                    };
                    Bitmap WarehousebarcodeImage = WarehousebarcodeWriter.Write(barcodeText);
                    g.DrawImage(WarehousebarcodeImage, new PointF(5, offsetY + 95));

                    // QR Code
                    var qrWriter = new BarcodeWriter
                    {
                        Format = BarcodeFormat.QR_CODE,
                        Options = new EncodingOptions
                        {
                            Height = 70,
                            Width = 70,
                            Margin = 0
                        }
                    };
                    Bitmap qrImage = qrWriter.Write(barcodeText);
                    g.DrawImage(qrImage, new PointF(labelImage.Width - 90, offsetY + 15));

                    
                    g.DrawString("Qty : ", fontRegular, Brushes.Black, new PointF(labelImage.Width - 90, offsetY + 85));

                    //Barcode Quantity
                    var QuantitybarcodeWriter = new BarcodeWriter
                    {
                        Format = BarcodeFormat.CODE_128,
                        Options = new EncodingOptions
                        {
                            Height = 20,
                            Width = 25,
                            Margin = 0,
                            PureBarcode = true
                        }
                    };
                    Bitmap QuantitybarcodeImage = QuantitybarcodeWriter.Write(qty);
                    g.DrawImage(QuantitybarcodeImage, new PointF(labelImage.Width - 85, offsetY + 100));
                }
            }

            panelPreview.Image = labelImage;
            panelPreview.Size = labelImage.Size;
            //panelPreview.BackgroundImage = labelImage;

        }

        private void Preview_Click(object sender, EventArgs e)
        {
            if(!FormValidation()) return;


            int getTotalprint = Convert.ToInt32(PrintCount.Value);
            DateTime selectedDate = dateTimePicker1.Value;

            // =============== FOR THE DATE DISPLAY FORMATED  =====================
            string formattedDate = $"{selectedDate.Year % 10}{selectedDate.Month:D2}{selectedDate.Day:D2}";

            string sdpPrefix = $"SDP{Ambassador.Text.Trim()} {formattedDate}";
            string warehouse = WarehouseText.Text;
            string quantity = QuantityText.Text;
            // ====================================================================
            var labelDataList = new List<(string PartNum, string BarcodeText, string Qty)>(getTotalprint);

            for (int i = 0; i < getTotalprint; i++)
            {
                Todayprint++;
                string reelId = LabelGenerator.GetReeIDnumber(Todayprint.ToString());
                string fullSDP = $"{sdpPrefix}{reelId}";

                var labelData = new FinalLabelData
                {
                    SDP = fullSDP,
                    Warehouse = warehouse,
                    Quantity = quantity
                };

                _labelPrint.Add(labelData);
                labelDataList.Add((fullSDP, warehouse, quantity));
            }

            DrawLabelPreview(labelDataList);

            // Update UI
            Preview.Visible = false;
            Cancelbtn.Visible = true;
            Printbtn.Visible = true;
        }

        private async void SearchAmbassador(object sender, KeyEventArgs e)
        {
          
            if (e.KeyCode != Keys.Enter) return;
            
            string partnum = Ambassador.Text.Trim();
            Ambassador.Text = partnum;

            //Checks if the input is Empty
            if (String.IsNullOrEmpty(partnum)) return;

            var data = await _master.GetAmbassadordata(partnum);

            // Checkss if the Data is Exist
            if (data == null || data.Count == 0)
            {
                DialogResult result = MessageBox.Show(
                    "Do you want to add this Partnumber to the Database? ",
                    "No partnum found",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    var addform = new AddMasterlist(this, Ambassador.Text, _master);
                    addform.Show();
                }
            
                return;
            }

            // Gets only one Data
            var prod = data.First();
            Warehouse = prod.WarehouseLocal;
            LocationQty = prod.Qty;
            WarehouseText.Text = Warehouse;
            QuantityText.Text = LocationQty.ToString(); 

            FinalRealIDText.Text = LabelGenerator.GenerateReelID(
               dateTimePicker1.Value,
               partnum,
               Todayprint,
               Convert.ToInt32(PrintCount.Value)
           );
            Ambassador.Text = partnum;

            if (prod.Qty == 0)
            {
                EditMasterlist ed = new EditMasterlist(this, _master, partnum);  
                ed.Show();
            }
        }

      

    
        private void Cancelbtn_Click(object sender, EventArgs e) => ResetForms();
     
        //===================== FOR FORM VALIDATION ======================
        public bool FormValidation()
        {
            bool IsEmpPartnum = string.IsNullOrWhiteSpace(Ambassador.Text);
            bool IsQuanValue = Convert.ToInt32(PrintCount.Value) == 0;

            part_error.Visible = IsEmpPartnum;
            Numb_error.Visible = IsQuanValue;


            return !(IsEmpPartnum || IsQuanValue);
        }
        //===================== RESETS FORMS ==============================
        public void ResetForms()
        {
            Ambassador.Text = string.Empty;
            WarehouseText.Text = "N/A";
            QuantityText.Text = "N/A";
            FinalRealIDText.Text = "N/A";
            PrintCount.Value = 0;
            dateTimePicker1.Value = DateTime.Now;

            if (panelPreview.Image != null)
            {
                panelPreview.Image.Dispose();
                panelPreview.Image = null;
            }
            _labelPrint.Clear();
            Preview.Visible = true;
            Cancelbtn.Visible = false;
            Printbtn.Visible = false;
        }

        private async  void timer1_Tick(object sender, EventArgs e)
        {
            if (await ZebraProcess.IsZebraPrinterConnectedAndOnline())
            {
                printerStats.Text = "Zebra printer is Connected";
                printerStats.ForeColor = Color.FromArgb(19, 211, 176);
            }
            else
            {
                printerStats.Text = "Zebra printer is not Connected";
                printerStats.ForeColor = Color.FromArgb(219, 36, 36);
            }
        }

        private async void timer2_Tick(object sender, EventArgs e)
        {
            await FileServices.ResetCountPrinterAsync();
        }

        private void Exitbtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                    "Are you sure you want to exit the application?",
                    "Exit the Application",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            FinalRealIDText.Text = LabelGenerator.GenerateReelID(dateTimePicker1.Value, Ambassador.Text.Trim(), Todayprint, Convert.ToInt32(PrintCount.Value));
        }

        public void SetDataBack(string partnum, string warehouse, string Qty)
        {
            Ambassador.Text = "";
            Ambassador.Text = partnum;
            WarehouseText.Text = warehouse;
            QuantityText.Text = Qty;
        }

        public void EditQuantityBack(string Qty)
        {
            QuantityText.Text = Qty;
        }

        private void Historybtn_Click(object sender, EventArgs e)
        {
            Printer_History h = new Printer_History(_master);
            h.Show();
        }

        private void Ambassador_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control characters like Backspace
            if (char.IsControl(e.KeyChar))
                return;

            // Allow only letters, digits, and dash
            if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != '-')
            {
                e.Handled = true; // Block invalid character
                return;
            }

            // Enforce max length of 11
            if (Ambassador.Text.Length >= 11)
            {
                e.Handled = true;
            }
        }
    }
}
