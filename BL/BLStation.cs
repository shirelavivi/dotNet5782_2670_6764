using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;

namespace IBL
{
    namespace BO
    {
        public partial class BL : IBL
        {
            void AddBaseStation(BO.BaseStation station)
            {
                try
                {

                    IDAL.DO.Station stationDo = new IDAL.DO.Station();
                    stationDo.Id = station.Idnumber;
                    stationDo.Name = station.NameStation;
                    stationDo.Lattitude = station.locationOfStation.Lattitude;
                    stationDo.Longitude = station.locationOfStation.Longitude;
                    stationDo.ChargeSlots = station.ChargingAvailable;
                   

                    dl.AddStation(stationDo);
                }
                catch (IDAL.DO.DuplicateIdException ex)
                {
                    throw new DuplicateIdException(ex.ID, ex.EntityName);
                }

            }


        }
    }
}
