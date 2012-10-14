using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ECE_Showcase.Screens
{
    public class DataItem
    {
        private string Name { public get; set; }
        private UserControl ItemControl { public get; set;}

        public DataItem(string name, UserControl control)
        {
            Name = name;
            ItemControl = control;
        }
    }
}
