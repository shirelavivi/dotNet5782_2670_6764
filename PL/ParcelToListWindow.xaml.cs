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
    /// Interaction logic for DroneToListWindow.xaml
    /// </summary>
    public partial class DroneToListWindow : Window
    {
         IBL blParcelList; 
        public DroneToListWindow(IBL bl)
        {
            InitializeComponent();
            blParcelList = bl;
            parcelToListDataGrid.DataContext = blParcelList.GetALLParcelToList();
            //IEnumerable<IGrouping<int, int>> result = from w in GetALLParcelToList()
            //                                          group w by w into g
            //                                          select new { FirstLetter = g.Key, Words = g };
            ////var wordGroups = from w in GetALLParcelToList()
            ////                 group w by w into g
            ////                 select new { FirstLetter = g.Key, Words = g };


            //comboBoxSender.ItemsSource= /*GetALLParcelToList().*/
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ParcelWindow ParcelWindow = new ParcelWindow(blParcelList);
            ParcelWindow.Show();
            this.Close();
        }

        private void parcelToListDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            BO.ParcelToList dr = parcelToListDataGrid.SelectedItem as BO.ParcelToList;
            if (dr != null)
            {
                ParcelWindow parcelListWindow = new ParcelWindow(dr, blParcelList);
                parcelListWindow.Show();
                this.Close();
            }
        }
    }
}
