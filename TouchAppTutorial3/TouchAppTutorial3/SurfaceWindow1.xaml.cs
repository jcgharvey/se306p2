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
using Microsoft.Surface;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using Microsoft.Surface.Presentation.Input;

namespace TouchAppTutorial3
{
    /// <summary>
    /// Interaction logic for SurfaceWindow1.xaml
    /// </summary>
    public partial class SurfaceWindow1 : SurfaceWindow
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public SurfaceWindow1()
        {
            InitializeComponent();

            // Add handlers for window availability events
            AddWindowAvailabilityHandlers();
        }

        /// <summary>
        /// Occurs when the window is about to close. 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // Remove handlers for window availability events
            RemoveWindowAvailabilityHandlers();
        }

        /// <summary>
        /// Adds handlers for window availability events.
        /// </summary>
        private void AddWindowAvailabilityHandlers()
        {
            // Subscribe to surface window availability events
            ApplicationServices.WindowInteractive += OnWindowInteractive;
            ApplicationServices.WindowNoninteractive += OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable += OnWindowUnavailable;
        }

        /// <summary>
        /// Removes handlers for window availability events.
        /// </summary>
        private void RemoveWindowAvailabilityHandlers()
        {
            // Unsubscribe from surface window availability events
            ApplicationServices.WindowInteractive -= OnWindowInteractive;
            ApplicationServices.WindowNoninteractive -= OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable -= OnWindowUnavailable;
        }

        /// <summary>
        /// This is called when the user can interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowInteractive(object sender, EventArgs e)
        {
            //TODO: enable audio, animations here
        }

        /// <summary>
        /// This is called when the user can see but not interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowNoninteractive(object sender, EventArgs e)
        {
            //TODO: Disable audio here if it is enabled

            //TODO: optionally enable animations here
        }

        /// <summary>
        /// This is called when the application's window is not visible or interactive.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowUnavailable(object sender, EventArgs e)
        {
            //TODO: disable audio, animations here
        }

        private void _textEnter_Click(object sender, RoutedEventArgs e)
        {
            _textBlock.Text = _textbox.Text;
        }

        private void _imageClose_Click(object sender, RoutedEventArgs e)
        {
            _imageGrid.Visibility = System.Windows.Visibility.Collapsed;
            _imageHiddenText.Visibility = System.Windows.Visibility.Visible;
            _imageItem.Width = 80;
            _imageItem.Height = 20;
        }
        private void _imageHiddenText_Click(object sender, RoutedEventArgs e)
        {
            _imageGrid.Visibility = System.Windows.Visibility.Visible;
            _imageHiddenText.Visibility = System.Windows.Visibility.Collapsed;
            _imageItem.Width = 400;
            _imageItem.Height = 200;
        }

        private void _mediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            _mediaElement.Stop();
        }
        private void _mediaElement_PreviewTouchDown(object sender, InputEventArgs e)
        {
            if (_mediaElement.Tag.ToString() == "pause")
            {
                _mediaElement.Tag = "play";
                _mediaText.Visibility = System.Windows.Visibility.Hidden;
                _mediaElement.Play();
            }
            else
            {
                _mediaElement.Pause();
                _mediaElement.Tag = "pause";
                _mediaText.Visibility = System.Windows.Visibility.Visible;
            }
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            DataContext = this;
            _mediaElement.Pause();
        }
    }
}