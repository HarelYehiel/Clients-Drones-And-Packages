using BO;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Linq;

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
            clearAndResetFilter();
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

        private void StationsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StationListView.ItemsSource != null)
                new StationWindow(bl, (StationToTheList)e.AddedItems[0]).Show();

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
            EnableFiltersWithConditions();

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
        private void clearAndResetFilter()
        {
            HideAndReseteAllTextBox();

            EnableFiltersWithConditions();

        }
        void EnableFiltersWithConditions()
        {
            if (TurnOnFunctionFilters)
                Filters();
        }
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            EnableFiltersWithConditions();
        }

        private void AvailableChargingStations_Click(object sender, RoutedEventArgs e)
        {
            HideAndReseteAllTextBox();
            StationListView.ItemsSource = bl.GetAllStaionsBy(s => s.ChargeSlots > 0);
        }

        private void AddingNewStation(object sender, RoutedEventArgs e)
        {
            if (StationListView.ItemsSource != null)
                new StationWindow(bl).Show();

            EnableFiltersWithConditions();

        }
        private void ComboBox_Initialized(object sender, EventArgs e)
        {
            List<string> l = new List<string>() {
                "Choose",
                "Name",
                "Available Charging",
                "Unavailable Charging"
            };
            GroupByComboBox.ItemsSource = l;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<StationToTheList> l;

            switch (GroupByComboBox.SelectedIndex)
            {

                case 1:

                    IEnumerable<IGrouping<string, StationToTheList>> tsName = from item in bl.GetListOfBaseStations()
                                                                          group item by item.name into gs
                                                                          select gs;

                    l = new List<StationToTheList>();
                    foreach (var group1 in tsName)
                    {
                        foreach (StationToTheList item in group1)
                        {
                            l.Add(item);
                        }
                    }
                    StationListView.ItemsSource = l;
                    break;

                case 2:
                    IEnumerable<IGrouping<int, StationToTheList>> tsAvailableCharging = from item in bl.GetListOfBaseStations()
                                                                                            group item by item.availableChargingStations into gs
                                                                                            select gs;

                    l = new List<StationToTheList>();
                    foreach (var group1 in tsAvailableCharging)
                    {
                        foreach (StationToTheList item in group1)
                        {
                            l.Add(item);
                        }
                    }
                    StationListView.ItemsSource = l;
                    break;
                case 3:
                    IEnumerable<IGrouping<int, StationToTheList>> tsUnavailableCharging = from item in bl.GetListOfBaseStations()
                                                                                       group item by item.unAvailableChargingStations into gs
                                                                                       select gs;
                    l = new List<StationToTheList>();
                    foreach (var group1 in tsUnavailableCharging)
                    {
                        foreach (StationToTheList item in group1)
                        {
                            l.Add(item);
                        }
                    }
                    StationListView.ItemsSource = l;
                    break;

            }
        }
    }
}
