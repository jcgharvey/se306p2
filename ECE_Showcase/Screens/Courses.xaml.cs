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
        private ObservableCollection<DataItem> se_items;
        private ObservableCollection<DataItem> eee_items;
        private ObservableCollection<DataItem> cse_items;
        private ObservableCollection<DataItem> p1_items;

        private ObservableCollection<DataItem> targetItems;
        private UserControl Current_control { get; set; }

        private MediaElement accordian_fx;

        public ObservableCollection<DataItem> Cse_items
        {
            get
            {
                if (cse_items == null)
                {
                    cse_items = new ObservableCollection<DataItem>();
                }
                return cse_items;
            }
        }
        public ObservableCollection<DataItem> Se_items
        {
            get
            {
                if (se_items == null)
                {
                    se_items = new ObservableCollection<DataItem>();
                }
                return se_items;
            }
        }
        /// <summary>
        /// Items that bind with the drag source list box.
        /// </summary>
        public ObservableCollection<DataItem> Eee_items
        {
            get
            {
                if (eee_items == null)
                {
                   
                    eee_items = new ObservableCollection<DataItem>();
                }

                return eee_items;
            }
        }
        public ObservableCollection<DataItem> P1_items
        {
            get
            {
                if (p1_items == null)
                {

                    p1_items = new ObservableCollection<DataItem>();
                }

                return p1_items;
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

            accordian_fx = new MediaElement();
            accordian_fx.LoadedBehavior = MediaState.Manual;
            accordian_fx.UnloadedBehavior = MediaState.Manual;
            accordian_fx.LoadedBehavior = MediaState.Manual;
            accordian_fx.Volume = 1.0;
            accordian_fx.IsMuted = false;

            Cse_items.Add(new DataItem("description", new Controls.FlowDocControl("Resources/docs/specialisations/cse_info.xaml")));
            Cse_items.Add(new DataItem("careers", new Controls.FlowDocControl("Resources/docs/specialisations/cse_careers.xaml")));
            Cse_items.Add(new DataItem("courses", new Controls.CoursesControl()));
            Cse_items.Add(new DataItem("programme advisor", new Controls.FlowDocControl("Resources/docs/specialisations/cse_advisor.xaml")));

            Se_items.Add(new DataItem("description", new Controls.FlowDocControl("Resources/docs/specialisations/se_info.xaml")));
            Se_items.Add(new DataItem("careers", new Controls.FlowDocControl("Resources/docs/specialisations/se_careers.xaml")));
            Se_items.Add(new DataItem("courses", new Controls.CoursesControl()));
            Se_items.Add(new DataItem("programme advisor", new Controls.FlowDocControl("Resources/docs/specialisations/se_advisor.xaml")));

            Eee_items.Add(new DataItem("description", new Controls.FlowDocControl("Resources/docs/specialisations/eee_info.xaml")));
            Eee_items.Add(new DataItem("careers", new Controls.FlowDocControl("Resources/docs/specialisations/eee_careers.xaml")));
            Eee_items.Add(new DataItem("courses", new Controls.CoursesControl()));
            Eee_items.Add(new DataItem("programme advisor", new Controls.FlowDocControl("Resources/docs/specialisations/eee_advisor.xaml")));
            
            P1_items.Add(new DataItem("description", new Controls.FlowDocControl("Resources/docs/specialisations/p1_info.xaml")));
            P1_items.Add(new DataItem("courses", new Controls.CoursesControl()));

            setControl(new Controls.FlowDocControl("Resources/docs/tap_course.xaml"));
        }

        private void setControl(UserControl new_control)
        {
            theGrid.Children.Remove(Current_control);
            new_control.AllowDrop = true;
            Grid.SetColumn(new_control, 3);
            Grid.SetRow(new_control, 1);
            Current_control = new_control;
            theGrid.Children.Add(Current_control);
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
            if (e.Cursor.Effects == DragDropEffects.Move)
            {
                setControl((e.Cursor.Data as DataItem).ItemControl);
            }
        }

        private void Expander_TouchUp(object sender, TouchEventArgs e)
        {
            accordian_fx.Source = new Uri("Resources/sounds/expand.mp3", UriKind.Relative);
            accordian_fx.Play();

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

        private void Expander_TouchDown(object sender, TouchEventArgs e)
        {
            accordian_fx.Source = new Uri("Resources/sounds/collapse.wav", UriKind.Relative);
            accordian_fx.Play();

            Console.WriteLine("touch down");
            Expander exp = sender as Expander;

            if (exp == null)

                return;

            exp.CaptureTouch(e.TouchDevice);
            e.Handled = true;
        }
    }
}
