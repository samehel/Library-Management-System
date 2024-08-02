using LibraryManagementSystem.Frontend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibraryManagementSystem.Frontend.Views
{
    public partial class CheckoutView : UserControl
    {
        private readonly bool status;
        public CheckoutView(bool status)
        {
            InitializeComponent();
            this.status = status;
            this.Loaded += CheckoutView_Loaded;
        }

        private void GoHome_Click(object sender, RoutedEventArgs e)
        {
            HomeView homeView = new HomeView();
            this.Content = homeView;
        }

        private void CheckoutView_Loaded(object sender, RoutedEventArgs e)
        {
            if (status == false)
            {
                BorrowRequestSuccess.Visibility = Visibility.Collapsed;
                BorrowRequestFailure.Visibility = Visibility.Visible;
            }
            else
            {
                BorrowRequestSuccess.Visibility = Visibility.Visible;
                BorrowRequestFailure.Visibility = Visibility.Collapsed;
            }
        }
    }
}
