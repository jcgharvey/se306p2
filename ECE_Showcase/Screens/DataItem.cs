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

        public string Name
        {
            get { return name; }
        }

        public string FilePath
        {
            get { return filePath; }
        }

        public DataItem(string name, string filePath)
        {
            this.name = name;
            this.filePath = filePath;
        }
    }
}
