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
            InitializeComponent(); // Ensures UI components are initialized

            if (MainContentControl == null)
            {
                Console.WriteLine("MainContentControl is null after InitializeComponent");
            }

            SetTitle("Welcome!");

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            WindowState = WindowState.Maximized;

            // Set initial content safely
            if (MainContentControl != null)
                MainContentControl.Content = new TextBlock
                {
                    Text = "Welcome to Home",
                    FontSize = 20,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
        }

        private void SetTitle(string msg, bool? includeTimestamp = false)
        {
            StringBuilder sb = new();
            sb.Append(WINDOW_TITLE);
            sb.Append(msg);
            if (includeTimestamp == true)
            {
                string newTimestamp = StringHelper.GenerateTimestamp();
                sb.Append(newTimestamp);
            }

            Dispatcher.Invoke(() =>
            {
                Title = sb.ToString();
            });
        }

        private void OnTabSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainContentControl == null)
                return;

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
                        Console.WriteLine("OnTabSelectionChanged: Tag is null");
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

        private UIElement GetTabContent(string tab)
        {
            return new TextBlock
            {
                Text = tab + "Content",
                FontSize = 20,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };
        }
    }
}