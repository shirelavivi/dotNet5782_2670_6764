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
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>

    public partial class CustomerWindow : Window
    {
        IBL blCustomer;
        BO.CustomerToList customerWind;
        public CustomerWindow(IBL blcustomer)//add
        {
            InitializeComponent();
            blCustomer = blcustomer;
            UpdateCustomer.Visibility = Visibility.Hidden;
            GridUpDate.Visibility = Visibility.Hidden;
        }

        public CustomerWindow(BO.CustomerToList customerToList, IBL blcustomer)//Update
        {
            try
            {
                InitializeComponent();
                idTextBox.IsEnabled = false;
                blCustomer = blcustomer;
                AddCustomer.Visibility = Visibility.Hidden;
                gridAdd.Visibility = Visibility.Hidden;
                grid1.DataContext = blCustomer.GetCustomer(customerToList.Id);
                customerWind = customerToList;
                IEnumerable<BO.ParcelAtCustomer> st= blCustomer.GetCustomer(customerToList.Id).parcelsToCustomer;
                parcelsToCustomerDataGrid.DataContext = st; 
                parcelfromCustomerDataGrid.DataContext = blCustomer.GetCustomer(customerToList.Id).parcelfromCustomer;
            }
            catch (BO.MissingIdException ex)
            {
                MessageBox.Show(ex.ToString(), " ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Customer customer = new BO.Customer();
                customer.Id = Convert.ToInt32(idTextBoxAdd.Text);
                customer.Name = nameTextBoxAdd.Text;
                customer.Phone = phoneTextBoxAdd.Text;
                customer.location = new BO.Location();
                customer.location.Lattitude = Convert.ToDouble(lattitudeTextBox.Text);
                customer.location.Longitude = Convert.ToDouble(longitudeTextBox.Text);
                customer.parcelfromCustomer = null;
                customer.parcelsToCustomer = null;
                blCustomer.AddCustomer(customer);
                MessageBox.Show("The Customer Was Successfully Added");
                this.Close();
                CustomerToListWindow customerToList  = new CustomerToListWindow(blCustomer);
                customerToList.Show();

            }
            catch (BO.DuplicateIdException ex)
            {
                MessageBox.Show(ex.ToString(), "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateCustomer_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                blCustomer.UpdCustomer(int.Parse(idTextBox.Text), nameTextBox.Text, phoneTextBox.Text);
                MessageBox.Show("The customer Was Successfully Update");
                this.Close();
                CustomerToListWindow customerToList = new CustomerToListWindow(blCustomer);
                customerToList.Show();
            }
            catch (BO.MissingIdException ex)
            {
                MessageBox.Show(ex.ToString(), " ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
    
}
