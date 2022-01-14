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
    /// Interaction logic for BaseStationToListWindow.xaml
    /// </summary>
    public partial class BaseStationToListWindow : Window
    {
        IBL blstationList;
        public BaseStationToListWindow(IBL bl)
        {
            InitializeComponent();
            blstationList = bl;
            baseStationToListDataGrid.DataContext = bl.GetALLbaseStationToList();
        }
      

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            help_function();
        }

        private void chekd_Checked(object sender, RoutedEventArgs e)
        {
            help_function();
        }
        public void help_function()
        {
            if(chekd.IsChecked==true&&txtNumber.Text=="")
                baseStationToListDataGrid.DataContext = blstationList.GetAllStation(x => x.chargingAvailable > 0);
            if(txtNumber.Text!="")
               baseStationToListDataGrid.DataContext = blstationList.GetAllStation(x => x.chargingAvailable > Convert.ToInt32(txtNumber.Text));
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            help_function();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BaseStationWindow baseStationToListWindow = new BaseStationWindow(blstationList);
            baseStationToListWindow.Show();
            this.Close();
        }

        private void baseStationToListDataGrid_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            BO.BaseStationToList station = baseStationToListDataGrid.SelectedItem as BO.BaseStationToList;
            if (station != null)
            {
                BaseStationWindow baseStationToListWindow = new BaseStationWindow(station, blstationList);
                baseStationToListWindow.Show();
                this.Close();
            }
        }
    }
}


