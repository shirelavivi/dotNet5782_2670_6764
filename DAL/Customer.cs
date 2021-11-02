﻿using System;
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
            public void Tostring()
            {
                Console.WriteLine("Customer ID:"+this.Id );
                Console.WriteLine("the customer's name:"+ this.Name);
                Console.WriteLine("Customer cell phone number:"+ this.Phone);
                Console.WriteLine("Customer location:");
                Console.WriteLine("("+this.Longitude +","+this.Lattitude+")");
            }
        }
    }
}
