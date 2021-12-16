using System;
using System.Windows;
using System.Windows.Media;
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
            if (txtUsername.Text == "Yoni" && txtPassword.Password == "123456")
            {
                Login.Visibility = Visibility.Hidden;
                droneListButton.Visibility = Visibility.Visible;
                ParcelListButton.Visibility = Visibility.Visible;

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

    }
}
