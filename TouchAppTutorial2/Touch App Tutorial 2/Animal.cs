using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Touch_App_Tutorial_2
{
    public class Animal
    {
        private string name;
        private string weight;
        private string age;

        public string Name
        {
            get { return name; }
        }

        public string Weight
        {
            get { return weight; }
        }

        public string Age
        {
            get { return age; }
        }

        public Animal(string name, string weight, string age)
        {
            this.name = name;
            this.weight = weight;
            this.age = age;
        }
    }
}
