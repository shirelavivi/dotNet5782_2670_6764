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
using BlApi;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for ParcelWindow.xaml
    /// </summary>
    public partial class ParcelWindow : Window
    {
        IBL blparcel;
        BO.Parcel parcel1;
        public ParcelWindow(BO.ParcelToList parcel, IBL blParcel)//עדכון
        {
            InitializeComponent();
            try
            {  
                blparcel = blParcel;
                btnAdd.Visibility = Visibility.Hidden;
                parcel1 = blParcel.GetParcel(parcel.Id);
                gridUpDate.DataContext=blparcel.GetParcel(parcel.Id);
                gridAdd.Visibility = Visibility.Hidden;
                if (parcel1.Scheduled != null && parcel1.PickedUp == null)
                    btnUpDate.Content = "Pick Up";
                else
                { 
                    if (parcel1.PickedUp != null && parcel1.Delivered==null)
                        btnUpDate.Content = "Deliverd";
                    else
                        btnUpDate.Visibility = Visibility.Hidden;
                }

            }
            catch (BO.MissingIdException ex)
            {
                MessageBox.Show(ex.EntityName,ex.Message);
            }
        }
        public ParcelWindow(IBL blParcel)//הוספה
        {
            InitializeComponent();
            blparcel = blParcel;
            btnDelet.Visibility = Visibility.Hidden;
            btnUpDate.Visibility = Visibility.Hidden;
            gridUpDate.Visibility = Visibility.Hidden;
            ShowDrone.Visibility = Visibility.Hidden;
            ShowSender.Visibility = Visibility.Hidden;
            ShowTraget.Visibility = Visibility.Hidden;
           



        }

    private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            ParcelToListWindow ParcelWindow = new ParcelToListWindow(blparcel);
            ParcelWindow.Show();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)// הוספת חבילה חדשה 
        {
            try
            {
                BO.Parcel parcel = new Parcel();
                parcel.Priority = (BO.Priorities)priorityComboBox.SelectedIndex;
                parcel.Weight = (BO.Weightcategories)weightComboBox.SelectedIndex;
                if (blparcel.GetALLParce(x => x.TargetName == targetNameTextBox.Text) == null)
                {
                    MessageBox.Show("The target name is not found");
                    return;
                }
                if (blparcel.GetALLParce(x => x.SenderName == senderNameTextBox.Text) == null)
                {
                    MessageBox.Show("The sender name is not found");
                    return;
                }
                parcel.Sender = new CustomerAtParcels();
                parcel.Sender.Id =int.Parse(senderNameTextBox.Text);
                parcel.Sender.Name =blparcel.GetCustomer( int.Parse(senderNameTextBox.Text)).Name;
                parcel.Target = new CustomerAtParcels();
                parcel.Target.Id = int.Parse(targetNameTextBox.Text);
                parcel.Target.Name = blparcel.GetCustomer(int.Parse(targetNameTextBox.Text)).Name;
                blparcel.AddrParcel(parcel);
                MessageBox.Show("Addion parcel");
                this.Close();
                ParcelToListWindow ParcelWindow = new ParcelToListWindow(blparcel);
                ParcelWindow.Show();

            }
            catch (BO.MissingIdException ex)
            {
                MessageBox.Show(ex.EntityName, ex.Message);
            }

        }

        private void btnDelet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                blparcel.RemoveParcel(Convert.ToInt32(idTextBox.Text));
                MessageBox.Show("Removed");
                this.Close();
                ParcelToListWindow ParcelWindow = new ParcelToListWindow(blparcel);
                ParcelWindow.Show();
            }
            catch (BO.MissingIdException ex)
            {
                MessageBox.Show(ex.EntityName, ex.Message);
            }
        }

        private void ShowDrone_Click(object sender, RoutedEventArgs e)
        {
          
            Details detils = new Details(parcel1.droneAtParcel, blparcel);
            detils.Show();
        }

        private void ShowSender_Click(object sender, RoutedEventArgs e)
        {
           
            Details detils = new Details(parcel1.Sender, blparcel);
            detils.Show();
        }

        private void ShowTraget_Click(object sender, RoutedEventArgs e)
        {
            
            Details detils = new Details(parcel1.Target, blparcel);
            detils.Show();
        }

        private void btnUpDate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (btnUpDate.Content.ToString() == "Pick Up")
                {
                    blparcel.PickUpPackage(int.Parse(idTextBox.Text));
                    MessageBox.Show("The drone go to way");
                    this.Close();
                    ParcelToListWindow ParcelWindow = new ParcelToListWindow(blparcel);
                    ParcelWindow.Show();
                }
                else
                {
                    blparcel.SupplyParcelByDrone(int.Parse(idNumberTextBox.Text));
                    MessageBox.Show("The drone come to target");
                    this.Close();
                    ParcelToListWindow ParcelWindow = new ParcelToListWindow(blparcel);
                    ParcelWindow.Show();
                }
            }
            catch (BO.MissingIdException ex)
            {
                MessageBox.Show(ex.EntityName, ex.Message);
            }
        }

        private void weightComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //weightComboBox.SelectedItem = weightComboBox.SelectedIndex;
        }
    }
}
