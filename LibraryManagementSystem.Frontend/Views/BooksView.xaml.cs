using LibraryManagementSystem.Frontend.Models;
using LibraryManagementSystem.Frontend.ViewModels;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace LibraryManagementSystem.Frontend.Views
{
    public partial class BooksView : UserControl
    {
        private Book _selectedBook;

        public BooksView()
        {
            InitializeComponent();
        }

        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is BooksViewModel viewModel && sender is Button button && button.CommandParameter is int bookId)
            {
                viewModel.AddToCart(bookId);
            }
        }

        private void ViewDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is BooksViewModel viewModel && sender is Button button && button.CommandParameter is int bookId)
            {
                viewModel.ViewDetails(bookId);
            }
        }

        private void PreviousPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is BooksViewModel viewModel)
            {
                viewModel.PreviousPage();
            }
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is BooksViewModel viewModel)
            {
                viewModel.NextPage();
            }
        }
    }

}
