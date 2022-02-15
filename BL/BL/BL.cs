using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using BO;
using DO;
using BlApi;

namespace BL
{
    sealed partial class BL : IBL
    {

        static readonly IBL instance = new BL();
        public static IBL Instance { get => instance; }
        internal IDal dal = DalFactory.GetDal();    
        public List<DroneToList> dronesBl = new List<DroneToList>();
        //public  dl = new DalObject.DalObject();
        internal static double Free;
        internal static double Light;
        internal static double Medium;
        internal static double Heavy;
        internal static double ChargingRate;//אחוז טעינה לשעה



        public BL()
        {
            Random Rand = new Random(DateTime.Now.Millisecond);
            try
            {

                Location l = new Location();
                Free = dal.batteryArr()[0];
                Light = dal.batteryArr()[1];
                Medium = dal.batteryArr()[2];
                Heavy = dal.batteryArr()[3];
                ChargingRate = 25;// קצב טעינה לשעה

                foreach (DO.Drone item in dal.GetALLDrones())
                {
                    DroneToList drone = new DroneToList();
                    drone.Idnumber = item.id;
                    drone.Model = item.Model;
                    drone.Weightcategories = (BO.Weightcategories)item.MaxWeight;
                    dronesBl.Add(drone);
                }
                foreach (DroneToList item in dronesBl)
                {

                    foreach (DO.Parcel parcel in dal.GetALLParcel())
                    {
                        if (parcel.DroneId == item.Idnumber)// אם הרחפן שובץ לחבילה והרחפן הנוכחי זההים אז....
                        {
                            if (parcel.Delivered == new DateTime() && parcel.Scheduled != new DateTime())//אם החבילה שויכה אבל לא סופקה
                            {
                                item.DroneStatuses = DroneStatuses.transport;
                                if (parcel.PickedUp == new DateTime() && parcel.Scheduled != new DateTime())//אם החבילה שויכה אבל לא נאספה
                                {
                                    item.PackageNumberTransferred = 0;
                                    l.Lattitude = dal.GetCustomer(parcel.SenderId).Lattitude;// מיקום השולח
                                    l.Longitude = dal.GetCustomer(parcel.SenderId).Longitude;
                                    DO.Station station = MinFarToStation(l);// התחנה הקרובה לשלוח
                                    l.Lattitude = station.Lattitude;// מיקום התחנה הקרובה לשלוח
                                    l.Longitude = station.Longitude;
                                    item.ThisLocation = l;

                                }
                                if (parcel.PickedUp != new DateTime() && parcel.Delivered == new DateTime())//אם החבילה נאספה אבל עוד לא סופקה
                                {
                                    item.PackageNumberTransferred = parcel.Id;
                                    l.Lattitude = dal.GetCustomer(parcel.SenderId).Lattitude;// מיקום השולח
                                    l.Longitude = dal.GetCustomer(parcel.SenderId).Longitude;
                                    item.ThisLocation = l;
                                }

                            }
                            l.Lattitude = dal.GetCustomer(parcel.TargetId).Lattitude;// מיקום המקבל
                            l.Longitude = dal.GetCustomer(parcel.TargetId).Longitude;
                            DO.Station s = MinFarToStation(item.ThisLocation);//התחנה הקרובה ביותר למיקום של היעד
                            double howMuchBatrry = BatteryConsumption(DistanceTo(s.Lattitude, s.Longitude, l.Lattitude, l.Longitude), item.Weightcategories);
                            howMuchBatrry += BatteryConsumption(DistanceTo(l.Lattitude, l.Longitude, item.ThisLocation.Lattitude, item.ThisLocation.Longitude), item.Weightcategories);
                            item.ButerryStatus = Rand.Next((int)howMuchBatrry, 100);

                        }

                    }
                    
                    if (item.DroneStatuses != DroneStatuses.transport)//אם הרחפן לא מבצע משלוח
                    {
                        int a = Rand.Next(0, 2);
                        item.DroneStatuses =(DroneStatuses)a;
                    }
                    if (item.DroneStatuses == DroneStatuses.maintenance)//אם הרחפן בתחזוקה 
                    {
                        item.PackageNumberTransferred = 0;
                        int ran = Rand.Next(0, (dal.GetALLStations()).Count());
                        l.Lattitude = (dal.GetALLStations().ToList())[ran].Lattitude;
                        l.Longitude = (dal.GetALLStations().ToList())[ran].Longitude;
                        item.ThisLocation = l;
                        item.ButerryStatus = (int)Rand.Next(0, 20);
                    }

                    if (item.DroneStatuses == DroneStatuses.available)//אם הרחפןפנוי
                    {

                        int ran = Rand.Next(0, (dal.GetALLParcel()).Count());
                        int targetId = (dal.GetALLParcel().ToList())[ran].TargetId;//מגריל מתוך החבילות חבילה כלשהי ולוקח את תץז של המקבל
                        l.Lattitude = dal.GetCustomer(targetId).Lattitude;
                        l.Longitude = dal.GetCustomer(targetId).Longitude;
                        item.ThisLocation = l;
                        DO.Station s = MinFarToStation(item.ThisLocation);//התחנה הקרובה ביותר למיקום של הרחפן
                        int howMuchBatrry = (int)BatteryConsumption(DistanceTo(s.Lattitude, s.Longitude, item.ThisLocation.Lattitude, item.ThisLocation.Longitude), item.Weightcategories);
                        if (howMuchBatrry < 100)
                            item.ButerryStatus = (int)Rand.Next(howMuchBatrry, 100);
                        else
                        {
                            throw new NotEnoughBattery(howMuchBatrry, "error buttery");
                        }

                    }

                }
            }
            catch (DO.MissingIdException ex)
            {
                throw new BO.MissingIdException(ex.ID, ex.EntityName);
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
            foreach (DroneToList item in dronesBl)
            {
                if (item.Idnumber == dronId)


                    return item;


            }
            throw new BO.MissingIdException(dronId, "DroneToList");
        }
        public double BatteryConsumption(double kilometrs,BO.Weightcategories weightcategories)// פונקציה שמקבלת קליומטר ומחשבת כמה בטריה צריך כדי להגיע לשם
        {
            if (weightcategories == (BO.Weightcategories)0)
                return (100 / kilometrs * Light);
            if (weightcategories == (BO.Weightcategories)1)
                return (100 / kilometrs * Medium);
            return (100 / kilometrs * Heavy);

        }
        public double BatteryConsumption(double kilometrs)// דורסת את הפונקציה הקודמת במקרה והרחפן פנוי ולא נכנס שום קטגורית משקל
        {
            return kilometrs * Free;
        }
    }

}


