using NUnit.Framework;
using ImageProcessing;
using RGBConverter;
using System.Windows.Media.Imaging;
using System;

namespace TestProject1
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void ChangeColorTest()
        {
            ImageProcessing.ImageProcessing ip = new ImageProcessing.ImageProcessing();
            byte[] bytes = { 0, 255, 0 };
            Assert.AreEqual(bytes, ip.ChangeColor(1));

        }
        [Test]
        public void CheckMaxRGBTest()
        {
            ImageProcessing.ImageProcessing ip = new ImageProcessing.ImageProcessing();
            byte[] testByte = new byte[] { 22, 44, 33 };
            Assert.AreEqual(1, ip.CheckMaxRGB(testByte));
        }
        [Test]
        public void LoadBitmapTest()
        {
            ImageProcessing.ImageProcessing ip = new ImageProcessing.ImageProcessing();
            ip.bitmap = new System.Drawing.Bitmap(400, 400);
            Assert.NotNull(ip.LoadBitmap());
        }
    }
}
