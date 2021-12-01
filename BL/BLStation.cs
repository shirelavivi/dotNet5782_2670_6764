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
            public void AddBaseStation(BO.BaseStation station)
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
            public void UpdStation(int numStation, string nameStation = "", int countChargingSlots = 0)
            {
                try
                {
                    IDAL.DO.Station station = dl.GetStation(numStation);
                    station.Id = numStation;
                    if (nameStation != "")
                    {
                        station.Name = nameStation;
                    }
                    if (countChargingSlots != 0)
                    {
                        if (countChargingSlots >= station.ChargeSlots)
                        {
                            
                            List<IDAL.DO.DroneCharge> d = IDAL.DataSource.DronesCharge.FindAll(item => item.StationId == numStation);
                            station.ChargeSlots = countChargingSlots - d.Count;


                        }
                        else
                            throw new NotImplementedExceptin(ex, "ERROR");
                    }
                    dl.DelStation(numStation);
                    dl.AddStation(station);
                }
                catch (IDAL.DO.MissingIdException ex)
                {
                    throw new MissingIdException(ex.ID, ex.EntityName);
                }
            }

        }


        }
    }
}
