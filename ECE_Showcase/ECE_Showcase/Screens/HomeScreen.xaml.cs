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
    public partial class HomeScreen : UserControl
    {
        private InfoScreen infoScreen;
        private HODWelcomeScreen hodWelcomeScreen;

        public HomeScreen()
        {
            InitializeComponent();

            infoScreen = null;
            hodWelcomeScreen = null;
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            if (infoScreen == null)
            {
                infoScreen = new InfoScreen();
            }

            ((SurfaceWindow1)(this.Parent)).Content = infoScreen;
            
        }

        private void HODButton_Click(object sender, RoutedEventArgs e)
        {
            if (hodWelcomeScreen == null)
            {
                hodWelcomeScreen = new HODWelcomeScreen();
            }
            ((SurfaceWindow1)(this.Parent)).Content = hodWelcomeScreen;
        }
    }
}
