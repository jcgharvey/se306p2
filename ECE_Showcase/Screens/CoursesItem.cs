using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECE_Showcase.Screens
{
    public class CourseItem
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Information { get; set; }
        public string Prereq { get; set; }

        public CourseItem(string code, string name, string type, string information, string prereq)
        {
            Code = code;
            Name = name;
            Type = type;
            Information = information;
            Prereq = prereq;
        }

        public CourseItem()
        {

        }
    }
}
