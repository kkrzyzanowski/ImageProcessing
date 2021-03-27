using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Media;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ImageProcessing;
using System.Threading.Tasks;

namespace RGBConverter
{
    public class RGBConverterViewModel : INotifyPropertyChanged
    {
        private readonly DelegateCommand _convertImageCommand;
        private readonly DelegateCommand _convertImageAsyncCommand;
        private readonly DelegateCommand _loadImageCommand;
        public ICommand ConvertImageCommand => _convertImageCommand;
        public ICommand ConvertImageAsyncCommand => _convertImageAsyncCommand;
        public ICommand LoadImageCommand => _loadImageCommand;
        public ImageProcessing.ImageProcessing ip;
        private ImageModel imageModel;
        private bool _canSave = false;
        private string _operationTimeText;
        BitmapReaderWriter brw;
        public RGBConverterViewModel()
        {
            imageModel = new ImageModel
            {
                FileName = "",
                imageSource = null,
                OperationTime = 0
            };
            _convertImageCommand = new DelegateCommand(ConvertImage);
            _loadImageCommand = new DelegateCommand(LoadImage);
            _convertImageAsyncCommand = new DelegateCommand(ConvertImageAsync);
            brw = BitmapReaderWriter.CreateBMWInstance();
        }
        public string FileName
        {
            get { return imageModel.FileName; }
            set
            {
                if (imageModel.FileName != value)
                {
                    imageModel.FileName = value;
                    OnPropertyChange("FileName");
                }
            }
        }

        public ImageSource Image
        {
            get { return imageModel.imageSource; }
            set
            {
                if (imageModel.imageSource != value)
                {
                    imageModel.imageSource = value;
                    OnPropertyChange("Image");
                }
            }
        }

        public long OperationTime
        {
            get { return imageModel.OperationTime; }
            set
            {
                if (imageModel.OperationTime != value)
                {
                    imageModel.OperationTime = value;
                }
            }
        }
        public string OperationTimeText
        {
            get { return _operationTimeText; }
            set
            {
                if (_operationTimeText != value)
                {
                    _operationTimeText = value; 
                    OnPropertyChange("OperationTimeText");
                }
            }
        }

        public bool CanSave
        {
            get { return _canSave; }
            set
            {
                if (_canSave != value)
                {
                    _canSave = value;
                    OnPropertyChange("CanSave");
                }
            }
        }
        private void LoadImage(object param)
        {
            Microsoft.Win32.OpenFileDialog imageFileDialog = new OpenFileDialog();
            imageFileDialog.Filter = "JPEG files (*.jpg, *.jpeg)|*.jpg, *.jpeg|PNG files (*.png)|*.png|" +
                "BMP files (*.bmp)|*.bmp";
            imageFileDialog.FilterIndex = 1;
            imageFileDialog.RestoreDirectory = true;
            bool? result = imageFileDialog.ShowDialog();

            if (result == true)
            {
                FileName = imageFileDialog.FileName;
                ip = new ImageProcessing.ImageProcessing();
                ip.bitmap = brw.ReadImage(FileName);
                Image = ip.LoadBitmap();
            }
        }
        private void ConvertImage(object param)
        {
            if (ip != null)
            {
                FunctionTimeChecker ftc = new FunctionTimeChecker();
                ftc.CheckFunctionTime(ip.ToMainColors);
                OperationTime = ftc.ElapsedMs;
                OperationTimeText = "Function time was: " + OperationTime + "ms";
                Image = ip.LoadBitmap();
                CanSave = true;
            }
        }

        private void ConvertImageAsync(object param)
        {
            if (ip != null)
            {
                FunctionTimeChecker ftc = new FunctionTimeChecker();
                ftc.CheckFunctionTime(async() => await ip.ToMainColorsAsync());
                OperationTime = ftc.ElapsedMs;
                OperationTimeText = "Async Function time was: " + OperationTime + "ms";
                Image = ip.LoadBitmap();
                CanSave = true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
