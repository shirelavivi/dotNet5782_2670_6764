using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{

    interface IBL
    {
        #region Customer
        public void AddCustomer(BO.Customer customer);

        #endregion
        #region parcel
        public void AddrParcel(BO.Parcel parcel);
        #endregion
    }
    

}
