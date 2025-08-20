

using iTextSharp.text;
using iTextSharp.text.pdf;

namespace MSDMonitoring.Services
{
    public sealed class PdfHeaderFooter : PdfPageEventHelper
    {
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            PdfPTable footerTbl = new PdfPTable(1);
            footerTbl.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            footerTbl.DefaultCell.Border = 0;

            PdfPCell cell = new PdfPCell(new Phrase("PCFY-00052 Form 1F",
                new Font(Font.FontFamily.HELVETICA, 9, Font.ITALIC)));
            cell.Border = 0;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            footerTbl.AddCell(cell);

            footerTbl.WriteSelectedRows(0, -1,
                document.LeftMargin,
                document.BottomMargin - 5,
                writer.DirectContent);
        }
    }
}
