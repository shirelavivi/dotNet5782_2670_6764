﻿using System;
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
            blParcelList = bl;
            parcelToListDataGrid.DataContext = blParcelList.GetALLParcelToList();
            InitializeComponent();
           
        }
    }
}
