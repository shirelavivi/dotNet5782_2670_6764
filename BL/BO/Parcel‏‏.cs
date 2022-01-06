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
        public class Parcel
        {
            public int Id { get; set; }
            public CustomerAtParcels Sender;
            public CustomerAtParcels Target;
            public Weightcategories Weight { get; set; }
            public Priorities Priority { get; set; }
            public DroneInParcel droneAtParcel;//רחפן בחבילה
            public DateTime? Requested { get; set; }
            public DateTime? Scheduled { get; set; }
            public  DateTime? PickedUp { get; set; }
            public DateTime? Delivered { get; set; }
            public override string ToString()
            {
                return this.ToStringProperty();
            }
        }
    }

