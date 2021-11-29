using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IBL
{
    namespace BO
    {
        public class DroneInParcel
        {
            int IdNumber { get; set; }
            double ButerryStatus { get; set; }
            Location ThisLocation { get; set; }
            public override string ToString()
            {
                return this.ToStringProperty();
            }
        }
    }
}
