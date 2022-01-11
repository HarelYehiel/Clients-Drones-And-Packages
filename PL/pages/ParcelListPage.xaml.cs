using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BO;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PL
{
    /// <summary>
    /// Interaction logic for ParcelsPage.xaml
    /// </summary>
    public partial class ParcelListPage : Page
    {

        BlApi.IBL bl;
        public ParcelListPage(BlApi.IBL bl1)
        {
            bl = bl1;
            InitializeComponent();
            ParcelListView.ItemsSource = bl.DisplaysTheListOfParcels();
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ParcelListView.ItemsSource);
            view.Filter = UserFilter;
            openOptions.Visibility = Visibility.Hidden;
        }

        private void AddNewParcel(object sender, RoutedEventArgs e)
        {
            new ParcelWindow(bl).ShowDialog();
            ParcelListView.ItemsSource = bl.DisplaysTheListOfParcels();
            //CollectionViewSource.GetDefaultView(ParcelListView).Refresh();

        }

        private void ClearFilter(object sender, RoutedEventArgs e)
        {
            ParcelListView.ItemsSource = bl.DisplaysTheListOfParcels();

        }


        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            //this.Close();
            this.Visibility = Visibility.Hidden;

        }
        private void ParcelListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.OriginalSource is GridViewColumnHeader)
            {
                GridViewColumn clickedColumn = (e.OriginalSource as GridViewColumnHeader).Column;
                if (clickedColumn.Header.ToString() == "Sender")
                {
                    CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ParcelListView.ItemsSource);
                    view.SortDescriptions.Add(new SortDescription("Sender", ListSortDirection.Ascending));
                }
                if (clickedColumn.Header.ToString() == "Target")
                {
                    CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ParcelListView.ItemsSource);
                    view.SortDescriptions.Add(new SortDescription("Target", ListSortDirection.Ascending));
                }

            }
            else
            {
                openOptions.Visibility = Visibility.Visible;
                //ParcelListView.ItemsSource = bl.DisplaysTheListOfParcels();
            }
        }
        private void AddFilter(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ParcelListView.ItemsSource);
            view.Filter = UserFilter;
            CollectionView temp = view;
            filterCombo.SelectedIndex = -1;
            txtFilter.Text = "";
            ParcelListView.ItemsSource = temp;
        }
        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            else if (filterCombo.SelectedIndex == 0) //Parcl ID
                return ((item as BO.ParcelToList).uniqueID.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            else if (filterCombo.SelectedIndex == 1) //Sender name
                return ((item as BO.ParcelToList).namrSender.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            else if (filterCombo.SelectedIndex == 2) //Target name
                return ((item as BO.ParcelToList).nameTarget.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            else if (filterCombo.SelectedIndex == 3)// Priority
                return ((item as BO.ParcelToList).priority.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            else if (filterCombo.SelectedIndex == 4)// Weight
                return ((item as BO.ParcelToList).weight.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            else if (filterCombo.SelectedIndex == 5)// Situation
                return ((item as BO.ParcelToList).parcelsituation.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            else
                return true;
        }
        private void txtFilter_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ParcelListView.ItemsSource).Refresh();
        }
        private void filterCombo_Initialized(object sender, EventArgs e)
        {
            List<string> s = new List<string>() { "Parcl ID", "Sender name", "Target name", "Priority", "Weight", "Situation" };
            filterCombo.ItemsSource = s;
        }

        private void filterSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ParcelListView.ItemsSource).Refresh();
        }


        private void OpenParcelView_Click(object sender, RoutedEventArgs e)
        {
            BO.ParcelToList parcel = ParcelListView.SelectedItem as BO.ParcelToList;
            new ParcelWindow(bl, parcel).ShowDialog();
            openOptions.Visibility = Visibility.Hidden;
        }

        private void OpenSenderView_Click(object sender, RoutedEventArgs e)
        {
            BO.ParcelToList parcel = ParcelListView.SelectedItem as BO.ParcelToList;
            List<BO.CustomerToList> lst = bl.GetAllCustomersBy(C => C.name == parcel.namrSender).ToList();
            new CustomerWindow(bl, lst[0]).ShowDialog();
            openOptions.Visibility = Visibility.Hidden;
            //Close();
        }

        private void OpenTargetView_Click(object sender, RoutedEventArgs e)
        {
            BO.ParcelToList parcel = ParcelListView.SelectedItem as BO.ParcelToList;
            List<BO.CustomerToList> lst = bl.GetAllCustomersBy(C => C.name == parcel.nameTarget).ToList();
            new CustomerWindow(bl, lst[0]).ShowDialog();
            openOptions.Visibility = Visibility.Hidden;
           // Close();
        }

        private void OpenDroneView_Click(object sender, RoutedEventArgs e)
        {
            BO.ParcelToList parcel = ParcelListView.SelectedItem as BO.ParcelToList;
            try
            {
                BO.Parcel temp = bl.GetParcel(parcel.uniqueID);
                if (temp.droneInParcel == null)
                    throw new Exception();

                List<BO.DroneToList> lst = bl.GetAllDronesBy(D => D.uniqueID == temp.droneInParcel.uniqueID).ToList();
                new DroneWindow(bl, lst[0]).ShowDialog();
                openOptions.Visibility = Visibility.Hidden;
                //Close();
            }
            catch (Exception)
            {
                MessageBox.Show("this parcel not assign yet to drone", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ComboBox_Initialized(object sender, EventArgs e)
        {
            List<string> l = new List<string>() {
               "Choose",
                "Sender",
                "Target",
                "Prioritiy",
                "Weight",
                "Situation"
            };
            GroupByComboBox.ItemsSource = l;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<ParcelToList> l;

            switch (GroupByComboBox.SelectedIndex)
            {

                case 1://Sender

                    IEnumerable<IGrouping<string, ParcelToList>> tsSender = from item in bl.DisplaysTheListOfParcels()
                                                                            group item by item.namrSender into gs
                                                                            select gs;

                    l = new List<ParcelToList>();
                    foreach (var group1 in tsSender)
                    {
                        foreach (ParcelToList item in group1)
                        {
                            l.Add(item);
                        }
                    }
                    ParcelListView.ItemsSource = l;
                    break;

                case 2: // Target
                    IEnumerable<IGrouping<string, ParcelToList>> tsTarget = from item in bl.DisplaysTheListOfParcels()
                                                                            group item by item.nameTarget into gs
                                                                            select gs;

                    l = new List<ParcelToList>();
                    foreach (var group1 in tsTarget)
                    {
                        foreach (ParcelToList item in group1)
                        {
                            l.Add(item);
                        }
                    }
                    ParcelListView.ItemsSource = l;
                    break;

                case 3: //Prioritiy
                    IEnumerable<IGrouping<EnumBO.Priorities, ParcelToList>> tsPrioritiy = from item in bl.DisplaysTheListOfParcels()
                                                                                          group item by item.priority into gs
                                                                                          select gs;
                    l = new List<ParcelToList>();
                    foreach (var group1 in tsPrioritiy)
                    {
                        foreach (ParcelToList item in group1)
                        {
                            l.Add(item);
                        }
                    }
                    ParcelListView.ItemsSource = l;
                    break;

                case 4: // Weight
                    IEnumerable<IGrouping<EnumBO.WeightCategories, ParcelToList>> tsWeight = from item in bl.DisplaysTheListOfParcels()
                                                                                             group item by item.weight into gs
                                                                                             select gs;
                    l = new List<ParcelToList>();
                    foreach (var group1 in tsWeight)
                    {
                        foreach (ParcelToList item in group1)
                        {
                            l.Add(item);
                        }
                    }
                    ParcelListView.ItemsSource = l;
                    break;

                case 5: //Situation
                    IEnumerable<IGrouping<EnumBO.Situations, ParcelToList>> tsSituation = from item in bl.DisplaysTheListOfParcels()
                                                                                          group item by item.parcelsituation into gs
                                                                                          select gs;
                    l = new List<ParcelToList>();
                    foreach (var group1 in tsSituation)
                    {
                        foreach (ParcelToList item in group1)
                        {
                            l.Add(item);
                        }
                    }
                    ParcelListView.ItemsSource = l;
                    break;

            }
        }

        private void GridViewColumn_Click(object sender, RoutedEventArgs e)
        {

        }
        private void CancelOpenBarButton_Click(object sender, RoutedEventArgs e)
        {
            ParcelListView.SelectedItem = null;
            openOptions.Visibility = Visibility.Hidden;
        }
    }
}
