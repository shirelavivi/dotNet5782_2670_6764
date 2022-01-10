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
using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class DroneWindows : Window
    {
        
        IBL blDrone;
        BO.DroneToList droneWind;
        public DroneWindows(IBL bldrone)//הוספה
        {
            InitializeComponent();
            blDrone = bldrone;
            GridAddDrone.Visibility = Visibility.Visible;
            GridUpdateDrone.Visibility = Visibility.Hidden;
            //ComboBoxStatus.Text = IBL.BO.DroneStatuses.available.ToString();
            comboboxstion.ItemsSource = bldrone.GetALLStationWithFreeStation().Select(x => x.idnumber);
            //ComboBoxMaxWeight.ItemsSource =IBL.BO.Weightcategories;
           

        }

        private void FullDrone(BO.DroneToList drone)
        {
            TextIDUpdateDrone.Text = drone.Idnumber.ToString();
            TextIDUpdateDrone.IsEnabled = false;
            TextButteryUpdateDrone.Text = drone.ButerryStatus.ToString()+"%";
            TextButteryUpdateDrone.IsEnabled = false;      
            comboboxUpDate.Text = drone.Weightcategories.ToString();
            comboboxUpDate.IsEnabled = false;
            ComboBoxStatusUpdateDrone.Text = drone.DroneStatuses.ToString();
            ComboBoxStatusUpdateDrone.IsEnabled = false;
            TextLatitudeUpdateDrone.Text = drone.ThisLocation.Lattitude.ToString();
            TextLatitudeUpdateDrone.IsEnabled = false;
            TextLongitudeUpdateDrone.Text = drone.ThisLocation.Longitude.ToString();
            TextLongitudeUpdateDrone.IsEnabled = false;
            TextModelUpdateDrone.Text = "";
            TextModelUpdateDrone.IsEnabled = true;
            TextDeliveryUpdateDrone.Text = drone.PackageNumberTransferred.ToString();
            TextDeliveryUpdateDrone.IsEnabled = false;
            TextModelUpdateDrone.Text = drone.Model.ToString();
        }

        public DroneWindows(BO.DroneToList drone, IBL bldrone)//עדכון 
        {
            InitializeComponent();
            GridAddDrone.Visibility = Visibility.Hidden;
            GridUpdateDrone.Visibility = Visibility.Visible;
            droneWind = drone;
            blDrone = bldrone;
            if (drone.DroneStatuses != BO.DroneStatuses.maintenance)
            {
                btnSendingDroneForCharging.Content = "Sending Drone For Charging";
               
                saildTaimer.Visibility = Visibility.Hidden;
                if (drone.PackageNumberTransferred == 0)
                {
                    btnSentDrone.Visibility = Visibility.Visible;
                    btnCollectionParcel.Visibility = Visibility.Hidden;
                }
                else
                {
                    btnCollectionParcel.Visibility = Visibility.Visible;

                }
                lablTimer.Visibility = Visibility.Hidden;
            }
            else
            {
                btnSendingDroneForCharging.Content = "Out From Charge";
                saildTaimer.Visibility = Visibility.Visible;
                if (drone.PackageNumberTransferred == 0)
                {
                    btnSentDrone.Visibility = Visibility.Visible;
                    btnCollectionParcel.Visibility = Visibility.Hidden;
                }
                else
                {
                    btnSentDrone.Visibility = Visibility.Visible;
                }
                lablTimer.Visibility = Visibility.Visible;


            }
           
            if (drone.DroneStatuses != BO.DroneStatuses.transport)
                btnCollectionParcel.Content = "Collection Parcel";
            else
            {
                btnCollectionParcel.Content = "Supply Parcel";
            }
            

            FullDrone(drone);

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MessageBox.Show(e.NewValue.ToString());
        }



        private void ComboBoxMaxWeight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextModel_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_ClikUpdateDrone(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TextModelUpdateDrone.Text != "")
                {
                    blDrone.UpdateDrone(Convert.ToInt32(TextIDUpdateDrone.Text), TextModelUpdateDrone.Text);
                    MessageBox.Show("The modles drone is update");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("pleas typ the drone modle");
                }
            }
            catch(BO.MissingIdException)
            {
                MessageBox.Show("Erorr ID");
            }

        }
        private void Button_ClickAddDrone(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Drone drone = new BO.Drone();
                drone.IdDrone = int.Parse(TextID.Text);
                drone.Model = TextModel.Text;
                //drone.Weightcategories = (IBL.BO.Weightcategories)ComboBoxMaxWeight.SelectedItem;
                drone.ThisLocation = new BO.Location();
                //drone.PackageInTransfer = new IBL.BO.ParcelInTransfer();//לא נותן
                //drone.DroneStatuses = (IBL.BO.DroneStatuses)ComboBoxStatus.SelectedItem;
                blDrone.AddDrone(drone, Convert.ToInt32(comboboxstion.SelectedItem));
                MessageBox.Show("The Drone Was Successfully Added");
                this.Close();
                DroneListWindows droneListWindow = new DroneListWindows(blDrone);
                droneListWindow.Show();
               
            }
            //לגבי התז של תחנה בחרנו מתחנות פנויות אז לא אמורה להיות בעיה
            //catch (IBL.BO.MissingIdException ex)
            //{
            //     MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
            catch (BO.DuplicateIdException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        } 
       
        private void ComboBoxS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
        }

        private void btnSendingDroneForCharging_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (btnSendingDroneForCharging.Content.ToString() == "Sending Drone For Charging")
                {
                    blDrone.SendingDroneforCharging(droneWind.Idnumber);
                    MessageBox.Show("The drone sending for charge");
                    btnSendingDroneForCharging.Content = "Out From Charge";
                    btnSentDrone.Visibility = Visibility.Hidden;
                    btnCollectionParcel.Visibility = Visibility.Hidden;          
                    saildTaimer.Visibility = Visibility.Visible;
                    lablTimer.Visibility = Visibility.Visible;

                }
                else
                {

                    blDrone.ReleaseDroneFromChargeStation(droneWind.Idnumber,Convert.ToInt32(saildTaimer.Value));
                    MessageBox.Show("The drone realse form charge");
                    btnSendingDroneForCharging.Content = "Sending Drone For Charging";
                    FullDrone(blDrone.GetDroneToList(droneWind.Idnumber));
                    btnSentDrone.Visibility = Visibility.Visible;
                    saildTaimer.Visibility = Visibility.Hidden;
                    lablTimer.Visibility = Visibility.Hidden;
                }
                
            }
            catch (BO.UnsuitableDroneMode)
            {
                MessageBox.Show("Can not be shipped for loading in the middle of making a shipment!", "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.MissingIdException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.DuplicateIdException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)//שליחת רחפן למשלוח
        {
            try
            {
                blDrone.ConnectParcelToDrone(Convert.ToInt32(TextIDUpdateDrone.Text));//אם נזרקת כאן חריגה זה אומר שאין חבילה מתאימה
                FullDrone(blDrone.GetDroneToList(Convert.ToInt32(TextIDUpdateDrone.Text)));
                if (TextDeliveryUpdateDrone.Text.ToString() == "0")
                {
                    MessageBox.Show("No suitable package was found for example by this skimmer!");
                }
                else
                {
                    btnCollectionParcel.Visibility = Visibility.Visible;
                    saildTaimer.Visibility = Visibility.Hidden;
                    lablTimer.Visibility = Visibility.Hidden;
                    btnSentDrone.Visibility = Visibility.Hidden;
                }
            }
            catch (BO.MissingIdException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.DuplicateIdException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO .UnsuitableDroneMode ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnCollectionParcel_Click(object sender, RoutedEventArgs e)//איסוף חבילה על ידי רחפן
        {
            try
            {
                if (btnCollectionParcel.Content.ToString() == "Collection Parcel")
                {
                    blDrone.PickUpPackage(Convert.ToInt32(TextIDUpdateDrone.Text));
                    FullDrone(blDrone.GetDroneToList(Convert.ToInt32(TextIDUpdateDrone.Text)));
                    btnCollectionParcel.Content = "Supply Parcel";
                }
                else
                {
                    blDrone.SupplyParcelByDrone(Convert.ToInt32(TextIDUpdateDrone.Text));
                    FullDrone(blDrone.GetDroneToList(Convert.ToInt32(TextIDUpdateDrone.Text)));
                    btnCollectionParcel.Content = "Collection Parcel";
                }
            }
            catch (BO.MissingIdException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.DuplicateIdException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void comboboxUpDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextButteryUpdateDrone_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (Convert.ToInt32(TextButteryUpdateDrone.Text) - (int)'0' - (int)'%' < 15)
            //    TextButteryUpdateDrone.Background.g
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
            DroneListWindows droneListWindow = new DroneListWindows(blDrone);
            droneListWindow.Show();
        }
    }
}
