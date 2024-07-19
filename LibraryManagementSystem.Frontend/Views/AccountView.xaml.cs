using System.Windows;
using System.Windows.Controls;
using LibraryManagementSystem.Frontend.ViewModels;

namespace LibraryManagementSystem.Frontend.Views
{
    public partial class AccountView : UserControl
    {
        private AccountViewModel ViewModel => (AccountViewModel)DataContext;

        public AccountView()
        {
            InitializeComponent();
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoginAsync();
        }
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Logout();
        }

        private async void Register_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.RegisterAsync();
        }

        private async void Update_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.UpdateUserInfoAsync();
        }
    }
}
