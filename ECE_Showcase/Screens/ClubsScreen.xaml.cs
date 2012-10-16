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
    public partial class Clubs : Screen
    {
        private UserControl Current_control { get; set; }
        public ObservableCollection<DataItem> clubsItems;
        public ObservableCollection<DataItem> studentLifeItems;

        public ObservableCollection<DataItem> StudentLifeItems
        {
            get
            {
                if (studentLifeItems == null)
                {

                    studentLifeItems = new ObservableCollection<DataItem>();
                }

                return studentLifeItems;
            }
        }

        public ObservableCollection<DataItem> ClubsItems
        {
            get
            {
                if (clubsItems == null)
                {
                    clubsItems = new ObservableCollection<DataItem>();
                }

                return clubsItems;
            }
        }


        public Clubs(SurfaceWindow1 parentWindow)
            : base(parentWindow)
        {
            InitializeComponent();
            DataContext = this;

            StudentLifeItems.Add(new DataItem("sport", new Controls.FlowDocControl("Resources/docs/clubs/sport.xaml")));
            StudentLifeItems.Add(new DataItem("stein", new Controls.FlowDocControl("Resources/docs/clubs/stein.xaml")));
            StudentLifeItems.Add(new DataItem("lan", new Controls.FlowDocControl("Resources/docs/clubs/lan.xaml")));

            ClubsItems.Add(new DataItem("aues", new Controls.FlowDocControl("Resources/docs/clubs/aues.xaml")));
            ClubsItems.Add(new DataItem("spies", new Controls.FlowDocControl("Resources/docs/clubs/spies.xaml")));
            ClubsItems.Add(new DataItem("sesa", new Controls.FlowDocControl("Resources/docs/clubs/sesa.xaml")));
            ClubsItems.Add(new DataItem("hkesa", new Controls.FlowDocControl("Resources/docs/clubs/hkesa.xaml")));

            setControl(new Controls.FlowDocControl("Resources/docs/tap_course.xaml"));

        }

        private void setControl(UserControl new_control)
        {
            clubsGrid.Children.Remove(Current_control);
            new_control.AllowDrop = true;
            Grid.SetColumn(new_control, 3);
            Grid.SetRow(new_control, 1);
            Current_control = new_control;
            clubsGrid.Children.Add(Current_control);
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

        private void OnDragCompleted(object sender, SurfaceDragCompletedEventArgs e)
        {

            if (e.Cursor.Effects == DragDropEffects.Move)
            {
                setControl((e.Cursor.Data as DataItem).ItemControl);
            }
        }

        private void Expander_TouchDown(object sender, TouchEventArgs e)
        {

            Console.WriteLine("touch down");
            Expander exp = sender as Expander;

            if (exp == null)

                return;

            exp.CaptureTouch(e.TouchDevice);
            e.Handled = true;

        }

        private void Expander_TouchUp(object sender, TouchEventArgs e)
        {

            Expander exp = sender as Expander;

            if (exp == null)

                return;

            TouchPoint tp = e.GetTouchPoint(exp);

            Rect bounds = new Rect(new Point(0, 0), exp.RenderSize);



            if (bounds.Contains(tp.Position))
            {
                if (exp.IsExpanded)
                {

                    exp.IsExpanded = false;
                    setControl(new Controls.FlowDocControl("Resources/docs/tap_course.xaml"));
                }
                else
                {
                    exp.IsExpanded = true;
                    setControl(new Controls.FlowDocControl("Resources/docs/drag_here.xaml"));
                }

                acc.selectedExpander_Expanded(exp, e);
            }
            exp.ReleaseTouchCapture(e.TouchDevice);
            e.Handled = true;
        }
    }
}
