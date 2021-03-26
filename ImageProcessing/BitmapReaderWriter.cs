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

        public BitmapReaderWriter()
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
    }
}
