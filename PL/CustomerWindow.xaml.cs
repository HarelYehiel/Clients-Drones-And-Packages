using BO;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Runtime.CompilerServices;




namespace PL
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        BlApi.IBL bl;
        CustomerToList customerToList;
        BackgroundWorker worker;

        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        public CustomerWindow(BlApi.IBL bl1)
        // Constructor for adding customer.
        {
            bl = bl1;
            InitializeComponent();

            // Hide all tools of view castomer.
            FunctionsCustomerGrid.Visibility = Visibility.Hidden;
            ParcelsListView.Visibility = Visibility.Hidden;
            CustomersListView.Visibility = Visibility.Hidden;


        }
        public CustomerWindow(BlApi.IBL bl1, CustomerToList customerToList1)
        // Constructor for view customer and allow do actions.
        {
            bl = bl1;
            customerToList = customerToList1;
            InitializeComponent();

            // View the details of customer.
            List<CustomerToList> custonersToLists = new List<CustomerToList>();
            custonersToLists.Add(customerToList);
            CustomersListView.ItemsSource = custonersToLists;

            txtId.Text = customerToList.uniqueID.ToString();
            txtId.IsEnabled = false;
            txtName.Text = customerToList.name;
            txtName.Background = new SolidColorBrush(Colors.White);
            txtName.Foreground = new SolidColorBrush(Colors.Red);
            txtPhone.Text = customerToList.phone;
            txtPhone.Background = new SolidColorBrush(Colors.White);
            txtPhone.Foreground = new SolidColorBrush(Colors.Red);

            // take 'Latitude' for information 'packagesHeReceived'.
            labelLati.Content = "Quantity of parcels shipped";
            Latitude.Text = customerToList.packagesHeReceived.ToString();
            Latitude.IsEnabled = false;

            //take 'Longitude' for information  'packagesOnTheWayToTheCustomer'.
            labelLongi.Content = "Quantity of parcels on the way";
            Longitude.Text = customerToList.packagesOnTheWayToTheCustomer.ToString();
            Longitude.IsEnabled = false;

            Add.Content = "Update";

            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerAsync();
        }
        void updateTheViewListDronesInRealTime()
        {
             customerToList = bl.GetCustomerToTheList(customerToList.uniqueID); 

            // take 'Latitude' for information 'packagesHeReceived'.
            Latitude.Text = customerToList.packagesHeReceived.ToString();

            //take 'Longitude' for information  'packagesOnTheWayToTheCustomer'.
            Longitude.Text = customerToList.packagesOnTheWayToTheCustomer.ToString();
        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                Action theUpdateView = updateTheViewListDronesInRealTime;
                // Dispatcher to main thread to update the window drone.
                IDTextBlock.Dispatcher.BeginInvoke(theUpdateView);
                Thread.Sleep(200);
            }
        }

        private void CancelButtonX(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }
        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        void hideAllRemarks()
        {
            FunctionTextBlock.Visibility = Visibility.Hidden;
            IDTextBlock.Visibility = Visibility.Hidden;
            NameTextBlock.Visibility = Visibility.Hidden;
            PhoneTextBlock.Visibility = Visibility.Hidden;
            PhoneTextBlock.Text = "Type the phone";
            LatitudeTextBlock.Visibility = Visibility.Hidden;
            LongitudeTextBlock.Visibility = Visibility.Hidden;
        }
        bool IsInt(string s)
        {
            if (s.Length == 0) return false;
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] >= (int)'0' && (int)s[i] <= (int)'9')
                    continue;

                return false;
            }

            return true;
        }
        bool IsDouble(string s)
        {
            bool HaveOnePointInTheNumber = true;


            if (s.Length == 0) return false;
            if (s.Length == 1 && (int)s[0] == (int)'.') return false;

            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] >= (int)'0' && (int)s[i] <= (int)'9')
                    continue;
                else if (HaveOnePointInTheNumber && (int)s[i] == (int)'.')
                {
                    HaveOnePointInTheNumber = false;
                    continue;
                }

                return false;
            }

            return true;
        }
        bool existThisIdCustomer(int id)
        {
            try
            {
                Customer customer;
                 customer = bl.GetCustomer(id); 
                if (customer.uniqueID == id) return true; // Exist drine with this id.
                return false;
            }
            catch (Exception)
            {
                return false;// Don't exist drone with this id.
            }
        }
        private bool CheckThePhoneAndName()
        {
            if (!IsInt(txtPhone.Text))
            {
                PhoneTextBlock.Text = "Type only numbers";
                PhoneTextBlock.Visibility = Visibility.Visible;
                return false;
            }
            else
                PhoneTextBlock.Visibility = Visibility.Hidden;


            if (txtName.Text.Length == 0 && txtPhone.Text.Length == 0)
                return false;

            return true;
        }

        private void create_Click(object sender, RoutedEventArgs e)
        {
            if (Add.Content is "Create")
            {
                if (!IsInt(txtId.Text))
                {
                    IDTextBlock.Text = "Type the ID with only numbers.";
                    IDTextBlock.Visibility = Visibility.Visible;
                }
                else if (!IsInt(txtPhone.Text))
                {
                    PhoneTextBlock.Text = "Type the phone with only numbers.";
                    PhoneTextBlock.Visibility = Visibility.Visible;
                }
                else if (!IsDouble(Latitude.Text))
                {
                    LatitudeTextBlock.Text = "Type only numbers and one point.";
                    LatitudeTextBlock.Visibility = Visibility.Visible;
                }
                else if (!IsDouble(Longitude.Text))
                {
                    LongitudeTextBlock.Text = "Type only numbers and one point.";
                    LongitudeTextBlock.Visibility = Visibility.Visible;
                }
                else
                {

                    // If all input is proper add the customer,
                    // else ERROR with TextBlocks.
                    int ID = Convert.ToInt32(txtId.Text);
                    Double longi = Convert.ToDouble(Longitude.Text);
                    Double lati = Convert.ToDouble(Latitude.Text);
                    bool isAllProper = true;
                    if (existThisIdCustomer(ID))
                    {
                        IDTextBlock.Text = "This ID customer exists, select another.";
                        IDTextBlock.Visibility = Visibility.Visible;
                        isAllProper = false;
                    }
                    if (txtName.Text.Length == 0)// check Name
                    {
                        NameTextBlock.Visibility = Visibility.Visible;
                        isAllProper = false;
                    }

                    // If all proper add th customer.
                    if (isAllProper)
                    {
                        lock (bl) { bl.AddingCustomer(ID, txtName.Text, txtPhone.Text, lati, longi); }
                        MessageBox.Show("The customer added", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        
                    }

                }
            }
            if(Add.Content is "Update")
            {
                int ID = Convert.ToInt32(txtId.Text);
                lock (bl){ bl.UpdateCustomerData(ID, txtName.Text, txtPhone.Text); }
                UpdateBorder.Visibility = Visibility.Visible;
            }


        }

        private void newParcelByCustomer(object sender, RoutedEventArgs e)
        {
            ParcelWindow parcelWindow = new ParcelWindow(bl);
            parcelWindow.txtSender.Text = txtId.Text;
            parcelWindow.txtSender.IsEnabled = false;
            parcelWindow.Show();
            UpdateBorder.Visibility = Visibility.Hidden;


        }

        private void AllparcelsOfThisCust(object sender, RoutedEventArgs e)
        {
            ParclListWindow parclListWindow = new ParclListWindow(bl);
            parclListWindow.txtFilter.Text = txtName.Text;
            parclListWindow.txtFilter.IsEnabled = false;
            parclListWindow.filterCombo.SelectedIndex = 1;
            parclListWindow.filterCombo.IsEnabled = false;
            parclListWindow.OpenTargetView.IsEnabled = false;
            parclListWindow.OpenDroneView.IsEnabled = false;            
            parclListWindow.Show();
            UpdateBorder.Visibility = Visibility.Hidden;
        }
    }
}
