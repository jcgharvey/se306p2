using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineTutorial2
{
    public class DataItem
    {
        private string name;
        private bool canDrag;

        public string Name
        {
            get { return name; }
        }

        public bool CanDrag
        {
            get { return canDrag; }
        }

        public object DraggedElement
        {
            get;
            set;
        }

        public DataItem(string name, bool canDrag)
        {
            this.name = name;
            this.canDrag = canDrag;
        }
    }
}
