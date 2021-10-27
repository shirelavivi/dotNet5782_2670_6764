using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        class Station
        {
            int Id { get; set; }
            int Name { get; set; }
            double Longitude { get; set; }
            double Lattitude { get; set; }
            int ChargeSlots { get; set; }
        }
    }
}
