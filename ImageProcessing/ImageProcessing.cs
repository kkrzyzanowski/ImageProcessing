using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Runtime.InteropServices;

namespace ImageProcessing
{
    public class ImageProcessing
    {
        public Bitmap bitmap {get; set; }       
        private byte[] RGBbytes;

        public void ToMainColors()
        {
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            System.Drawing.Imaging.BitmapData bmpData =
            bitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
            bitmap.PixelFormat);

            IntPtr ptr = bmpData.Scan0;
            int bytes = Math.Abs(bmpData.Stride) * bmpData.Height;
            RGBbytes = new byte[bytes];

            System.Runtime.InteropServices.Marshal.Copy(ptr, RGBbytes, 0, bytes);

            RGBbytes = ConvertBytes(bytes);
            System.Runtime.InteropServices.Marshal.Copy(RGBbytes, 0, ptr, bytes);

            bitmap.UnlockBits(bmpData);
        }
        public async Task ToMainColorsAsync()
        {
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            System.Drawing.Imaging.BitmapData bmpData =
            bitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
            bitmap.PixelFormat);

            IntPtr ptr = bmpData.Scan0;
            int bytes = Math.Abs(bmpData.Stride) * bmpData.Height;
            RGBbytes = new byte[bytes];

            System.Runtime.InteropServices.Marshal.Copy(ptr, RGBbytes, 0, bytes);

            RGBbytes = await Task.Run(() => ConvertBytes(bytes));
            
            System.Runtime.InteropServices.Marshal.Copy(RGBbytes, 0, ptr, bytes);

            bitmap.UnlockBits(bmpData);
        }

        private byte[] ConvertBytes(int size)
        {
            byte[] bytes = RGBbytes;
            for (int i = 0; i < size; i += 3)
            {
                byte[] RGB = { bytes[i], bytes[i + 1], bytes[i + 2] };
                //0 - Red, 1 - Green, 2 - Blue
                int indexColor = CheckMaxRGB(RGB);
                byte[] newRGB = ChangeColor(indexColor);
                bytes[i] = newRGB[0];
                bytes[i + 1] = newRGB[1];
                bytes[i + 2] = newRGB[2];
            }
            return bytes;
        }
        [DllImport("gdi32")]
        static extern int DeleteObject(IntPtr o);
        public BitmapSource LoadBitmap()
        {
            IntPtr ip = bitmap.GetHbitmap();
            BitmapSource bs = null;
            try
            {
                bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ip,
                   IntPtr.Zero, Int32Rect.Empty,
                   System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            }
            finally
            {
                DeleteObject(ip);
            }

            return bs;
            
        }

        public byte[] ChangeColor(int indexColor)
        {
            Color convertedColor;
           
            switch(indexColor)
            {
                case 0:
                    convertedColor = ColorTranslator.FromHtml("#FF0000");
                    break;
                case 1:
                    convertedColor = ColorTranslator.FromHtml("#00FF00");
                    break;
                case 2:
                    convertedColor = ColorTranslator.FromHtml("#0000FF");
                    break;
                default:
                    convertedColor = ColorTranslator.FromHtml("#000000");
                    break;
            }
            byte[] rgb = {convertedColor.R, convertedColor.G, convertedColor.B };
            return rgb;
        }

        public int CheckMaxRGB(byte[] rgb)
        {
            int colorValue = rgb[0] > rgb[1] ? 0 : 1;
            colorValue = rgb[colorValue] > rgb[2] ? colorValue : 2;
            return colorValue;
        }

    }
}
