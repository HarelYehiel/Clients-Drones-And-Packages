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
using BO;

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
            new DroneListWindow(ref bl).Show();
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
    }
}
