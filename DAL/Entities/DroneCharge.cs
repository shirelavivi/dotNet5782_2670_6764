﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace IDAL
{
    namespace DO
    {
        public struct DroneCharge
        {
            public int Droneld { get; set; }
            public int StationId { get; set; }
            public override string ToString()
            {
                return this.ToStringProperty();
            }
        }
       
    }
}
