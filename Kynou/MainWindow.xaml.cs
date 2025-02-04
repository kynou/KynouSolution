using Kynou.Helpers;
using System.Text;
using System.Windows;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;

namespace Kynou
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string WINDOW_TITLE = "Kynou | ";

        public MainWindow()
        {
            InitializeComponent();

            SetTitle("Welcome!");

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            WindowState = WindowState.Maximized;
            UpdateMaximizeRestoreButton();
        }

        private void SetTitle(string msg, bool? includeTimestamp = false)
        {
            var sb = new StringBuilder();

            sb.Append(WINDOW_TITLE);
            sb.Append(msg);

            if (includeTimestamp == true)
            {
                string newTimestamp = StringHelper.GenerateTimestamp();
                sb.Append(newTimestamp);
            }

            this.Dispatcher.Invoke(() =>
            {
                Title = sb.ToString();
                txtTitle.Text = sb.ToString(); // Update custom title bar text
            });
        }

        private void MinimizeWindow_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaximizeRestoreWindow_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }

            UpdateMaximizeRestoreButton();
        }

        private void UpdateMaximizeRestoreButton()
        {
            if (iconMaxRestore != null)
            {
                iconMaxRestore.Kind = this.WindowState == WindowState.Maximized ? PackIconKind.WindowRestore : PackIconKind.WindowMaximize;
            }
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private DateTime? lastTitleBarClick = null;

        private int doubleClickInterval = 300;

        private bool IsTitleDoubleClick()
        {
            if (lastTitleBarClick == null)
            {
                lastTitleBarClick = DateTime.Now;
                return false;
            }

            var now = DateTime.Now;

            var delta = now - lastTitleBarClick;

            var r = delta != null && delta.Value.TotalMilliseconds <= doubleClickInterval;

            lastTitleBarClick = DateTime.Now;

            return r;
        }

        private void DoubleClickTitleBar()
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IsTitleDoubleClick())
            {
                DoubleClickTitleBar();
                return;
            }

            // Allow window dragging only when the left mouse button is pressed
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
