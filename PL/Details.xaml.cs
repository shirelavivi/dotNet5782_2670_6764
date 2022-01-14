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
    /// Interaction logic for Details.xaml
    /// </summary>
    public partial class Details : Window
    {
        //IBL bl;
        public Details()
        {
            InitializeComponent();
        }
        public Details(BO.DroneInParcel droneInParcel, IBL bl)
        {
            InitializeComponent();
            gridCustomer.Visibility = Visibility.Hidden;
            gridDrone.DataContext = droneInParcel;
        }
        public Details(BO.CustomerAtParcels customerAtParcels, IBL bl)
        {
            InitializeComponent();
            gridDrone.Visibility = Visibility.Hidden;
            gridCustomer.DataContext = customerAtParcels;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
