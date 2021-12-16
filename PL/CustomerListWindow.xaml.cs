using BO;
using System.Windows;
using System.Windows.Controls;

namespace PL
{
    /// <summary>
    /// Interaction logic for CustomerListWindow.xaml
    /// </summary>
    public partial class CustomerListWindow : Window
    {
        BlApi.IBL bl;
        public CustomerListWindow( BlApi.IBL bL1)
        {
            bl = bL1;

            InitializeComponent();
            CustomersListView.ItemsSource = bl.GetListOfCustomers();
        }

        private void CustomersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (CustomersListView.ItemsSource != null)
                new CustomerWindow(bl, CustomersListView.SelectedItem as CustomerToList).ShowDialog();

            CustomersListView.ItemsSource = null;
            CustomersListView.ItemsSource = bl.GetListOfCustomers();

        }

        private void AddingNewCustomer(object sender, RoutedEventArgs e)
        {
            new CustomerWindow(bl).Show();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void ClearFilter(object sender, RoutedEventArgs e)
        {
            CustomersListView.ItemsSource = bl.GetListOfCustomers();
        }
    }
}
