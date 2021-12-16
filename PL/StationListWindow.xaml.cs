using System;
using System.Windows;
using System.Windows.Controls;
using BO;
namespace PL
{
    /// <summary>
    /// Interaction logic for StationListWindow.xaml
    /// </summary>
    public partial class StationListWindow : Window
    {
        public StationListWindow(BlApi.IBL bl1)
        {
            InitializeComponent();
        }

        private void DronesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddingNewDrone(object sender, RoutedEventArgs e)
        {

        }

        private void ClearFilter(object sender, RoutedEventArgs e)
        {

        }

        private void AllFilters(object sender, RoutedEventArgs e)
        {

        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SituationCombo_Initialized(object sender, EventArgs e)
        {

        }

        private void WieghtCombo_Initialized(object sender, EventArgs e)
        {

        }

        private void WieghtCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void StatusDroneSituation(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
