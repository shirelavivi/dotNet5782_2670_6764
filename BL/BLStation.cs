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
                    station.Name = nameStation;


                    if (countChargingSlots >= station.ChargeSlots)
                    {

                        List<IDAL.DO.DroneCharge> d = IDAL.DataSource.DronesCharge.FindAll(item => item.StationId == numStation);
                        station.ChargeSlots = countChargingSlots - d.Count;


                    }
                    
                    dl.DelStation(numStation);
                    dl.AddStation(station);
                }
                catch (IDAL.DO.MissingIdException ex)
                {
                    throw new MissingIdException(ex.ID, ex.EntityName);
                }
            }


            public IDAL.DO.Station MinFarToStation(BO.Location location)//התחנה הכי קרובה לנקודה הנל שיש לה עמדות טעינה פנויות
            {
                IDAL.DO.Station station = new IDAL.DO.Station();
                double MinDistanc = 1000000;// צריך לאתחל במרחק כלשהו גדול מאוד כך שכל מרחק יהיה קטן ממנו
                double temp;
                int counter = 0;
               //מיוי הנתונים ב BL מתוך DAL
                 
                foreach (IDAL.DO.Station item in dl.GetALLStations())
                {
                    foreach (IDAL.DO.DroneCharge itemDroneCharge in dl.GetALLDroneCharge())// כמה עמדות טעינה תפוסות יש בתחנה הזאת?
                    {
                        if (itemDroneCharge.StationId == item.Id)
                            counter++;
                    }
                        if (item.ChargeSlots-counter > 0)// אם יש עמדות טעינה פנויות? 
                        {
                            temp = DistanceTo(item.Longitude, item.Lattitude, location.Lattitude, location.Longitude);
                            if (temp < MinDistanc)
                            {
                                MinDistanc = temp;//שמירת המרחק הקטן עד עכשיו
                                station = item;
                            }

                        }
                    
                }
                return station;
                throw new NoFreeCharging();//אין עמדות טעינה פנויות
            }
            
            public BaseStation GetBaseStation(int id)
            {
                if (id < 0)
                    throw new ArgumentOutOfRangeException("id", "The station number must be greater or equal to 0");

                try
                {
                    var station = dl.GetStation(id);
                    return stationToBaseStation(station);
                }
                catch (IDAL.DO.MissingIdException ex)
                {
                    throw new MissingIdException(ex.ID, ex.EntityName);
                }
            }
            public IEnumerable<BO.BaseStationToList> GetALLbaseStationToList()
            {
                List<BO.BaseStationToList> basStationBl = new List<BO.BaseStationToList>();
                foreach (IDAL.DO.Station item in dl.GetALLStations())//מיוי הנתונים ב BL מתוך DAL
                {
                    BaseStationToList station = new BaseStationToList();
                    station.idnumber = item.Id;
                    station.nameStation = item.Name;
                    List < IDAL.DO.DroneCharge > d = IDAL.DataSource.DronesCharge.FindAll(itemstation => itemstation.StationId == item.Id);
                    station.chargingAvailable = item.ChargeSlots - d.Count; 
                    station.chargingNotAvailable = d.Count;
                    basStationBl.Add(station);
                }
                return basStationBl;
            }
            /// <summary>
            /// help function
            /// </summary>
            /// <param name="station"></param>
            /// <returns></returns>

            private BaseStation stationToBaseStation(IDAL.DO.Station station)//פונקצית עזר
            {
                BaseStation baseStation = new BaseStation();
                try
                {

                    baseStation. Idnumber = station.Id;
                    baseStation. NameStation = station.Name;
                    baseStation.locationOfStation = new Location
                    {
                        Lattitude = station.Lattitude,
                        Longitude = station.Longitude
                    };
                      baseStation.ChargingAvailable = station.ChargeSlots;
                    baseStation.droneInCharging = (from DronesCharge in dl.GetALLDroneCharge()//אכלוס רשימת הרחפנים בטעינה
                                                   select new DroneInCharging
                                                   {
                                                       IdNumber = DronesCharge.Droneld,
                                                       ButerryStatus = dronesBl.Find(d => d.Idnumber == DronesCharge.Droneld).ButerryStatus
                                                   }).ToList();                    
                }
                catch (IDAL.DO.MissingIdException ex)
                {
                    throw new MissingIdException(ex.ID, ex.EntityName);
                }
                return baseStation; 
            }
            public IEnumerable<BO.BaseStationToList> GetALLStationWithFreeStation()
            {
                List<BaseStationToList> st = new List<BaseStationToList>();
                List<BaseStationToList> newst = new List<BaseStationToList>();
                st = GetALLbaseStationToList().ToList();
                foreach(BaseStationToList item in st)
                {
                    if (item.chargingAvailable > 0)
                        newst.Add(item);
                }
                if (newst == null)

                    throw new NoFreeCharging();
                return newst;
            }
        }
    }

}  


