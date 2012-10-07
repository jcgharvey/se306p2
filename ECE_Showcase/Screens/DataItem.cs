using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECE_Showcase.Screens
{
    public class DataItem
    {
        private string name;
        public string filePath;
        public string imagePath;

        public string Name
        {
            get { return name; }
        }

        public string FilePath
        {
            get { return filePath; }
        }

        public string ImagePath
        {
            get { return imagePath; }
        }

        public DataItem(string name, string filePath, string imagePath)
        {
            this.name = name;
            this.filePath = filePath;
            this.imagePath = imagePath;
        }
    }
}
