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
        public class ParcelAtCustomer
        {
            public int Idnumber { get; set; }
            public Weightcategories Weightcategories { get; set; }
            public Priorities Priorities { get; set; }
            public PacketStatuses PacketStatuses { get; set; }
            public CustomerAtParcels CustomerInPackage { get; set; }
            public override string ToString()
            {
                return this.ToStringProperty();
            }
        }
    }

