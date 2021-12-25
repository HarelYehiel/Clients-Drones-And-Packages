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
        DateTime? dateTime;

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

            // Hide the all tools from view drone.
            FunctionConbo.Visibility = Visibility.Collapsed;
            CharginDroneDatePicker.Visibility = Visibility.Collapsed;
            OkayButton.Visibility = Visibility.Collapsed;

        }
        public DroneWindow(BlApi.IBL bl1, DroneToList droneToList1)
        // Constructor for view drone and allow do actions.
        {
            bl = bl1;
            InitializeComponent();

            PrepareTheToolsForDroneDisplay(droneToList1);

        }
        void PrepareTheToolsForDroneDisplay(DroneToList droneToList)
        {
            FunctionConbo.SelectedIndex = 0; // Select Function

            // Details ID
            IDTextBlock.Visibility = Visibility.Collapsed;
            IDTextBox.IsEnabled = false;
            IDTextBox.Text = droneToList.uniqueID.ToString();

            // Details Model
            ModelTextBlock.Visibility = Visibility.Collapsed;
            ModelTextBox.Text = droneToList.Model;

            // Details Wieght For View
            WieghtForViewDroneIDLabel.Visibility = Visibility.Visible;
            WieghtForViewDroneIDTextBox.Visibility = Visibility.Visible;
            WieghtForViewDroneIDTextBox.Text = ((EnumBO.WeightCategories)droneToList.weight).ToString();
            WieghtForViewDroneIDTextBox.IsEnabled = false;

            // Details  Wieght - not need
            WieghtTextBlock.Visibility = Visibility.Collapsed;
            WieghtDroneLabel.Visibility = Visibility.Collapsed;
            WieghtCombo.Visibility = Visibility.Collapsed;

            // Details station ID - not need
            StationIDLabel.Visibility = Visibility.Collapsed;
            StationTextBlock.Visibility = Visibility.Collapsed;
            StationIDTextBox.Visibility = Visibility.Collapsed;

            // Details status
            statusDroneLabel.Visibility = Visibility.Visible;
            statusTextBox.Visibility = Visibility.Visible;
            statusTextBox.Text = ((EnumBO.DroneStatus)droneToList.status).ToString();
            statusTextBox.IsEnabled = false;

            // Details Battery
            BatteryLabel.Visibility = Visibility.Visible;
            BatteryTextBox.Visibility = Visibility.Visible;
            BatteryTextBox.Text = droneToList.Battery.ToString();
            BatteryTextBox.IsEnabled = false;

            // Details package Delivered
            packageDeliveredLabel.Visibility = Visibility.Visible;
            packageDeliveredTextBox.Visibility = Visibility.Visible;
            packageDeliveredTextBox.Text = droneToList.packageDelivered.ToString();
            statusTextBox.IsEnabled = false;

            DroneButton.Content = "Update";
            title.Content = "Update Drone";
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
        void UpdateDrone(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ModelTextBox.Text != "")
                {
                    bl.UpdateDroneData(Convert.ToInt32(IDTextBox.Text), ModelTextBox.Text);
                    MessageBox.Show("The drone update", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("The drone not update", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }
        void AddDrone(object sender, RoutedEventArgs e)
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
                        StationTextBlock.Text = "This ID station not exists.";
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

        private void ActionOfDrone(object sender, RoutedEventArgs e)
        {
            if (DroneButton.Content.ToString() == "Add Drone")
                AddDrone(sender, e);
            else if (DroneButton.Content.ToString() == "Update")
                UpdateDrone(sender, e);
        }
        private void Okay(object sender, RoutedEventArgs e)
        {
            try
            {
                if (FunctionConbo.SelectedIndex == 0 || FunctionConbo.SelectedIndex == -1)
                {
                    FunctionsTextBlock.Visibility = Visibility.Visible;
                    return; // Back to fix.
                }
                else if (FunctionConbo.SelectedIndex == 1) // send drone to charge at station
                {
                    bl.SendingDroneToCharging(Convert.ToInt32(IDTextBox.Text));
                }
                else if (FunctionConbo.SelectedIndex == 2)  // send drone from charge in station
                {
                    if (CharginDroneDatePicker.SelectedDate.Value == new DateTime())
                    {
                        DatePickerTextBlock.Text = "No selected date";
                        DatePickerTextBlock.Visibility = Visibility.Visible;
                        return; // Back to fix.
                    }
                    try
                    {
                        // Get the drone in charging from the list in dataSource.
                        BO.DroneInCharging droneInCharging = bl.GetDroneInCharging(Convert.ToInt32(IDTextBox.Text));

                        // Calculate the range between the times
                        TimeSpan timeSpan = CharginDroneDatePicker.SelectedDate.Value - droneInCharging.startCharge;
                        bl.ReleaseDroneFromCharging(Convert.ToInt32(IDTextBox.Text), timeSpan.TotalMinutes);

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("The drone is not maintained at all", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        return; // Back to fix.
                    }
                }
                else if (FunctionConbo.SelectedIndex == 3) // assign drone to parcel
                    bl.AssignPackageToDrone(Convert.ToInt32(IDTextBox.Text));
                else if (FunctionConbo.SelectedIndex == 4) // update picked up parcel by drone
                    bl.CollectionOfPackageByDrone(Convert.ToInt32(IDTextBox.Text));
                else if (FunctionConbo.SelectedIndex == 5) // update delivered parcel by drone
                    bl.DeliveryOfPackageByDrone(Convert.ToInt32(IDTextBox.Text));

                UpdateDroneData();


                MessageBox.Show("The drone updated", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception)
            {
                MessageBox.Show("The drone not updated", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        void UpdateDroneData()
            // Update the view drone data after click the Update button.
        {
            DroneToList droneToList = bl.GetDroneToTheList(Convert.ToInt32(IDTextBox.Text));

            ModelTextBox.Text = droneToList.Model;
            statusTextBox.Text = ((EnumBO.DroneStatus)droneToList.status).ToString();
            BatteryTextBox.Text = droneToList.Battery.ToString();
            packageDeliveredTextBox.Text = droneToList.packageDelivered.ToString();

        }
        private void FunctionConbo_Initialized(object sender, EventArgs e)
        {
            List<string> s = new List<string>() {
                "Select Function"
                , "send drone to charge at station"
                , "release drone from charge in station"
                , "assign drone to parcel"
                , "update picked up parcel by drone"
                , "update delivered parcel by drone"
            };
            FunctionConbo.ItemsSource = s;
        }
        private void FunctionConbo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                if (FunctionConbo.SelectedIndex == 2) // "release drone from charge in station"
                {
                    // Get the drone in charging from the list in dataSource.
                    BO.DroneInCharging droneInCharging = bl.GetDroneInCharging(Convert.ToInt32(IDTextBox.Text));
                    // Limit the date to the minimum date in DatePicker
                    CharginDroneDatePicker.DisplayDateStart = droneInCharging.startCharge;

                    CharginDroneDatePicker.Visibility = Visibility.Visible;

                    dateTime = new DateTime();

                }
                else // Other functions
                {
                    CharginDroneDatePicker.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Don't have this drone in charging, send before to charge.", "Eroor", MessageBoxButton.OK, MessageBoxImage.Error);
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
            IDTextBlock.Visibility = Visibility.Collapsed;
            ModelTextBlock.Visibility = Visibility.Collapsed;
            StationTextBlock.Visibility = Visibility.Collapsed;
            WieghtTextBlock.Visibility = Visibility.Collapsed;
        }

        private void CharginDroneDatePicker_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            dateTime = CharginDroneDatePicker.SelectedDate;
        }
    }
}

