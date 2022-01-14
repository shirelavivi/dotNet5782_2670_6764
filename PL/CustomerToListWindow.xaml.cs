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
    /// Interaction logic for CustomerToListWindow.xaml
    /// </summary>
    public partial class CustomerToListWindow : Window
    {
        IBL blCustomerList;
        public CustomerToListWindow(IBL bl)
        {
            InitializeComponent();
            blCustomerList = bl;
            customerToListDataGrid.DataContext = bl.GetALLCostumerToList();
        }
       
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            CustomerWindow customerListWindow = new CustomerWindow(blCustomerList);
            customerListWindow.Show();
            this.Close();
        }

        private void customerToListDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            BO.CustomerToList customer = customerToListDataGrid.SelectedItem as BO.CustomerToList;
            if (customer != null)
            {
                CustomerWindow customerListWindow = new CustomerWindow(customer, blCustomerList);
                customerListWindow.Show();
                this.Close();
            }
        }
    }

}
    

