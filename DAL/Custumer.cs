using System;

namespace DAL
{
    namespace DO
    {
        public struct Customer
        {
            public int id { get; set; }//Customer serial number
            public string name { get; set; }//the customer's name
            public string phone { get; set; }// customer's cell phone
            public double longitude { get; set; }//Location
            public double lattitude { get; set; }//Location
        }
    }
}
