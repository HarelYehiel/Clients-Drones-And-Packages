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
using BO;


namespace PL
{
    /// <summary>
    /// Interaction logic for ParcelWindow.xaml
    /// </summary>
    public partial class ParcelWindow : Window
    {
        BlApi.IBL bl;
        BO.ParcelToList parcel = new BO.ParcelToList();
        public ParcelWindow(BlApi.IBL bl1, BO.ParcelToList Parcel1)
        {
            bl = bl1;
            parcel = Parcel1;
            InitializeComponent();
            txtId.IsEnabled = false;
            txtSender.IsEnabled = false;
            txtTarget.IsEnabled = false;
            comboWeight.IsEnabled = false;
            comboPriority.IsEnabled = false;


            if (Parcel1 != null)
            {
                title.Content = "update Parcel";
                txtId.Text = Parcel1.uniqueID.ToString();
                txtSender.Text = Parcel1.namrSender;
                txtTarget.Text = Parcel1.nameTarget;
                comboWeight.SelectedIndex = (int)Parcel1.weight;
                comboPriority.SelectedIndex = (int)Parcel1.priority;
                Add.Content = "Update";
                OptinalCustomer.Visibility = Visibility.Hidden;
                LabelCustomers.Visibility = Visibility.Hidden;
                UpdateBorder.Visibility = Visibility.Hidden;
            }
            else
            {
                title.Content = "The Parcel detleted!";
                txtId.Foreground = new SolidColorBrush(Colors.Red);
                txtId.Background = new SolidColorBrush(Colors.White);
                txtId.Text = "DELETED";
                txtSender.Background = new SolidColorBrush(Colors.White);
                txtSender.Foreground = new SolidColorBrush(Colors.Red);
                txtSender.Text = "DELETED";
                txtTarget.Background = new SolidColorBrush(Colors.White);
                txtTarget.Foreground = new SolidColorBrush(Colors.Red);
                txtTarget.Text = "DELETED";
                Add.Visibility = Visibility.Hidden;
                OptinalCustomer.Visibility = Visibility.Hidden;
                LabelCustomers.Visibility = Visibility.Hidden;
                UpdateBorder.Visibility = Visibility.Hidden;
            }
        }
        public ParcelWindow(BlApi.IBL bl1)
        {
            bl = bl1;
            InitializeComponent();
            OptinalCustomer.Visibility = Visibility.Hidden;
            LabelCustomers.Visibility = Visibility.Hidden;
            UpdateBorder.Visibility = Visibility.Hidden;
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

        private void create_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Add.Content.ToString() == "Create")
                {
                    if (!isNumber(txtTarget.Text) || !isNumber(txtSender.Text) || comboWeight.SelectedIndex == -1 || comboPriority.SelectedIndex == -1)
                        throw new MyExeption_BO("Error");

                    int ID = Convert.ToInt32(txtId.Text);
                    int SenderId = Convert.ToInt32(txtSender.Text);
                    int TargetId = Convert.ToInt32(txtTarget.Text);
                    bl.ReceiptOfPackageForDelivery(ID, SenderId, TargetId, comboWeight.SelectedIndex, comboPriority.SelectedIndex);
                    this.Close();
                }
                else if (Add.Content.ToString() == "Update")
                {
                    UpdateBorder.Visibility = Visibility.Visible;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Note that you have filled in all the fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtSender_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (LabelCustomers.Visibility == Visibility.Hidden && OptinalCustomer.Visibility == Visibility.Hidden)
            {
                LabelCustomers.Visibility = Visibility.Visible;
                OptinalCustomer.Visibility = Visibility.Visible;
                OptinalCustomer.ItemsSource = bl.GetListOfCustomers();
            }
        }

        private void txtTarget_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (LabelCustomers.Visibility is Visibility.Hidden && OptinalCustomer.Visibility is Visibility.Hidden)
            {
                LabelCustomers.Visibility = Visibility.Visible;
                OptinalCustomer.Visibility = Visibility.Visible;
                OptinalCustomer.ItemsSource = bl.GetListOfCustomers();
            }
        }

        private void txtId_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (LabelCustomers.Visibility is Visibility.Visible && OptinalCustomer.Visibility is Visibility.Visible)
            {
                LabelCustomers.Visibility = Visibility.Hidden;
                OptinalCustomer.Visibility = Visibility.Hidden;
            }
        }

        private void comboWeight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LabelCustomers.Visibility is Visibility.Visible && OptinalCustomer.Visibility is Visibility.Visible)
            {
                LabelCustomers.Visibility = Visibility.Hidden;
                OptinalCustomer.Visibility = Visibility.Hidden;
            }
        }

        private void comboPriority_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LabelCustomers.Visibility is Visibility.Visible && OptinalCustomer.Visibility is Visibility.Visible)
            {
                LabelCustomers.Visibility = Visibility.Hidden;
                OptinalCustomer.Visibility = Visibility.Hidden;
            }
        }


        private void updatecollection_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                parcel.parcelsituation = BO.EnumBO.Situations.collected;
                bl.updateParcel(parcel.uniqueID, 1);
                Close();
            }
            catch (Exception)
            {
                updatecollection.IsChecked = false;
                MessageBox.Show("This update is unlegal", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void updateDelivered_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                parcel.parcelsituation = BO.EnumBO.Situations.provided;
                bl.updateParcel(parcel.uniqueID, 2);
                Close();
            }
            catch (Exception)
            {
                updateDelivered.IsChecked = false;
                MessageBox.Show("This update is unlegal", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void comboWeight_Initialized(object sender, EventArgs e)
        {
            comboWeight.ItemsSource = Enum.GetValues(typeof(BO.EnumBO.WeightCategories));
        }

        private void comboPriority_Initialized(object sender, EventArgs e)
        {
            comboPriority.ItemsSource = Enum.GetValues(typeof(BO.EnumBO.Priorities));

        }

        private void deleteParcel_Click(object sender, RoutedEventArgs e)
        {
            bl.DelParcel(parcel.uniqueID);
            this.Close();
        }
    }
}
