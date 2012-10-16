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
        private bool down;
       
        private SurfaceButton newImage;

        public HomeScreen(SurfaceWindow1 parentWindow) : base(parentWindow)
        {
            InitializeComponent();

            touch1 = null;
            touch2 = null;
            initialDist = Double.MaxValue;
            triggered = false;
            down = false;
            newImage = null;
        }


        private void Button_TouchUp(object sender, TouchEventArgs e)
        {
            down = false;
            
            if (touch1 == null ^ touch2 == null)
            {
                Storyboard sb = this.FindResource("newScreen") as Storyboard;
                sb.Begin(this);

                Screen screenToPush;
                switch ((sender as SurfaceButton).Name)
                {
                    case "InfoButton":
                           screenToPush = new FirstLevelScreen(ParentWindow, "information");
                           ((FirstLevelScreen)screenToPush).setLeft(new Controls.ImageControl("/ECE_Showcase;component/Resources/img/info.jpg"));
                           ((FirstLevelScreen)screenToPush).setRight(new Controls.FlowDocControl("Resources/docs/info.xaml"));

                        break;
                    case "HodButton":
                            screenToPush = new FirstLevelScreen(ParentWindow, "department welcome");
                            ((FirstLevelScreen)screenToPush).setLeft(new Controls.ImageControl("/ECE_Showcase;component/Resources/img/salcic.jpg"));
                            ((FirstLevelScreen)screenToPush).setRight(new Controls.FlowDocControl("Resources/docs/hod_welcome.xaml"));
                        break;
                    case "ProgrammesButton":
                            screenToPush = new Courses(ParentWindow);
                        break;
                    case "ContactButton":
                            screenToPush = new FirstLevelScreen(ParentWindow, "contact us");
                            UserControl rightControl = new Controls.ImageControl("/ECE_Showcase;component/Resources/img/map_with_pin.png");
                           Thickness marg = rightControl.Margin;
                            marg.Left = 10;
                            marg.Top = 4;
                            rightControl.Margin = marg;
                            ((FirstLevelScreen)screenToPush).setRight(rightControl);
                            ((FirstLevelScreen)screenToPush).setLeft(new Controls.ContactsControl());
                        break;
                    case "RNDButton":
                            screenToPush = new Research(ParentWindow);
                        break;
					case "ClubsButton":
                        screenToPush = new Clubs(ParentWindow);
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

                if (newImage != null && !(sender as SurfaceButton).Name.Equals(newImage.Name))
                {
                    resetScreens(sender, e);
                    triggered = false;
                }

                if (touchDist(touch1, touch2) - initialDist > 75 && !triggered)
                {
                    triggered = true;
                    down = true;

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
                else if ((touchDist(touch1, touch2) - initialDist < -75 && triggered) && (!down))
                {
                    triggered = false;

                    Storyboard sb;
                    Image myImage3 = new Image();
                    BitmapImage bi3 = new BitmapImage();

                     if(newImage.Name.Equals("InfoButton"))
                   {
                         sb = this.FindResource("gridout") as Storyboard;
                         sb.Begin(this);
                         InfoButton.Height = 185;
                         
                         bi3.BeginInit();
                         bi3.UriSource = new Uri("/ECE_Showcase;component/Resources/img/info_btn.png", UriKind.Relative);
                         bi3.EndInit();
                         myImage3.Stretch = Stretch.Uniform;
                         myImage3.Source = bi3;
                       
                   }
                   else if (newImage.Name.Equals("HodButton"))
                   {
                         sb = this.FindResource("gridout1") as Storyboard;
                         sb.Begin(this);
                         HodButton.Height = 185;

                         bi3.BeginInit();
                         bi3.UriSource = new Uri("/ECE_Showcase;component/Resources/img/HOD_btn.png", UriKind.Relative);
                         bi3.EndInit();
                         myImage3.Stretch = Stretch.UniformToFill;
                         myImage3.Source = bi3;

                       
                   }
                     else if (newImage.Name.Equals("ProgrammesButton"))
                     {
                         sb = this.FindResource("gridout2") as Storyboard;
                         sb.Begin(this);
                         ProgrammesButton.Height = 185;

                         bi3.BeginInit();
                         bi3.UriSource = new Uri("/ECE_Showcase;component/Resources/img/Programmes_btn.png", UriKind.Relative);
                         bi3.EndInit();

                     }

                     else if (newImage.Name.Equals("ContactButton"))
                     {
                         sb = this.FindResource("gridout3") as Storyboard;
                         sb.Begin(this);
                         ContactButton.Height = 185;

                         bi3.BeginInit();
                         bi3.UriSource = new Uri("/ECE_Showcase;component/Resources/img/Contact_us_btn.png", UriKind.Relative);
                         bi3.EndInit();

                     }

                     myImage3.Stretch = Stretch.UniformToFill;
                     myImage3.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                     myImage3.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                     myImage3.Height = 185;
                     myImage3.Width = 735;

                     myImage3.Source = bi3;

                     newImage.Content = myImage3;

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

            
            if (newImage.Name.Equals("InfoButton"))
            {
                sb = this.FindResource("fadein") as Storyboard;
                sb.Begin(this);

                Image myImage3 = new Image();
                BitmapImage bi3 = new BitmapImage();

                bi3.BeginInit();
                bi3.UriSource = new Uri("/ECE_Showcase;component/Resources/img/info_exp.png", UriKind.Relative);
                bi3.EndInit();
                myImage3.Stretch = Stretch.UniformToFill;
                myImage3.Source = bi3;

              //  myImage3.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;

                newImage.Content = myImage3;
            }
            else if (newImage.Name.Equals("HodButton"))
            {
                sb = this.FindResource("fadein1") as Storyboard;
                sb.Begin(this);

                Image myImage3 = new Image();
                BitmapImage bi3 = new BitmapImage();

                bi3.BeginInit();
                bi3.UriSource = new Uri("/ECE_Showcase;component/Resources/img/HOD_exp.png", UriKind.Relative);
                bi3.EndInit();
                myImage3.Stretch = Stretch.UniformToFill;
                myImage3.Source = bi3;

                myImage3.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;

                newImage.Content = myImage3;
            }
            else if (newImage.Name.Equals("ProgrammesButton"))
            {
                sb = this.FindResource("fadein2") as Storyboard;
                sb.Begin(this);

                Image myImage3 = new Image();
                BitmapImage bi3 = new BitmapImage();

                bi3.BeginInit();
                bi3.UriSource = new Uri("/ECE_Showcase;component/Resources/img/programmes_exp.png", UriKind.Relative);
                bi3.EndInit();
                myImage3.Stretch = Stretch.UniformToFill;
                myImage3.Source = bi3;

                myImage3.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;

                newImage.Content = myImage3;
            }
            else if (newImage.Name.Equals("ContactButton"))
            {
                sb = this.FindResource("fadein3") as Storyboard;
                sb.Begin(this);

                Image myImage3 = new Image();
                BitmapImage bi3 = new BitmapImage();

                bi3.BeginInit();
                bi3.UriSource = new Uri("/ECE_Showcase;component/Resources/img/contact_exp.png", UriKind.Relative);
                bi3.EndInit();
                myImage3.Stretch = Stretch.UniformToFill;
                myImage3.Source = bi3;

                myImage3.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;

                newImage.Content = myImage3;
            }

        }

        private void resetScreens(object sender, EventArgs e)
        {

            triggered = false;
            Image myImage3 = new Image();
            BitmapImage bi3 = new BitmapImage();
            if (newImage != null)
            {


                if (newImage.Name.ToString().Equals("InfoButton"))
                {
                    InfoButton.Height = 185;
                    bi3.BeginInit();
                    bi3.UriSource = new Uri("/ECE_Showcase;component/Resources/img/info_btn.png", UriKind.Relative);
                    bi3.EndInit();
                }
                else if (newImage.Name.ToString().Equals("HodButton"))
                {
                    HodButton.Height = 185;
                    bi3.BeginInit();
                    bi3.UriSource = new Uri("/ECE_Showcase;component/Resources/img/hod_btn.png", UriKind.Relative);
                    bi3.EndInit();
                }
                else if (newImage.Name.ToString().Equals("ProgrammesButton"))
                {
                    ProgrammesButton.Height = 185;
                    bi3.BeginInit();
                    bi3.UriSource = new Uri("/ECE_Showcase;component/Resources/img/programmes_btn.png", UriKind.Relative);
                    bi3.EndInit();
                }
                else if (newImage.Name.ToString().Equals("ContactButton"))
                {
                    ContactButton.Height = 185;
                    bi3.BeginInit();
                    bi3.UriSource = new Uri("/ECE_Showcase;component/Resources/img/contact_us_btn.png", UriKind.Relative);
                    bi3.EndInit();
                }


                myImage3.Stretch = Stretch.Uniform;
                myImage3.Source = bi3;
                myImage3.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                myImage3.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                myImage3.Height = 185;
                myImage3.Width = 735;

                myImage3.Stretch = Stretch.UniformToFill;

                myImage3.Source = bi3;

                newImage.Content = myImage3;
            }
            
        }

    }
}
