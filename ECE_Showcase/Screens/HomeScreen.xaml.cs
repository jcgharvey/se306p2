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

        private FirstLevelScreen infoScreen;
        private FirstLevelScreen hodWelcomeScreen;

        private TouchPoint touch1;
        private TouchPoint touch2;
        private double initialDist;
        private bool triggered;

        private Courses coursesScreen;
        private FirstLevelScreen contactScreen;
        private SurfaceButton newImage;

        public HomeScreen(SurfaceWindow1 parentWindow) : base(parentWindow)
        {
            InitializeComponent();

            infoScreen = null;
            hodWelcomeScreen = null;

            contactScreen = null;

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
                        if (infoScreen == null)
                        {
                           infoScreen = new FirstLevelScreen(ParentWindow, "information");
                           infoScreen.setLeft(new Controls.ImageControl("/ECE_Showcase;component/Resources/img/electrical.png"));
                            infoScreen.setRight(new Controls.FlowDocControl("Resources/docs/info.xaml"));
                        }
                        screenToPush = infoScreen;
                        break;
                    case "HODButton":
                        if (hodWelcomeScreen == null)
                        {
                            hodWelcomeScreen = new FirstLevelScreen(ParentWindow, "hod welcome");
                            hodWelcomeScreen.setLeft(new Controls.ImageControl("/ECE_Showcase;component/Resources/img/salcic.jpg"));
                            hodWelcomeScreen.setRight(new Controls.FlowDocControl("Resources/docs/hod_welcome.xaml"));
                        }
                        screenToPush = hodWelcomeScreen;
                        break;
                    case "ProgrammesButton":
                        if (coursesScreen == null)
                        {
                            coursesScreen = new Courses(ParentWindow);
                        }
                        screenToPush = coursesScreen;
                        break;
                    case "ContactButton":
                        if (contactScreen == null)
                        {
                            contactScreen = new FirstLevelScreen(ParentWindow, "contact us");
                            contactScreen.setLeft(new Controls.FlowDocControl("Resources/docs/contact_us.xaml"));
                            contactScreen.setRight(new Controls.MapControl());
                        }
                        screenToPush = contactScreen;
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
                    triggered = true;

                    Debug.Print((sender as SurfaceButton).Name);
                   //Animate the grid control
                   Storyboard sb;
                   if((sender as SurfaceButton).Name.Equals("InfoButton"))
                   {
                       sb = this.FindResource("gridin") as Storyboard;
                       sb.Begin(this);
                       InfoButton.Height = 390;
                     //  (sender as SurfaceButton).Content = "";
                       newImage = (sender as SurfaceButton);

                   }
                   else if ((sender as SurfaceButton).Name.Equals("HodButton"))
                   {
                       sb = this.FindResource("gridin1") as Storyboard;
                       sb.Begin(this);
                       HodButton.Height = 390;

                       newImage = (sender as SurfaceButton);
                   }
                   else if ((sender as SurfaceButton).Name.Equals("ProgrammesButton"))
                   {
                       sb = this.FindResource("gridin2") as Storyboard;
                       sb.Begin(this);
                       ProgrammesButton.Height = 390;

                       newImage = (sender as SurfaceButton);
                   }

                   else if ((sender as SurfaceButton).Name.Equals("ContactButton"))
                   {
                       sb = this.FindResource("gridin3") as Storyboard;
                       sb.Begin(this);
                       ContactButton.Height = 390;

                       newImage = (sender as SurfaceButton);
                   }
                   
                }
                else if (touchDist(touch1, touch2) - initialDist < 75 && triggered)
                {
                    triggered = false;

                    Storyboard sb;
                    Image myImage3 = new Image();
                    BitmapImage bi3 = new BitmapImage();

                     if((sender as SurfaceButton).Name.Equals("InfoButton"))
                   {
                         sb = this.FindResource("gridout") as Storyboard;
                         sb.Begin(this);
                         InfoButton.Height = 195;
                         
                         bi3.BeginInit();
                         bi3.UriSource = new Uri("/ECE_Showcase;component/Resources/img/info_btn.png", UriKind.Relative);
                         bi3.EndInit();
                         myImage3.Stretch = Stretch.Uniform;
                         myImage3.Source = bi3;
                         myImage3.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;

                       
                   }
                   else if ((sender as SurfaceButton).Name.Equals("HodButton"))
                   {
                         sb = this.FindResource("gridout1") as Storyboard;
                         sb.Begin(this);
                         HodButton.Height = 195;

                         bi3.BeginInit();
                         bi3.UriSource = new Uri("/ECE_Showcase;component/Resources/img/HOD_btn.png", UriKind.Relative);
                         bi3.EndInit();
                         myImage3.Stretch = Stretch.UniformToFill;
                         myImage3.Source = bi3;

                         myImage3.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                       
                   }
                     else if ((sender as SurfaceButton).Name.Equals("ProgrammesButton"))
                     {
                         sb = this.FindResource("gridout2") as Storyboard;
                         sb.Begin(this);
                         ProgrammesButton.Height = 195;

                         bi3.BeginInit();
                         bi3.UriSource = new Uri("/ECE_Showcase;component/Resources/img/Programmes_btn.png", UriKind.Relative);
                         bi3.EndInit();

                         myImage3.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;

                     }

                     else if ((sender as SurfaceButton).Name.Equals("ContactButton"))
                     {
                         sb = this.FindResource("gridout3") as Storyboard;
                         sb.Begin(this);
                         ContactButton.Height = 195;

                         bi3.BeginInit();
                         bi3.UriSource = new Uri("/ECE_Showcase;component/Resources/img/Contact_us_btn.png", UriKind.Relative);
                         bi3.EndInit();

                         myImage3.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;

                     }
                     myImage3.Stretch = Stretch.UniformToFill;

                     myImage3.Source = bi3;
                     Thickness thick = new Thickness(1);
                     myImage3.Margin = thick;
                     (sender as SurfaceButton).Margin = thick;

                     (sender as SurfaceButton).Content = myImage3;

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

        private void Storyboard_Completed(object sender, EventArgs e)
        {
            Storyboard sb;

            newImage.Content = "there are going to be lots of words here";
            
            if (newImage.Name.Equals("InfoButton"))
            {
                sb = this.FindResource("fadein") as Storyboard;
                sb.Begin(this);

                Image myImage3 = new Image();
                BitmapImage bi3 = new BitmapImage();

                bi3.BeginInit();
                bi3.UriSource = new Uri("/ECE_Showcase;component/Resources/img/forthelolz.png", UriKind.Relative);
                bi3.EndInit();
                myImage3.Stretch = Stretch.UniformToFill;
                myImage3.Source = bi3;

                myImage3.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;

                newImage.Content = myImage3;
            }
            else if (newImage.Name.Equals("HodButton"))
            {
                sb = this.FindResource("fadein1") as Storyboard;
                sb.Begin(this);
            }
            else if (newImage.Name.Equals("ProgrammesButton"))
            {
                sb = this.FindResource("fadein2") as Storyboard;
                sb.Begin(this);
            }
            else if (newImage.Name.Equals("ContactButton"))
            {
                sb = this.FindResource("fadein3") as Storyboard;
                sb.Begin(this);
            }

        }

        private void Storyboard_Completed_1(object sender, EventArgs e)
        {


        }

    }
}
