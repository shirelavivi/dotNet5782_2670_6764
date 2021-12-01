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
        public void UpdCustomer(int idCustomer, string nameCustomer = "", string newNumPhone = "");
        #endregion

        #region parcel
        public void AddrParcel(BO.Parcel parcel);
        #endregion

        #region BaseStation
        public void AddStation(BO.BaseStation baseStation);

        #endregion

        #region Drone
        public void UpdateDrone(int id, string model);
        public void AddDrone(BO.Drone drone);
        public void ReleaseDroneFromChargeStation(int droneId, int timeInCharging);
        public void PickUpPackage(int id);
       // public Drone GetDrone(int droneId);

        #endregion


    }


}
