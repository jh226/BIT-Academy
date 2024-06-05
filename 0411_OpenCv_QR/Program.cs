using OpenCvSharp;
using System;
using System.Collections.Generic;
using ZXing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OpenCvSharp.ConnectedComponents;
using System.Drawing;
using System.Windows.Forms;

namespace _0411_OpenCv
{
    internal class Program
    {
        #region QR Code
        static void Exam1()
        {
            Mat src = Cv2.ImRead("sample1.png");

            // create a barcode reader instance
            var barcodeReader = new BarcodeReader();

            // create an in memory bitmap
            var barcodeBitmap = (Bitmap)Bitmap.FromFile("qr.png");

            // decode the barcode from the in memory bitmap
            var barcodeResult = barcodeReader.Decode(barcodeBitmap);

            // output results to consoles
            Console.WriteLine($"Decoded barcode text: {barcodeResult?.Text}");
            Console.WriteLine($"Barcode format: {barcodeResult?.BarcodeFormat}");


            Cv2.ImShow("src", src);

            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }
        #endregion

        [STAThread]
        static void Main(string[] args)
        {
            //Exam1();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
