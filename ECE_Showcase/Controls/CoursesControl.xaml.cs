using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using ECE_Showcase.Screens;

namespace ECE_Showcase.Controls
{
    /// <summary>
    /// Interaction logic for CoursesControl.xaml
    /// </summary>
    public partial class CoursesControl : UserControl
    {
        private ObservableCollection<CourseItem> partII;
        private ObservableCollection<CourseItem> partIII;
        private ObservableCollection<CourseItem> partIV;

        public ObservableCollection<CourseItem> PartII
        {
            get
            {
                if (partII == null)
                {
                    partII = new ObservableCollection<CourseItem>();
                }
                return partII;
            }
        }
        public ObservableCollection<CourseItem> PartIII
        {
            get
            {
                if (partIII == null)
                {
                    partIII = new ObservableCollection<CourseItem>();
                }
                return partIII;
            }
        }
        /// <summary>
        /// Items that bind with the drag source list box.
        /// </summary>
        public ObservableCollection<CourseItem> PartIV
        {
            get
            {
                if (partIV == null)
                {

                    partIV = new ObservableCollection<CourseItem>();
                }

                return partIV;
            }
        }
        public CoursesControl()
        {
            InitializeComponent();
            DataContext = this;

            PartII.Add(new CourseItem("Course Name", "201", "this course rocks", "15", "304, 405"));
            PartII.Add(new CourseItem("Course Name", "250", "this course rocks", "15", "304, 405"));
            PartII.Add(new CourseItem("Course Name", "254", "this course rocks", "15", "304, 405"));
            PartII.Add(new CourseItem("Course Name", "206", "this course rocks", "15", "304, 405"));

            PartIII.Add(new CourseItem("Course Name", "301", "this course rocks", "15", "304, 405"));
            PartIV.Add(new CourseItem("Course Name", "401", "this course rocks", "15", "304, 405"));
        }
    }
}
