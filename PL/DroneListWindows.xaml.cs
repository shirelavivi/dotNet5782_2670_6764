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

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneList.xaml
    /// </summary>
    public partial class DroneListWindows : Window
    {
        IBL blDroneList;
        public DroneListWindows(IBL bl)
        {           
            InitializeComponent();
            blDroneList = bl;
            droneToListDataGrid.DataContext = bl.GetALLDroneToList();
        }

        private void comboBoxWeightstatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            droneToListDataGrid.DataContext = blDroneList.GetALLDroneToList(dro => dro.DroneStatuses == (BO.DroneStatuses)comboBoxWeightstatus.SelectedIndex);
        
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DroneWindows droneListWindow = new DroneWindows(blDroneList);
            droneListWindow.Show();
            this.Close();
        }

        private void droneToListDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            BO.DroneToList dr = droneToListDataGrid.SelectedItem as BO.DroneToList;
            if (dr != null)
            {
                DroneWindows droneListWindow = new DroneWindows(dr, blDroneList);
                droneListWindow.Show();
                this.Close();
            }
        }
        public void help_function()
        {
            if (comboBoxWeight.SelectedItem == null && comboBoxWeightstatus.SelectedItem == null)
                droneToListDataGrid.DataContext = blDroneList.GetALLDroneToList();
            if (comboBoxWeight.SelectedItem != null && comboBoxWeightstatus.SelectedItem == null)
                droneToListDataGrid.DataContext = blDroneList.GetALLDroneToList(dro => dro.Weightcategories == (BO.Weightcategories)comboBoxWeight.SelectedIndex);
            if (comboBoxWeight.SelectedItem == null && comboBoxWeightstatus.SelectedItem != null)
                droneToListDataGrid.DataContext = blDroneList.GetALLDroneToList(dro => dro.DroneStatuses == (BO.DroneStatuses)comboBoxWeightstatus.SelectedIndex);
            if (comboBoxWeight.SelectedItem != null && comboBoxWeightstatus.SelectedItem != null)
            {
                droneToListDataGrid.DataContext = blDroneList.GetALLDroneToList(dro => dro.DroneStatuses == (BO.DroneStatuses)comboBoxWeightstatus.SelectedIndex && dro.Weightcategories == (BO.Weightcategories)comboBoxWeight.SelectedIndex);
            }
        }

        private void comboBoxWeight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            help_function();

            //droneToListDataGrid.DataContext = blDroneList.GetALLDroneToList(dro => dro.Weightcategories == (IBL.BO.Weightcategories)comboBoxWeight.SelectedIndex);
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            help_function();
        }
    }

}
