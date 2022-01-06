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
        public class CustomerToList
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public int numOfParcelProvided { get; set; }
            public int numOfParcelDontProvided { get; set; }
            public int numOfParcelGet { get; set; }
            public int numOfParcelOnTheWay { get; set; }
            public override string ToString()
            {
                return this.ToStringProperty();
            }
        }
    }



