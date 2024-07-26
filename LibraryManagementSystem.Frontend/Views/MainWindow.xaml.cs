using LibraryManagementSystem.Frontend.Models;
using LibraryManagementSystem.Frontend.ViewModels;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Threading;
using LibraryManagementSystem.Frontend.Services;

namespace LibraryManagementSystem.Frontend.Views
{
    public partial class MainWindow : Window
    {

        public static User CurrentUser { get; set; }
        public static Token UserToken { get; set; }
        public static List<Book> Books { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            LoadView("HomeView");
            UpdateAdminPanelVisibility();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }


        private void NavigationButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string selectedView)
            {
                LoadView(selectedView);
            }
        }

        private void LoadView(string viewName)
        {
            UserControl view = viewName switch
            {
                "HomeView" => new HomeView(),
                "BooksView" => new BooksView(),
                "CartView" => new CartView(),
                "AccountView" => new AccountView(),
                "AdminPanelView" => new AdminPanelView(),
                _ => new HomeView(),
            };

            ContentArea.Content = view;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string targetView = e!.Parameter as string;
            if (!string.IsNullOrEmpty(targetView))
            {
                switch (targetView)
                {
                    case "#BooksView":
                        ContentArea.Content = new BooksView(); 
                        break;
                    case "#CartView":
                        ContentArea.Content = new CartView(); 
                        break;
                    case "#AccountView":
                        ContentArea.Content = new AccountView();
                        break;
                    case "#AdminPanelView":
                        ContentArea.Content = new AdminPanelView();
                        break;
                    default:
                        break;
                }
            }
        }
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        public static void SetCurrentUser(User user)
        {
            CurrentUser = user;

            Application.Current.Dispatcher.Invoke(() =>
            {
                var mainWindow = Application.Current.MainWindow as MainWindow;
                if (mainWindow != null)
                {
                    mainWindow.UpdateAdminPanelVisibility();
                }
            });
        }

        public static void SetUserToken(Token token)
        {
            UserToken = token;
        }

        public static void SetBooks(List<Book> books)
        {
            Books = books;
        }

        private void UpdateAdminPanelVisibility()
        {
            if (CurrentUser != null && CurrentUser.Role == "Admin")
            {
                AdminPanelButton.Visibility = Visibility.Visible;
            }
            else
            {
                AdminPanelButton.Visibility = Visibility.Collapsed;
            }
        }
    }
}
