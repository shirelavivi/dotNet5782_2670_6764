﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IBL
{
    namespace BO
    {
        public class DroneInParcel
        {
            public int IdNumber { get; set; }
            public double ButerryStatus { get; set; }
            public Location ThisLocation { get; set; }
            public override string ToString()
            {
                return this.ToStringProperty();
            }
        }
    }
}
