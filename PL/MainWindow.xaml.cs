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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BlApi;
using BO;




namespace PL
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBL bl;
        public MainWindow()
        {
            try
            {  bl = BlFactory.GetBl(); }//ככה?

            catch (Exception e)
            { MessageBox.Show(e.Message); }
            InitializeComponent();
           
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DroneListWindows droneListWindow = new DroneListWindows(bl);
            droneListWindow.Show();
        }

        private void Button_Click_Customers(object sender, RoutedEventArgs e)
        {
            CustomerToListWindow customerToListWindow = new CustomerToListWindow(bl);
            customerToListWindow.Show();
        }

        private void btnParcels_Click(object sender, RoutedEventArgs e)
        {
            ParcelToListWindow parcelListWindow = new ParcelToListWindow(bl);
            parcelListWindow.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            BaseStationToListWindow stationListWindow = new BaseStationToListWindow(bl);
            stationListWindow.Show();
        }
    }
}
