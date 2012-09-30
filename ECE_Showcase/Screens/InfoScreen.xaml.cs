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
using System.Windows.Markup;
using System.IO;
using System.Windows.Resources;
using System.Reflection;

namespace ECE_Showcase.Screens
{
    /// <summary>
    /// Interaction logic for InfoScreen.xaml
    /// </summary>
    public partial class InfoScreen : Screen
    {
        public InfoScreen(SurfaceWindow1 parentWindow) : base(parentWindow)
        {
            InitializeComponent();
            // H:\ECE_Showcase\Resources\eee_info.xaml
            infoViewer.Document = (FlowDocument)XamlReader.Load(File.OpenRead("/ECE_Showcase/Resources/eee_info.xaml"));
     
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            ParentWindow.popAll();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ParentWindow.popScreen();
        }

        

       
    }
}
