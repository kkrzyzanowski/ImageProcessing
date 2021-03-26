using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    public class PathModifier
    {
        public string LocationPath;
        private string savedFileNamed = "converted";
        public void ConvertSavedName()
        {
            var extension = LocationPath.Substring(LocationPath.Length - 4);
            LocationPath = LocationPath.Substring(0, LocationPath.Length - 4) + savedFileNamed + extension;
        }
    }
}
