using System;
using IDAL.DO;

namespace IDAL
{
    namespace DO
    {
        public struct Customer
        {
            public int Id { get; set; }//Customer serial number
            public string Name { get; set; }//the customer's name
            public string Phone { get; set; }// customer's cell phone
            public double Longitude { get; set; }//Location
            public double Lattitude { get; set; }//Location
            public override string ToString()
            {
                return this.ToStringProperty();
            }
        }
    }
}
