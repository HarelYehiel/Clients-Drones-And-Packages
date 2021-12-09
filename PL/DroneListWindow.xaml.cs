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
using System.Runtime.InteropServices;
using System.Windows.Interop;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneListWindow.xaml
    /// </summary>
    public partial class DroneListWindow : Window
    {
        BlApi.IBL bl;

        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        public DroneListWindow(ref BlApi.IBL bL1)
        {
            bl = bL1;
            InitializeComponent();
        }
        private void StatusDroneWeight(object sender, SelectionChangedEventArgs e)
        {
            if (WieghtCombo.SelectedIndex > -1)
                DronesListView.ItemsSource = bl.GetAllDronesBy(D => D.weight == (EnumBO.WeightCategories)WieghtCombo.SelectedItem);
        }
        private void StatusDroneSituation(object sender, SelectionChangedEventArgs e)
        {
            if (SituationCombo.SelectedIndex > -1)
                DronesListView.ItemsSource = bl.GetAllDronesBy(D => D.status == (EnumBO.DroneStatus)SituationCombo.SelectedItem);
        }
        private void WieghtCombo_Initialized(object sender, EventArgs e)
        {
            WieghtCombo.ItemsSource = Enum.GetValues(typeof(EnumBO.WeightCategories));
        }
        private void SituationCombo_Initialized(object sender, EventArgs e)
        {
            SituationCombo.ItemsSource = Enum.GetValues(typeof(EnumBO.DroneStatus));
        }
        private void ClearFilter(object sender, RoutedEventArgs e)
        {
            DronesListView.ItemsSource = bl.GetTheListOfDrones();

            SituationCombo.SelectedIndex = -1;
            WieghtCombo.SelectedIndex = -1;
        }
        private void AllFilters(object sender, RoutedEventArgs e)
        {
            if (SituationCombo.SelectedIndex == -1 || WieghtCombo.SelectedIndex == -1)
                MessageBox.Show("One of the filters was not selected", "Error",MessageBoxButton.OK);
            else if (SituationCombo.SelectedItem != Enum.GetValues(typeof(EnumBO.Situations)) && WieghtCombo.SelectedItem != Enum.GetValues(typeof(EnumBO.WeightCategories)))
                DronesListView.ItemsSource = bl.GetAllDronesBy
                    (D => D.status == (EnumBO.DroneStatus)SituationCombo.SelectedItem
                    && D.weight == (EnumBO.WeightCategories)WieghtCombo.SelectedItem);
        }
        private void DronesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DronesListView.ItemsSource != null)
                new DroneWindow(bl, DronesListView.SelectedItem as BO.DroneToList).ShowDialog();

            DronesListView.ItemsSource = null;
            if (SituationCombo.SelectedIndex == -1 && WieghtCombo.SelectedIndex == -1) // No filter
                DronesListView.ItemsSource = bl.GetTheListOfDrones();
            else // Have filter/s.
                ShowTheSkimmersAgain();

        }
        private void AddingNewDrone(object sender, RoutedEventArgs e)
        {
            DronesListView.ItemsSource = null; // Reset the list drone.
            new DroneWindow(bl).ShowDialog();

            if (SituationCombo.SelectedIndex == -1 && WieghtCombo.SelectedIndex == -1) // No filter
                DronesListView.ItemsSource = bl.GetTheListOfDrones();
            else // Have filter/s.
                ShowTheSkimmersAgain();

        }
        private void ShowTheSkimmersAgain()
        // Show the skimmers again with the one filter on
        {
            if (SituationCombo.SelectedIndex == -1)
                DronesListView.ItemsSource = bl.GetAllDronesBy
                   (D => D.weight == (EnumBO.WeightCategories)WieghtCombo.SelectedItem);
            else if (WieghtCombo.SelectedIndex == -1)
                DronesListView.ItemsSource = bl.GetAllDronesBy
                   (D => D.status == (EnumBO.DroneStatus)SituationCombo.SelectedItem);
        }
        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cancelButtonX(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }
    }
}
