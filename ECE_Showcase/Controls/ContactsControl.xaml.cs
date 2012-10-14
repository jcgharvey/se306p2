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
using System.IO;
using System.Windows.Markup;
using ECE_Showcase.Screens;
using System.Collections.ObjectModel;

namespace ECE_Showcase.Controls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ContactsControl : UserControl
    {

        private ObservableCollection<DataItem> locations;

        public ObservableCollection<DataItem> Locations
        {
            get
            {
                if (locations == null)
                {
                    locations = new ObservableCollection<DataItem>();
                }
                return locations;
            }
        }

        public ContactsControl()
        {
            InitializeComponent();

            DataContext = this;

            FlowDocument flowDocument = (FlowDocument)XamlReader.Load(File.OpenRead("Resources/docs/contact_us.xaml"));

            infoViewer.Document = flowDocument;

            locations = new ObservableCollection<DataItem>();
            /*
            locations.Add(new DataItem("From the Engineering Building", "Resources/docs/specialisations/cse_info.xaml", "/ECE_Showcase;component/Resources/img/map_from_engineering.png"));
            locations.Add(new DataItem("From the Clocktower", "Resources/docs/specialisations/cse_info.xaml", "/ECE_Showcase;component/Resources/img/map_from_clocktower.png"));
            locations.Add(new DataItem("From 38 Princes St", "Resources/docs/specialisations/cse_info.xaml", "/ECE_Showcase;component/Resources/img/map_from_38princes.png"));
            */
             }


        private void Expander_TouchUp(object sender, TouchEventArgs e)
        {
            if (TopDef.Height != new GridLength(320))
            {
                TopDef.Height = new GridLength(320);
            }
            else
            {
                TopDef.Height = new GridLength(160);
            }


            Expander exp = sender as Expander;

            if (exp == null)

                return;

            TouchPoint tp = e.GetTouchPoint(exp);

            Rect bounds = new Rect(new Point(0, 0), exp.RenderSize);



            if (bounds.Contains(tp.Position))
            {
                if (exp.IsExpanded)
                {

                    exp.IsExpanded = false;
                    //infoViewer.Document = (FlowDocument)XamlReader.Load(File.OpenRead("Resources/docs/tap_course.xaml"));
                }
                else
                {
                    exp.IsExpanded = true;
                    //infoViewer.Document = (FlowDocument)XamlReader.Load(File.OpenRead("Resources/docs/drag_here.xaml"));
                }

                //acc.selectedExpander_Expanded(exp, e);
            }
            exp.ReleaseTouchCapture(e.TouchDevice);
            e.Handled = true;

        }

        private void Expander_TouchDown(object sender, TouchEventArgs e)
        {

            Console.WriteLine("touch down");
            Expander exp = sender as Expander;

            if (exp == null)

                return;

            exp.CaptureTouch(e.TouchDevice);
            e.Handled = true;
        }

        

        private void EngineeringPath (object sender, TouchEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("pressed");
            ((FirstLevelScreen)((Grid)this.Parent).Parent).setRight(new Controls.ImageControl("/ECE_Showcase;component/Resources/img/map_from_engineering.png"));
        }

        private void ClocktowerPath (object sender, TouchEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("pressed");
            ((FirstLevelScreen)((Grid)this.Parent).Parent).setRight(new Controls.ImageControl("/ECE_Showcase;component/Resources/img/map_from_clocktower.png"));
        }

        private void PrincesPath(object sender, TouchEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("pressed");
            ((FirstLevelScreen)((Grid)this.Parent).Parent).setRight(new Controls.ImageControl("/ECE_Showcase;component/Resources/img/map_from_38princes.png"));
        }

    }

}