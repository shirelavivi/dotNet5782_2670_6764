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
    public partial class ParcelToListWindow : Window
    {
        IBL blParcelList;
        public ParcelToListWindow(IBL bl)
        {
            InitializeComponent();
            blParcelList = bl;
            parcelToListDataGrid.DataContext = blParcelList.GetALLParcelToList();
           
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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            parcelToListDataGrid.DataContext = blParcelList.GetALLParce(prcel => prcel.SenderName.StartsWith(txtSender.Text.ToString()));
        }

        private void txtTarget_TextChanged(object sender, TextChangedEventArgs e)
        {
            parcelToListDataGrid.DataContext = blParcelList.GetALLParce(prcel => prcel.TargetName.StartsWith(txtTarget.Text.ToString()));
        }

        private void comboweight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            help_function();
        }

        private void comboPriority_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            help_function();
        }
        public void help_function()
        {
            if (comboweight.SelectedItem != null&& comboPriority.SelectedItem == null)
                parcelToListDataGrid.DataContext = blParcelList.GetALLParce(prcel => prcel.Weight == (BO.Weightcategories)comboweight.SelectedIndex);
            if (comboweight.SelectedItem == null && comboPriority.SelectedItem == null)
                parcelToListDataGrid.DataContext = blParcelList.GetALLParcelToList();
            if (comboweight.SelectedItem == null && comboPriority.SelectedItem != null)
                parcelToListDataGrid.DataContext = blParcelList.GetALLParce(prcel => prcel.Priority == (BO.Priorities)comboPriority.SelectedIndex);
            if (comboweight.SelectedItem != null && comboPriority.SelectedItem != null)
                parcelToListDataGrid.DataContext = blParcelList.GetALLParce(prcel => prcel.Priority == (BO.Priorities)comboPriority.SelectedIndex && prcel.Weight == (BO.Weightcategories)comboweight.SelectedIndex);

        }

        private void Window_Activated(object sender, EventArgs e)
        {
             help_function();
        }
    }
}
