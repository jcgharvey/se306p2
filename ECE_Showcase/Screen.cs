using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ECE_Showcase
{

    public class Screen : UserControl
    {
        private SurfaceWindow1 parentWindow;

        protected SurfaceWindow1 ParentWindow { get{ return parentWindow;} }

        protected Screen() { }

        public Screen(SurfaceWindow1 parentWindow)
        {
            this.parentWindow = parentWindow;
        }
    }
}
