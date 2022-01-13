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
            UpdateStationButton.Visibility = Visibility.Hidden;

        }
        public BaseStationWindow(BO.BaseStationToList baseStation, IBL blstation)//update
        {
            InitializeComponent();
            blStation = blstation;
            AddStationButton.Visibility = Visibility.Hidden;
            stationWind = baseStation;
            FullStation(baseStation);
        }
        private void FullStation(BO.BaseStationToList basestation)
        {
            IdnumberTextBox.Text = basestation.idnumber.ToString();
            NameStationTextBox.Text = basestation.nameStation.ToString();
            ChargingAvailableTextBox.Text = basestation.chargingAvailable.ToString();
            ChargingNotAvailableTextBox.Text = basestation.chargingNotAvailable.ToString();
            //צריך לעשות isEnable?
        }

        private void Button_ClikUpdateStation(object sender, RoutedEventArgs e)
        {
            try
            {
                if (NameStationTextBox.Text != "")
                {
                    blStation.UpdStation(Convert.ToInt32(NameStationTextBox.Text), ChargingAvailableTextBox.Text);
                    MessageBox.Show("The station is update");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("please typ name of station");
                }
            }
            catch (BO.MissingIdException)
            {
                MessageBox.Show("Erorr ID");
            }
        }


        private void AddStationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.BaseStation baseStation = new BO.BaseStation();
                baseStation.Idnumber = int.Parse(IdnumberTextBox.Text);
                baseStation.NameStation = NameStationTextBox.Text;
                baseStation.ChargingAvailable = int.Parse(ChargingAvailableTextBox.Text);
                //baseStation.droneInCharging = new BO.DroneInParcel();
                baseStation.locationOfStation = new BO.Location();
                blStation.AddBaseStation(baseStation);
                MessageBox.Show("The Station Was Successfully Added");
                this.Close();
                BaseStationWindow baseStationtWindow = new BaseStationWindow(blStation);
                baseStationtWindow.Show();

            }
            catch (BO.DuplicateIdException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
