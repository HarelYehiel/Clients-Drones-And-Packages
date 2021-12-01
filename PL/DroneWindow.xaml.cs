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
using System.Windows.Shapes;
using IBL.BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneWindow.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        IBL.IBL bl;
        DroneToList droneToList;
        public DroneWindow(IBL.IBL bl1)
        // Constructor for adding drone.
        {
            bl = bl1;
            InitializeComponent();


            // Hide the all tools from view drone.
            DronesListView.Visibility = Visibility.Hidden;
            FunctionConbo.Visibility = Visibility.Hidden;
            OkayButton.Visibility = Visibility.Hidden;
            ModalDroneTextBox.Visibility = Visibility.Hidden;
            ModeDronelLabel.Visibility = Visibility.Hidden;

        }
        public DroneWindow(IBL.IBL bl1, DroneToList droneToList1)
        // Constructor for view drone and allow do actions.
        {
            bl = bl1;
            droneToList = droneToList1;
            InitializeComponent();

            // View the details of drone.
            List<DroneToList> dronesToLists = new List<DroneToList>();
            dronesToLists.Add(droneToList1);
            DronesListView.ItemsSource = dronesToLists;

            // Save the id of drone for the functions in combobox.
            //SaveTheDrineID.Text = droneToList1.uniqueID.ToString();

            // Just frome function 'update drone', if choose this function this tools visible
            ModalDroneTextBox.Visibility = Visibility.Hidden;
            ModeDronelLabel.Visibility = Visibility.Hidden;

            // Hide the all tools from adding drone
            AddDroneButton.Visibility = Visibility.Hidden;
            IDLabel.Visibility = Visibility.Hidden;
            ModelLabel.Visibility = Visibility.Hidden;
            StationIDLabel.Visibility = Visibility.Hidden;
            WieghtDroneLabel.Visibility = Visibility.Hidden;
            WieghtCombo.Visibility = Visibility.Hidden;
            IDTextBox.Visibility = Visibility.Hidden;
            ModelTextBox.Visibility = Visibility.Hidden;
            StationIDTextBox.Visibility = Visibility.Hidden;
        }
        private void AddDrone(object sender, RoutedEventArgs e)
        {
            try
            {
                Random r = new Random();

                int IdDrone = Convert.ToInt32(IDTextBox.Text);
                List<StationToTheList> stationToTheLists = bl.GetAllStaionsBy(s => s.ChargeSlots > 0).ToList();
                int IdStation = stationToTheLists[r.Next(0, stationToTheLists.Count)].uniqueID;
                bl.AddingDrone(IdDrone, ModelTextBox.Text,
                    (int)WieghtCombo.SelectedItem, IdStation);

                MessageBox.Show("The drone added", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close(); //? איך מפעילים מפה את הכפתור close

            }
            catch (Exception)
            {
                MessageBox.Show("The drone not added", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Okay(object sender, RoutedEventArgs e)
        {
            if (FunctionConbo.SelectedIndex == -1) ;
            // בעיה שלא בחרו אופציה

            else if (FunctionConbo.SelectedIndex == 0)  // update drone
            {
                if (ModelTextBox.SelectedText == "" || ModelTextBox.SelectedText == "Type Modal Drone") ;
                // הודעה של בעיה לא כתב מודל
                else
                {
                    // Change the model drone in datasource.
                    bl.UpdateDroneData(Convert.ToInt32(SaveTheDrineID.Text), ModelTextBox.Text);

                    // Change the model drone in listview
                    droneToList.Model = ModelTextBox.Text;
                    List<DroneToList> dronesToLists = new List<DroneToList>();
                    dronesToLists.Add(droneToList);
                    DronesListView.ItemsSource = dronesToLists;
                }

            }
            else if (FunctionConbo.SelectedIndex == 1) // send drone to charge at station
                bl.SendingDroneToCharging(droneToList.uniqueID);
            else if (FunctionConbo.SelectedIndex == 2) ; // send drone from charge in station
                                                         //bl.ReleaseDroneFromCharging(Convert.ToInt32(SaveTheDrineID.Text),);
                                                         //חסר את הזמן שהיה בטעינה.  ;


            else if (FunctionConbo.SelectedIndex == 3) // assign drone to parcel
                bl.AssignPackageToDrone(droneToList.uniqueID);
            else if (FunctionConbo.SelectedIndex == 4) // update picked up parcel by drone
                bl.CollectionOfPackageByDrone(Convert.ToInt32(SaveTheDrineID.Text));
            else if (FunctionConbo.SelectedIndex == 5) // update delivered parcel by drone
                bl.DeliveryOfPackageByDrone(Convert.ToInt32(SaveTheDrineID.Text));


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
            if (FunctionConbo.SelectedIndex == 0) // "update drone"
            {
                ModalDroneTextBox.Visibility = Visibility.Visible;
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
    }
}

