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
            //*****************************************להבין מה הסינון הנדרש בשביל זה כמו בכל השלבים של factory nams
            bl = BlApi.BlFactory.GetBl();
            ImageBrush image = new ImageBrush();
            image.ImageSource = new BitmapImage(new Uri("MainBackground.jpeg", UriKind.Relative));
            InitializeComponent();
        }

        private void ClickToShowDroneList(object sender, RoutedEventArgs e)
        {
            new DroneListWindow(bl).Show();
        }
        private void ClickToShowCustomerList(object sender, RoutedEventArgs e)
        {
            new CustomerListWindow(bl).Show();
        }

        private void ClickToShowStatoinList(object sender, RoutedEventArgs e)
        {
            new StationListWindow(bl).Show();

        }

    }
}
