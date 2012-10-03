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

namespace ECE_Showcase.Screens
{
    /// <summary>
    /// Interaction logic for HomeScreen.xaml
    /// </summary>
    public partial class HomeScreen : Screen
    {
        private InfoScreen infoScreen;
        private HODWelcomeScreen hodWelcomeScreen;
        private Courses coursesScreen;
        private TouchPoint touch1;
        private TouchPoint touch2;
        private double initialDist;
        private bool triggered;

        public HomeScreen(SurfaceWindow1 parentWindow) : base(parentWindow)
        {
            InitializeComponent();

            infoScreen = null;
            hodWelcomeScreen = null;
            touch1 = null;
            touch2 = null;
            initialDist = Double.MaxValue;
            triggered = false;
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            if (infoScreen == null)
            {
                infoScreen = new InfoScreen(ParentWindow);
            }

            ParentWindow.pushScreen(infoScreen);
            
        }

        private void HODButton_Click(object sender, RoutedEventArgs e)
        {
            if (hodWelcomeScreen == null)
            {
                hodWelcomeScreen = new HODWelcomeScreen(ParentWindow);
            }
            ParentWindow.pushScreen(hodWelcomeScreen);
        }

        private void courses_button_click(object sender, RoutedEventArgs e)
        {
            if (coursesScreen == null)
            {
                coursesScreen = new Courses(ParentWindow);
            }
            ParentWindow.pushScreen(coursesScreen);
        }


        private void RNDButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_TouchUp(object sender, TouchEventArgs e)
        {
            if (touch1 == null ^ touch2 == null)
            {
                if (hodWelcomeScreen == null)
                {
                    hodWelcomeScreen = new HODWelcomeScreen(ParentWindow);
                }
                ParentWindow.pushScreen(hodWelcomeScreen);
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
                    MessageBox.Show((sender as SurfaceButton).Name);
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
