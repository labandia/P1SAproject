using MSDMonitoring.Data;
using MSDMonitoring.Interface;
using MSDMonitoring.View.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Word = Microsoft.Office.Interop.Word;
using System.Runtime.InteropServices;
using MSDMonitoring.Services;

namespace MSDMonitoring
{
    public partial class MSDMasterlist : Form
    {
        private readonly IMSD _msd;
        private List<MSDMasterlistodel> _masterData = new List<MSDMasterlistodel>();

        public MSDMasterlist(IMSD msd)
        {
            InitializeComponent();
            _msd = msd;

            MonitorTable.CellContentClick += MonitorTable_CellContentClick;
        }

        public async Task DisplayData()
        {
            _masterData = await _msd.GetMSDMasterlist();
            MonitorTable.DataSource = _masterData;
            MonitorTable.Columns["Edit"].DisplayIndex = 6;
            MonitorTable.Columns["AmbassadorPartnum"].DisplayIndex = 1;
            MonitorTable.Columns["Edit"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            MonitorTable.Columns["Edit"].Width = 70;
        }

        private async void MSDMasterlist_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.Sizable; // keeps title bar

            await DisplayData();
            
        }

        private void Exitbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      

        private void MonitorTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void Exportbtn_Click(object sender, EventArgs e)
        {
            AddMasterList add = new AddMasterList(_msd, this);
            add.ShowDialog();
        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            string filterText = searchBox.Text.ToLower();
            var filteredList = new List<MSDMasterlistodel>();


            filteredList = _masterData.Where(p => p.AmbassadorPartnum.ToLower().Contains(filterText)).ToList();
            MonitorTable.DataSource =  filteredList;
            MonitorTable.Columns["Edit"].DisplayIndex = 6;
            MonitorTable.Columns["AmbassadorPartnum"].DisplayIndex = 1;
            MonitorTable.Columns["Edit"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            MonitorTable.Columns["Edit"].Width = 70;
        }

        private void MonitorTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Ignore header

            int editColumnIndex = 0; // 7th column (zero-based)

            if (e.ColumnIndex == editColumnIndex)
            {

                var obj = new MSDMasterlistodel
                {
                    AmbassadorPartnum = MonitorTable.Rows[e.RowIndex].Cells[1].Value.ToString(),
                    Partname = MonitorTable.Rows[e.RowIndex].Cells[2].Value.ToString(),
                    SupplyPartName = MonitorTable.Rows[e.RowIndex].Cells[3].Value.ToString(),
                    SupplyName = MonitorTable.Rows[e.RowIndex].Cells[4].Value.ToString(),
                    Level = Convert.ToInt32(MonitorTable.Rows[e.RowIndex].Cells[5].Value),
                    FloorLife = Convert.ToInt32(MonitorTable.Rows[e.RowIndex].Cells[6].Value),
                };

                EditMasterlist ed = new EditMasterlist(_msd, this, obj);
                ed.ShowDialog();
            }
        }

        private async  void button1_Click(object sender, EventArgs e)
        {
            // Use async to keep UI responsive
            await Task.Run(() =>
            {
                try
                {
                    string fixedFilePath = @"C:\Exports\MSD_Masterlist.pdf";
                    Directory.CreateDirectory(Path.GetDirectoryName(fixedFilePath));

                    ViewExportPDF.ExportListToPdf(_masterData, fixedFilePath);

                    // Use Invoke to show message box on UI thread
                    this.Invoke(new Action(() =>
                    {
                        DialogResult openResult = MessageBox.Show(
                            "Would you like to open the file?",
                            "Open PDF",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information
                        );

                        if (openResult == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(fixedFilePath);
                        }
                    }));
                }
                catch (Exception ex)
                {
                    this.Invoke(new Action(() =>
                    {
                        MessageBox.Show(
                            $"Error exporting PDF: {ex.Message}",
                            "Export Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }));
                }
                finally
                {
                    
                }
            });



            //try
            //{
            //    // Define a fixed file path
            //    string fixedFilePath = @"C:\Exports\MSD_Masterlist.pdf";

            //    // Create directory if it doesn't exist
            //    Directory.CreateDirectory(Path.GetDirectoryName(fixedFilePath));

            //    // Check if file exists and confirm overwrite if needed
            //    //if (File.Exists(fixedFilePath))
            //    //{
            //    //    DialogResult overwriteResult = MessageBox.Show(
            //    //        "File already exists. Do you want to overwrite it?",
            //    //        "Overwrite Confirmation",
            //    //        MessageBoxButtons.YesNo,
            //    //        MessageBoxIcon.Warning
            //    //    );

            //    //    if (overwriteResult != DialogResult.Yes)
            //    //    {
            //    //        return; // User cancelled overwrite
            //    //    }
            //    //}

            //    // Export the PDF
            //    ExportListToPdf(_masterData, fixedFilePath);

            //    // Ask if user wants to open the file
            //    DialogResult openResult = MessageBox.Show(
            //        $"Would you like to open the file?",
            //        "Open PDF",
            //        MessageBoxButtons.YesNo,
            //        MessageBoxIcon.Information
            //    );

            //    if (openResult == DialogResult.Yes)
            //    {
            //        System.Diagnostics.Process.Start(fixedFilePath);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(
            //        $"Error exporting PDF: {ex.Message}",
            //        "Export Error",
            //        MessageBoxButtons.OK,
            //        MessageBoxIcon.Error
            //    );
            //}
        }


    


        public void ExportListToPdf(List<MSDMasterlistodel> masterList, string outputPath)
        {
            if (masterList == null || masterList.Count == 0)
            {
                throw new ArgumentException("Master list cannot be null or empty");
            }

            Word.Application wordApp = null;
            Word.Document doc = null;

            try
            {
                wordApp = new Word.Application();
                wordApp.Visible = false;
                wordApp.DisplayAlerts = Word.WdAlertLevel.wdAlertsNone;

                doc = wordApp.Documents.Add();

                // -------------------------
                // Set proper margins for better layout
                // -------------------------
                doc.PageSetup.TopMargin = 36;    // 0.5 inch
                doc.PageSetup.BottomMargin = 36; // 0.5 inch
                doc.PageSetup.LeftMargin = 36;   // 0.5 inch
                doc.PageSetup.RightMargin = 36;  // 0.5 inch
                doc.PageSetup.Gutter = 0;

                // -------------------------
                // Add Header (centered, bold, size 14)
                // -------------------------
                foreach (Word.Section section in doc.Sections)
                {
                    Word.HeaderFooter header = section.Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary];
                    header.Range.Text = "MASTER LIST OF MOISTURE SENSITIVE DEVICES";
                    header.Range.Font.Size = 14;
                    header.Range.Font.Bold = 1;
                    header.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft; 
                }

                Word.Paragraph spaceParagraph1 = doc.Content.Paragraphs.Add();
                spaceParagraph1.Range.Text = ""; // Empty text
                spaceParagraph1.Format.SpaceAfter = 12; // Add space after this paragraph (12 points)
                spaceParagraph1.Range.InsertParagraphAfter();

                Word.Paragraph spaceParagraph2 = doc.Content.Paragraphs.Add();
                spaceParagraph2.Range.Text = ""; // Empty text
                spaceParagraph2.Format.SpaceAfter = 12; // Add more space
                spaceParagraph2.Range.InsertParagraphAfter();

                // Method 2: Add a title paragraph with space
                Word.Paragraph titleParagraph = doc.Content.Paragraphs.Add();
                titleParagraph.Range.Text = ""; // Empty title (just for spacing)
                titleParagraph.Format.SpaceAfter = 18; // 18 points = 0.25 inch
                titleParagraph.Range.InsertParagraphAfter();


                // -------------------------
                // Insert table with exact column structure
                // -------------------------
                int rowCount = masterList.Count + 1;
                int colCount = 6;

                // Add minimal space before table
                Word.Paragraph spacePara = doc.Content.Paragraphs.Add();
                spacePara.Format.SpaceAfter = 15;
                spacePara.Range.InsertParagraphAfter();

                Word.Table table = doc.Tables.Add(doc.Content, rowCount, colCount);

                // Set table properties
                table.Borders.Enable = 1;
                table.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                table.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                table.Range.Font.Size = 9; // Slightly larger for better readability
                table.Range.Font.Name = "Arial";
                table.AllowAutoFit = true;

                // Enable header row repetition across pages
                table.Rows[1].HeadingFormat = -1;

                // Use available page width
                double availableWidth = doc.PageSetup.PageWidth - doc.PageSetup.LeftMargin - doc.PageSetup.RightMargin;
                table.PreferredWidth = (float)availableWidth;

                // Set optimized column widths
                table.Columns[1].PreferredWidth = (float)(availableWidth * 0.15); // PART NAME
                table.Columns[2].PreferredWidth = (float)(availableWidth * 0.20); // AMBA PART NUMBER
                table.Columns[3].PreferredWidth = (float)(availableWidth * 0.25); // SUPPLIER PART NAME
                table.Columns[4].PreferredWidth = (float)(availableWidth * 0.20); // SUPPLIER NAME
                table.Columns[5].PreferredWidth = (float)(availableWidth * 0.10); // MSD LEVEL
                table.Columns[6].PreferredWidth = (float)(availableWidth * 0.10); // FLOOR LIFE

                // Allow table to break across pages
                table.AllowPageBreaks = true;

                // Set column headers - FIXED: Corrected "FLOOR LIFE" spelling
                table.Cell(1, 1).Range.Text = "PART NAME";
                table.Cell(1, 2).Range.Text = "AMBA PART NUMBER";
                table.Cell(1, 3).Range.Text = "SUPPLIER PART NAME";
                table.Cell(1, 4).Range.Text = "SUPPLIER NAME";
                table.Cell(1, 5).Range.Text = "MSD LEVEL";
                table.Cell(1, 6).Range.Text = "FLOOR LIFE"; // FIXED: Correct spelling

                // Format header row
                for (int c = 1; c <= colCount; c++)
                {
                    table.Cell(1, c).Range.Bold = 1;
                    table.Cell(1, c).Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15;
                    table.Cell(1, c).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    table.Cell(1, c).VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                    // Set cell padding
                    table.Cell(1, c).TopPadding = 2;
                    table.Cell(1, c).BottomPadding = 2;
                    table.Cell(1, c).LeftPadding = 3;
                    table.Cell(1, c).RightPadding = 3;
                }

                // Fill table with data
                for (int r = 0; r < masterList.Count; r++)
                {
                    var item = masterList[r];

                    // Ensure data is properly formatted
                    table.Cell(r + 2, 1).Range.Text = item.Partname ?? string.Empty;
                    table.Cell(r + 2, 2).Range.Text = item.AmbassadorPartnum ?? string.Empty;
                    table.Cell(r + 2, 3).Range.Text = item.SupplyPartName ?? string.Empty;
                    table.Cell(r + 2, 4).Range.Text = item.SupplyName ?? string.Empty;
                    table.Cell(r + 2, 5).Range.Text = item.Level.ToString() ?? string.Empty;
                    table.Cell(r + 2, 6).Range.Text = item.FloorLife.ToString() ?? string.Empty;

                    // Center align MSD Level and Floor Life columns
                    table.Cell(r + 2, 5).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    table.Cell(r + 2, 6).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                    // Left align other columns
                    table.Cell(r + 2, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    table.Cell(r + 2, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    table.Cell(r + 2, 3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    table.Cell(r + 2, 4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

                    // Set cell padding for data rows
                    table.Cell(r + 2, 1).TopPadding = 1;
                    table.Cell(r + 2, 1).BottomPadding = 1;
                    table.Cell(r + 2, 1).LeftPadding = 3;
                    table.Cell(r + 2, 1).RightPadding = 3;
                }

                // Auto-fit the table to ensure proper formatting
                table.AutoFitBehavior(Word.WdAutoFitBehavior.wdAutoFitWindow);

                // -------------------------
                // Add Footer - FIXED: Center aligned
                // -------------------------
                foreach (Word.Section section in doc.Sections)
                {
                    Word.HeaderFooter footer = section.Footers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary];
                    footer.Range.Text = "PCFY-00052 Form 1F";
                    footer.Range.Font.Size = 10;
                    footer.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                }

                // -------------------------
                // Save as PDF
                // -------------------------
                doc.ExportAsFixedFormat(
                    outputPath,
                    Word.WdExportFormat.wdExportFormatPDF,
                    OpenAfterExport: false,
                    OptimizeFor: Word.WdExportOptimizeFor.wdExportOptimizeForPrint,
                    Range: Word.WdExportRange.wdExportAllDocument,
                    IncludeDocProps: false,
                    KeepIRM: false,
                    CreateBookmarks: Word.WdExportCreateBookmarks.wdExportCreateNoBookmarks,
                    DocStructureTags: false,
                    BitmapMissingFonts: true,
                    UseISO19005_1: false
                );
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error generating PDF: " + ex.Message, ex);
            }
            finally
            {
                // Cleanup code remains the same
                if (doc != null)
                {
                    doc.Close(false);
                    Marshal.ReleaseComObject(doc);
                }

                if (wordApp != null)
                {
                    wordApp.Quit(false);
                    Marshal.ReleaseComObject(wordApp);
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
    }


   
}
