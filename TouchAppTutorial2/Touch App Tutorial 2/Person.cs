using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Touch_App_Tutorial_2
{
    public class Person
    {
        private string name;
        private string id;
        public string Name
        {
            get { return name; }
        }
        public string ID
        {
            get { return id; }
        }
        public Person(string name, string id)
        {
            this.name = name;
            this.id = id;
        }
    }
}
