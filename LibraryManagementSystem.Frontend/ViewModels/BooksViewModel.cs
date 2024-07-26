using LibraryManagementSystem.Frontend.Models;
using LibraryManagementSystem.Frontend.Services;
using LibraryManagementSystem.Frontend.Utilities;
using LibraryManagementSystem.Frontend.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace LibraryManagementSystem.Frontend.ViewModels
{
    public class BooksViewModel : INotifyPropertyChanged
    {
        private readonly BookService _bookService;
        private List<Book> _allBooks;
        private ObservableCollection<Book> _pagedBooks;
        private bool _canGoToPreviousPage;
        private bool _canGoToNextPage;
        private int _currentPage;
        private const int BooksPerPage = 9;
        private Visibility _loadingBarVisibility = Visibility.Collapsed;
        private Visibility _itemsControlVisibility = Visibility.Collapsed;

        public event PropertyChangedEventHandler PropertyChanged;

        public Visibility LoadingBarVisibility
        {
            get => _loadingBarVisibility;
            set
            {
                if (_loadingBarVisibility != value)
                {
                    _loadingBarVisibility = value;
                    OnPropertyChanged(nameof(LoadingBarVisibility));
                }
            }
        }

        public Visibility ItemsControlVisibility
        {
            get => _itemsControlVisibility;
            set
            {
                if (_itemsControlVisibility != value)
                {
                    _itemsControlVisibility = value;
                    OnPropertyChanged(nameof(ItemsControlVisibility));
                }
            }
        }

        public ObservableCollection<Book> PagedBooks
        {
            get => _pagedBooks;
            set
            {
                _pagedBooks = value;
                OnPropertyChanged(nameof(PagedBooks));
            }
        }

        public bool CanGoToPreviousPage
        {
            get => _canGoToPreviousPage;
            set
            {
                _canGoToPreviousPage = value;
                OnPropertyChanged(nameof(CanGoToPreviousPage));
            }
        }

        public bool CanGoToNextPage
        {
            get => _canGoToNextPage;
            set
            {
                _canGoToNextPage = value;
                OnPropertyChanged(nameof(CanGoToNextPage));
            }
        }

        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }

        public BooksViewModel()
        {
            _bookService = new BookService();
            LoadBooks();
        }

        public async void LoadBooks()
        {
            // Show loading bar and hide books
            LoadingBarVisibility = Visibility.Visible;
            ItemsControlVisibility = Visibility.Collapsed;

            // Load books asynchronously
            if (MainWindow.Books == null || MainWindow.Books.Count == 0)
            {
                MainWindow.Books = await _bookService.GetAllBooksAsync();
                await ImageCache.DownloadAndCacheImagesAsync(MainWindow.Books);
            }

            _allBooks = MainWindow.Books;
            CurrentPage = 1;
            UpdatePagedBooks();

            // Update UI to hide loading bar and show books
            LoadingBarVisibility = Visibility.Collapsed;
            ItemsControlVisibility = Visibility.Visible;
        }

        public void UpdatePagedBooks()
        {
            var books = _allBooks.Skip((CurrentPage - 1) * BooksPerPage).Take(BooksPerPage).ToList();

            foreach (Book book in books)
                book.Image = ImageCache.GetImage(book.PictureUrl);
            
            PagedBooks = new ObservableCollection<Book>(books);
            CanGoToPreviousPage = CurrentPage > 1;
            CanGoToNextPage = CurrentPage < (_allBooks.Count + BooksPerPage - 1) / BooksPerPage;
        }

        public void PreviousPage()
        {
            if (CanGoToPreviousPage)
            {
                CurrentPage--;
                UpdatePagedBooks();
            }
        }

        public void NextPage()
        {
            if (CanGoToNextPage)
            {
                CurrentPage++;
                UpdatePagedBooks();
            }
        }

        public void ViewDetails(int bookId)
        {
            // Implement the logic to view book details
        }

        public void AddToCart(int bookId)
        {
            // Implement the logic to add the book to the cart
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
