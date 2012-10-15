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
using System.Diagnostics;
using Microsoft.Surface.Presentation.Controls;
using System.Windows.Media.Animation;

namespace ECE_Showcase.Screens
{
    /// <summary>
    /// Interaction logic for HomeScreen.xaml
    /// </summary>
    public partial class HomeScreen : Screen
    {
        private TouchPoint touch1;
        private TouchPoint touch2;
        private double initialDist;
        private bool triggered;

        private Research researchScreen;
        public HomeScreen(SurfaceWindow1 parentWindow) : base(parentWindow)
        {
            InitializeComponent();

            touch1 = null;
            touch2 = null;
            initialDist = Double.MaxValue;
            triggered = false;

        }


        private void Button_TouchUp(object sender, TouchEventArgs e)
        {
            if (touch1 == null ^ touch2 == null)
            {
                
                Screen screenToPush;
                switch ((sender as SurfaceButton).Name)
                {
                    case "InfoButton":
                           screenToPush = new FirstLevelScreen(ParentWindow, "information");
                           ((FirstLevelScreen)screenToPush).setLeft(new Controls.ImageControl("/ECE_Showcase;component/Resources/img/info.jpg"));
                           ((FirstLevelScreen)screenToPush).setRight(new Controls.FlowDocControl("Resources/docs/info.xaml"));

                        break;
                    case "HODButton":
                            screenToPush = new FirstLevelScreen(ParentWindow, "hod welcome");
                            ((FirstLevelScreen)screenToPush).setLeft(new Controls.ImageControl("/ECE_Showcase;component/Resources/img/salcic.jpg"));
                            ((FirstLevelScreen)screenToPush).setRight(new Controls.FlowDocControl("Resources/docs/hod_welcome.xaml"));
                        break;
                    case "ProgrammesButton":
                            screenToPush = new Courses(ParentWindow);
                        break;
                    case "ContactButton":
                            screenToPush = new FirstLevelScreen(ParentWindow, "contact us");
                            ((FirstLevelScreen)screenToPush).setRight(new Controls.ImageControl("/ECE_Showcase;component/Resources/img/map_with_pin.png"));
                            ((FirstLevelScreen)screenToPush).setLeft(new Controls.ContactsControl());
                        break;
                    case "RNDButton":
                            screenToPush = new Research(ParentWindow);
                        break;
                    default:
                        //This shouldn't ever happen.
                        screenToPush = null;
                        break;
                }
                
                ParentWindow.pushScreen(screenToPush);
                
            }

            touch1 = null;
            touch2 = null;
        }


        private void Button_PreviewTouchMove(object sender, TouchEventArgs e)
        {
            if (touch1 != null && touch2 != null)
            {
                //Debug.WriteLine((sender as SurfaceButton).Name);
                TouchPoint newTouch = e.GetTouchPoint(sender as SurfaceButton);
                
                if (touchDist(newTouch, touch1) < touchDist(newTouch, touch2))
                {
                    touch1 = newTouch;
                }
                else
                {
                    touch2 = newTouch;
                }


                if (touchDist(touch1, touch2) - initialDist > 75 && !triggered)
                {
                    triggered = false;
                    

                   //Animate the grid control
                   Storyboard sb;
                   sb = this.FindResource("gridin") as Storyboard;
                   sb.Begin(this);
                   (sender as SurfaceButton).Content = "SUP";
                   
                   (sender as SurfaceButton).Height = 370;
                }

            }
           
        }


        private double touchDist(TouchPoint t1, TouchPoint t2)
        {
            return Math.Sqrt(Math.Pow((t1.Position.X - t2.Position.X), 2) + Math.Pow((int)(t1.Position.Y - t2.Position.Y), 2));
        }

        private void Button_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            if (touch1 == null)
            {
                touch1 = e.GetTouchPoint(sender as SurfaceButton);
            }
            else
            {
                touch2 = e.GetTouchPoint(sender as SurfaceButton);
               
                initialDist = touchDist(touch1, touch2);
            }
        }

    }
}
