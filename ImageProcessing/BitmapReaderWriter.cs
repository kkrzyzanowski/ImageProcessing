using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ImageProcessing
{
    public class BitmapReaderWriter
    {
        private PathModifier pm;
        private static BitmapReaderWriter _bmwInstance;
        public static BitmapReaderWriter CreateBMWInstance() 
        {
            if (_bmwInstance == null) 
                _bmwInstance = new BitmapReaderWriter(); 
            return _bmwInstance; 
        }  
        private BitmapReaderWriter()
        {
            pm = new PathModifier();
        }
        public Bitmap ReadImage(string path)
        {
            pm.LocationPath = path;
            return new Bitmap(path);
        }

        public void SaveImage(Bitmap bmp)
        {
            pm.ConvertSavedName();
            bmp.Save(pm.LocationPath);
        }
        
        public string GetFileImageName()
        {
            return pm.GetImageFileName();
        }
    }
}
