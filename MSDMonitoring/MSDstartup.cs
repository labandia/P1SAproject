using MSDMonitoring.Data;
using MSDMonitoring.Interface;
using System;
using System.Collections.Generic;
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

        // ---------------------------
        // Class-level fields
        // ---------------------------
        private List<MSDCardModel> _cardDataCache = new List<MSDCardModel>();

        public MSDstartup(IMSD msd)
        {
            InitializeComponent();
            _msd = msd;
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
            
                if (ReelData.RemainQuantity == 0)
                {
                    MessageBox.Show("Reel ID is Already Used ... ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                FloorLifeText.Text = ReelData.FloorLife.ToString();
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
                }
                else
                {
                    FloorLifeText.Text = "168"; // default value
                    strFloorlife = 168; // default value
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
                    TimeIn = c.TimeIn
                }).ToList();

            BuildCards();
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
                AddCardReelID(card.RecordID, card.ReelID, card.DateIn, card.Line, card.QuantityIN, card.TimeIn, cardWidth);
            }
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
            FloorLifeText.Text = "";
            QtyIn.Text = "";
            LineText.Text = "";
            InputIn.Text = "";
            LotText.Text ="";
        }


        // ========================================================================== //
        // =================== CARD LAYOUT AND FUNCTIONALITY ======================== //
        // ========================================================================== //
        public void AddCardReelID(int id, string reelID, string DateIn, int Line, int Quan, string TimeIn, int cardWidth)
        {
            // Main Card Panel (dynamic width)
            Panel card = new Panel
            {
                Width = cardWidth,
                Height = 130,
                BackColor = Color.White,
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
                Text = "Quantity : " + Quan,
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
        private async void NewEntryBtn_Click(object sender, EventArgs e)
        {
            if (await FormValidation())
            {
                await _msd.UpdateChecker(strReelID, strFloorlife, Convert.ToInt32(QtyIn.Text));


                DateTime selectedDate = DateTime.Now;


                var entryobj = new InputIN_MSD
                {
                    ReelID = strReelID,
                    AmbassadorPartnum = strpartnum,
                    DateIn =  selectedDate.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                    QuantityIN = Convert.ToInt32(QtyIn.Text),
                    Line = Convert.ToInt32(LineText.Text),
                    RemainFloor = Convert.ToDouble(FloorLifeText.Text),
                    InputIn = InputIn.Text, 
                    LotNo = LotText.Text
                };

                bool result = await _msd.AddComponentsData(entryobj);

                if (!result) return;

                await LoadData();
                ResetForm();    
            }
        }
        public async Task<bool> FormValidation()
        {
            var data = await _msd.GetListComponentIN();

            if(string.IsNullOrEmpty(Ambassador.Text) || string.IsNullOrEmpty(QtyIn.Text) || string.IsNullOrEmpty(InputIn.Text) || string.IsNullOrEmpty(LineText.Text))
            {
                ReelID_error.Visible = string.IsNullOrEmpty(Ambassador.Text) ? true : false;
                QuanError.Visible = string.IsNullOrEmpty(QtyIn.Text) ? true : false;
                NameError.Visible = string.IsNullOrEmpty(InputIn.Text) ? true : false;
                LineError.Visible = string.IsNullOrEmpty(LineText.Text) ? true : false;
                return false;
            }

            // Validate Line number is integer
            if (!int.TryParse(LineText.Text, out int lineNumber))
            {
                MessageBox.Show("Line must be a number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validate range 1 - 12
            if (lineNumber < 1 || lineNumber > 12)
            {
                MessageBox.Show("Line must be between 1 and 12.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Check for duplicates in data
            var isDuplicate = data.Any(res => res.Line == lineNumber);
            if (isDuplicate)
            {
                MessageBox.Show($"Line {lineNumber} is already input.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        // ========================================================================== //
        // ========================= TEXT BOX CONTROLS ============================== //
        // ========================================================================== //
        private void QtyIn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Reject the input
            }
        }
        private void LineText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Reject the input
            }
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
    }
}
