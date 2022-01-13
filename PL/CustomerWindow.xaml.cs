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
        }

        public CustomerWindow(BO.CustomerToList customerToList, IBL blcustomer)//Update
        {
            InitializeComponent();
            blCustomer = blcustomer;
            AddCustomer.Visibility = Visibility.Hidden;
            customerWind = customerToList;
            FullCustomer(customerToList);
        }
        private void FullCustomer(BO.CustomerToList customerToList)
        {
            IdTextBox.Text = customerToList.Id.ToString();
            NameTextBox.Text = customerToList.Name.ToString();
            NumOfParcelProvidedTextBox.Text = customerToList.numOfParcelProvided.ToString();
            NumOfParcelGetTextBox.Text = customerToList.numOfParcelGet.ToString();
            NumOfParcelOnTheWayTextBox.Text = customerToList.numOfParcelOnTheWay.ToString();
            NumOfParcelProvidedTextBox.Text = customerToList.numOfParcelProvided.ToString();
            PhoneTextBox.Text = customerToList.Phone.ToString();
            //צריך לעשות isEnable?
        }
        private void AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Customer customer = new BO.Customer();
                customer.Id= int.Parse(IdTextBox.Text);
                customer.Name =NameTextBox.Text;
                customer.Phone = PhoneTextBox.Text;
                customer.location = new BO.Location();
                customer.parcelfromCustomer = null;//??
                customer.parcelsToCustomer = null;
                //customer.numOfParcelOnTheWay =int.Parse(NumOfParcelOnTheWayTextBox.Text);
                //customer.numOfParcelDontProvided = int.Parse(NumOfParcelDontProvidedTextBox.Text);
                //customer.numOfParcelDontProvided = int.Parse(NumOfParcelDontProvidedTextBox.Text);
                blCustomer.AddCustomer(customer);
                MessageBox.Show("The Customer Was Successfully Added");
                this.Close();
                CustomerWindow customerWindow = new CustomerWindow(blCustomer);
                customerWindow.Show();

            }
            catch (BO.DuplicateIdException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            
                try
                {
                    if (NameTextBox.Text != "")
                    {
                        blCustomer.UpdCustomer(Convert.ToInt32(NameTextBox.Text), IdTextBox.Text);
                        MessageBox.Show("The station is update");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("please typ name of station");
                    }
                }
                catch (BO.MissingIdException)
                {
                    MessageBox.Show("Erorr ID");
                }
            
        }
    }
}
