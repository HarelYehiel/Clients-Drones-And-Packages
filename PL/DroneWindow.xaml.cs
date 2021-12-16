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
    /// Interaction logic for DroneWindow.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        BlApi.IBL bl;
        DroneToList droneToList;
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        public DroneWindow(BlApi.IBL bl1)
        // Constructor for adding drone.
        {
            bl = bl1;
            InitializeComponent();

            // View the details of drone.
            DronesListView.ItemsSource = bl.GetListOfBaseStations();

            // Hide the all tools from view drone.
            FunctionConbo.Visibility = Visibility.Hidden;
            CharginDroneDatePicker.Visibility = Visibility.Hidden;
            OkayButton.Visibility = Visibility.Hidden;
            ModalDroneTextBox.Visibility = Visibility.Hidden;
            ModeDronelLabel.Visibility = Visibility.Hidden;

        }
        public DroneWindow(BlApi.IBL bl1, DroneToList droneToList1)
        // Constructor for view drone and allow do actions.
        {
            bl = bl1;
            droneToList = droneToList1;
            InitializeComponent();

            // View the details of drone.
            List<DroneToList> dronesToLists = new List<DroneToList>();
            dronesToLists.Add(droneToList1);
            DronesListView.ItemsSource = dronesToLists;

            // Just frome function 'update drone', if choose this function this tools visible
            ModalDroneTextBox.Visibility = Visibility.Hidden;
            ModeDronelLabel.Visibility = Visibility.Hidden;

            // Just frome function 'send drone from charge in station', if choose this function this tools visible
            CharginDroneDatePicker.Visibility = Visibility.Hidden;

            // Hide the all tools from adding drone
            addDroneGrid.Visibility = Visibility.Hidden;

            //AddDroneButton.Visibility = Visibility.Hidden;
            //IDLabel.Visibility = Visibility.Hidden;
            //ModelLabel.Visibility = Visibility.Hidden;
            //StationIDLabel.Visibility = Visibility.Hidden;
            //WieghtDroneLabel.Visibility = Visibility.Hidden;
            //WieghtCombo.Visibility = Visibility.Hidden;
            //IDTextBox.Visibility = Visibility.Hidden;
            //ModelTextBox.Visibility = Visibility.Hidden;
            //StationIDTextBox.Visibility = Visibility.Hidden;
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
        private void AddDrone(object sender, RoutedEventArgs e)
        {
            try
            {
                bool AreAllTestsNormal = true;
                hideAndReseteAllTextBlocks(); // Hide and resete all the TextBlocks

                if (!isNumber(IDTextBox.Text))
                // Check if the drone ID is typed only with numbers.
                {
                    IDTextBlock.Text = "Type only numbers";
                    IDTextBlock.Visibility = Visibility.Visible;
                }
                else if (!isNumber(StationIDTextBox.Text))
                // Check if the station ID is typed only with numbers.
                {
                    StationTextBlock.Text = "Type only numbers";
                    StationTextBlock.Visibility = Visibility.Visible;
                }
                else
                {
                    int IdDrone = Convert.ToInt32(IDTextBox.Text);
                    int IdStaion = Convert.ToInt32(StationIDTextBox.Text);


                    if (existThisIdDrone(IdDrone) || IDTextBox.Text.Length == 0)
                    // ID drone exist in the list ? if true (yes), so error
                    {
                        IDTextBlock.Text = "This ID drone exists, select another.";
                        IDTextBlock.Visibility = Visibility.Visible;
                        AreAllTestsNormal = false;
                    }
                    if (ModelTextBox.Text.Length == 0 || ModelTextBox.Text == "Type model drone")
                    // Model drone
                    {
                        ModelTextBlock.Visibility = Visibility.Visible;
                        AreAllTestsNormal = false;

                    }
                    if (!existThisIdStation(IdStaion))
                    // ID station exist ? if false (no), so error.
                    {
                        StationTextBlock.Text = "This ID station not exists, select another.";
                        StationTextBlock.Visibility = Visibility.Visible;
                        AreAllTestsNormal = false;
                    }
                    if (WieghtCombo.SelectedIndex == -1) // Wieght drone
                    {
                        WieghtTextBlock.Visibility = Visibility.Visible;
                        AreAllTestsNormal = false;
                    }
                    if (AreAllTestsNormal) // Yes, all the tests normal.
                    {                       
                        bl.AddingDrone(IdDrone, ModelTextBox.Text, (int)WieghtCombo.SelectedItem, IdStaion);

                        MessageBox.Show("The drone added", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        Close();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("The drone not added", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void Okay(object sender, RoutedEventArgs e)
        {
            try
            {
                InfoTextBlock.Visibility = Visibility.Hidden;
                if (FunctionConbo.SelectedIndex == -1)
                {
                    MessageBox.Show("You not choose anything", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else if (FunctionConbo.SelectedIndex == 0)  // update drone
                {
                    if (ModalDroneTextBox.Text == "" || ModalDroneTextBox.Text == "Type Model Drone to update")
                    {
                        InfoTextBlock.Text = "not get new model";
                        InfoTextBlock.Visibility = Visibility.Visible;
                        return; // Back to fix.
                    }

                    // Change the model drone in datasource.
                    bl.UpdateDroneData(droneToList.uniqueID, ModalDroneTextBox.Text);

                    // Change the model drone in listview
                    droneToList.Model = ModalDroneTextBox.Text;
                    List<DroneToList> dronesToLists = new List<DroneToList>();
                    dronesToLists.Add(droneToList);
                    DronesListView.ItemsSource = dronesToLists;


                }
                else if (FunctionConbo.SelectedIndex == 1) // send drone to charge at station
                {
                    bl.SendingDroneToCharging(droneToList.uniqueID);
                }
                else if (FunctionConbo.SelectedIndex == 2)  // send drone from charge in station
                {
                    if (CharginDroneDatePicker.SelectedDate.Value == new DateTime())
                    {
                        InfoTextBlock.Text = "No selected date";
                        InfoTextBlock.Visibility = Visibility.Visible;
                        return; // Back to fix.
                    }
                    try
                    {
                        // Get the drone in charging from the list in dataSource.
                        BO.DroneInCharging droneInCharging = bl.GetDroneInCharging(droneToList.uniqueID);

                        // Calculate the range between the times
                        TimeSpan timeSpan = CharginDroneDatePicker.SelectedDate.Value - droneInCharging.startCharge;
                        bl.ReleaseDroneFromCharging(droneToList.uniqueID, timeSpan.TotalMinutes);

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("The drone is not maintained at all", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        return; // Back to fix.
                    }
                }
                else if (FunctionConbo.SelectedIndex == 3) // assign drone to parcel
                    bl.AssignPackageToDrone(droneToList.uniqueID);
                else if (FunctionConbo.SelectedIndex == 4) // update picked up parcel by drone
                    bl.CollectionOfPackageByDrone(droneToList.uniqueID);
                else if (FunctionConbo.SelectedIndex == 5) // update delivered parcel by drone
                    bl.DeliveryOfPackageByDrone(droneToList.uniqueID);

                MessageBox.Show("The drone updated", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception)
            {
                MessageBox.Show("The drone not updated", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }



        }
        private void FunctionConbo_Initialized(object sender, EventArgs e)
        {
            List<string> s = new List<string>() {
                "update drone"
                , "send drone to charge at station"
                , "send drone from charge in station"
                , "assign drone to parcel"
                , "update picked up parcel by drone"
                , "update delivered parcel by drone"
            };
            FunctionConbo.ItemsSource = s;
        }
        private void FunctionConbo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InfoTextBlock.Visibility = Visibility.Hidden;
            CharginDroneDatePicker.Visibility = Visibility.Hidden;
            ModalDroneTextBox.Visibility = Visibility.Hidden;

            if (FunctionConbo.SelectedIndex == 0) // "update drone"
            {
                ModeDronelLabel.Content = "Drone Model";
                ModalDroneTextBox.Text = "Type Model Drone to update";
                ModalDroneTextBox.Visibility = Visibility.Visible;
                ModeDronelLabel.Visibility = Visibility.Visible;
            }
            else if (FunctionConbo.SelectedIndex == 2)
            {
                // Get the drone in charging from the list in dataSource.
                BO.DroneInCharging droneInCharging = bl.GetDroneInCharging(droneToList.uniqueID);
                // Limit the date to the minimum date in DatePicker
                CharginDroneDatePicker.DisplayDateStart = droneInCharging.startCharge;

                ModeDronelLabel.Content = "Select the end date";
                CharginDroneDatePicker.Visibility = Visibility.Visible;
                ModeDronelLabel.Visibility = Visibility.Visible;

            }
            else // Other functions
            {
                ModalDroneTextBox.Visibility = Visibility.Hidden;
                ModeDronelLabel.Visibility = Visibility.Hidden;
            }
        }
        private void Close(object sender, RoutedEventArgs e)
        {

        }
        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
        private void WieghtCombo_Initialized(object sender, EventArgs e)
        {
            WieghtCombo.ItemsSource = Enum.GetValues(typeof(EnumBO.WeightCategories));

        }
        private void CancelButtonX(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }

        bool existThisIdDrone(int id)
        {
            try
            {
                Drone drone;
                drone = bl.GetDrone(id);
                if (drone.uniqueID == id) return true; // Exist drine with this id.
                return false;
            }
            catch (Exception)
            {
                return false;// Don't exist drone with this id.
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
        void hideAndReseteAllTextBlocks()
        {
            IDTextBlock.Visibility = Visibility.Hidden;
            ModelTextBlock.Visibility = Visibility.Hidden;
            StationTextBlock.Visibility = Visibility.Hidden;
            WieghtTextBlock.Visibility = Visibility.Hidden;
        }
    }
}

