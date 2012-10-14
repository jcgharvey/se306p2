using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECE_Showcase.Screens
{
    public class CourseItem
    {
        private string Name { get; private set; }
        private string Number { get; private set; }
        private string Info { get; private set; }
        private string Points { get; private set; }
        private string Prereq { get; private set; }

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
