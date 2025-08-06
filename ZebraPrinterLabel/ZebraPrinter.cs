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
using System.Threading.Tasks;
using System.Windows.Forms;
using ZebraPrinterLabel.Services;
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

        private void LoadSampleLabels()
        {
            _labelsToPrint = new List<LabelData>
        {
            new LabelData { PartNumber = "SDP00715310-02", BarcodeText = "RCC3A001001001", Quantity = "50" },
            new LabelData { PartNumber = "SDP0038921-12", BarcodeText = "RCC3B002003003", Quantity = "30" },
            new LabelData { PartNumber = "SDP12345678-99", BarcodeText = "RCC3C003004004", Quantity = "75" },
            // Add as many labels as needed
        };
        }

        public ZebraPrinter(IMasterlist master)
        {
            InitializeComponent();
            _master=master;

        
        }


        public static string GenerateZplLabel(string topText, string bottomText, string qty)
        {
            return $@"^XA
            ^CI28
            ^PW559
            ^LL208

            ^FO30,10
            ^A0N,30,30
            ^FD*{topText}*^FS

            ^FO30,40
            ^BY2
            ^BCN,60,Y,N,N
            ^FD*{topText}*^FS

            ^FO430,10
            ^BQN,2,6
            ^FDLA,{topText}^FS

            ^FO30,120
            ^A0N,25,25
            ^FDSMT WH *{bottomText}*^FS

            ^FO30,145
            ^BY2
            ^BCN,50,Y,N,N
            ^FD*{bottomText}*^FS

            ^FO430,130
            ^A0N,20,20
            ^FDQTY.:^FS

            ^FO430,150
            ^A0N,40,40
            ^FD*{qty}*^FS

            ^XZ";
        }
        private int _currentLabelIndex = 0;
        private  void Printbtn_Click(object sender, EventArgs e)
        {

      
            List<(string SDP, string Warehouse, string Quantity)> labelData = new List<(string SDP, string Warehouse, string Quantity)>();

            foreach (var item in _labelPrint)
            {
                //Debug.WriteLine($@"SDP : {item.SDP} - Warehouse : {item.Warehouse} - Quantity : {item.Quantity}");
                labelData.Add((item.SDP, item.Warehouse, item.Quantity));
  
            }

            string zplCode = ZebraProcess.GenerateMultiLabelZpl(labelData);

            bool printed = ZebraProcess.SendZplToPrinter("ZDesigner ZD421-203dpi ZPL", zplCode);
            MessageBox.Show(printed ? "Printed successfully!" : "Print failed.");
        
            int UpdateTotal = Todayprint + Convert.ToInt32(PrintCount.Value);
            FileServices.UpdateCountToday(UpdateTotal);

        }


        public async Task<Image> GetLabelPreviewAsync(string zpl)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(zpl);
                var response = await client.PostAsync("http://api.labelary.com/v1/printers/8dpmm/labels/4x6/0/", content);

                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return Image.FromStream(stream);
                }
                else
                {
                    throw new Exception("Failed to get label preview: " + response.StatusCode);
                }
            }
        }
        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            int dpi = 300;
            int labelWidthPx = (int)(264.56692913);   // ≈ 70 mm equivalent
            int labelHeightPx = (int)(98.267716535);  // ≈ 26 mm equivalent
            int marginLeft = 10;
            int marginTop = 10;
            int spacingY = 20;
            int labelsPerPage = 3;

            for (int i = 0; i < labelsPerPage; i++)
            {
                // Creating a Rectangle Form
                int yOffset = marginTop + i * (labelHeightPx + spacingY);

                g.DrawRectangle(Pens.Black, marginLeft, yOffset, labelWidthPx, labelHeightPx);

                // Sample Data
                string sdp = "SDP00715310-02 410210268";
                string location = "RCC3A001001001";
                string qty = "50";

                using (Font fontNormal = new Font("Arial", 7))
                {
                    int xText = marginLeft;
                    int yText = yOffset;

                    // SDP Text
                    g.DrawString("SDP: " + sdp, fontNormal, Brushes.Black, xText, yText);
                    Bitmap sdpBarcode = CodeGenerator.GenerateBarcode(sdp);
                    g.DrawImage(sdpBarcode, xText, yText + 30, 300, 40); // Adjust width/height

                    // QTY Text
                    g.DrawString("QTY: " + qty, fontNormal, Brushes.Black, xText, yText + 80);
                    Bitmap qtyBarcode = CodeGenerator.GenerateBarcode(qty);
                    g.DrawImage(qtyBarcode, xText, yText + 110, 200, 40);

                    // LOC Text
                    g.DrawString("LOC: " + location, fontNormal, Brushes.Black, xText, yText + 160);
                    Bitmap locBarcode = CodeGenerator.GenerateBarcode(location);
                    g.DrawImage(locBarcode, xText, yText + 190, 300, 40);
                }

                // QR Code (top-right)
                Bitmap qr = CodeGenerator.GenerateQRCode(sdp);
                g.DrawImage(qr, marginLeft + labelWidthPx - 200, 10 + 20, 80, 80);
            }

            e.HasMorePages = false;
        }


        private void ZebraPrinter_Load(object sender, EventArgs e)
        {
            CountToday ct = FileServices.GetReelIDCount();

            timer1 = new Timer();
            timer1.Interval = 3000; // Every 5 seconds
            timer1.Tick += timer1_Tick;

            timer1.Start();

            timer2 = new Timer();
            timer2.Interval = 3000;
            timer2.Tick += timer2_Tick;
            timer2.Start();



            Todayprint = (ct != null) ? ct.Count : 0;

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
                Font fontRegular = new Font("Arial", 6);

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
                        Format = BarcodeFormat.CODE_93,
                        Options = new EncodingOptions
                        {
                            Height = 15,
                            Width = 50,
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
                        Format = BarcodeFormat.CODE_93,
                        Options = new EncodingOptions
                        {
                            Height = 15,
                            Width = 50,
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
                            Height = 80,
                            Width = 80,
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

            // =============== FOR THE DATE DISPLAY FORMATED  =====================

            DateTime selectedDate = dateTimePicker1.Value;
            // Get last digit of the year
            int oneDigitYear = selectedDate.Year % 10;

            // Remove leading zeros for month and day
            string month = selectedDate.Month.ToString("D2");
            string day = selectedDate.Day.ToString("D2");

            // Combine to format
            string formatted = $"{oneDigitYear}{month}{day}";
            // ====================================================================



            for (int i = 0; i < getTotalprint; i++)
            {
                Todayprint += 1;
                string strCount = GetReeIDnumber(Todayprint.ToString());
                var newData = new FinalLabelData
                {
                    SDP =  $@"*SDP{Ambassador.Text.Trim()} {formatted}{strCount}*",
                    Warehouse = WarehouseText.Text,
                    Quantity = QuantityText.Text
                };

                _labelPrint.Add(newData);
            }





            List<(string partNum, string barcodeText, string qty)> labelDataList = new List<(string, string, string)>();

            foreach (var item in _labelPrint)
            {
                labelDataList.Add((item.SDP, item.Warehouse, item.Quantity));
            }

            // Draw all in one preview
            DrawLabelPreview(labelDataList);



            //int UpdateTotal = Todayprint + Convert.ToInt32(PrintCount.Value);
            //string NewUpdateCount = GetReeIDnumber(UpdateTotal.ToString());

            ////FileServices.UpdateCountToday(UpdateTotal);



            //reelID = $@"*SDP{Ambassador.Text.Trim()} {formatted}{NewUpdateCount}*";

            //MessageBox.Show($@"COUNT : " + NewUpdateCount);
            //MessageBox.Show("ReelID " + reelID);

            Preview.Visible = false;
            Cancelbtn.Visible = true;
            Printbtn.Visible = true;
        }

        private async void SearchAmbassador(object sender, KeyEventArgs e)
        {
          
            if (e.KeyCode != Keys.Enter) return;
            
            string partnum = Ambassador.Text.Trim();

            //Checks if the input is Empty
            if (String.IsNullOrEmpty(partnum)) return;

            var data = await _master.GetAmbassadordata(partnum);

            // Checkss if the Data is Exist
            if (data == null || !data.Any())
            {
                DialogResult result = MessageBox.Show(
                    "Do you want to add this Partnumber to the Database? ",
                    "No partnum found",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                     MessageBox.Show("OPEN DIALOG");
                }
                else if (result == DialogResult.No)
                {
                    MessageBox.Show("CLOSE THE ad");
                }
                
                return;
            }


            foreach (var prod in data)
            {
                Warehouse = prod.WarehouseLocal;
                LocationQty = prod.Qty;

                WarehouseText.Text = Warehouse;
                QuantityText.Text = LocationQty.ToString();


                Debug.WriteLine($@"Partnumber : {prod.Partnum} - Warehouse Location : {prod.WarehouseLocal}");
                FinalRealIDText.Text = CreationReelID(prod.Partnum, Todayprint);
            }

            Ambassador.Text = partnum;
        }

        public string CreationReelID(string partnum, int LatestCount)
        {
            string strcount; 
            // =============== FOR THE DATE DISPLAY FORMATED  =====================

            DateTime selectedDate = dateTimePicker1.Value;
            // Get last digit of the year
            int oneDigitYear = selectedDate.Year % 10;

            // Remove leading zeros for month and day
            string month = selectedDate.Month.ToString("D2");
            string day = selectedDate.Day.ToString("D2");

            // Combine to format
            string formatted = $"{oneDigitYear}{month}{day}";
            // ====================================================================

            // ==================== PRINT COUNT VALUE   ===========================
            if (Convert.ToInt32(PrintCount.Value) != 0)
            {
                int UpdateTotal = Todayprint + Convert.ToInt32(PrintCount.Value);
                string NewUpdateCount = GetReeIDnumber(UpdateTotal.ToString());
                strcount = NewUpdateCount;
                Todayprint = UpdateTotal;
            }
            else
            {
                string NewUpdateCount = GetReeIDnumber(Todayprint.ToString());
                strcount = NewUpdateCount;
            }
            // ====================================================================


            reelID = $@"*SDP{partnum} {formatted}{strcount}*";


            return reelID;
        }

        public string GetReeIDnumber(string count)
        {
            int Rcount = Convert.ToInt32(count.Length);
            switch (Rcount)
            {
                case 1:
                    return "000" + count;
                case 2:
                    return "00" + count;
                case 3:
                    return "0" + count;
                default:
                    return count;
            }
        }

        private void Cancelbtn_Click(object sender, EventArgs e)
        {
            ResetForms();
        }


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
            Application.Exit(); 
        }

        private void panelPreview_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            FinalRealIDText.Text = CreationReelID(Ambassador.Text.Trim(), Todayprint);
        }

        
    }
}
