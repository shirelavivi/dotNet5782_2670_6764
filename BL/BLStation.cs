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


            public BO.BaseStationToList MinFarToStation(BO.Location location)//התחנה הכי קרובה לנקודה הנל שיש לה עמדות טעינה פנויות
            {
                BaseStationToList station = new BaseStationToList();
                double MinDistanc = 1000000;// צריך לאתחל במרחק כלשהו גדול מאוד כך שכל מרחק יהיה קטן ממנו
                double temp;
                foreach (IDAL.DO.Station item in dl.GetALLStations())//מיוי הנתונים ב BL מתוך DAL
                {
                    station.idnumber = item.Id;
                    station.nameStation = item.Name;
                    station.chargingAvailable = item.ChargeSlots;
                    station.chargingNotAvailable = 0;
                    basStationBl.Add(station);
                }
                IDAL.DO.Station stationDO = new IDAL.DO.Station();
                foreach (BO.BaseStationToList itemStation in basStationBl)
                {

                    stationDO = dl.GetStation(itemStation.idnumber);//התחנה מתוך שכבת הנתונים כדי לקבל את המיקום
                    if (itemStation.chargingAvailable > 0)// אם יש עמדות טעינה פנויות? 
                    {
                        temp = DistanceTo(stationDO.Lattitude, stationDO.Longitude, location.Lattitude, location.Longitude);
                        if (temp < MinDistanc)
                        {
                            MinDistanc = temp;//שמירת המרחק הקטן עד עכשיו
                            station = itemStation;
                        }

                    }
                }

                return station;
                throw new CanNotSentForCharging();//אין עמדות טעינה פנויות
            }
            public IEnumerable<BO.BaseStationToList> GetALLbaseStationToList()
            {
                BaseStationToList station = new BaseStationToList();
                foreach (IDAL.DO.Station item in dl.GetALLStations())//מיוי הנתונים ב BL מתוך DAL
                {
                    station.idnumber = item.Id;
                    station.nameStation = item.Name;
                    station.chargingAvailable = item.ChargeSlots;
                    station.chargingNotAvailable = 0;
                    basStationBl.Add(station);
                }
                return basStationBl;
            }
        }
    }

}  


