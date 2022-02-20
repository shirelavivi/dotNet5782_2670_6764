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
    /// Interaction logic for BaseStationWindow.xaml
    /// </summary>
    public partial class BaseStationWindow : Window
    {
        IBL blStation;
        BO.BaseStationToList stationWind;
        public BaseStationWindow(IBL blstation)//add
        {
            InitializeComponent();
            blStation = blstation;
            gridUodate.Visibility = Visibility.Hidden;

        }
        public BaseStationWindow(BO.BaseStationToList baseStation, IBL blstation)//update
        {
            InitializeComponent();
            blStation = blstation;
            gridAdd.Visibility = Visibility.Hidden;
            stationWind = baseStation;
            grid1_Copy.DataContext = blStation.GetBaseStation(baseStation.idnumber);
            droneInChargingDataGrid.DataContext = blStation.GetBaseStation(baseStation.idnumber).droneInCharging;

        }



        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                blStation.UpdStation(Convert.ToInt32(idnumberTextBoxUpDate.Text), nameStationTextBoxUpDate.Text, Convert.ToInt32(chargingAvailableTextBoxUpDate.Text));
                MessageBox.Show("The Station Was Successfully Update");
                this.Close();
                BaseStationToListWindow customerToList = new BaseStationToListWindow(blStation);
                customerToList.Show();
            }
            catch (BO.MissingIdException ex)
            {
                MessageBox.Show(ex.ToString(), " ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        } 

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.BaseStation baseStation = new BO.BaseStation();
                baseStation.Idnumber = Convert.ToInt32(idnumberTextBox.Text);
                baseStation.NameStation = nameStationTextBox.Text;
                baseStation.ChargingAvailable = Convert.ToInt32(chargingAvailableTextBox.Text);
                baseStation.locationOfStation = new BO.Location();
                baseStation.locationOfStation.Lattitude = Convert.ToInt32(lattitudeTextBox.Text);
                baseStation.locationOfStation.Longitude = Convert.ToInt32(longitudeTextBox.Text);
                blStation.AddBaseStation(baseStation);
                MessageBox.Show("The Station Was Successfully Added");
                this.Close();
                BaseStationToListWindow customerToList = new BaseStationToListWindow(blStation);
                customerToList.Show();
            }
            catch ( BO.DuplicateIdException ex)
            {
                MessageBox.Show(ex.ToString()," ",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
    }
}

