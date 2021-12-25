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
    public partial class Drone : Window
    {
        IBL.BO.BL bl;
        
        public Drone(IBL.BO.BL bldrone)//הוספה
        {
            InitializeComponent();
            bl = bldrone;
            GridAddDrone.Visibility = Visibility.Visible;
            GridUpdateDrone.Visibility = Visibility.Hidden;

        }
        public Drone(IBL.BO.DroneToList drone,IBL.BO.BL bldrone)//עדכון 
        {
            bl = bldrone;
            InitializeComponent();
            TextIDUpdateDrone.Text = drone.Idnumber.ToString();
            TextIDUpdateDrone.IsEnabled = false;
            TextButteryUpdateDrone.Text = drone.ButerryStatus.ToString();
            TextButteryUpdateDrone.IsEnabled = false;
            ComboBoxMaxWeightUpdateDrone.SelectedItem = drone.Weightcategories;
            ComboBoxMaxWeightUpdateDrone.IsEnabled = false;
            ComboBoxStatusUpdateDrone.SelectedItem = drone.DroneStatuses;
            ComboBoxStatusUpdateDrone.IsEnabled = false;
            TextLatitudeUpdateDrone.Text = drone.ThisLocation.Lattitude.ToString();
            TextLatitudeUpdateDrone.IsEnabled = false;
            TextLongitudeUpdateDrone.Text = drone.ThisLocation.Longitude.ToString();
            TextLongitudeUpdateDrone.IsEnabled = false;
            TextModelUpdateDrone.Text = "";
            TextModelUpdateDrone.IsEnabled = true;
            GridAddDrone.Visibility = Visibility.Hidden;
            GridUpdateDrone.Visibility = Visibility.Visible;



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
                MessageBox.Show((string)ComboBoxMaxWeight.SelectedItem);
        }

        private void TextModel_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ComboBoxStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show((string)ComboBoxStatus.SelectedItem);
        }

        

        private void Button_ClikUpdateDrone(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_ClickAddDrone(object sender, RoutedEventArgs e)
        {
            IBL.BO.Drone drone = new IBL.BO.Drone();
            drone.IdDrone = int.Parse(TextID.Text);
            drone.Model = TextModel.Text;
            drone.ButerryStatus = double.Parse(TextButtery.Text);
            drone.ThisLocation.Longitude = double.Parse(TextLongitude.Text);
            drone.ThisLocation.Lattitude = double.Parse(TextLatitude.Text);
            drone.Weightcategories = (IBL.BO.Weightcategories)ComboBoxMaxWeight.SelectedItem;//weight
            bl.
           
            //להוסיף אחר כך add
            //TextID.IsEnabled = false;
        }
    }
}
