using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;


namespace PL
{
    /// <summary>
    /// Interaction logic for DronePage.xaml
    /// </summary>
    public partial class DronePage : Page
    {

        BlApi.IBL bl;
        List<DroneToList> dronesToTheLists;
        DroneToList droneToListChoose;
        BackgroundWorker worker;



        // When true allows the 'filters' function to be activated, otherwise there is no access.
        //We usually use this when initializing or resetting the TextBox.
        bool TurnOnFunctionFilters;
        public DronePage(BlApi.IBL bL1)
        {
            bl = bL1;
            dronesToTheLists = new List<DroneToList>();
            dronesToTheLists.AddRange(bl.GetTheListOfDrones());

            TurnOnFunctionFilters = false;
            InitializeComponent();
            TurnOnFunctionFilters = true;

            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.WorkerSupportsCancellation = true;
            worker.RunWorkerAsync();

            DronesListView.ItemsSource = dronesToTheLists;
        }
        /// <summary>
        /// update the viewListDrones in real time.
        /// When the drone run in simultor we can sea the change.
        /// </summary>
        void updateTheViewListDronesInRealTime()
        {

            EnableFiltersWithConditions();
        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!worker.CancellationPending)
            {
                Action theUpdateView = updateTheViewListDronesInRealTime;
                //Dispatcher to main-thread to update the window drone.
                DronesListView.Dispatcher.Invoke(theUpdateView);
                Thread.Sleep(500);
            }
        }
        private void DronesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DronesListView.SelectedItem != null)
                droneToListChoose = DronesListView.SelectedItem as BO.DroneToList;


            openOptions.Visibility = Visibility.Visible;
            // HideOrVisibleDronesListViewAndOpenOptionsTheOpposite();
        }

        private void AddingNewDrone(object sender, RoutedEventArgs e)
        {
            new DroneWindow(bl).ShowDialog();

            EnableFiltersWithConditions();
        }
        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            worker.CancelAsync();

            this.Visibility = Visibility.Hidden;
        }

        private void cancelButtonX(object sender, RoutedEventArgs e)
        // Canael the button X.
        {

        }
        bool isNumber(string s)
        // return true if is number, else false.
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
        // return true if is double, else false.
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

        #region Initializ.

        private void WieghtCombo_Initialized(object sender, EventArgs e)
        {
            WieghtCombo.ItemsSource = Enum.GetValues(typeof(EnumBO.WeightCategories));
        }
        private void StatusCombo_Initialized(object sender, EventArgs e)
        {
            StatusCombo.ItemsSource = Enum.GetValues(typeof(EnumBO.DroneStatus));
        }

        #endregion

        #region All filters
        private void Refresh_Click(object sender, RoutedEventArgs e)
        // Refreah the listView.
        {
            EnableFiltersWithConditions();
        }

        /// <summary>
        /// If TurnOnFunctionFilters = true search with the filters
        /// else don't do nathing.
        /// </summary>
        void EnableFiltersWithConditions()
        {
            if (TurnOnFunctionFilters)
                Filters();
        }
        private void StatusDroneWeight(object sender, SelectionChangedEventArgs e)
        {
            EnableFiltersWithConditions();
        }
        private void StatusDroneSituation(object sender, SelectionChangedEventArgs e)
        {
            EnableFiltersWithConditions();

        }
        private void ClearFilter(object sender, RoutedEventArgs e)
        {

            DronesListView.ItemsSource = bl.GetTheListOfDrones();

            HideAndReseteAllTextBox();
        }

        /// <summary>
        /// Hide And Resete All TextBox.
        /// All textBox = "search".
        /// </summary>
        private void HideAndReseteAllTextBox()

        {
            TurnOnFunctionFilters = false;

            FilterIDTextBox.Text = "Search";
            FilterIDTextBox.Visibility = Visibility.Hidden;

            FilterModelTextBox.Text = "Search";
            FilterModelTextBox.Visibility = Visibility.Hidden;

            StatusCombo.SelectedIndex = -1;
            StatusCombo.Text = "Choose status";
            StatusCombo.Visibility = Visibility.Hidden;

            WieghtCombo.SelectedIndex = -1;
            WieghtCombo.Text = "Choose wieght";
            WieghtCombo.Visibility = Visibility.Hidden;

            FilterBatteryTextBox.Text = "Search";
            FilterBatteryTextBox.Visibility = Visibility.Hidden;

            FilterLocationTextBox.Text = "Search";
            FilterLocationTextBox.Visibility = Visibility.Hidden;

            TurnOnFunctionFilters = true;
        }

        private void Filters()
        // Search by all filter togther.
        {

            try
            {
                DronesListView.ItemsSource = null;
                dronesToTheLists.Clear();
                dronesToTheLists.AddRange(bl.GetTheListOfDrones());

                if (isNumber(FilterIDTextBox.Text)) // Filter ID
                {
                    string id = FilterIDTextBox.Text;
                    dronesToTheLists = dronesToTheLists.FindAll
                        (s => s.uniqueID.ToString().Contains(id));
                }
                if (isNumber(FilterParcelTextBox.Text)) // Filter ID parcel
                {
                    string idParcel = FilterParcelTextBox.Text;
                    dronesToTheLists = dronesToTheLists.FindAll
                        (s => s.packageDelivered.ToString().Contains(idParcel));
                }
                if (WieghtCombo.SelectedIndex != -1) // Filter wieght
                {
                    dronesToTheLists = dronesToTheLists.FindAll
                        (s => s.weight == (EnumBO.WeightCategories)WieghtCombo.SelectedIndex);
                }

                if (IsDouble(FilterBatteryTextBox.Text))
                // Filter battrey
                {
                    string Battery = FilterBatteryTextBox.Text;
                    dronesToTheLists = dronesToTheLists.FindAll
                        (s => s.Battery.ToString().Contains(Battery));
                }
                if (FilterModelTextBox.Text != "Search" &&
                    FilterModelTextBox.Text != "")
                // Filter model
                {
                    string model = FilterModelTextBox.Text;
                    dronesToTheLists = dronesToTheLists.FindAll
                        (s => s.Model.Contains(model));
                }
                if (IsDouble(FilterLocationTextBox.Text))
                // Filter loction
                {
                    string Loction = FilterLocationTextBox.Text;
                    dronesToTheLists = dronesToTheLists.FindAll
                        (s => s.location.latitude.ToString().Contains(Loction) ||
                         s.location.longitude.ToString().Contains(Loction));
                }
                if (StatusCombo.SelectedIndex != -1)
                // Filter status
                {
                    dronesToTheLists = dronesToTheLists.FindAll
                        (s => s.status == (EnumBO.DroneStatus)StatusCombo.SelectedIndex);
                }

                DronesListView.ItemsSource = dronesToTheLists;
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

        private void SearchModelButton_Click(object sender, RoutedEventArgs e)
        {
            if (FilterModelTextBox.Visibility == Visibility.Hidden)
                FilterModelTextBox.Visibility = Visibility.Visible;
            else
            {
                FilterModelTextBox.Text = "Search";
                FilterModelTextBox.Visibility = Visibility.Hidden;
            }
        }

        private void SearchWeightButton_Click(object sender, RoutedEventArgs e)
        {
            if (WieghtCombo.Visibility == Visibility.Hidden)
                WieghtCombo.Visibility = Visibility.Visible;
            else
            {
                WieghtCombo.SelectedIndex = -1;
                WieghtCombo.Text = "Choose wieght";
                WieghtCombo.Visibility = Visibility.Hidden;
            }
        }

        private void SearchStatusButton_Click(object sender, RoutedEventArgs e)
        {
            if (StatusCombo.Visibility == Visibility.Hidden)
                StatusCombo.Visibility = Visibility.Visible;
            else
            {
                StatusCombo.SelectedIndex = -1;
                StatusCombo.Text = "Choose status";
                StatusCombo.Visibility = Visibility.Hidden;
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

        private void SearchLocationButton_Click(object sender, RoutedEventArgs e)
        {
            if (FilterLocationTextBox.Visibility == Visibility.Hidden)
                FilterLocationTextBox.Visibility = Visibility.Visible;
            else
            {
                FilterLocationTextBox.Text = "Search";
                FilterLocationTextBox.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// All the text box for search grt to hare if did change in the tex.
        /// </summary>
        private void AnyFilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            EnableFiltersWithConditions();
        }

        private void SearchParcelButton_Click(object sender, RoutedEventArgs e)
        {
            if (FilterParcelTextBox.Visibility == Visibility.Hidden)
                FilterParcelTextBox.Visibility = Visibility.Visible;
            else
            {
                FilterParcelTextBox.Text = "Search";
                FilterParcelTextBox.Visibility = Visibility.Hidden;
            }

        }
        #endregion

        #region OpenBar Buttons
        private void ViewDroneButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (DronesListView.ItemsSource != null)
                    new DroneWindow(bl, droneToListChoose).Show();

                DronesListView.SelectedItem = null;
                EnableFiltersWithConditions();
            }
            catch (Exception)
            {
                DronesListView.SelectedItem = null;
            }

        }

        private void ViewParcelDeliveredButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                int IDPacel = droneToListChoose.packageDelivered;
                if (IDPacel == 0)
                    throw new MyExeption_BO("check if the drone associated to parcel");

                if (DronesListView.ItemsSource != null)
                {
                    ParcelToList parcelToList;
                    parcelToList = bl.GetParcelToTheList(IDPacel);
                    new ParcelWindow(bl, parcelToList).Show();
                }

                DronesListView.SelectedItem = null;
                EnableFiltersWithConditions();
            }
            catch (Exception ex)
            {
                if (ex.Message == "check if the drone associated to parcel")
                    MessageBox.Show("Don't have this parcel, check if the drone associated to parcel.", "Eroor", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }

        private void CancelOpenBarButton_Click(object sender, RoutedEventArgs e)
        {
            openOptions.Visibility = Visibility.Hidden;
        }
        #endregion

        #region CombBoxes
        private void ComboBox_Initialized(object sender, EventArgs e)
        {
            List<string> l = new List<string>() {
                "Choose",
                "Model",
                "Weight",
                "Status"
            };
            GroupByComboBox.ItemsSource = l;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<DroneToList> l;

            switch (GroupByComboBox.SelectedIndex)
            {

                case 1: //"Model"

                    IEnumerable<IGrouping<string, DroneToList>> tsModel = from item in bl.GetTheListOfDrones()
                                                                          group item by item.Model into gs
                                                                          select gs;


                    l = new List<DroneToList>();
                    foreach (var group1 in tsModel)
                    {
                        foreach (DroneToList item in group1)
                        {
                            l.Add(item);
                        }
                    }
                    DronesListView.ItemsSource = l;
                    break;

                case 2: // "Weight"
                    IEnumerable<IGrouping<EnumBO.WeightCategories, DroneToList>> tsweight;

                    tsweight = from item in bl.GetTheListOfDrones()
                               group item by item.weight into gs
                               select gs;


                    l = new List<DroneToList>();
                    foreach (var group1 in tsweight)
                    {
                        foreach (DroneToList item in group1)
                        {
                            l.Add(item);
                        }
                    }
                    DronesListView.ItemsSource = l;
                    break;
                case 3: // "Status"
                    IEnumerable<IGrouping<EnumBO.DroneStatus, DroneToList>> tsStatus;

                    tsStatus = from item in bl.GetTheListOfDrones()
                               group item by item.status into gs
                               select gs;


                    l = new List<DroneToList>();
                    foreach (var group1 in tsStatus)
                    {
                        foreach (DroneToList item in group1)
                        {
                            l.Add(item);
                        }
                    }
                    DronesListView.ItemsSource = l;
                    break;

            }
        }
        #endregion

    }
}


