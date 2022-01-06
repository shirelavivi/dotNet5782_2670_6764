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
        public class Drone
        {
           public int IdDrone { get; set; }
            public string Model { get; set; }
            public Weightcategories Weightcategories { get; set; }
            public double ButerryStatus { get; set; }
            public EnumBL.DroneStatuses DroneStatuses { get; set; }
            public List <ParcelInTransfer> PackageInTransfer { get; set; }//לשנות
            public Location ThisLocation { get; set; }
            public override string ToString()
            {
                return this.ToStringProperty();
            }
        }
    }

