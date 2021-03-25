using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Media;
using System.Runtime.CompilerServices;

namespace RGBConverter
{
    class RGBConverterViewModel : INotifyPropertyChanged
    {
        private ImageModel imageModel;
        public RGBConverterViewModel()
        {
            imageModel = new ImageModel
            {
                FileName = "",
                imageSource = null,
                OperationTime = 0
            };
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
                    OnPropertyChange("OperationTime");
                }
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
