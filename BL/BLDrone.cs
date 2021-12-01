using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public partial class BL : IBL
        {

            public void UpdateDrone(int id, string model)
            {
                IDAL.DO.Drone drone= dl.GetDrone(id);
                drone.Model = model;
                dl.DelDrone(id);
                dl.AddDrone(drone); 
            }
            public void SendingDroneforCharging(int droneId)
            {
                double MinDistanc=1000000;// צריך לאתחל במרחק כלשהו גדול מאוד כך שכל מרחק יהיה קטן ממנו
                double temp,battery=0;
                int stationId=0;

                if (GetDroneToList(droneId).DroneStatuses!= (DroneStatuses)0)//אם הסטטוס שונה מפנוי יש חריגה
                            throw new UnsuitableDroneMode(GetDroneToList(droneId).DroneStatuses, "Drone");
                 else
                 {
                    foreach(IDAL.DO.Station itemStation in dl.GetALLStations())
                    {
                        if(itemStation.ChargeSlots>0)// אם יש עמדות טעינה פנויות? 
                        {
                                     temp = DistanceTo(itemStation.Lattitude, itemStation.Longitude, GetDroneToList(droneId).ThisLocation.Lattitude, GetDroneToList(droneId).ThisLocation.Longitude);// המרחק בין התחנה לרחפן 
                            if (temp< MinDistanc)
                            {
                               if (GetDroneToList(droneId).ButerryStatus > BatteryConsumption(temp, GetDroneToList(droneId).Weightcategories))// האם כמות הבטריה ברחפן גדולה מהכמות הנדרשת כדי להגיע לתחנה 
                               MinDistanc = temp;//שמירת המרחק הקטן עד עכשיו
                               stationId = itemStation.Id;//שמירת מזהה התחנה
                                battery = BatteryConsumption(temp, GetDroneToList(droneId).Weightcategories);//שמירת כמות הבטריה שמתבזבזת
                            }

                        }
                       
                    }
                    if (MinDistanc != 1000000 && stationId == 0)
                        throw new NotEnoughBattery(GetDroneToList(droneId).ButerryStatus, "Drone");
                    if (MinDistanc == 1000000 && stationId == 0)
                        throw new CanNotSentForCharging(); // מזהה תחנה אני שלולחת לחריגה ?)אין עמדות טעינה פנויות (איזה 
                    
                    else
                    {
                        dl.SendDroneTpCharge(stationId, droneId);
                        DroneToList drone=GetDroneToList(droneId);//מכאן נשנה את המצב של הרחפן והבטריה ברשימה של ה bl
                        drone.ButerryStatus -= battery;
                        drone.DroneStatuses = DroneStatuses.maintenance;
                        Location l = new Location();
                        l.Lattitude = dl.GetStation(stationId).Lattitude;
                        l.Longitude = dl.GetStation(stationId).Longitude;
                        drone.ThisLocation = l;
                        dronesBl.Add(drone);
                        dronesBl.Remove(GetDroneToList(droneId));

                    }
                            
                 }

            }
            public IEnumerable<BO.DroneToList> GetALLDroneToList()
            {
                DroneToList drone = new DroneToList();
                foreach (IDAL.DO.Drone item in dl.GetALLDrones())//מיוי הנתונים ב BL מתוך DAL
                {
                    
                    drone.Idnumber = item.id;
                    drone.Model = item.Model;
                    drone.Weightcategories = (Weightcategories)item.MaxWeight;
                    dronesBl.Add(drone);
                }
                return dronesBl;
            }

        }
    }
}
