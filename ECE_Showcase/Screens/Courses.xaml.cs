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
using System.Collections.ObjectModel;
using System.Windows.Markup;
using System.IO;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;

namespace ECE_Showcase.Screens
{
    /// <summary>
    /// Interaction logic for Courses.xaml
    /// </summary>
    public partial class Courses : Screen
    {
        private ObservableCollection<DataItem> sourceItems;
        private ObservableCollection<DataItem> targetItems;
        /// <summary>
        /// Items that bind with the drag source list box.
        /// </summary>
        public ObservableCollection<DataItem> SourceItems
        {
            get
            {
                if (sourceItems == null)
                {
                    sourceItems = new ObservableCollection<DataItem>();
                }

                return sourceItems;
            }
        }

        /// <summary>
        /// Items that bind with the drop target list box.
        /// </summary>
        public ObservableCollection<DataItem> TargetItems
        {
            get
            {
                if (targetItems == null)
                {
                    targetItems = new ObservableCollection<DataItem>();
                }

                return targetItems;
            }
        }
        public Courses(SurfaceWindow1 parentWindow)
            : base(parentWindow)
        {
            InitializeComponent();
            DataContext = this;
            SourceItems.Add(new DataItem("Software Engineering", "Resources/docs/specialisations/se.xaml", "../Resources/img/software.png"));
            SourceItems.Add(new DataItem("Electrical and Electronic Engineering", "Resources/docs/specialisations/eee.xaml", "../Resources/img/electrical.png"));
            SourceItems.Add(new DataItem("Computer Systems Engineering", "Resources/docs/specialisations/cse.xaml", "../Resources/img/compsys.png"));
            infoViewer.Document = (FlowDocument)XamlReader.Load(File.OpenRead("Resources/docs/specialisations/se.xaml"));

        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            ParentWindow.popAll();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ParentWindow.popScreen();
        }
        private void OnDragSourcePreviewTouchDown(object sender, InputEventArgs e)
        {
            FrameworkElement findSource = e.OriginalSource as FrameworkElement;
            SurfaceListBoxItem draggedElement = null;

            // Find the touched SurfaceListBoxItem object.
            while (draggedElement == null && findSource != null)
            {
                if ((draggedElement = findSource as SurfaceListBoxItem) == null)
                {
                    findSource = VisualTreeHelper.GetParent(findSource) as FrameworkElement;
                }
            }

            if (draggedElement == null)
            {
                return;
            }

            // Create the cursor visual.
            ContentControl cursorVisual = new ContentControl()
            {
                Content = draggedElement.DataContext,
                Style = FindResource("CursorStyle") as Style
            };

            // Add a handler. This will enable the application to change the visual cues.
            SurfaceDragDrop.AddTargetChangedHandler(cursorVisual, OnTargetChanged);

            // Create a list of input devices. Add the touches that
            // are currently captured within the dragged element and
            // the current touch (if it isn't already in the list).
            List<InputDevice> devices = new List<InputDevice>();
            devices.Add(e.Device);
            foreach (TouchDevice touch in draggedElement.TouchesCapturedWithin)
            {
                if (touch != e.Device)
                {
                    devices.Add(touch);
                }
            }

            // Get the drag source object
            ItemsControl dragSource = ItemsControl.ItemsControlFromItemContainer(draggedElement);

            SurfaceDragCursor startDragOkay =
                SurfaceDragDrop.BeginDragDrop(
                  dragSource,                 // The SurfaceListBox object that the cursor is dragged out from.
                  draggedElement,             // The SurfaceListBoxItem object that is dragged from the drag source.
                  cursorVisual,               // The visual element of the cursor.
                  draggedElement.DataContext, // The data associated with the cursor.
                  devices,                    // The input devices that start dragging the cursor.
                  DragDropEffects.Move);      // The allowed drag-and-drop effects of the operation.

            // If the drag began successfully, set e.Handled to true. 
            // Otherwise SurfaceListBoxItem captures the touch 
            // and causes the drag operation to fail.
            e.Handled = (startDragOkay != null);
        }

        private void OnDropTargetDragEnter(object sender, SurfaceDragDropEventArgs e)
        {

        }

        private void OnDropTargetDragLeave(object sender, SurfaceDragDropEventArgs e)
        {
            // Reset the effects.
            e.Effects = e.Cursor.AllowedEffects;
        }

        private void OnTargetChanged(object sender, TargetChangedEventArgs e)
        {
            if (e.Cursor.CurrentTarget != null)
            {
                DataItem data = e.Cursor.Data as DataItem;
                e.Cursor.Visual.Tag = "CanDrop";
            }
            else
            {
                e.Cursor.Visual.Tag = null;
            }
        }

        private void OnDropTargetDrop(object sender, SurfaceDragDropEventArgs e)
        {
            TargetItems.Clear();
            TargetItems.Add(e.Cursor.Data as DataItem);
        }

        private void OnDragCompleted(object sender, SurfaceDragCompletedEventArgs e)
        {
            // If the operation is Move, remove the data from drag source.
            //if (e.Cursor.Effects == DragDropEffects.Move)
            //{
            //    SourceItems.Remove(e.Cursor.Data as DataItem);
            //}
        }


        private void DropTargetRichTextBox_Drop(object sender, Microsoft.Surface.Presentation.SurfaceDragDropEventArgs e)
        {
            infoViewer.Document = (FlowDocument)XamlReader.Load(File.OpenRead((e.Cursor.Data as DataItem).FilePath));
        }



        private void Expander_TouchUp(object sender, TouchEventArgs e)
        {

            Expander exp = sender as Expander;

            if (exp == null)

                return;



            TouchPoint tp = e.GetTouchPoint(exp);

            Rect bounds = new Rect(new Point(0, 0), exp.RenderSize);

            if (exp.IsExpanded)
            {
                exp.IsExpanded = false;
            }
            else
            {
                exp.IsExpanded = true;
            }
            acc.selectedExpander_Expanded(exp, e);

            if (bounds.Contains(tp.Position))
            {

            }
            exp.ReleaseTouchCapture(e.TouchDevice);
            e.Handled = true;

        }

        private void Expander_TouchDown(object sender, TouchEventArgs e)
        {

            Console.WriteLine("touch down");
            Expander exp = sender as Expander;

            if (exp == null)

                return;

            acc.selectedExpander_Expanded(exp, e);
            exp.CaptureTouch(e.TouchDevice);
            e.Handled = true;
        }


    }
}
