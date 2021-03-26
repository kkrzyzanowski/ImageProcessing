using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ImageProcessing;

namespace RGBConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly RGBConverterViewModel _rGBConverterViewModel;
        ImageProcessing.ImageProcessing ip;
        BitmapReaderWriter brw;
        string filePath = string.Empty;
        public MainWindow()
        {
            InitializeComponent();
            _rGBConverterViewModel = new RGBConverterViewModel();
            brw = new BitmapReaderWriter();
            DataContext = _rGBConverterViewModel;
        }

        /// <summary>
        /// Load bitmap from file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadImage(object sender, RoutedEventArgs e)
        {
            OpenFileDialog imageFileDialog = new OpenFileDialog();
            imageFileDialog.Filter = "JPEG files (*.jpg, *.jpeg)|*.jpg, *.jpeg|PNG files (*.png)|*.png|" +
                "BMP files (*.bmp)|*.bmp";
            imageFileDialog.FilterIndex = 1;
            imageFileDialog.RestoreDirectory = true;
            bool? result = imageFileDialog.ShowDialog();

            if(result == true)
            {
                filePath = imageFileDialog.FileName;
                ip = new ImageProcessing.ImageProcessing();
                ip.bitmap = brw.ReadImage(filePath);
                _rGBConverterViewModel.Image = ip.LoadBitmap();
            }
        }
        /// <summary>
        /// Convert image button event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConvertImage(object sender, RoutedEventArgs e)
        {
            if(ip != null)
            {
                FunctionTimeChecker ftc = new FunctionTimeChecker();
                _rGBConverterViewModel.OperationTime = ftc.GetFunctionTime(ip.ToMainColors);
                _rGBConverterViewModel.Image = ip.LoadBitmap();
                SaveButton.IsEnabled = true;
            }
        }
        /// <summary>
        /// Save Image Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveImage(object sender, RoutedEventArgs e)
        {
            if(SaveButton.IsEnabled)
            {
                brw.SaveImage(ip.bitmap);
                SaveButton.IsEnabled = false;
            }
        }
    }
}
