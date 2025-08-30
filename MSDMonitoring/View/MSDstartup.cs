using MSDMonitoring.Data;
using MSDMonitoring.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSDMonitoring
{
    public partial class MSDstartup : Form
    {
        private readonly IMSD _msd;

        public string strReelID;
        public double strFloorlife;
        public string strpartnum;
        public string strstartTime;
        public string strEndTime;

        private Timer liveTimer;
        private Timer ScanTimer;
        private bool isScanning = false;
        private bool scanHandled = false;

        private Timer LoadTimer;

        // ---------------------------
        // Class-level fields
        // ---------------------------
        private List<MSDCardModel> _cardDataCache = new List<MSDCardModel>();

        public MSDstartup(IMSD msd)
        {
            InitializeComponent();
            _msd = msd;

            // Setup scan detection timer
            ScanTimer = new System.Windows.Forms.Timer();
            ScanTimer.Interval = 200; // Adjust depending on scanner speed
            ScanTimer.Tick += ScanTimer_Tick;


            LoadDataTimer();

            Ambassador.TextChanged += Ambassador_TextChanged;
        }
        private async void Ambassador_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            // Step 1 : Set the Reel ID
            string partnum = Ambassador.Text;
            strReelID = partnum;

            string ambassadorpartnum = partnum.Substring(3).Split(' ')[0];
            strpartnum = ambassadorpartnum;

            // Step 2 : Get the History List
            var ReelData = await _msd.GetReelID(partnum);

            if(ReelData != null)
            {
            
                if (ReelData.RemainQuantity == 0 || ReelData.FloorLife == 0)
                {
                    MessageBox.Show("Reel ID is Already Used ... ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Ambassador.Text = "";
                    Ambassador.Focus();
                    return;
                }
                FloorLifeText.Text = ReelData.FloorLife.ToString();
                LevelText.Text = ReelData.Level.ToString();
                QtyIn.Text = ReelData.RemainQuantity.ToString();
                QtyIn.ReadOnly = true;
                strFloorlife = ReelData.FloorLife;
            }
            else
            {
                // Get the Data of Masterlist
             
                var GetMastelistData = await _msd.GetMSDMasterlist();

                var filterdata = GetMastelistData.SingleOrDefault(res => res.AmbassadorPartnum == ambassadorpartnum);

                // Checks if the Partnumber is Exist in the MasterlistData
                if (filterdata != null)
                {
                    FloorLifeText.Text = filterdata.FloorLife.ToString();
                    strFloorlife = filterdata.FloorLife;
                    LevelText.Text = filterdata.Level.ToString();
                }
                else
                {
                    MessageBox.Show("No Partnum in the Masterlist ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                QtyIn.Focus();
            }

            Ambassador.Text = partnum;
        }

        // ========================================================================== //
        // =================== LOAD DATA FUNCTIONALITY  ============================= //
        // ========================================================================== //
        private async void MSDstartup_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.Sizable; // keeps title bar
            LineSelect.SelectedIndex = 0;

            liveTimer = new Timer { Interval = 1000 }; // 1 second updates
            liveTimer.Tick += LiveTimer_Tick;
            liveTimer.Start();


            await LoadData();
        }
        public async Task LoadData()
        {
            flowLayoutPanel1.WrapContents = true;
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Padding = new Padding(10);
            flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanel1.WrapContents = true;

            // Get fresh data and store in cache
            _cardDataCache = (await _msd.GetListComponentIN())
                .Select(c => new MSDCardModel
                {
                    RecordID = c.RecordID,
                    ReelID = c.ReelID,
                    DateIn = c.DateIn,
                    Line = c.Line,
                    QuantityIN = c.QuantityIN,
                    TimeIn = c.TimeIn, 
                    InputIn = c.InputIn
                }).ToList();

            BuildCards();
        }

      

        // ---------------------------
        // Resize Event Handler
        // ---------------------------
        private void flowLayoutPanel1_Resize(object sender, EventArgs e)
        {
            if (_cardDataCache.Count > 0)
                BuildCards(); // Redraw with new sizes
        }

        public void ResetForm()
        {
            Ambassador.Text = "";
            LevelText.Text = "N/A";
            FloorLifeText.Text = "N/A";
            QtyIn.Text = "";
            //LineText.Text = "";
            LineSelect.SelectedIndex = 0;
            InputIn.Text = "";
            LotText.Text ="";
        }


        // ========================================================================== //
        // =================== CARD LAYOUT AND FUNCTIONALITY ======================== //
        // ========================================================================== //
        public void AddCardReelID(int id, string reelID, string DateIn, int Line, int Quan, string TimeIn, int cardWidth, string inputIn)
        {
            // Parse TimeIn
            DateTime timeInValue;
            if (!DateTime.TryParse(TimeIn, out timeInValue))
            {
                timeInValue = DateTime.Now; // fallback in case of invalid time
            }

            // Calculate exposed hours (decimal)
            double exposedHours = (DateTime.Now - timeInValue).TotalHours;

            // Main Card Panel (dynamic width)
            Panel card = new Panel
            {
                Width = cardWidth,
                Height = 130,
                BackColor = exposedHours >= 1.0 ? Color.Yellow : Color.White, // 🔹 Change color if >= 1 hour
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10),
                Cursor = Cursors.Hand,
                Tag = id
            };

            // ===== HEADER =====
            Panel header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = Color.FromArgb(9, 98, 82)
            };

            Label lblPlan = new Label
            {
                Text = reelID,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(10, 8),
                AutoSize = true
            };

            Label lblDate = new Label
            {
                Text = $"Line " + Line,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(card.Width - 70, 10)
            };

            header.Controls.Add(lblPlan);
            header.Controls.Add(lblDate);

            // ===== BODY LAYOUT (3 columns) =====
            TableLayoutPanel bodyLayout = new TableLayoutPanel
            {
                Location = new Point(10, 50),
                Size = new Size(card.Width - 20, 50),
                ColumnCount = 3,
                RowCount = 2,
                AutoSize = false
            };
            bodyLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            bodyLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            bodyLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));

            // Time IN
            Label lblSerial = new Label { Text = "Time IN:", AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            Label lblModel = new Label { Text = TimeIn, AutoSize = true, Font = new Font("Segoe UI", 11, FontStyle.Regular) };

            // Time OUT (live clock)
            Label lblLine = new Label { Text = "Time OUT:", AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            Label lblTotal = new Label { Text = "", AutoSize = true, Font = new Font("Segoe UI", 11, FontStyle.Regular) };
            lblTotal.Tag = TimeIn; // Store TimeIn for calculation

            // Hours exposed
            Label lblLineV = new Label { Text = "Hours Exposed:", AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            Label lblTotalV = new Label { Text = "", AutoSize = true, Font = new Font("Segoe UI", 11, FontStyle.Regular) };
            lblTotalV.Tag = TimeIn; // Store TimeIn for calculation

            // Add to body
            bodyLayout.Controls.Add(lblSerial, 0, 0);
            bodyLayout.Controls.Add(lblLine, 1, 0);
            bodyLayout.Controls.Add(lblLineV, 2, 0);
            bodyLayout.Controls.Add(lblModel, 0, 1);
            bodyLayout.Controls.Add(lblTotal, 1, 1);
            bodyLayout.Controls.Add(lblTotalV, 2, 1);

            // ===== FOOTER =====
            Label lblUser = new Label
            {
                Text = "Quantity : " + Quan + "   Name : " + inputIn,
                Location = new Point(10, 100),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.Black,
                AutoSize = true
            };

            Label lblClock = new Label
            {
                AutoSize = true,
                Location = new Point(card.Width - 100, 100),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.Black
            };
            lblClock.Tag = DateIn;

            UpdateCardClock(lblClock);

            // Add to card
            card.Controls.Add(header);
            card.Controls.Add(bodyLayout);
            card.Controls.Add(lblUser);
            card.Controls.Add(lblClock);

            // Click events
            card.Click += Card_Click;
            foreach (Control c in card.Controls)
            {
                c.Click += (s, e) => Card_Click(card, e);
                if (c is Panel innerPanel)
                {
                    foreach (Control innerControl in innerPanel.Controls)
                    {
                        innerControl.Click += (s, e) => Card_Click(card, e);
                    }
                }
            }

            flowLayoutPanel1.Controls.Add(card);
        }
        // ---------------------------
        // Build Cards (Responsive Width)
        // ---------------------------
        private void BuildCards()
        {
            flowLayoutPanel1.Controls.Clear();

            if (_cardDataCache.Count == 0)
                return;

            int panelWidth = flowLayoutPanel1.ClientSize.Width;
            int columns;

            if (panelWidth >= 1280)       // Around 1366px monitor width
                columns = 3;
            else if (panelWidth >= 900)   // Medium screens
                columns = 2;
            else                          // Small screens
                columns = 1;

            int totalSpacing = flowLayoutPanel1.Padding.Left + flowLayoutPanel1.Padding.Right + (columns * 20);
            int cardWidth = (panelWidth - totalSpacing) / columns;

            foreach (var card in _cardDataCache)
            {
                AddCardReelID(card.RecordID, card.ReelID, card.DateIn, card.Line, card.QuantityIN, card.TimeIn, cardWidth, card.InputIn);
            }
        }
        private void Card_Click(object sender, EventArgs e)
        {
            if (sender is Panel cardPanel && cardPanel.Tag is int id)
            {
                // Open new form and pass the ID
                ComponentsOut outform = new ComponentsOut(this, _msd, id);
                outform.ShowDialog();
            }
        }
        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            foreach (Control card in flowLayoutPanel1.Controls)
            {
                foreach (Control ctrl in card.Controls)
                {
                    if (ctrl is Label lbl && lbl.Tag != null)
                    {
                        UpdateCardClock(lbl);
                    }
                }
            }
        }
        private void UpdateCardClock(Label lblClock)
        {
            string date = lblClock.Tag.ToString();
            lblClock.Text = $"{date}";
        }
        private void LiveTimer_Tick(object sender, EventArgs e)
        {
            foreach (Control card in flowLayoutPanel1.Controls)
            {
                foreach (Control ctrl in card.Controls)
                {
                    if (ctrl is TableLayoutPanel body)
                    {
                        foreach (Control item in body.Controls)
                        {
                            if (item is Label lbl && lbl.Tag != null)
                            {
                                if (DateTime.TryParse(lbl.Tag.ToString(), out DateTime timeIn))
                                {
                                    int col = body.GetPositionFromControl(lbl).Column;

                                    if (col == 1) // Time OUT column
                                    {
                                        lbl.Text = DateTime.Now.ToString("HH:mm:ss");
                                    }
                                    else if (col == 2) // Hours Exposed column
                                    {
                                        TimeSpan diff = DateTime.Now - timeIn;
                                        double hoursDecimal = diff.TotalHours; // Convert to decimal hours
                                        lbl.Text = hoursDecimal.ToString("0.00"); // 2 decimal places
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        // ========================================================================== //
        // ========================= ADD FUNCTIONALITY ============================== //
        // ========================================================================== //
        private async void NewEntryBtn_Click(object sender, EventArgs e) => await ComponentsDataEntry();
        
        public async Task<bool> FormValidation()
        {
            var data = await _msd.GetListComponentIN();

            if(string.IsNullOrEmpty(Ambassador.Text) || string.IsNullOrEmpty(QtyIn.Text) 
                || string.IsNullOrEmpty(InputIn.Text) || string.IsNullOrEmpty(LotText.Text)
                || string.IsNullOrEmpty(SupplierText.Text))
            {
                ReelID_error.Visible = string.IsNullOrEmpty(Ambassador.Text) ? true : false;
                QuanError.Visible = string.IsNullOrEmpty(QtyIn.Text) ? true : false;
                NameError.Visible = string.IsNullOrEmpty(InputIn.Text) ? true : false;
                lotError.Visible = string.IsNullOrEmpty(LotText.Text) ? true : false;
                SupplyError.Visible = string.IsNullOrEmpty(SupplierText.Text) ? true : false;
                return false;
            }

            if(LineSelect.SelectedIndex == 0)
            {
                LineError.Visible = LineSelect.SelectedIndex == 0 ? true : false;
                return false;
            }


            // Check for duplicates in data
            int selectedLine = Convert.ToInt32(LineSelect.Text);

            // ✅ Different record limits depending on line
            int maxAllowed = (selectedLine == 9 || selectedLine == 11) ? 4 : 2;

            int lineCount = data.Count(res => res.Line == selectedLine);

            if (lineCount >= maxAllowed)
            {
                MessageBox.Show(
                    $"Line {selectedLine} already has {maxAllowed} entries. Maximum limit reached.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error
                );
                return false;
            }

            return true;
        }

        // ========================================================================== //
        // ========================= TEXT BOX CONTROLS ============================== //
        // ========================================================================== //
        private void LineText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true; // Reject the input
        }
        // ========================================================================== //
        // ========================= ACTION BUTTONS ================================= //
        // ========================================================================== //

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
        private void Historybtn_Click(object sender, EventArgs e)
        {
            MSDHIstory md = new MSDHIstory(_msd);
            md.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MSDMasterlist md = new MSDMasterlist(_msd);
            md.ShowDialog();
        }
        //private bool _isUpdating = false;
        private void QtyIn_TextChanged(object sender, EventArgs e)
        {
            // Reset timer every time text changes
            if (scanTimer == null)
            {
                scanTimer = new System.Windows.Forms.Timer();
                scanTimer.Interval = 50; // 50 ms after last character
                scanTimer.Tick += ScanTimerV2_Tick;
            }

            scanTimer.Stop();
            scanTimer.Start();
        }

        private async void InputIn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            await ComponentsDataEntry();
        }


        public async Task ComponentsDataEntry()
        {
            if (await FormValidation())
            {
                await _msd.UpdateChecker(strReelID, strFloorlife, Convert.ToInt32(QtyIn.Text));

                int selectedLine = Convert.ToInt32(LineSelect.Text);
                DateTime selectedDate = DateTime.Now;


                var entryobj = new InputIN_MSD
                {
                    ReelID = strReelID,
                    AmbassadorPartnum = strpartnum,
                    DateIn =  selectedDate.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                    QuantityIN = Convert.ToInt32(QtyIn.Text),
                    Line = selectedLine,
                    RemainFloor = Convert.ToDouble(FloorLifeText.Text),
                    InputIn = InputIn.Text,
                    LotNo = LotText.Text,
                    SupplierName = SupplierText.Text
                };

                bool result = await _msd.AddComponentsData(entryobj);

                if (!result) return;

                await LoadData();
                ResetForm();
            }
        }

        private void LineSelect_SelectedIndexChanged(object sender, EventArgs e) => InputIn.Focus();
      
        private void Ambassador_TextChanged(object sender, EventArgs e)
        {
            if (scanHandled) return; // Ignore changes after processing

            isScanning = true;
            ScanTimer.Stop();
            ScanTimer.Start();
        }

        private async void ScanTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                ScanTimer.Stop();

                if (isScanning && !scanHandled)
                {
                    isScanning = false;
                    scanHandled = true; // Prevent double execution

                    // === Your original scan handling code ===
                    string partnum = Ambassador.Text;
                    strReelID = partnum;

                    if (string.IsNullOrEmpty(partnum) || partnum.Length <= 3)
                    {
                        MessageBox.Show("Invalid scanned Reel ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Ambassador.Text = "";
                        scanHandled = false; // Allow next scan
                        Ambassador.Focus();
                        return;
                    }


                    string ambassadorpartnum = partnum.Substring(3).Split(' ')[0];
                    strpartnum = ambassadorpartnum;

                    // Step 2 : Get the History List
                    var ReelData = await _msd.GetReelID(partnum);

                    if (ReelData != null)
                    {
                        if (ReelData.RemainQuantity == 0 || ReelData.FloorLife == 0)
                        {
                            MessageBox.Show("Reel ID is Already Used ... ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Ambassador.Text = "";
                            scanHandled = false; // Allow next scan
                            Ambassador.Focus();
                            return;
                        }
                        FloorLifeText.Text = ReelData.FloorLife.ToString();
                        LevelText.Text = ReelData.Level.ToString();
                        QtyIn.Text = ReelData.RemainQuantity.ToString();
                        QtyIn.ReadOnly = true;
                        strFloorlife = ReelData.FloorLife;
                    }
                    else
                    {
                        // Get the Data of Masterlist
                        var GetMastelistData = await _msd.GetMSDMasterlist();
                        var filterdata = GetMastelistData.SingleOrDefault(res => res.AmbassadorPartnum == ambassadorpartnum);

                        if (filterdata != null)
                        {
                            FloorLifeText.Text = filterdata.FloorLife.ToString();
                            strFloorlife = filterdata.FloorLife;
                            LevelText.Text = filterdata.Level.ToString();
                        }
                        else
                        {
                            MessageBox.Show("No Partnum in the Masterlist ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Ambassador.Text = "";
                            scanHandled = false; // Allow next scan
                            Ambassador.Focus();
                            return;
                        }

                        QtyIn.Focus();
                    }

                    Ambassador.Text = partnum;
                    // === End of your code ===
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private System.Windows.Forms.Timer scanTimer;

        private void ScanTimerV2_Tick(object sender, EventArgs e)
        {
            scanTimer.Stop();

            if (QtyIn.Text.Length > 0)
            {
                // Parse safely
                if (int.TryParse(QtyIn.Text, out int value))
                {
                    QtyIn.Text = value.ToString(); // 00067 -> 67
                    QtyIn.SelectionStart = QtyIn.Text.Length;

                    // Move to LotText after scan
                    LotText.Focus();
                }
                else
                {
                    QtyIn.Text = "0";
                    QtyIn.SelectionStart = QtyIn.Text.Length;
                }
            }
        }


        private void LoadDataTimer()
        {
            LoadTimer = new Timer();
            LoadTimer.Interval = 10000; // 10 seconds = 10000 milliseconds
            LoadTimer.Tick += TimerLoad_Tick;
            LoadTimer.Start(); // start the timer
        }

        private async void TimerLoad_Tick(object sender, EventArgs e)
        {
            await LoadData();
        }

        private async void Refreshbtn_Click(object sender, EventArgs e)
        {
           await LoadData();
        }

        private async void NewEntryBtn_Click_1(object sender, EventArgs e)
        {
            await ComponentsDataEntry();
        }
    }
}
