using NUnit.Framework;
using ImageProcessing;
using RGBConverter;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject1
{
    [TestFixture]
    public class RGBViewModelTest
    {
        [Test]
        public void ViewModelImageTest()
        {
            var viewModel = new RGBConverterViewModel();
            viewModel.ip = new ImageProcessing.ImageProcessing();
            viewModel.ip.bitmap = new System.Drawing.Bitmap(400, 400);
            viewModel.ConvertImageCommand.Execute("ConvertImage");
            Assert.NotNull(viewModel.Image);
        }
        [Test]
        public void ViewModelTimeTest()
        {
            var viewModel = new RGBConverterViewModel();
            viewModel.ip = new ImageProcessing.ImageProcessing();
            viewModel.ip.bitmap = new System.Drawing.Bitmap(400, 400);
            viewModel.ConvertImageCommand.Execute("ConvertImage");
            Assert.Greater(viewModel.OperationTime, 0);
        }
    }
}
