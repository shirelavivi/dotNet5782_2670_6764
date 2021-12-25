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

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneList.xaml
    /// </summary>
    public partial class DroneListWindose : Window
    {
        public DroneListWindose(IBL.BO.BL bl)
        {
            InitializeComponent();
            int i = 0;
            foreach (IBL.BO.DroneToList item in bl.GetALLDroneToList()) ;
               DataGridListDrone.data



        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        
    }

}
