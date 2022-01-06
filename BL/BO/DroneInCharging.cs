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
        public class DroneInCharging
        {
            public int IdNumber { get; set; }
            public double ButerryStatus { get; set; }
            public override string ToString()
            {
                return this.ToStringProperty();
            }
        }
    }

