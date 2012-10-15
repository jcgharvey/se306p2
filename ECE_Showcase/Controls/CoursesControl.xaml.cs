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
using System.Xml;
using System.Diagnostics;

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
        XmlDocument xd;

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
        public CoursesControl(String course)
        {
            InitializeComponent();
            DataContext = this;

            partII = new ObservableCollection<CourseItem>();
            partIII = new ObservableCollection<CourseItem>();
            partIV = new ObservableCollection<CourseItem>();
            
            // Load the XML document
            xd = new XmlDocument();
            xd.Load("Resources/docs/specialisations/Courses.xml");

            // Fill the lists based on the course
            if (course.Equals("cse"))
            {
                Debug.WriteLine("reading CSE");
                fillLists("CSE");
            }
            else if (course.Equals("se"))
            {
                fillLists("SE");
            }
            else if (course.Equals("eee"))
            {
                fillLists("EEE");
            }
            else if (course.Equals("first"))
            {
                // Needs to be fixed
                fillLists("Common");
            }
            else
            {
                //Throw exception of some sort
                // Just going to let it do nothing :) 
                // If stuff goes wrong with loading courses then look here
            }
            
        }

        private void fillLists(String program)
        {
            Debug.Write("XML READING " + program);

            XmlNodeList nodelist = xd.SelectNodes("/programs/program/course"); // get all <course> nodes that are part of that program

            foreach (XmlNode course in nodelist)
            {
                Debug.WriteLine("in de loopz");

                CourseItem ci = new CourseItem();

                Debug.WriteLine(course.Attributes.GetNamedItem("code").Value);
                Debug.WriteLine(course.Attributes.GetNamedItem("year").Value);

                ci.Code = course.Attributes.GetNamedItem("code").Value;
                ci.Name = course.Attributes.GetNamedItem("name").Value;
                ci.Type = course.Attributes.GetNamedItem("type").Value;
                ci.Prereq = course.Attributes.GetNamedItem("prereq").Value;
                ci.Information = course.Attributes.GetNamedItem("info").Value;
                
                //Set the list that we are going to add the course to
                ObservableCollection<CourseItem> yearLevel;
                if ((course.Attributes.GetNamedItem("year").Value).Equals("2") || (course.Attributes.GetNamedItem("year").Value).Equals("1"))
                {
                    yearLevel = partII;
                }
                else if ((course.Attributes.GetNamedItem("year").Value).Equals("3"))
                {
                    yearLevel = partIII;
                }
                else
                {
                    yearLevel = partIV;
                }

                yearLevel.Add(ci);
            }
        }

        private void Expander_TouchDown(object sender, TouchEventArgs e)
        {

        }

        private void Expander_TouchUp(object sender, TouchEventArgs e)
        {

        }
    }
}
