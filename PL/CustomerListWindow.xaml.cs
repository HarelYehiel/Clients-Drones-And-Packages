using BO;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;

namespace PL
{
    /// <summary>
    /// Interaction logic for CustomerListWindow.xaml
    /// </summary>
    public partial class CustomerListWindow : Window
    {
        BlApi.IBL bl;
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

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
        private void CancelButtonX(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
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
