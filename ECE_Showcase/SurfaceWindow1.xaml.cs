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

        /// <summary>
        /// Default constructor.
        /// </summary>
        

        public SurfaceWindow1()
        {
            InitializeComponent();

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
            MediaElement back = new MediaElement();
            back.LoadedBehavior = MediaState.Manual;
            back.UnloadedBehavior = MediaState.Manual;
            back.LoadedBehavior = MediaState.Manual;
            back.Volume = 100.0;
            back.IsMuted = false;
            back.Source = new Uri("Resources/sound/back.mp3", UriKind.Relative);
            back.Play();

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
            MediaElement click = new MediaElement();
            click.LoadedBehavior = MediaState.Manual;
            click.UnloadedBehavior = MediaState.Manual;
            click.LoadedBehavior = MediaState.Manual;
            click.Volume = 100.0;
            click.IsMuted = false;
            click.Source = new Uri("Resources/sound/click.mp3", UriKind.Relative);
            click.Play();

            screenStack.Push(screen);
            this.Content = screenStack.Peek();
            this.WindowState = WindowState.Maximized;
            this.WindowStyle = WindowStyle.None;

        }

        public void popAll()
        {
            MediaElement home = new MediaElement();
            home.LoadedBehavior = MediaState.Manual;
            home.UnloadedBehavior = MediaState.Manual;
            home.LoadedBehavior = MediaState.Manual;
            home.Volume = 100.0;
            home.IsMuted = false;
            home.Source = new Uri("Resources/sound/back.mp3", UriKind.Relative);
            home.Play();

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