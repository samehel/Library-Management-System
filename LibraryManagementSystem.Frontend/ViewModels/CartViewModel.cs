﻿using LibraryManagementSystem.Frontend.Models;
using LibraryManagementSystem.Frontend.Services;
using LibraryManagementSystem.Frontend.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Frontend.ViewModels
{
    public class CartViewModel : INotifyPropertyChanged
    {
        private readonly CartService _cartService;
        public ObservableCollection<CartBook> _cartBooks { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CartViewModel()
        {
            _cartService = new CartService();
            CartBooks = new ObservableCollection<CartBook>();
        }

        public ObservableCollection<CartBook> CartBooks
        {
            get => _cartBooks;
            set
            {
                _cartBooks = value;
                OnPropertyChanged(nameof(CartBooks));
            }
        }

        public async Task LoadCart()
        {
            if (MainWindow.CurrentUser != null)
            {
                var cart = await this._cartService.GetCartAsync(MainWindow.CurrentUser.ID);
                CartBooks.Clear();
                foreach (var cartBook in cart.CartBooks)
                {
                    CartBooks.Add(cartBook);
                }
            }
        }

        public async void RemoveFromCart(int bookId)
        {
            if (MainWindow.CurrentUser != null)
            {
                var cart = await this._cartService.RemoveFromCartAsync(MainWindow.CurrentUser.ID, bookId);
                await LoadCart();  
            }
        }

        public async void ClearCart()
        {
            if (MainWindow.CurrentUser != null)
            {
                var cart = await this._cartService.ClearCartAsync(MainWindow.CurrentUser.ID);
                await LoadCart();
            }
        }

        public async void IncreaseQuantity(int bookId)
        {
            var cartBook = CartBooks.FirstOrDefault(cb => cb.BookID == bookId);
            if (cartBook != null && cartBook.Quantity < cartBook.Book.Quantity)
            {
                cartBook.Quantity++;
                await _cartService.UpdateCartBookQuantityAsync(MainWindow.CurrentUser.ID, bookId, cartBook.Quantity);
                await LoadCart();
            }
        }

        public async void DecreaseQuantity(int bookId)
        {
            var cartBook = CartBooks.FirstOrDefault(cb => cb.BookID == bookId);
            if (cartBook != null)
            {
                if (cartBook.Quantity > 1)
                {
                    cartBook.Quantity--;
                    await _cartService.UpdateCartBookQuantityAsync(MainWindow.CurrentUser.ID, bookId, cartBook.Quantity);
                    await LoadCart();
                }
                else
                {
                    RemoveFromCart(bookId);
                }
            }
        }
    }
}
