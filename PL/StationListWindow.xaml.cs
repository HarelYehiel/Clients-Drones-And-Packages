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
    /// Interaction logic for StationListWindow.xaml
    /// </summary>
    public partial class StationListWindow : Window
    {
        BlApi.IBL bl;
        List<StationToTheList> stationsToTheLists;

        // When true allows the 'filters' function to be activated, otherwise there is no access.
        //We usually use this when initializing or resetting the TextBox.
        bool TurnOnFunctionFilters = false;

        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        public StationListWindow(BlApi.IBL bl1)
        {
            bl = bl1;

            stationsToTheLists = new List<StationToTheList>();
            stationsToTheLists.AddRange(bl.GetListOfBaseStations());

            TurnOnFunctionFilters = false;
            InitializeComponent();
            TurnOnFunctionFilters = true;

            // Filll the list view.
            StationListView.ItemsSource = stationsToTheLists;
        }
        private void CancelButtonX(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }

        private void ClearFilter(object sender, RoutedEventArgs e)
        {
            stationsToTheLists.Clear();
            stationsToTheLists.AddRange(bl.GetListOfBaseStations());
            StationListView.ItemsSource = stationsToTheLists;

            HideAndReseteAllTextBox();
        }
        private void HideAndReseteAllTextBox()
        {
            TurnOnFunctionFilters = false;
            FilterIDTextBox.Text = "Search";
            FilterIDTextBox.Visibility = Visibility.Hidden;

            FilterNameTextBox.Text = "Search";
            FilterNameTextBox.Visibility = Visibility.Hidden;

            FilterAvailableChargingTextBox.Text = "Search";
            FilterAvailableChargingTextBox.Visibility = Visibility.Hidden;

            FilterUnavailableChargingTextBox.Text = "Search";
            FilterUnavailableChargingTextBox.Visibility = Visibility.Hidden;
            TurnOnFunctionFilters = true;
        }
        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void AddingNewStation(object sender, RoutedEventArgs e)
        {

        }

        private void StationsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        bool isNumber(string s)
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
        private void Filters()
         // Search by all filter togther.
        {

            try
            {
                StationListView.ItemsSource = null;
                stationsToTheLists.Clear();
                stationsToTheLists.AddRange(bl.GetListOfBaseStations());

                if (isNumber(FilterIDTextBox.Text)) // Filter ID
                {
                    string id = FilterIDTextBox.Text;
                    stationsToTheLists = stationsToTheLists.FindAll
                        (s => s.uniqueID.ToString().Contains(id));
                }
                if (FilterNameTextBox.Text != "Search" &&
                    FilterNameTextBox.Text != "") // Filter name
                {
                    string name = FilterNameTextBox.Text;
                    stationsToTheLists = stationsToTheLists.FindAll(s => s.name.Contains(name));
                }

                if (isNumber(FilterAvailableChargingTextBox.Text))
                // Filter availableCharging
                {
                    string AvailableCharging = FilterAvailableChargingTextBox.Text;
                    stationsToTheLists = stationsToTheLists.FindAll
                        (s => s.availableChargingStations.ToString().Contains(AvailableCharging));
                }
                if (isNumber(FilterUnavailableChargingTextBox.Text))
                    // Filter unavailableCharging
                {
                    string UnavailableCharging = FilterUnavailableChargingTextBox.Text;
                    stationsToTheLists = stationsToTheLists.FindAll
                        (s => s.unAvailableChargingStations.ToString().Contains(UnavailableCharging));
                }

                StationListView.ItemsSource = stationsToTheLists;
            }
            catch (Exception)
            {

            }

        }
        private void SearchIDButton_Click(object sender, RoutedEventArgs e)
        {
            if (FilterIDTextBox.Visibility == Visibility.Hidden)
                FilterIDTextBox.Visibility = Visibility.Visible;
            else
            {
                FilterIDTextBox.Text = "Search";
                FilterIDTextBox.Visibility = Visibility.Hidden;
            }
        }
        public delegate bool Predicate<station>(station station);
        public bool MyFunc1(station station) { return station.availableChargingStations > 0; }

        private void FilterIDTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TurnOnFunctionFilters)
                Filters();

        }

        private void SearchNameButton_Click(object sender, RoutedEventArgs e)
        {
            if (FilterNameTextBox.Visibility == Visibility.Hidden)
                FilterNameTextBox.Visibility = Visibility.Visible;
            else
            {
                FilterNameTextBox.Text = "Search";
                FilterNameTextBox.Visibility = Visibility.Hidden;
            }
        }
        private void SearchavailableChargingButton_Click(object sender, RoutedEventArgs e)
        {
            if (FilterAvailableChargingTextBox.Visibility == Visibility.Hidden)
                FilterAvailableChargingTextBox.Visibility = Visibility.Visible;
            else
            {
                FilterAvailableChargingTextBox.Text = "Search";
                FilterAvailableChargingTextBox.Visibility = Visibility.Hidden;
            }
        }

        private void SearchUnavailableChargingButton_Click(object sender, RoutedEventArgs e)
        {
            if (FilterUnavailableChargingTextBox.Visibility == Visibility.Hidden)
                FilterUnavailableChargingTextBox.Visibility = Visibility.Visible;
            else
            {
                FilterUnavailableChargingTextBox.Text = "Search";
                FilterUnavailableChargingTextBox.Visibility = Visibility.Hidden;
            }
        }
    }
}
