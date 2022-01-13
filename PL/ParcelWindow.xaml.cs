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
    /// Interaction logic for ParcelWindow.xaml
    /// </summary>
    public partial class ParcelWindow : Window
    {
        IBL blparcel;
        public ParcelWindow(BO.ParcelToList parcel, IBL blParcel)//עדכון
        {
            InitializeComponent();
            blparcel = blParcel;
            btnAdd.Visibility = Visibility.Hidden;
           
        }
        public ParcelWindow(IBL blParcel)//הוספה
        {
            InitializeComponent();
            blparcel = blParcel;
            btnDelet.Visibility = Visibility.Hidden;
            btnUpDate.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
