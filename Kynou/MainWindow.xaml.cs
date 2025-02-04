using Kynou.Helpers;
using System.Text;
using System.Windows;
using System.Windows.Input;

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

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}