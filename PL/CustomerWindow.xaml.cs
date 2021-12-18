using BO;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;

namespace PL
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        BlApi.IBL bl;
        CustomerToList customerToList;
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
            DronesListView.Visibility = Visibility.Hidden;


        }
        public CustomerWindow(BlApi.IBL bl1, CustomerToList customerToList1)
        // Constructor for view customer and allow do actions.
        {
            bl = bl1;
            customerToList = customerToList1;
            InitializeComponent();

            // View the details of customer.
            List<CustomerToList> dronesToLists = new List<CustomerToList>();
            dronesToLists.Add(customerToList);
            DronesListView.ItemsSource = dronesToLists;

            // Just for functions 'My shipments' and 'My shipments'.
            ParcelsListView.Visibility = Visibility.Hidden;

            // Hide all tools of add castomer.
            IDTextBox.Visibility = Visibility.Hidden;
            IDLabel.Visibility = Visibility.Hidden;
            NameTextBox.Visibility = Visibility.Hidden;
            PhoneTextBox.Visibility = Visibility.Hidden;
            NameLabel.Visibility = Visibility.Hidden;
            PhoneLabel.Visibility = Visibility.Hidden;
            LatitudeLabel.Visibility = Visibility.Hidden;
            LongitudeLabel.Visibility = Visibility.Hidden;
            LatitudeTextBox.Visibility = Visibility.Hidden;
            LongitudeTextBox.Visibility = Visibility.Hidden;
            AddDroneButton.Visibility = Visibility.Hidden;

        }
        private void CancelButtonX(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }
        private void FunctionConbo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            if (FunctionConbo.SelectedIndex == 0) // update customer
            {
                NameTextBox.Visibility = Visibility.Visible;
                PhoneTextBox.Visibility = Visibility.Visible;
                NameLabel.Visibility = Visibility.Visible;
                PhoneLabel.Visibility = Visibility.Visible;
                ParcelsListView.Visibility = Visibility.Hidden;

            }
            else if (FunctionConbo.SelectedIndex == 4 || FunctionConbo.SelectedIndex == 3)
                ParcelsListView.Visibility = Visibility.Visible;

            else
            {
                ParcelsListView.Visibility = Visibility.Hidden;
                NameTextBox.Visibility = Visibility.Hidden;
                PhoneTextBox.Visibility = Visibility.Hidden;
                NameLabel.Visibility = Visibility.Hidden;
                PhoneLabel.Visibility = Visibility.Hidden;
            }
        }

        private void AddDrone(object sender, RoutedEventArgs e)
        {
            hideAllRemarks();

            if (!IsInt(IDTextBox.Text))
            {
                IDTextBlock.Text = "Type the ID with only numbers.";
                IDTextBlock.Visibility = Visibility.Visible;
            }
            else if(!IsInt(PhoneTextBox.Text))
            {
                PhoneTextBlock.Text = "Type the phone with only numbers.";
                PhoneTextBlock.Visibility = Visibility.Visible;
            }
            else if(!IsDouble(LatitudeTextBox.Text))
            {
                LatitudeTextBlock.Text = "Type only numbers and one point.";
                LatitudeTextBlock.Visibility = Visibility.Visible;
            }
            else if(!IsDouble(LongitudeTextBox.Text))
            {
                LongitudeTextBlock.Text = "Type only numbers and one point.";
                LongitudeTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                // If all input is proper add the customer,
                // else ERROR with TextBlocks.
                bool isAllProper = true;

                int IdCustome = Convert.ToInt32(IDTextBox.Text);
                string Phone = PhoneTextBox.Text;
                double Latitude = Convert.ToDouble(LatitudeTextBox.Text);
                double Longitude = Convert.ToDouble(LongitudeTextBox.Text);
                string Name = NameTextBox.Text;

                if (existThisIdCustomer(IdCustome))
                {
                    IDTextBlock.Text = "This ID customer exists, select another.";
                    IDTextBlock.Visibility = Visibility.Visible;
                    isAllProper = false;
                }
                if (Name.Length == 0 || Name == "Type customer's name")// check Name
                {
                    NameTextBlock.Visibility = Visibility.Visible;
                    isAllProper = false;
                }

                // If all proper add th customer.
                if (isAllProper)
                {
                    bl.AbsorptionNewCustomer(IdCustome, Name, Phone, Latitude, Longitude);

                    MessageBox.Show("The customer added", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void FunctionConbo_Initialized(object sender, EventArgs e)
        {
            List<string> options = new List<string>() {
                "Update customer"
                , "Creat new order"
                , "Creat new delivery"
                , "My order"
                , "My shipments"
                };
            FunctionConbo.ItemsSource = options;
        }
        private void Okay(object sender, RoutedEventArgs e)
        {
            hideAllRemarks();

            if (FunctionConbo.SelectedIndex == -1)
            {

                FunctionTextBlock.Visibility = Visibility.Visible;
                return;

            }
            else if (FunctionConbo.SelectedIndex == 0) //Update customer
            {
                try
                {
                    // Not typed new name and a new phone.
                    if (!CheckThePhoneAndName())
                    {
                        NameTextBlock.Visibility = Visibility.Visible;
                        PhoneTextBlock.Visibility = Visibility.Visible;
                    }
                    else if (CheckIfWantNameButNotPhone()) //Update only name
                    {
                        bl.UpdateCustomerData(customerToList.uniqueID, NameTextBox.Text, "");
                        customerToList.name = NameTextBox.Text;
                    }
                    else if (CheckIfWantPhoneButNotName()) // Update only phone
                    {
                        bl.UpdateCustomerData(customerToList.uniqueID, "", PhoneTextBox.Text);
                        customerToList.phone = PhoneTextBox.Text;
                    }
                    else // Update name and phone
                    {
                        bl.UpdateCustomerData(customerToList.uniqueID, NameTextBox.Text, PhoneTextBox.Text);
                        customerToList.name = NameTextBox.Text;
                        customerToList.phone = PhoneTextBox.Text;
                    }

                    // Update the list in this window.
                    List<CustomerToList> dronesToLists = new List<CustomerToList>();
                    dronesToLists.Add(customerToList);
                    DronesListView.ItemsSource = dronesToLists;

                    MessageBox.Show("Update the details", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("Can't Update the details", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else if (FunctionConbo.SelectedIndex == 1)
            {
                // יפנה לחלון 
            }
            else if (FunctionConbo.SelectedIndex == 2)
            {
                // יפנה לחלון 
            }
            else if (FunctionConbo.SelectedIndex == 3) // My order
                ParcelsListView.ItemsSource = bl.GetAllParcelsBy(c => c.TargetId == customerToList.uniqueID);
            else if (FunctionConbo.SelectedIndex == 4) // My shipments            
                ParcelsListView.ItemsSource = bl.GetAllParcelsBy(c => c.SenderId == customerToList.uniqueID);

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
                else if(HaveOnePointInTheNumber &&(int)s[i] == (int)'.')
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
            if (PhoneTextBox.Text != "Type customer's phone" && !IsInt(PhoneTextBox.Text))
            {
                PhoneTextBlock.Text = "Type only numbers";
                return false;
            }

            if ((NameTextBox.Text.Length == 0 && PhoneTextBox.Text.Length == 0)
                    || (NameTextBox.Text == "Type customer's name" && PhoneTextBox.Text == "Type customer's phone")
                    || (NameTextBox.Text.Length == 0 && PhoneTextBox.Text == "Type customer's phone")
                    || (PhoneTextBox.Text.Length == 0 && NameTextBox.Text == "Type customer's name"))
                return false;

            return true;
        }
        private bool CheckIfWantNameButNotPhone()
        {
            if ((NameTextBox.Text.Length != 0 && NameTextBox.Text != "Type customer's name")
               && (PhoneTextBox.Text.Length == 0 || PhoneTextBox.Text == "Type customer's phone"))
                return true;

            return false;
        }
        private bool CheckIfWantPhoneButNotName()
        {
            if ((NameTextBox.Text.Length == 0 || NameTextBox.Text == "Type customer's name")
               && (PhoneTextBox.Text.Length != 0 && PhoneTextBox.Text != "Type customer's phone"))
                return true;

            return false;
        }
    }
}
