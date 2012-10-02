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

        public HomeScreen(SurfaceWindow1 parentWindow) : base(parentWindow)
        {
            InitializeComponent();

            infoScreen = null;
            hodWelcomeScreen = null;
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

        private void HODButton_TouchUp(object sender, TouchEventArgs e)
        {
            if (hodWelcomeScreen == null)
            {
                hodWelcomeScreen = new HODWelcomeScreen(ParentWindow);
            }
            ParentWindow.pushScreen(hodWelcomeScreen);
        }

        private void HODButton_PreviewTouchMove(object sender, TouchEventArgs e)
        {
            
        }

        private void HODButton_PreviewTouchDown(object sender, TouchEventArgs e)
        {
           TouchPoint touchPoint = e.GetTouchPoint(sender as SurfaceButton);
            Debug.WriteLine(touchPoint.Position.X + " " + touchPoint.Position.Y);


        }
    }
}
