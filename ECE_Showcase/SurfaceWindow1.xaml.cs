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

namespace ECE_Showcase
{
    /// <summary>
    /// Interaction logic for SurfaceWindow1.xaml
    /// </summary>
    public partial class SurfaceWindow1 : SurfaceWindow
    {
        private Stack<Screen> screenStack;
        private MediaElement sound_fx;

        /// <summary>
        /// Default constructor.
        /// </summary>
        

        public SurfaceWindow1()
        {
            InitializeComponent();

            sound_fx = new MediaElement();
            sound_fx.LoadedBehavior = MediaState.Manual;
            sound_fx.UnloadedBehavior = MediaState.Manual;
            sound_fx.LoadedBehavior = MediaState.Manual;
            sound_fx.Volume = 1.0;
            sound_fx.IsMuted = false;

            // Add handlers for window availability events
            AddWindowAvailabilityHandlers();

            screenStack = new Stack<Screen>();
            //Setting starting Screen here, maybe should be in one of the other OnXXX methods of this class
            pushScreen(new Screens.HomeScreen(this));

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
        public void popScreen()
        {
            sound_fx.Source = new Uri("Resources/sounds/back.mp3", UriKind.Relative);
            sound_fx.Play();

            if (screenStack.Count > 1)
            {
                screenStack.Pop();
                this.Content = screenStack.Peek();
                this.WindowState = WindowState.Maximized;
                this.WindowStyle = WindowStyle.None;
            }
        }
        public void pushScreen(Screen screen)
        {
            sound_fx.Source = new Uri("Resources/sounds/click.mp3", UriKind.Relative);
            sound_fx.Play();

            screenStack.Push(screen);
            this.Content = screenStack.Peek();
            this.WindowState = WindowState.Maximized;
            this.WindowStyle = WindowStyle.None;

        }

        public void popAll()
        {
            sound_fx.Source = new Uri("Resources/sounds/home.wav", UriKind.Relative);
            sound_fx.Play();

            while (screenStack.Count > 1)
            {
                screenStack.Pop();           
            }
            this.Content = screenStack.Peek();
            this.WindowState = WindowState.Maximized;
            this.WindowStyle = WindowStyle.None;
        }

    }
}