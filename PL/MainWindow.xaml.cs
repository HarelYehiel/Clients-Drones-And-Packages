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
using System.Windows.Navigation;
using System.Windows.Shapes;
using IBL.BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBL.IBL bl;
        public MainWindow()
        {
            bl = new IBL.BL();
            InitializeComponent();
        }

        private void ClickToShowDroneList(object sender, RoutedEventArgs e)
        {
            DronesListView.ItemsSource = bl.GetTheListOfDrones();
            new DroneListWindow(ref bl).Show();
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
            WieghtCombo.SelectedIndex = 0;
        }

        private void SituationCombo_Initialized(object sender, EventArgs e)
        {
            SituationCombo.ItemsSource = Enum.GetValues(typeof(EnumBO.Situations));
            SituationCombo.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClearFilter(object sender, RoutedEventArgs e)
        {
            DronesListView.ItemsSource = bl.GetTheListOfDrones();
            
            SituationCombo.SelectedIndex = 0;
            WieghtCombo.SelectedIndex = 0;
 
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DronesListView.ItemsSource = bl.GetAllDronesBy
                (D => D.status == (EnumBO.DroneStatus)SituationCombo.SelectedItem 
                && D.weight == (EnumBO.WeightCategories)WieghtCombo.SelectedItem);

        }
    }
}
