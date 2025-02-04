using Kynou.Helpers;
using MaterialDesignThemes.Wpf;
using System.Text;
using System.Windows;
using System.Windows.Controls;
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
            InitializeComponent();  // Ensures UI components are initialized

            if (MainContentControl == null)
            {
                Console.WriteLine("MainContentControl is null after InitializeComponent");
            }

            SetTitle("Welcome!");
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            WindowState = WindowState.Maximized;
            UpdateMaximizeRestoreButton();

            // Set initial content safely
            if (MainContentControl != null)
            {
                MainContentControl.Content = new TextBlock
                {
                    Text = "Welcome to Home",
                    FontSize = 20,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
            }
        }

        private void SetTitle(string msg, bool? includeTimestamp = false)
        {
            StringBuilder sb = new();
            _ = sb.Append(WINDOW_TITLE);
            _ = sb.Append(msg);

            if (includeTimestamp == true)
            {
                string newTimestamp = StringHelper.GenerateTimestamp();
                _ = sb.Append(newTimestamp);
            }

            Dispatcher.Invoke(() =>
            {
                Title = sb.ToString();
                txtTitle.Text = sb.ToString(); // Update custom title bar text
            });
        }

        private void MinimizeWindow_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeRestoreWindow_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            UpdateMaximizeRestoreButton();
        }

        private void UpdateMaximizeRestoreButton()
        {
            if (iconMaxRestore != null)
            {
                iconMaxRestore.Kind = WindowState == WindowState.Maximized ? PackIconKind.WindowRestore : PackIconKind.WindowMaximize;
            }
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private DateTime? lastTitleBarClick = null;
        private readonly int doubleClickInterval = 300;

        private bool IsTitleDoubleClick()
        {
            if (lastTitleBarClick == null)
            {
                lastTitleBarClick = DateTime.Now;
                return false;
            }

            DateTime now = DateTime.Now;
            TimeSpan? delta = now - lastTitleBarClick;
            bool isDoubleClick = delta != null && delta.Value.TotalMilliseconds <= doubleClickInterval;
            lastTitleBarClick = DateTime.Now;

            return isDoubleClick;
        }

        private void DoubleClickTitleBar()
        {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            UpdateMaximizeRestoreButton();
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IsTitleDoubleClick())
            {
                DoubleClickTitleBar();
                return;
            }

            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        /// <summary>
        /// Handles tab selection changes and updates content accordingly.
        /// </summary>
        private void OnTabSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainContentControl == null)
            {
                return;
            }

            if (sender == null)
            {
                Console.WriteLine("OnTabSelectionChanged: sender is null");
                return;
            }

            if (sender is ListBox listBox)
            {
                if (listBox.SelectedItem == null)
                {
                    Console.WriteLine("OnTabSelectionChanged: SelectedItem is null");
                    return;
                }

                if (listBox.SelectedItem is ListBoxItem selectedItem)
                {
                    if (selectedItem.Tag == null)
                    {
                        Console.WriteLine("OnTabSelectionChanged: Tag is null");
                    }
                    else
                    {
                        string selectedTab = selectedItem.Tag.ToString();
                        Console.WriteLine($"OnTabSelectionChanged: Selected tab is {selectedTab}");
                        UIElement returnedTab = GetTabContent(selectedTab);
                        MainContentControl.Content = returnedTab;
                    }
                }
            }
        }

        /// <summary>
        /// Returns appropriate content for each selected tab.
        /// </summary>
        private UIElement GetTabContent(string tab)
        {
            return new TextBlock
            {
                Text = tab + " Content",
                FontSize = 20,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };
        }
    }
}
