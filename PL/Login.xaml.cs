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
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : Window
    {
        private IBL BL;

        public MainWindow NewWindow;
        private string adminUsername = "Shirel and Hadasa";
        private string adminPassword = "12345678";
        //private object passwordBox;

        public LogIn()
        {
            try
            {
                InitializeComponent();
                BL = BlFactory.GetBl();
                PasswordBox.Password= string.Empty; 
                customerList_combobox.Visibility = Visibility.Hidden;
                sign.Visibility = Visibility.Hidden;
                customerList_combobox.ItemsSource=from st in BL.GetALLusers() select st.Name ;
                //this.DataContext = BL.GetALLusers();
              
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
        }

        private void LogIn_btn(object sender, RoutedEventArgs e)
        {

            if ((string)TitleManager.Content == "Welcome Manager") //login to admin
            {
                if (namManager.Text == adminUsername && PasswordBox.Password == adminPassword)
                {
                    NewWindow = new MainWindow();
                    PasswordBox.Password = string.Empty;
                    NewWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Wrong input , please try again");
                    namManager.Text = "";
                    PasswordBox.Password = string.Empty;
                }
            }
            else // login from user customer
            {
                IEnumerable<BO.User> user = new List<BO.User>();
                user = BL.GetAllUsersByPredicat(x => x.PassWord == Convert.ToInt32(PasswordBox.Password) && x.Name == customerList_combobox.SelectedItem.ToString());
                if (user.Count() != 0)
                {
                    NewWindow = new MainWindow();
                    NewWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Wrong input , please try again");
                    PasswordBox.Password = string.Empty;
                }

            }
        }


        private void ChangeTitle(object sender, RoutedEventArgs e)
        {
            if (changeUser_btn.Content.ToString() == "Switch To User")
            {
                TitleManager.Content = "Welcome User Name";
                changeUser_btn.Content = "Switch To Manager";
                customerList_combobox.Visibility = Visibility.Visible;
                sign.Visibility = Visibility.Visible;
                namManager.Visibility= Visibility.Hidden;
            }
            else // switch to admin
            {
                namManager.Text = "";
                TitleManager.Content = "Welcome Manager";
                changeUser_btn.Content = "Switch To User";
                customerList_combobox.Visibility = Visibility.Hidden;
                namManager.Visibility = Visibility.Visible;
                sign.Visibility = Visibility.Hidden;
            }
        }

        private void sign_Click(object sender, RoutedEventArgs e)
        {
            if (sign.Content.ToString() == "save")
            {
                BO.User user = new User();
                user.Name = namManager.Text.ToString();
                user.PassWord = Convert.ToInt32(PasswordBox.Password);
                BL.AddUser(user);
                MessageBox.Show("Sign is completed");
                namManager.Visibility = Visibility.Hidden;
                customerList_combobox.Visibility = Visibility.Visible;
                sign.Visibility = Visibility.Hidden;
                PasswordBox.Password = "";
                customerList_combobox.ItemsSource = from st in BL.GetALLusers() select st.Name;
                sign.Content = "sign in";
            }
            else
            {
                namManager.Visibility = Visibility.Visible;
                customerList_combobox.Visibility = Visibility.Hidden;
                sign.Content = "save";
            }
            
            
        }
    }

}
