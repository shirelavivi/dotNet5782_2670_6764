using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
   public class User
    {
        public string Name { get; set; }
        public int PassWord { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
