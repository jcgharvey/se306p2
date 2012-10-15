using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECE_Showcase.Screens
{
    public class CourseItem : IComparable
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Information { get; set; }
        public string Prereq { get;  set; }

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


        public int CompareTo(Object obj)
        {
            if (obj == null)
            {
                return 1;
            }
            CourseItem otherCourse = obj as CourseItem;

            return String.Compare(Name, otherCourse.Name, true);


        }
    }
}
