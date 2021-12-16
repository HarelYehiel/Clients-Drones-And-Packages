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
                txtId.IsEnabled = false;
                txtSender.Background = new SolidColorBrush(Colors.White);
                txtSender.Foreground = new SolidColorBrush(Colors.Red);
                txtSender.Text = "DELETED";
                txtSender.IsEnabled = false;
                txtTarget.Background = new SolidColorBrush(Colors.White);
                txtTarget.Foreground = new SolidColorBrush(Colors.Red);
                txtTarget.Text = "DELETED";
                txtTarget.IsEnabled = false;
                comboWeight.IsEnabled = false;
                comboPriority.IsEnabled = false;
                Add.Visibility = Visibility.Hidden;
                OptinalCustomer.Visibility = Visibility.Hidden;
                LabelCustomers.Visibility = Visibility.Hidden;
                UpdateBorder.Visibility = Visibility.Hidden;
            }
            //if (Parcel1 == null)
            //    this.Close();

        }
        public ParcelWindow(BlApi.IBL bl1)
        {
            bl = bl1;
            InitializeComponent();
            OptinalCustomer.Visibility = Visibility.Hidden;
            LabelCustomers.Visibility = Visibility.Hidden;
            UpdateBorder.Visibility = Visibility.Hidden;
        }

        private void create_Click(object sender, RoutedEventArgs e)
        {
            if (Add.Content.ToString() == "Create")
            {
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
            parcel.parcelsituation = BO.EnumBO.Situations.collected;
            /////////// צריך להוסיף פונקציית עדכון חבילה 
        }

        private void updateDelivered_Checked(object sender, RoutedEventArgs e)
        {
            parcel.parcelsituation = BO.EnumBO.Situations.provided;
            /////////// צריך להוסיף פונקציית עדכון חבילה 
        }
        private void updateAssociated_Checked(object sender, RoutedEventArgs e)
        {
            parcel.parcelsituation = BO.EnumBO.Situations.associated;
            /////////// צריך להוסיף פונקציית עדכון חבילה
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
