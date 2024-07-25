using LibraryManagementSystem.Frontend.Models;
using LibraryManagementSystem.Frontend.Services;
using LibraryManagementSystem.Frontend.Views;
using System;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Frontend.ViewModels
{
    public class AccountViewModel : INotifyPropertyChanged
    {
        private readonly UserService _userService;
        private readonly TokenService _tokenService;
        private User _currentUser;
        private Token _userToken;
        private bool _isLoggedIn;

        public AccountViewModel()
        {
            _userService = new UserService();
            _tokenService = new TokenService();
            CheckLoginStatus();
        }

        public User CurrentUser
        {
            get => _currentUser;
            private set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
                OnPropertyChanged(nameof(IsLoggedIn));
                OnPropertyChanged(nameof(RegistrationVisibility));
                OnPropertyChanged(nameof(LoginVisibility));
                OnPropertyChanged(nameof(UpdateVisibility));
                OnPropertyChanged(nameof(SeperatorVisibility));
            }
        }

        public Token UserToken
        {
            get => _userToken;
            private set
            {
                _userToken = value;
                OnPropertyChanged(nameof(UserToken));
                OnPropertyChanged(nameof(IsLoggedIn));
                OnPropertyChanged(nameof(RegistrationVisibility));
                OnPropertyChanged(nameof(LoginVisibility));
                OnPropertyChanged(nameof(UpdateVisibility));
                OnPropertyChanged(nameof(SeperatorVisibility));
            }
        }

        public bool IsLoggedIn
        {
            get => _isLoggedIn;
            private set
            {
                _isLoggedIn = value;
                OnPropertyChanged(nameof(IsLoggedIn));
                OnPropertyChanged(nameof(RegistrationVisibility));
                OnPropertyChanged(nameof(LoginVisibility));
                OnPropertyChanged(nameof(UpdateVisibility));
                OnPropertyChanged(nameof(SeperatorVisibility));
            }
        }

        [Required(ErrorMessage = "Username is required.")]
        public string LoginUsername { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string LoginPassword { get; set; }
        public string RegisterFullname { get; set; }
        public string RegisterEmail { get; set; }
        public string RegisterUsername { get; set; }
        public string RegisterPassword { get; set; }

        public Visibility RegistrationVisibility => IsLoggedIn ? Visibility.Collapsed : Visibility.Visible;
        public Visibility LoginVisibility => IsLoggedIn ? Visibility.Collapsed : Visibility.Visible;
        public Visibility SeperatorVisibility => IsLoggedIn ? Visibility.Collapsed : Visibility.Visible;
        public Visibility UpdateVisibility => IsLoggedIn ? Visibility.Visible : Visibility.Collapsed;

        public async Task LoginAsync()
        {
            UserToken = await _tokenService.AuthenticateUserAsync(LoginUsername, LoginPassword);

            if (UserToken != null)
            {
                CurrentUser = _tokenService.DecodeToken(UserToken.TokenValue);
                MainWindow.SetCurrentUser(CurrentUser);
                MainWindow.SetUserToken(UserToken);
                IsLoggedIn = true;
                MessageBox.Show("Login successful.");
            }
            else
            {
                MessageBox.Show("Login failed. Please check your username and password.");
            }
        }

        public void Logout()
        {
            UserToken = null;
            CurrentUser = null;
            IsLoggedIn = false;
            MainWindow.SetCurrentUser(null);
            MainWindow.SetUserToken(null);
            MessageBox.Show("Logout successful.");
        }

        public async Task RegisterAsync()
        {
            var newUser = new User
            {
                Fullname = RegisterFullname,
                Email = RegisterEmail,
                Role = "Member",
                Username = RegisterUsername,
                Password = RegisterPassword
            };

            try
            {
                var createdUser = await _userService.CreateUserAsync(newUser);
                MessageBox.Show("Registration successful.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Registration failed: {ex.Message}");
            }
        }

        public async Task UpdateUserInfoAsync()
        {
            if (CurrentUser == null)
            {
                MessageBox.Show("User is not logged in.");
                return;
            }

            var updatedUser = new User
            {
                ID = CurrentUser.ID,
                Fullname = CurrentUser.Fullname,
                Email = CurrentUser.Email,
                Username = CurrentUser.Username,
                Password = CurrentUser.Password
            };

            try
            {
                User newUserData = await this._userService.UpdateUserAsync(CurrentUser.ID, updatedUser, UserToken.TokenValue);

                if (newUserData != null)
                    MessageBox.Show("User information successfully updated.");
                else
                    MessageBox.Show("You either don\'t exist or you do not have permissions (you should not be seeing this)");
            } catch (Exception ex) {
                MessageBox.Show($"User information failed to update: {ex}");
            }
        }


        private void CheckLoginStatus()
        {
            // Use application-wide user object to determine login status
            CurrentUser = MainWindow.CurrentUser;
            UserToken = MainWindow.UserToken;
            IsLoggedIn = CurrentUser != null;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
