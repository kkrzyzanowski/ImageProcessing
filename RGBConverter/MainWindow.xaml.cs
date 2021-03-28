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
        BitmapReaderWriter brw;
        string filePath = string.Empty;
        public MainWindow()
        {
            brw = BitmapReaderWriter.CreateBMWInstance();
            InitializeComponent();
            _rGBConverterViewModel = new RGBConverterViewModel();
            DataContext = _rGBConverterViewModel;
            ConvertImage.IsEnabled = false;
            ConvertAsyncImage.IsEnabled = false;
        }
        private void SaveImage(object sender, RoutedEventArgs e)
        {
            if(SaveButton.IsEnabled)
            {
                brw.SaveImage(_rGBConverterViewModel.ip.bitmap);
                _rGBConverterViewModel.CanSave = false;
            }
        }

        private void LoadImageClick(object sender, RoutedEventArgs e)
        {
            ConvertImage.IsEnabled = true;
            ConvertAsyncImage.IsEnabled = true;
        }
    }
}
