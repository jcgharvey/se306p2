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
    /// Interaction logic for InfoScreen.xaml
    /// </summary>
    public partial class InfoScreen : UserControl
    {
        private HomeScreen homeScreen;
        public InfoScreen(HomeScreen homeScreen)
        {
            this.homeScreen = homeScreen;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((SurfaceWindow1)(this.Parent)).Content = homeScreen;
        }
    }
}
