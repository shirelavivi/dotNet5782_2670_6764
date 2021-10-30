using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct Parcel 
        {
            public int Id { get; set; }//Parcel serial number
            public int Senderld { get; set; }//
            public int Targetid { get; set; }// 
            public Weightcategories Weight { get; set; }//Location
            public Priorities Priority { get; set; }//Location
            public DateTime Requested  { get; set; }
            public int Droneld { get; set; }
            public DateTime Scheduled { get; set; }
            public DateTime PickedUp { get; set; }
            public DateTime Delivered { get; set; }

        }

    }
}
