using BO;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Runtime.CompilerServices;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneInChaeginngWindow.xaml
    /// </summary>
    public partial class DroneInChargingWindow : Window
    {
        BackgroundWorker worker;



        BlApi.IBL bl;

        // Work with this list
        List<DroneInCharging> RunDronesInCharging;

        int StationID;

        // When true allows the 'filters' function to be activated, otherwise there is no access.
        //We usually use this when initializing or resetting the TextBox.
        bool TurnOnFunctionFilters;

        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        public DroneInChargingWindow(BlApi.IBL bL1, int StationID1)
        {
            bl = bL1;
            StationID = StationID1;
            RunDronesInCharging = new List<DroneInCharging>();

            RunDronesInCharging.AddRange(bl.GetAllDronesInCharging(C => C.staitionId == StationID)); 

            TurnOnFunctionFilters = false;
            InitializeComponent();
            TurnOnFunctionFilters = true;

            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerAsync();

            DronesInChargingListView.ItemsSource = RunDronesInCharging;
        }
        void updateViewDronesInCharging()
        {
            Filters();
           //  DronesInChargingListView.ItemsSource = (bl.GetAllDronesInCharging(C => C.staitionId == StationID)); 
        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!worker.CancellationPending)
            {
                Action action = updateViewDronesInCharging;
                DronesInChargingListView.Dispatcher.BeginInvoke(action);

                Thread.Sleep(200);
            }
        }
        private void StatusDroneWeight(object sender, SelectionChangedEventArgs e)
        {
            Filters();
        }
        private void StatusDroneSituation(object sender, SelectionChangedEventArgs e)
        {
            Filters();

        }
        private void ClearFilter(object sender, RoutedEventArgs e)
        {

            DronesInChargingListView.ItemsSource = RunDronesInCharging;

            HideAndReseteAllTextBox();
        }
        private void DronesInChargingListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HideOrVisibleDronesListViewAndOpenOptionsTheOpposite();
        }
        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            worker.CancelAsync();
            this.Close();
        }

        private void cancelButtonX(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
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
        private void HideAndReseteAllTextBox()
        {
            TurnOnFunctionFilters = false;

            FilterIDTextBox.Text = "Search";
            FilterIDTextBox.Visibility = Visibility.Hidden;

            FilterBatteryTextBox.Text = "Search";
            FilterBatteryTextBox.Visibility = Visibility.Hidden;

            TurnOnFunctionFilters = true;
        }

        private void Filters()
        // Search by all filter togther.
        {

            try
            {
                DronesInChargingListView.ItemsSource = null;
                RunDronesInCharging.Clear();
                RunDronesInCharging.AddRange(bl.GetAllDronesInCharging(C => C.staitionId == StationID)); 

                if (isNumber(FilterIDTextBox.Text)) // Filter ID
                {
                    string id = FilterIDTextBox.Text;
                    RunDronesInCharging = RunDronesInCharging.FindAll
                        (s => s.uniqueID.ToString().Contains(id));
                }
                if (IsDouble(FilterBatteryTextBox.Text)) // Filter battrey
                {
                    string Battery = FilterBatteryTextBox.Text;
                    RunDronesInCharging = RunDronesInCharging.FindAll
                        (s => s.batteryStatus.ToString().Contains(Battery));
                }

                DronesInChargingListView.ItemsSource = RunDronesInCharging;
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
        private void SearchBattryButton_Click(object sender, RoutedEventArgs e)
        {
            if (FilterBatteryTextBox.Visibility == Visibility.Hidden)
                FilterBatteryTextBox.Visibility = Visibility.Visible;
            else
            {
                FilterBatteryTextBox.Text = "Search";
                FilterBatteryTextBox.Visibility = Visibility.Hidden;
            }
        }

        private void AnyFilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            EnableFiltersWithConditions();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            TurnOnFunctionFilters = true;
            EnableFiltersWithConditions();
        }
        void EnableFiltersWithConditions()
        {
            if (TurnOnFunctionFilters)
                Filters();
        }
        private void RemoveFromCharging_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int IDDrone = (DronesInChargingListView.SelectedItem as DroneInCharging).uniqueID;

                if (DronesInChargingListView.ItemsSource != null)
                    bl.ReleaseDroneFromCharging(IDDrone, DateTime.Now); 

                DronesInChargingListView.SelectedItem = null;
                EnableFiltersWithConditions();
            }
            catch (Exception)
            {
                DronesInChargingListView.SelectedItem = null;
            }

        }

        private void ViewParcelDeliveredButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                int IDPacel = (DronesInChargingListView.SelectedItem as BO.DroneToList).packageDelivered;
                if (IDPacel == 0)
                    throw new MyExeption_BO("check if the drone associated to parcel");

                if (DronesInChargingListView.ItemsSource != null)
                {
                    ParcelToList parcelToList;
                     parcelToList = bl.GetParcelToTheList(IDPacel); 
                    new ParcelWindow(bl, parcelToList).ShowDialog();
                }
                DronesInChargingListView.SelectedItem = null;
                EnableFiltersWithConditions();
            }
            catch (Exception ex)
            {
                if (ex.Message == "check if the drone associated to parcel")
                    MessageBox.Show("Don't have this parcel, check if the drone associated to parcel.", "Eroor", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                    DronesInChargingListView.SelectedItem = null;
            }

        }

        private void CancelOpenBarButton_Click(object sender, RoutedEventArgs e)
        {
            DronesInChargingListView.SelectedItem = null;
        }
        void HideOrVisibleDronesListViewAndOpenOptionsTheOpposite()
        // Hide oe visible all button on DronesInChargingListView and DronesInChargingListView,
        // DronesInChargingListView The Opposite.
        {
            if (DronesInChargingListView.Visibility == Visibility.Visible)
            {
                openOptions.Visibility = Visibility.Visible;

                DronesInChargingListView.Visibility = Visibility.Hidden;
                SearchIDButton.Visibility = Visibility.Hidden;
                SearchBattryButton.Visibility = Visibility.Hidden;
            }
            else
            {
                openOptions.Visibility = Visibility.Hidden;

                DronesInChargingListView.Visibility = Visibility.Visible;
                SearchIDButton.Visibility = Visibility.Visible;
                SearchBattryButton.Visibility = Visibility.Visible;
            }

        }
    }
}
