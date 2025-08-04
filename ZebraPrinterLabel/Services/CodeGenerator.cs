using System;
using System.Collections.Generic;
using System.Drawing;

using ZXing.Common;
using ZXing;

namespace ZebraPrinterLabel
{
    public sealed class CodeGenerator
    {
        public static Bitmap GenerateQRCode(string text)
        {
            // Convert mm to pixels (for 203 DPI Zebra printer)
            int MmToPx(double mm) => (int)(40);

            // Example: Generate a QR code 20mm x 20mm
            int qrWidth = MmToPx(5);  // ~160 pixels
            int qrHeight = MmToPx(5); // ~160 pixels

            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new EncodingOptions
                {
                    Width = 45,
                    Height = qrHeight,
                    Margin = 0
                },
                Renderer = new ZXing.Rendering.BitmapRenderer()
            };

            return writer.Write(text);
        }
        public static Bitmap GenerateBarcode(string text)
        {
            BarcodeWriterPixelData writer = new BarcodeWriterPixelData
            {
                Format = BarcodeFormat.CODE_128,
                Options = new EncodingOptions
                {
                    Height = 30,
                    Width = 200,
                    Margin = 1,
                    PureBarcode = true // 👈 This disables the label/text
                }
            };

            var pixelData = writer.Write(text);

            // Convert to Bitmap
            var bitmap = new Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            var bitmapData = bitmap.LockBits(new Rectangle(0, 0, pixelData.Width, pixelData.Height),
                                             System.Drawing.Imaging.ImageLockMode.WriteOnly,
                                             System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
            bitmap.UnlockBits(bitmapData);

            return bitmap;
        }
    }
}
