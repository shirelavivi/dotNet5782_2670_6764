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
    /// Interaction logic for DroneList.xaml
    /// </summary>
    public partial class DroneListWindose : Window
    {
        IBL.BO.BL blDroneList;
        public DroneListWindose(IBL.BO.BL bl)
        {
            blDroneList = bl;
            InitializeComponent();
            droneToListDataGrid.DataContext = bl.GetALLDroneToList();
        }

        private void comboBoxWeightstatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            droneToListDataGrid.DataContext = blDroneList.GetALLDroneToList(dro => dro.DroneStatuses == (IBL.BO.DroneStatuses)comboBoxWeightstatus.SelectedIndex);
        
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DroneWindose droneListWindow = new DroneWindose(blDroneList);
            droneListWindow.Show();
        }

        private void droneToListDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            IBL.BO.DroneToList dr = droneToListDataGrid.SelectedItem as IBL.BO.DroneToList;
            if (dr != null)
            {
                DroneWindose droneListWindow = new DroneWindose(dr, blDroneList);
                droneListWindow.Show();
            }
        }


        private void comboBoxWeight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            droneToListDataGrid.DataContext = blDroneList.GetALLDroneToList(dro => dro.Weightcategories == (IBL.BO.Weightcategories)comboBoxWeight.SelectedIndex);
        }
    }

}
