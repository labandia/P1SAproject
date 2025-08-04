using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing.Common;
using ZXing;

namespace ZebraPrinterLabel
{
    public class LabelGenerator
    {
        public static Bitmap GenerateLabel(string partnum, string location, string qty)
        {
            int dpi = 300;
            float mmToInch = 0.0393701f;

            int widthPx = (int)(70 * mmToInch * dpi);
            int heightPx = (int)(26 * mmToInch * dpi);

            Bitmap label = new Bitmap(widthPx, heightPx);
            using (Graphics g = Graphics.FromImage(label))
            {
                g.Clear(Color.White);
                Font font = new Font("Arial", 18, FontStyle.Bold);
                Font smallFont = new Font("Arial", 14, FontStyle.Regular);
                Brush brush = Brushes.Black;

                // Barcode generator
                var barcodeWriter = new BarcodeWriterPixelData
                {
                    Format = BarcodeFormat.CODE_128,
                    Options = new EncodingOptions
                    {
                        Height = 50,
                        Width = 400,
                        Margin = 0
                    }
                };

                var qrWriter = new BarcodeWriter
                {
                    Format = BarcodeFormat.QR_CODE,
                    Options = new EncodingOptions
                    {
                        Width = 150,
                        Height = 150,
                        Margin = 0
                    }
                };

                int x = 20;
                int y = 10;

                // SDP Text and Barcode
                g.DrawString($"*{partnum}*", font, brush, x, y);
                y += 35;

                using (var bmp = barcodeWriter.Write(partnum).ToBitmap())
                    g.DrawImage(bmp, x, y);

                // QR Code
                using (var qr = qrWriter.Write(partnum))
                    g.DrawImage(qr, widthPx - 160, 10);

                // Location Text and Barcode
                y += 70;
                g.DrawString($"SMT WH *{location}*", smallFont, brush, x, y);
                y += 30;
                using (var locBarcode = barcodeWriter.Write(location).ToBitmap())
                    g.DrawImage(locBarcode, x, y);

                // QTY text and barcode
                int qtyX = widthPx - 160;
                int qtyY = 180;
                g.DrawString($"QTY.:", smallFont, brush, qtyX, qtyY);

                using (var qtyBarcode = barcodeWriter.Write(qty).ToBitmap())
                    g.DrawImage(qtyBarcode, qtyX, qtyY + 25);
            }

            return label;
        }
    }
}
