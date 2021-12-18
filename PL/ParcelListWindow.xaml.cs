using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace PL
{
    /// <summary>
    /// Interaction logic for ParclListWindow.xaml
    /// </summary>
    public partial class ParclListWindow : Window
    {
        BlApi.IBL bl;
        public ParclListWindow(BlApi.IBL bl1)
        {
            bl = bl1;
            InitializeComponent();
            ParcelListView.ItemsSource = bl.DisplaysTheListOfParcels();
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ParcelListView.ItemsSource);
            view.Filter = UserFilter;
        }

        private void AddNewParcel(object sender, RoutedEventArgs e)
        {
            new ParcelWindow(bl).ShowDialog();
            ParcelListView.ItemsSource = bl.DisplaysTheListOfParcels();

        }

        private void ClearFilter(object sender, RoutedEventArgs e)
        {
            ParcelListView.ItemsSource = bl.DisplaysTheListOfParcels();

        }

        private void AllFilters(object sender, RoutedEventArgs e)
        {

        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void ParcelListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (ParcelListView.ItemsSource != null)
            {
                
                new ParcelWindow(bl, ParcelListView.SelectedItem as BO.ParcelToList).ShowDialog();
            }
            ParcelListView.ItemsSource = bl.DisplaysTheListOfParcels();
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
            List<string> s = new List<string>() { "Parcl ID" , "Sender name", "Target name", "Priority", "Weight", "Situation" };
            filterCombo.ItemsSource = s;
        }
        
        private void filterSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ParcelListView.ItemsSource).Refresh();
        }
    }
    
}
