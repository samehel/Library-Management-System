using LibraryManagementSystem.Frontend.Models;
using LibraryManagementSystem.Frontend.Services;
using LibraryManagementSystem.Frontend.Utilities;
using LibraryManagementSystem.Frontend.Utilities.Enums;
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
        private readonly CartService _cartService;
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
            _cartService = new CartService();
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

        /// Book Properties for display

        private bool _isPopupOpen;
        private BitmapImage _popupImage;
        private string _bookTitle;
        private string _bookDescription;
        private string _bookAuthor;
        private string _bookISBN;
        private string _bookGenre;
        private int _bookQuantity;
        private string _bookDeweyDecimalNumber;

        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set
            {
                if (_isPopupOpen != value)
                {
                    _isPopupOpen = value;
                    OnPropertyChanged(nameof(IsPopupOpen));
                }
            }
        }

        public BitmapImage PopupImage
        {
            get => _popupImage;
            set
            {
                if (_popupImage != value)
                {
                    _popupImage = value;
                    OnPropertyChanged(nameof(PopupImage));
                }
            }
        }

        public string BookTitle
        {
            get => _bookTitle;
            set
            {
                if( _bookTitle != value)
                {
                    _bookTitle = value;
                    OnPropertyChanged(nameof(BookTitle));
                }
            }
        }

        public string BookDescription
        {
            get => _bookDescription;
            set
            {
                if(_bookDescription != value)
                {
                    _bookDescription = value;
                    OnPropertyChanged(nameof(BookDescription));
                } 
            }
        }

        public string BookAuthor
        {
            get => _bookAuthor;
            set
            {
                if (_bookAuthor != value)
                {
                    _bookAuthor = value;
                    OnPropertyChanged(nameof(BookAuthor));
                }
            }
        }

        public string BookISBN
        {
            get => _bookISBN;
            set
            {
                if (_bookISBN != value)
                {
                    _bookISBN = value;
                    OnPropertyChanged(nameof(BookISBN));
                }
            }
        }

        public string BookGenre
        {
            get => _bookGenre;
            set
            {
                if ( _bookGenre != value)
                {
                    _bookGenre = value;
                    OnPropertyChanged(nameof(BookGenre));
                }
            }
        }

        public int BookQuantity
        {
            get => _bookQuantity;
            set
            {
                if ( _bookQuantity != value)
                {
                    _bookQuantity = value;
                    OnPropertyChanged(nameof(BookQuantity));
                }
            }
        }

        public string BookDeweyDecimalNumber
        {
            get => _bookDeweyDecimalNumber;
            set
            {
                if (_bookDeweyDecimalNumber != value)
                {
                    _bookDeweyDecimalNumber = value;
                    OnPropertyChanged(nameof(BookDeweyDecimalNumber));
                }
            }
        }
        ///

        public void ViewDetails(int bookId)
        {
            Book book = this._allBooks.FirstOrDefault(b => b.ID == bookId);

            PopupImage = book.Image;
            BookTitle = book.Title;
            BookDescription = book.Description;
            BookAuthor = book.Author;
            BookISBN = book.ISBN;
            BookGenre = ((Genre)int.Parse(book.Genre)).ToString();
            BookQuantity = book.Quantity;
            BookDeweyDecimalNumber = book.DeweyDecimalNumber;
            IsPopupOpen = true;
        }

        public void ClosePopup()
        {
            IsPopupOpen = false;
        }

        public async void AddToCart(int bookId)
        {
            if (MainWindow.CurrentUser == null || MainWindow.UserToken == null)
            {
                MessageBox.Show("You need to be logged in to do that!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                Cart cart = await this._cartService.AddToCartAsync(MainWindow.CurrentUser.ID, bookId);
                MessageBox.Show("Book added to cart successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            } catch (Exception ex)
            {
                MessageBox.Show($"Error adding book to cart: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
