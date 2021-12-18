using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;

namespace PL
{
    /// <summary>
    /// Interaction logic for StationWindow.xaml
    /// </summary>
    public partial class StationWindow : Window
    {

        BlApi.IBL bl;

        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        public StationWindow()
        {
            InitializeComponent();
        }
        private void CancelButtonX(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }
        void hideAllRemarks()
        {
            FunctionTextBlock.Visibility = Visibility.Hidden;
            IDTextBlock.Visibility = Visibility.Hidden;
            NameTextBlock.Visibility = Visibility.Hidden;
            ChargeSlotsTextBlock.Visibility = Visibility.Hidden;
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
        private void AddDrone(object sender, RoutedEventArgs e)
        {
            hideAllRemarks();

            

            if (!IsInt(IDTextBox.Text))
            {
                IDTextBlock.Text = "Type the ID with only numbers.";
                IDTextBlock.Visibility = Visibility.Visible;
            }
            else if (!IsInt(ChargeSlotsTextBox.Text))
            {
                ChargeSlotsTextBlock.Text = "Type the charge slots with only numbers.";
                ChargeSlotsTextBlock.Visibility = Visibility.Visible;
            }
            else if (!IsDouble(LatitudeTextBox.Text))
            {
                LatitudeTextBlock.Text = "Type only numbers and one point.";
                LatitudeTextBlock.Visibility = Visibility.Visible;
            }
            else if (!IsDouble(LongitudeTextBox.Text))
            {
                LongitudeTextBlock.Text = "Type only numbers and one point.";
                LongitudeTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                // If all input is proper add the station,
                // else ERROR with TextBlocks.
                bool isAllProper = true;

                int IdStation = Convert.ToInt32(IDTextBox.Text);
                int ChargeSlots = Convert.ToInt32(ChargeSlotsTextBlock.Text);
                double Latitude = Convert.ToDouble(LatitudeTextBox.Text);
                double Longitude = Convert.ToDouble(LongitudeTextBox.Text);
                string Name = NameTextBox.Text;
  
                if (existThisIdStation(IdStation))
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
                    bl.AddingBaseStation(IdStation, Name, Latitude, Longitude, ChargeSlots);
                    MessageBox.Show("The customer added", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
        bool existThisIdStation(int id)
        {
            try
            {
                if (bl.getBaseStation(id).uniqueID == id) return true; // Exist staion with this id.
                return false;
            }
            catch (Exception)
            {
                return false;// Not exist station with this id.
            }
        }
        private void Okay(object sender, RoutedEventArgs e)
        {

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

        private void FunctionConbo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {

        }
    }
}
