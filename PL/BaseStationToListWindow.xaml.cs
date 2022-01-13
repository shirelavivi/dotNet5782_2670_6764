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
      
        private void baseStationToListDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            BO.BaseStationToList station = baseStationToListDataGrid.SelectedItem as BO.BaseStationToList;
            if (station != null)
            {
                BaseStationToListWindow baseStationToListWindow = new BaseStationToListWindow(blstationList);
                baseStationToListWindow.Show();
                this.Close();
            }
        }
    }
}

