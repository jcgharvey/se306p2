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

namespace ECE_Showcase.Screens
{
    /// <summary>
    /// Interaction logic for HomeScreen.xaml
    /// </summary>
    public partial class HomeScreen : Screen
    {
        private FirstLevelScreen infoScreen;
        private FirstLevelScreen hodWelcomeScreen;
        private Courses coursesScreen;
        private FirstLevelScreen contactScreen;

        public HomeScreen(SurfaceWindow1 parentWindow) : base(parentWindow)
        {
            InitializeComponent();

            infoScreen = null;
            hodWelcomeScreen = null;
            contactScreen = null;
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            if (infoScreen == null)
            {
                infoScreen = new FirstLevelScreen(ParentWindow, "information");
                infoScreen.setRight(new Controls.FlowDocControl("Resources/docs/info.xaml"));
            }

            ParentWindow.pushScreen(infoScreen);
        }

        private void HODButton_Click(object sender, RoutedEventArgs e)
        {
            if (hodWelcomeScreen == null)
            {
                hodWelcomeScreen = new FirstLevelScreen(ParentWindow, "hod welcome");
                hodWelcomeScreen.setLeft(new Controls.ImageControl("Resources/img/zoran.png"));
                hodWelcomeScreen.setRight(new Controls.FlowDocControl("Resources/docs/hod_welcome.xaml"));
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

        private void Contact_Click (object sender, RoutedEventArgs e)
        {
            if (contactScreen == null)
            {
                contactScreen = new FirstLevelScreen(ParentWindow, "contact us");
                contactScreen.setLeft(new Controls.FlowDocControl("Resources/docs/contact_us.xaml"));
                contactScreen.setRight(new Controls.MapControl());
                
            }
            ParentWindow.pushScreen(contactScreen);
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            ParentWindow.popScreen();
        }

        private void SurfaceButton_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
