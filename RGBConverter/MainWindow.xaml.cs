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
        string filePath = string.Empty;
        ImageProcessing.ImageProcessing ip;
        public MainWindow()
        {
            InitializeComponent();
            _rGBConverterViewModel = new RGBConverterViewModel();
            DataContext = _rGBConverterViewModel;
        }

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
                ip.ReadImage(filePath);
                _rGBConverterViewModel.Image = ip.loadBitmap();
            }
        }

        private void ConvertImage(object sender, RoutedEventArgs e)
        {
            if(ip != null)
            {
                FunctionTimeChecker ftc = new FunctionTimeChecker();
                _rGBConverterViewModel.OperationTime = ftc.GetFunctionTime(ip.ToMainColors);
                _rGBConverterViewModel.Image = ip.loadBitmap();
                SaveButton.IsEnabled = true;
            }
        }

        private void SaveImage(object sender, RoutedEventArgs e)
        {
            if(SaveButton.IsEnabled)
            {
                ip.SaveImage();
                SaveButton.IsEnabled = false;
            }
        }
    }
}
