﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DO;

namespace DAL
{
    namespace DO
    {
        public struct Drone
        {

            public int id { get; set; }
            public string Model { get; set; } 
            public Weightcategories MaxWeight { get; set; }
          
            public override string ToString()
            {
                return this.ToStringProperty();
            }

        }

    }
}
