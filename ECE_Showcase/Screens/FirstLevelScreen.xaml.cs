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

namespace ECE_Showcase.Screens
{
	/// <summary>
	/// Interaction logic for UserControl1.xaml
	/// </summary>
	public partial class FirstLevelScreen : Screen
    

	{
		public FirstLevelScreen(SurfaceWindow1 parentWindow, String flowdoc) : base(parentWindow)
		{
			InitializeComponent();
            FlowDocument flowDocument = (FlowDocument)XamlReader.Load(File.OpenRead(flowdoc));
            
            infoViewer.Document = flowDocument;
            /*
            flowDocument.ColumnWidth = ScrollViewer.Width;
            flowDocument.PageWidth = ScrollViewer.Width;
            ScrollViewer.Content = flowDocument;
           */
            

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