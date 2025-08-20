using MSDMonitoring.Data;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Word = Microsoft.Office.Interop.Word;

namespace MSDMonitoring.Services
{
    public sealed class ViewExportPDF
    {
        public static void ExportListToPdf(List<MSDMasterlistodel> masterList, string outputPath)
        {
            if (masterList == null || masterList.Count == 0)
                throw new ArgumentException("Master list cannot be null or empty");

            Word.Application wordApp = null;
            Word.Document doc = null;

            try
            {
                wordApp = new Word.Application();
                wordApp.Visible = false;
                wordApp.DisplayAlerts = Word.WdAlertLevel.wdAlertsNone;
                wordApp.ScreenUpdating = false; // Disable screen updating for performance

                doc = wordApp.Documents.Add();

                // Set margins in one go
                doc.PageSetup.TopMargin = 36;
                doc.PageSetup.BottomMargin = 36;
                doc.PageSetup.LeftMargin = 36;
                doc.PageSetup.RightMargin = 36;
                doc.PageSetup.Gutter = 0;

                // Configure header once
                ConfigureHeader(doc);

                // Add spacing more efficiently
                AddSpacing(doc);

                // Create table with optimized performance
                CreateOptimizedTable(doc, masterList);

                // Configure footer
                ConfigureFooter(doc);

                // Save as PDF with minimal options for better performance
                doc.ExportAsFixedFormat(
                    outputPath,
                    Word.WdExportFormat.wdExportFormatPDF,
                    OpenAfterExport: false,
                    OptimizeFor: Word.WdExportOptimizeFor.wdExportOptimizeForPrint,
                    Range: Word.WdExportRange.wdExportAllDocument
                );
            }
            finally
            {
                CleanupWordObjects(doc, wordApp);
            }
        }


        private static void ConfigureHeader(Word.Document doc)
        {
            foreach (Word.Section section in doc.Sections)
            {
                Word.HeaderFooter header = section.Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary];
                Word.Range headerRange = header.Range;

                headerRange.Text = "MASTER LIST OF MOISTURE SENSITIVE DEVICES";
                headerRange.Font.Size = 14;
                headerRange.Font.Bold = 1;
                headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

                Marshal.ReleaseComObject(headerRange);
                Marshal.ReleaseComObject(header);
            }
        }

        private static void AddSpacing(Word.Document doc)
        {
            // More efficient spacing - single paragraph with larger spacing
            Word.Paragraph spacingParagraph = doc.Content.Paragraphs.Add();
            Word.Range spacingRange = spacingParagraph.Range;

            spacingRange.Text = "";
            spacingParagraph.Format.SpaceAfter = 36; // Combined spacing
            spacingRange.InsertParagraphAfter();

            Marshal.ReleaseComObject(spacingRange);
            Marshal.ReleaseComObject(spacingParagraph);
        }

        private static void CreateOptimizedTable(Word.Document doc, List<MSDMasterlistodel> masterList)
        {
            int rowCount = masterList.Count + 1;
            int colCount = 6;
            double availableWidth = doc.PageSetup.PageWidth - 72; // 36*2 margins

            Word.Table table = doc.Tables.Add(doc.Content, rowCount, colCount);

            // Bulk table configuration
            table.Borders.Enable = 1;
            table.Range.Font.Size = 9;
            table.Range.Font.Name = "Arial";
            table.PreferredWidth = (float)availableWidth;
            table.Rows[1].HeadingFormat = -1;
            table.AllowPageBreaks = true;

            // Set column widths in bulk
            float[] columnWidths = {
                (float)(availableWidth * 0.15f),
                (float)(availableWidth * 0.20f),
                (float)(availableWidth * 0.25f),
                (float)(availableWidth * 0.20f),
                (float)(availableWidth * 0.10f),
                (float)(availableWidth * 0.10f)
            };

            for (int c = 1; c <= colCount; c++)
            {
                table.Columns[c].PreferredWidth = columnWidths[c - 1];
            }

            // Set header cells efficiently
            string[] headers = { "PART NAME", "AMBASSADOR PART NUMBER", "SUPPLIER PART NAME", "SUPPLIER NAME", "MSD LEVEL", "FLOOR LIFE" };

            for (int c = 1; c <= colCount; c++)
            {
                Word.Cell headerCell = table.Cell(1, c);
                Word.Range headerRange = headerCell.Range;

                headerRange.Text = headers[c - 1];
                headerRange.Bold = 1;
                headerRange.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15;
                headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                headerCell.TopPadding = 2;
                headerCell.BottomPadding = 2;

                Marshal.ReleaseComObject(headerRange);
                Marshal.ReleaseComObject(headerCell);
            }

            // Fill data rows with minimal COM calls
            for (int r = 0; r < masterList.Count; r++)
            {
                var item = masterList[r];

                // Get all cells for this row at once
                Word.Cell[] cells = new Word.Cell[colCount];
                for (int c = 1; c <= colCount; c++)
                {
                    cells[c - 1] = table.Cell(r + 2, c);
                }

                // Set cell values
                cells[0].Range.Text = item.Partname ?? "";
                cells[1].Range.Text = item.AmbassadorPartnum ?? "";
                cells[2].Range.Text = item.SupplyPartName ?? "";
                cells[3].Range.Text = item.SupplyName ?? "";
                cells[4].Range.Text = item.Level.ToString() ?? "";
                cells[5].Range.Text = item.FloorLife.ToString() ?? "";

                // Set alignment for each cell
                for (int c = 0; c < colCount; c++)
                {
                    if (c < 4)
                        cells[c].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft; // first 4 left
                    else
                        cells[c].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter; // last 2 center
                }

                // Release COM objects
                foreach (var cell in cells)
                {
                    Marshal.ReleaseComObject(cell.Range);
                    Marshal.ReleaseComObject(cell);
                }
            }

            // Fix table width and reduce row height
            table.AutoFitBehavior(Word.WdAutoFitBehavior.wdAutoFitFixed);
            foreach (Word.Row row in table.Rows)
            {
                row.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly;
                row.Height = 30f; // adjust to make rows more compact
                Marshal.ReleaseComObject(row);
            }

            Marshal.ReleaseComObject(table);
        }

        private static void ConfigureFooter(Word.Document doc)
        {
            foreach (Word.Section section in doc.Sections)
            {
                Word.HeaderFooter footer = section.Footers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary];
                Word.Range footerRange = footer.Range;

                footerRange.Text = "PCFY-00052 Form 1F";
                footerRange.Font.Size = 10;
                footerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

                Marshal.ReleaseComObject(footerRange);
                Marshal.ReleaseComObject(footer);
            }
        }

        private static void CleanupWordObjects(Word.Document doc, Word.Application wordApp)
        {
            try
            {
                if (doc != null)
                {
                    doc.Close(false);
                    Marshal.ReleaseComObject(doc);
                }
            }
            catch { /* Ignore cleanup errors */ }

            try
            {
                if (wordApp != null)
                {
                    wordApp.Quit(false);
                    Marshal.ReleaseComObject(wordApp);
                }
            }
            catch { /* Ignore cleanup errors */ }

            // Force cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }
}
