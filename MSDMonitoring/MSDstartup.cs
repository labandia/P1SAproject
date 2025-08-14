using MSDMonitoring.Data;
using MSDMonitoring.Interface;
using System;
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
                Debug.WriteLine("HERE");
                if (ReelData.RemainQuantity == 0)
                {
                    MessageBox.Show("Reel ID is Already Used ... ");
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
                FloorLifeText.Text = filterdata.FloorLife.ToString();
                strFloorlife = filterdata.FloorLife;
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
            flowLayoutPanel1.Controls.Clear();

            var cardData = await _msd.GetListComponentIN();

            flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanel1.WrapContents = false;
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Padding = new Padding(10);

            // Displays the card layout
            foreach (var card in cardData)
            {
                AddCardReelID(card.RecordID, card.ReelID, card.DateIn, card.Line, card.QuantityIN, card.TimeIn);
            }


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
        public void AddCardReelID(int id, string reelID, string DateIn, int Line,  int Quan, string TimeIn)
        {
            // Main Card Panel
            Panel card = new Panel
            {
                Width = 450,
                Height = 130,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(0, 0, 0, 15),
                Cursor = Cursors.Hand,
                Tag = id // Store the ID
            };

            // ===== HEADER =====
            Panel header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = Color.FromArgb(0, 166, 90)
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
                Location = new Point(card.Width - 120, 10)
            };

            header.Controls.Add(lblPlan);
            header.Controls.Add(lblDate);
            // ===== HEADER LAYOUT =====

            // ===== BODY LAYOUT =====
            TableLayoutPanel bodyLayout = new TableLayoutPanel
            {
                Location = new Point(10, 50),
                Size = new Size(card.Width - 20, 50),
                ColumnCount = 3,
                RowCount = 2,
                AutoSize = false
            };
            bodyLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            bodyLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            bodyLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            bodyLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            // Time IN
            Label lblSerial = new Label { Text = "Time IN:", AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            Label lblModel = new Label { Text = TimeIn, AutoSize = true, Font = new Font("Segoe UI", 11, FontStyle.Regular) };

            // Time OUT (live clock)
            Label lblLine = new Label { Text = "Time OUT:", AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            Label lblTotal = new Label { Text = "", AutoSize = true, Font = new Font("Segoe UI", 11, FontStyle.Regular) };
            lblTotal.Tag = TimeIn; // Store TimeIn for calculation

            // Hours exposed
            Label lblLineV = new Label { Text = "Hours Exposed:", AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            Label lblTotalV = new Label { Text = "", AutoSize = true , Font = new Font("Segoe UI", 11, FontStyle.Regular) };
            lblTotalV.Tag = TimeIn; // Store TimeIn for calculation

            // Row 1
            bodyLayout.Controls.Add(lblSerial, 0, 0);
            bodyLayout.Controls.Add(lblLine, 1, 0);
            bodyLayout.Controls.Add(lblLineV, 2, 0);

            // Row 2
            bodyLayout.Controls.Add(lblModel, 0, 1);
            bodyLayout.Controls.Add(lblTotal, 1, 1);
            bodyLayout.Controls.Add(lblTotalV, 2, 1);


            // ===== FOOTER =====
            Label lblUser = new Label
            {
                Text = "Quantity : " + Quan,
                Location = new Point(10, 100),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                AutoSize = true
            };

            Label lblClock = new Label
            {
                AutoSize = true,
                Location = new Point(350, 100),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.Black
            };
            lblClock.Tag = DateIn; // Store original date

            UpdateCardClock(lblClock); // Initial update

            // Add to card
            card.Controls.Add(header);
            card.Controls.Add(bodyLayout);
            card.Controls.Add(lblUser);
            card.Controls.Add(lblClock);

            // ===== CLICK EVENT (card + children) =====
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

            // Add card to FlowLayoutPanel
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
                    InputIn = InputIn.Text
                };

                bool result = await _msd.AddComponentsData(entryobj);

                if (!result) return;

                MessageBox.Show("INSERT NEW QUANTITY");
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
                MessageBox.Show("Line must be a number.");
                return false;
            }

            // Validate range 1 - 12
            if (lineNumber < 1 || lineNumber > 12)
            {
                MessageBox.Show("Line must be between 1 and 12.");
                return false;
            }

            // Check for duplicates in data
            var isDuplicate = data.Any(res => res.Line == lineNumber);
            if (isDuplicate)
            {
                MessageBox.Show($"Line {lineNumber} is already input.");
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
    }
}
