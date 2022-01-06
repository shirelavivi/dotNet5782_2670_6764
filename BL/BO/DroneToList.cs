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
        public class DroneToList
        {
            public int Idnumber { get; set; }
            public string Model { get; set; }
            public Weightcategories Weightcategories { get; set; }
            public double ButerryStatus { get; set; }
            public DroneStatuses DroneStatuses { get; set; }
            public Location ThisLocation { get; set; }
            public int PackageNumberTransferred { get; set; }
            public override string ToString()
            {
                return this.ToStringProperty();
            }
        }
    }

