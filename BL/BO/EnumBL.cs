using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BL;
using IBL;
using DO;

namespace BL
{
    public sealed partial class EnumBL
    {
        public enum Weightcategories { easy, middle, weighty }
        public enum DroneStatuses { available, maintenance, transport }
        public enum Priorities { Standard, fast, emergency }
        public enum PacketStatuses { Created, associated, collected, provided }
    }

}

