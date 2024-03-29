﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ECE_Showcase.Screens
{
    public class DataItem
    {
        public string Name { get; private set; }
        public UserControl ItemControl { get; private set;}
        public Image ItemImage { get; private set; }

        public DataItem(string name, UserControl control)
        {
            Name = name;
            ItemControl = control;
        }

        public DataItem(string name, UserControl control, Image image)
        {
            Name = name;
            ItemControl = control;
            ItemImage = image;
        }
    }
}
