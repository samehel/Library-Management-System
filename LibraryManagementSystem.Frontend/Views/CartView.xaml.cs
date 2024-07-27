using LibraryManagementSystem.Frontend.ViewModels;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LibraryManagementSystem.Frontend.Views
{
    public partial class CartView : UserControl
    {
        public CartView()
        {
            InitializeComponent();
            this.Loaded += CartView_Loaded;
        }

        private async void CartView_Loaded(object sender, RoutedEventArgs e)
        {
            if (MainWindow.CurrentUser == null)
            {
                NotLoggedInMessage.Visibility = Visibility.Visible;
                EmptyCartMessage.Visibility = Visibility.Collapsed;
                CartItemsPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                NotLoggedInMessage.Visibility = Visibility.Collapsed;
                if (DataContext is CartViewModel viewModel)
                {
                    await viewModel.LoadCart();
                    Debug.WriteLine($"CartBooks count after LoadCart: {viewModel.CartBooks.Count}");
                    if (viewModel.CartBooks.Count == 0)
                    {
                        EmptyCartMessage.Visibility = Visibility.Visible;
                        CartItemsPanel.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        EmptyCartMessage.Visibility = Visibility.Collapsed;
                        CartItemsPanel.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void RemoveFromCart_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is int bookId && DataContext is CartViewModel viewModel)
            {
                viewModel.RemoveFromCart(bookId);
            }
        }

        private void ClearCart_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is CartViewModel viewModel)
            {
                viewModel.ClearCart();
            }
        }

        private void IncreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is int bookId && DataContext is CartViewModel viewModel)
            {
                viewModel.IncreaseQuantity(bookId);
            }
        }

        private void DecreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is int bookId && DataContext is CartViewModel viewModel)
            {
                viewModel.DecreaseQuantity(bookId);
            }
        }
    }
}
