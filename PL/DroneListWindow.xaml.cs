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
    /// Interaction logic for DroneListWindow.xaml
    /// </summary>
    public partial class DroneListWindow : Window
    {
        IBL.IBL bl;
        public DroneListWindow(ref IBL.IBL bL1)
        {
            bl = bL1;
            InitializeComponent();
        }

        private void StatusDroneWeight(object sender, SelectionChangedEventArgs e)
        {
            DronesListView.ItemsSource = bl.GetAllDronesBy(D => D.weight == (EnumBO.WeightCategories)WieghtCombo.SelectedItem);
        }

        private void StatusDroneSituation(object sender, SelectionChangedEventArgs e)
        {
            DronesListView.ItemsSource = bl.GetAllDronesBy(D => D.status == (EnumBO.DroneStatus)SituationCombo.SelectedItem);
        }

        private void WieghtCombo_Initialized(object sender, EventArgs e)
        {
            WieghtCombo.ItemsSource = Enum.GetValues(typeof(EnumBO.WeightCategories));
            //WieghtCombo.SelectedIndex = 0;
        }

        private void SituationCombo_Initialized(object sender, EventArgs e)
        {
            SituationCombo.ItemsSource = Enum.GetValues(typeof(EnumBO.Situations));
            //SituationCombo.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClearFilter(object sender, RoutedEventArgs e)
        {
            DronesListView.ItemsSource = bl.GetTheListOfDrones();

            //SituationCombo.Text = "Choose status";
            //WieghtCombo.Text = "Choose wieght";
        }

        private void BothFilter(object sender, RoutedEventArgs e)
        {
            if (SituationCombo.SelectedItem != Enum.GetValues(typeof(EnumBO.Situations)) && WieghtCombo.SelectedItem != Enum.GetValues(typeof(EnumBO.WeightCategories)))
                DronesListView.ItemsSource = bl.GetAllDronesBy
                    (D => D.status == (EnumBO.DroneStatus)SituationCombo.SelectedItem
                    && D.weight == (EnumBO.WeightCategories)WieghtCombo.SelectedItem);
        }

        private void DronesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            new DroneWindow(bl, DronesListView.SelectedItem as IBL.BO.DroneToList).Show();
        }

        private void AddingNewDrone(object sender, RoutedEventArgs e)
        {
            new DroneWindow(bl).Show();

        }
    }
}
