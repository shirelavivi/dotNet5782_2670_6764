﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    namespace DO
    {
       public struct Station
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public double Longitude { get; set; }
            public double Lattitude { get; set; }
            public int ChargeSlots { get; set; }
        }
    }
}
