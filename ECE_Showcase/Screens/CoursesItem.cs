using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECE_Showcase.Screens
{
    public class CourseItem
    {
        public string Name { get; private set; }
        public string Number { get; private set; }
        public string Info { get; private set; }
        public string Points { get; private set; }
        public string Prereq { get; private set; }

        public CourseItem(string name, string number, string info, string points, string prereq)
        {
            Name = name;
            Number = number;
            Info = info;
            Points = points;
            Prereq = prereq;
        }
    }
}
