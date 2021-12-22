using System;
using System.Windows;
using System.Windows.Media;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Imaging;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BlApi.IBL bl;
        public MainWindow()
        {
            bl = BlApi.BlFactory.GetBl();
            ImageBrush image = new ImageBrush();
            image.ImageSource = new BitmapImage(new Uri("MainBackground.jpeg", UriKind.Relative));
            InitializeComponent();
            CustomerListButton.Visibility = Visibility.Hidden;
            StationListButton.Visibility = Visibility.Hidden;
            droneListButton.Visibility = Visibility.Hidden;
            ParcelListButton.Visibility = Visibility.Hidden;
            Login.Visibility = Visibility.Visible;

        }

        private void ClickToShowDroneList(object sender, RoutedEventArgs e)
        {
            new DroneListWindow(bl).Show();
        }
        private void ClickToShowCustomerList(object sender, RoutedEventArgs e)
        {
            new CustomerListWindow(bl).Show();
        }

        private void enter_Click(object sender, RoutedEventArgs e)
        {
            if (TitleBox.Content is "Manager - Login")
            {
                if ((txtUsername.Text == "Yoni" || txtUsername.Text == "Harel") && txtPassword.Password == "123456")
                {
                    Login.Visibility = Visibility.Hidden;
                    droneListButton.Visibility = Visibility.Visible;
                    ParcelListButton.Visibility = Visibility.Visible;
                    CustomerListButton.Visibility = Visibility.Visible;
                    StationListButton.Visibility = Visibility.Visible;
                    txtPassword.Password = "";
                    txtUsername.Text = "";

                }
            }
            if (TitleBox.Content is "Login")
            {
                try
                {
                    int checkID = Convert.ToInt32(txtPassword.Password);
                    BO.Customer existsCustomer = bl.GetCustomer(checkID);
                    if (existsCustomer.name == txtUsername.Text && existsCustomer.uniqueID == checkID)
                    {
                        List<BO.CustomerToList> lst = bl.GetAllCustomersBy(D => D.name == existsCustomer.name).ToList();
                        new CustomerWindow(bl, lst[0]).Show();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Username or ID is unlegal!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    txtPassword.Password = "";
                    txtUsername.Text = "";
                }
            }
            if (TitleBox.Content is "registration")
            {
                new CustomerWindow(bl).Show();
            }

        }

        private void ClickToShowParcelList(object sender, RoutedEventArgs e)
        {
            new ParclListWindow(bl).Show();
        }
        private void ClickToShowStatoinList(object sender, RoutedEventArgs e)
        {
            new StationListWindow(bl).Show();

        }
        private void situationLogin_Click(object sender, RoutedEventArgs e)
        {
            if (TitleBox.Content is "Manager - Login")
            {
                // swope to Login of register custoner
                TitleBox.Content = "Login";
                username.Content = "Enter your name";
                password.Content = "Enter your ID";
                changeHierarchy.Content = "New client? Sign up!";
            }
            else if (TitleBox.Content is "Login")
            {
                // swope to registration screen(Add new customer button)
                TitleBox.Content = "registration";
                username.Visibility = Visibility.Hidden;
                txtUsername.Visibility = Visibility.Hidden;
                password.Visibility = Visibility.Hidden;
                txtPassword.Visibility = Visibility.Hidden;
                enter.Content = "Enter for start!";
                // = "Enter phone number"
                // = "Enter Latitude"
                // = "Enter Longitude"
                changeHierarchy.Content = "Return To Manage";

            }
            else if (TitleBox.Content is "registration")
            {
                username.Visibility = Visibility.Visible;
                txtUsername.Visibility = Visibility.Visible;
                password.Visibility = Visibility.Visible;
                txtPassword.Visibility = Visibility.Visible;
                // return to the manager screen
                TitleBox.Content = "Manager - Login";
                username.Content = "Username";
                password.Content = "Password";
                enter.Content = "Enter";
                changeHierarchy.Content = "Not Manager?";

            }

        }

        private void signOut_Click(object sender, RoutedEventArgs e)
        {
            Login.Visibility = Visibility.Visible;
            droneListButton.Visibility = Visibility.Hidden;
            ParcelListButton.Visibility = Visibility.Hidden;
            CustomerListButton.Visibility = Visibility.Hidden;
            StationListButton.Visibility = Visibility.Hidden;
            txtPassword.Password = "";
            txtUsername.Text = "";
        }
    }
}
