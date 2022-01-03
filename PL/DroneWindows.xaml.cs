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
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class DroneWindows : Window
    {

        IBL.BO.BL bl;
        IBL.BO.DroneToList droneWind;
        public DroneWindows(IBL.BO.BL bldrone)//הוספה
        {
            InitializeComponent();
            bl = bldrone;
            GridAddDrone.Visibility = Visibility.Visible;
            GridUpdateDrone.Visibility = Visibility.Hidden;
            //ComboBoxStatus.Text = IBL.BO.DroneStatuses.available.ToString();
            comboboxstion.ItemsSource = bldrone.GetALLStationWithFreeStation().Select(x => x.idnumber);
            //ComboBoxMaxWeight.ItemsSource =IBL.BO.Weightcategories;

        }

        private void FullDrone(IBL.BO.DroneToList drone)
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

        public DroneWindows(IBL.BO.DroneToList drone, IBL.BO.BL bldrone)//עדכון 
        {
            InitializeComponent();
            GridAddDrone.Visibility = Visibility.Hidden;
            GridUpdateDrone.Visibility = Visibility.Visible;
            droneWind = drone;
            bl = bldrone;
            if (drone.DroneStatuses != IBL.BO.DroneStatuses.maintenance)
            {
                btnSendingDroneForCharging.Content = "Sending Drone For Charging";
                btnSentDrone.Visibility = Visibility.Visible;
                btnCollectionParcel.Visibility = Visibility.Visible;
                saildTaimer.Visibility = Visibility.Visible;
                lablTimer.Visibility = Visibility.Visible;
            }
            else
            {
                btnSendingDroneForCharging.Content = "Out From Charge";
                btnSentDrone.Visibility = Visibility.Hidden;
                btnCollectionParcel.Visibility = Visibility.Hidden;
                saildTaimer.Visibility = Visibility.Hidden;
                lablTimer.Visibility = Visibility.Hidden;

            }
          
            if (drone.DroneStatuses != IBL.BO.DroneStatuses.transport)
                btnCollectionParcel.Content = "Collection Parce";
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
                    bl.UpdateDrone(Convert.ToInt32(TextIDUpdateDrone.Text), TextModelUpdateDrone.Text);
                    MessageBox.Show("The modles drone is update");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("pleas typ the drone modle");
                }
            }
            catch(IBL.BO.MissingIdException)
            {
                MessageBox.Show("Erorr ID");
            }

        }
        private void Button_ClickAddDrone(object sender, RoutedEventArgs e)
        {
            try
            {
                IBL.BO.Drone drone = new IBL.BO.Drone();
                drone.IdDrone = int.Parse(TextID.Text);
                drone.Model = TextModel.Text;
                //drone.Weightcategories = (IBL.BO.Weightcategories)ComboBoxMaxWeight.SelectedItem;
                drone.ThisLocation = new IBL.BO.Location();
                //drone.PackageInTransfer = new IBL.BO.ParcelInTransfer();//לא נותן
                //drone.DroneStatuses = (IBL.BO.DroneStatuses)ComboBoxStatus.SelectedItem;
                bl.AddDrone(drone, Convert.ToInt32(comboboxstion.SelectedItem));
                MessageBox.Show("The Drone Was Successfully Added");
                this.Close();
            }
            //לגבי התז של תחנה בחרנו מתחנות פנויות אז לא אמורה להיות בעיה
            //catch (IBL.BO.MissingIdException ex)
            //{
            //     MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
            catch (IBL.BO.DuplicateIdException ex)
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
                    bl.SendingDroneforCharging(droneWind.Idnumber);
                    MessageBox.Show("The drone sending for charge");
                    btnSendingDroneForCharging.Content = "Out From Charge";
                   
                }
                else
                {

                    bl.ReleaseDroneFromChargeStation(droneWind.Idnumber,Convert.ToInt32(saildTaimer.Value));
                    MessageBox.Show("The drone realse form charge");
                    btnSendingDroneForCharging.Content = "Sending Drone For Charging";
                    FullDrone(bl.GetDroneToList(droneWind.Idnumber));
                    btnSentDrone.Visibility = Visibility.Visible;
                    btnCollectionParcel.Visibility = Visibility.Visible;
                    saildTaimer.Visibility = Visibility.Visible;
                    lablTimer.Visibility = Visibility.Visible;
                }
                
            }
            catch (IBL.BO.MissingIdException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IBL.BO.DuplicateIdException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            //this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)//שליחת רחפן למשלוח
        {
            try
            {
                bl.ConnectParcelToDrone(Convert.ToInt32(TextIDUpdateDrone.Text));
                FullDrone(bl.GetDroneToList(Convert.ToInt32(TextIDUpdateDrone.Text)));
            }
            catch (IBL.BO.MissingIdException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IBL.BO.DuplicateIdException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCollectionParcel_Click(object sender, RoutedEventArgs e)//איסוף חבילה על ידי רחפן
        {
            try
            {
                bl.PickUpPackage(Convert.ToInt32(TextIDUpdateDrone.Text));
                FullDrone(bl.GetDroneToList(Convert.ToInt32(TextIDUpdateDrone.Text)));
            }
            catch (IBL.BO.MissingIdException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IBL.BO.DuplicateIdException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
