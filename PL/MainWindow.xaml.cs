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
using IBL.BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBL.IBL bl;
        public MainWindow()
        {
            bl = new IBL.BL();
            ImageBrush image = new ImageBrush();
            image.ImageSource = new BitmapImage(new Uri("C:\\Users\\Thee\\source\\repos\\Yonithee\\CourseMiniProject1\\PL\\MainBackground.jpeg"));
            InitializeComponent();
        }

        private void ClickToShowDroneList(object sender, RoutedEventArgs e)
        {
            new DroneListWindow(ref bl).Show();
        }

        
    }
}
