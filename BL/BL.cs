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
       
       
        public partial class BL :IBL
        {
            static Random Rand = new Random(DateTime.Now.Millisecond);
            List<DroneToList> dronesBl = new List<DroneToList>();
            List<ParcelToList> parcelBl = new List<ParcelToList>();
            List<CustomerToList> customerBl = new List<CustomerToList>();
            List<BaseStationToList> basStationBl = new List<BaseStationToList>();
            DalObject.DalObject dl = new DalObject.DalObject();
            internal static double Free ;
            internal static double Light ;
            internal static double Medium;
            internal static double Heavy ; 
            internal static double ChargingRate;//אחוז טעינה לשעה

            public BL()
            {
                
                Free = dl.batteryArr()[0];
                Light=dl.batteryArr()[1];
                Medium = dl.batteryArr()[2];
                Heavy = dl.batteryArr()[3];
                ChargingRate = 25;
                dronesBl = GetALLDroneToList().ToList();// מילוי רשימת הרחפנים

                foreach (DroneToList item in dronesBl)
                {
                    if(item.DroneStatuses!= (DroneStatuses)2)//אם הרחפן לא מבצע משלוח
                    {
                        item.DroneStatuses = (DroneStatuses)Rand.Next(1,2);
                    }
                
                
                    if (item.DroneStatuses != (DroneStatuses)1)//אם הרחפן בתחזוקה 
                    {
                        Location l = new Location();
                        int ran= Rand.Next(0, (dl.GetALLStations()).Count());
                        l.Lattitude = (dl.GetALLStations().ToList())[ran].Lattitude;
                        l.Longitude = (dl.GetALLStations().ToList())[ran].Longitude;
                        item.ThisLocation = l;
                        item.ButerryStatus = Rand.Next(0, 20);
                    }
               
                    if (item.DroneStatuses != (DroneStatuses)0)//אם הרחפןפנוי
                    {
                        Location l = new Location();
                        int ran = Rand.Next(0, (dl.GetALLParcel()).Count());
                        int targetId = (dl.GetALLParcel().ToList())[ran].TargetId;//מגריל מתוך החבילות חבילה כלשהי ולוקח את תץז של המקבל
                        l.Lattitude = dl.GetCustomer(targetId).Lattitude;
                        l.Longitude = dl.GetCustomer(targetId).Longitude;
                        item.ThisLocation = l;
                        //item.ButerryStatus = Rand.Next(, 100); לסיים את הפונקציה 
                        
                    }
                   
                }
            }
            public static double DistanceTo(double lat1, double lon1, double lat2, double lon2, char unit = 'K')
            {
                double rlat1 = Math.PI * lat1 / 180;
                double rlat2 = Math.PI * lat2 / 180;
                double theta = lon1 - lon2;
                double rtheta = Math.PI * theta / 180;
                double dist =
                    Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                    Math.Cos(rlat2) * Math.Cos(rtheta);
                dist = Math.Acos(dist);
                dist = dist * 180 / Math.PI;
                dist = dist * 60 * 1.1515;

                switch (unit)
                {
                    case 'K': //Kilometers -> default
                        return dist * 1.609344;
                    case 'N': //Nautical Miles 
                        return dist * 0.8684;
                    case 'M': //Miles
                        return dist;
                }

                return dist;
            }//פונקציה שמחשבת מרחק בין שתי מיקומים 
            public DroneToList GetDroneToList(int dronId)
            {
                foreach(DroneToList item in dronesBl)
                {
                    if (item.Idnumber == dronId)
                        return item;
                }
                throw new MissingIdException(dronId, "DroneToList");
            }
            public double BatteryConsumption(double kilometrs, Weightcategories weightcategories)// פונקציה שמקבלת קליומטר ומחשבת כמה בטריה צריך כדי להגיע לשם
            {
                if (weightcategories == (Weightcategories)0)
                    return (kilometrs * Light);
                if (weightcategories == (Weightcategories)1)
                    return (kilometrs * Medium);
                    return (kilometrs * Heavy);
                
            }
            public double BatteryConsumption(double kilometrs)// דורסת את הפונקציה הקודמת במקרה והרחפן פנוי ולא נכנס שום קטגורית משקל
            {
                return kilometrs * Free;
            }
        }
    }
}