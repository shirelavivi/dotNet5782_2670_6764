using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BL;
using IBL;
using DO;

namespace BO
    {
        public class BaseStationToList
        {
            public int idnumber { get; set; }
            public string nameStation { get; set; }
            public  int chargingAvailable { get; set; }
            public int chargingNotAvailable { get; set; }
            public override string ToString()
            {
                return this.ToStringProperty();
            }
        }
    }

