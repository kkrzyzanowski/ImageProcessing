using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace RGBConverter
{
    class ImageModel
    {
        public string FileName { get; set; }
        public ImageSource imageSource { get; set; }
        public long OperationTime { get; set; }
    }
}
