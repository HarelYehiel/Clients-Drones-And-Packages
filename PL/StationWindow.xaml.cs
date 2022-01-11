using BO;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Runtime.CompilerServices;

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
        public StationWindow(BlApi.IBL bl1)
        // Add station
        {
            bl = bl1;

            InitializeComponent();


        }
        public StationWindow(BlApi.IBL bl1, StationToTheList stationToTheList)
        // View station
        {
            bl = bl1;

            InitializeComponent();
            ChangeNamesAndTitlesAccordingToStationPresentation(stationToTheList);
        }

        void ChangeNamesAndTitlesAccordingToStationPresentation(StationToTheList stationToTheList)
        // Function to change names and titles according to the station display.
        {
            title.Content = "Station";
            title.IsEnabled = false;

            IDTextBox.Text = stationToTheList.uniqueID.ToString();
            IDTextBox.IsEnabled = false;

            NameTextBox.Text = stationToTheList.name;

            ChargeSlotsLabel.Content = "Available Charging Stations";
            ChargeSlotsLabel.IsEnabled = false;
            ChargeSlotsTextBox.Text = stationToTheList.availableChargingStations.ToString();

            LatitudeLabel.Content = "Unavailable Charging Stations";
            LatitudeLabel.IsEnabled = false;
            LatitudeTextBox.Text = stationToTheList.unAvailableChargingStations.ToString();
            LatitudeTextBox.IsEnabled = false;

            StationButton.Content = "Update";

            LongitudeLabel.Visibility = Visibility.Collapsed;
            LongitudeTextBox.Visibility = Visibility.Collapsed;
        }
        void addStation()
        {
            try
            {
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
                    int ChargeSlots = Convert.ToInt32(ChargeSlotsTextBox.Text);
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
                        lock (bl) { bl.AddingBaseStation(IdStation, Name, Latitude, Longitude, ChargeSlots); }
                        MessageBox.Show("The station added", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("The station not added", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }
        void update()
        {
            try
            {
                if (!IsInt(ChargeSlotsTextBox.Text))
                    ChargeSlotsTextBlock.Visibility = Visibility.Visible;
                else
                {
                    lock (bl) { bl.UpdateStationData(Convert.ToInt32(IDTextBox.Text), NameTextBox.Text, Convert.ToInt32(ChargeSlotsTextBox.Text)); }
                    MessageBox.Show("The station update.", "Information", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                    StationToTheList stationToTheList = new StationToTheList();

                    // Update the data of view station.
                    lock (bl) { stationToTheList = bl.GetStationToTheList(Convert.ToInt32(IDTextBox.Text)); }
                    NameTextBox.Text = stationToTheList.name;
                    ChargeSlotsTextBox.Text = stationToTheList.availableChargingStations.ToString();

                }


            }
            catch (Exception)
            {
                MessageBox.Show("The station not update.", "Information", MessageBoxButton.OKCancel, MessageBoxImage.Information);
            }

        }

        private void CancelButtonX(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }
        void hideAllRemarks()
        {
            IDTextBlock.Visibility = Visibility.Collapsed;
            NameTextBlock.Visibility = Visibility.Collapsed;
            ChargeSlotsTextBlock.Visibility = Visibility.Collapsed;
            LatitudeTextBlock.Visibility = Visibility.Collapsed;
            LongitudeTextBlock.Visibility = Visibility.Collapsed;
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
        private void addStation_Button(object sender, RoutedEventArgs e)
        {
            hideAllRemarks();

            if ((string)StationButton.Content == "Add Station")
                addStation();
            else if ((string)StationButton.Content == "Update")
            {
                update();
            }
        }
        bool existThisIdStation(int id)
        {
            try
            {
                lock (bl)
                {
                    if (bl.getBaseStation(id).uniqueID == id) return true; // Exist staion with this id.
                    return false;
                }
            }
            catch (Exception)
            {
                return false;// Not exist station with this id.
            }
        }
        private void RemoveAllSkimmersFromTheStation(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult d = MessageBox.Show("You sure you want to get all the skimmers out of charge at this station?", "Question", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Yes);

                if (d == MessageBoxResult.None || d == MessageBoxResult.Cancel)
                    return;

                if (IsInt(IDTextBox.Text))
                    lock (bl) { bl.RemoveAllSkimmersFromTheStation(Convert.ToInt32(IDTextBox.Text)); }

                MessageBox.Show("All the skimmers out of charge at this station", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                BO.StationToTheList stationToTheList;
                lock (bl) { stationToTheList = bl.GetStationToTheList(Convert.ToInt32(IDTextBox.Text)); }
                ChargeSlotsTextBox.Text = stationToTheList.availableChargingStations.ToString();
                LatitudeTextBox.Text = stationToTheList.unAvailableChargingStations.ToString();

            }
            catch (Exception)
            {
                MessageBox.Show("Can't out all the skimmers of charge at this station or don't have skimmers of charge at this station", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void ViewAllSkimmersFromTheCharge_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DroneInChargingWindow droneInChaeginngWindow = new DroneInChargingWindow(bl, Convert.ToInt32(IDTextBox.Text));
                droneInChaeginngWindow.Title = $"Drone In Charging At Station {IDTextBox}";
                droneInChaeginngWindow.Show();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Don't have drones in charging"))
                    MessageBox.Show("Don't have drones in charging at this station.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

            }

        }
    }
}
