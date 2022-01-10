using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BL;
using DO;

namespace BO
    {
        public class CustomerAtParcels
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public override string ToString()
            {
                return this.ToStringProperty();
            }
        }
    }

