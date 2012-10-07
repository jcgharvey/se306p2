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
using System.IO;
using System.Windows.Markup;

namespace ECE_Showcase.Controls
{
    /// <summary>
    /// Interaction logic for FlowDocControl.xaml
    /// </summary>
    public partial class FlowDocControl : UserControl
    {
        public FlowDocControl(String flowdoc)
        {
            InitializeComponent();

            FlowDocument flowDocument = (FlowDocument)XamlReader.Load(File.OpenRead(flowdoc));

            infoViewer.Document = flowDocument;
            
            flowDocument.ColumnWidth = ScrollViewer.Width;
            flowDocument.PageWidth = ScrollViewer.Width;
            ScrollViewer.Content = flowDocument;
           
        }
    }
}
