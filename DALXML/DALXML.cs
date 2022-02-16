using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using DalApi;
using DO;


namespace Dal
{
    sealed class DLXML : IDal
    {
        #region singelton
        static readonly DLXML instance = new DLXML();
        public static DLXML Instance { get => instance; }// The public Instance property to use
        ///*static DLXML() { }/*// static ctor to ensure instance init is done just before first usage
        DLXML() { } // default => private

        string droneChargingPath = @"droneChargingPath.xml"; //XElement
        //XMLSerializer
        string customerPath = @"customerPathXml.xml";
        string stationPath = @"stationPathXml.xml";
        string dronePath = @"dronePathXml.xml";
        string parcelPath = @"parcelPathXml.xml";
        string configPath = @"configPathXml.xml";
        public void AddCustomer(Customer c)
        {
            List<Customer> listCustomer = XMLTools.LoadListFromXMLSerializer<Customer>(customerPath);
            if (CheckCusromer(c.Id))
                throw new DO.DuplicateIdException(c.Id, "Customer");
            listCustomer.Add(c);
            XMLTools.SaveListToXMLSerializer(listCustomer, customerPath);
        }

        public bool CheckCusromer(int id)
        {
            List<Customer> listCustomer = XMLTools.LoadListFromXMLSerializer<Customer>(customerPath);
            return listCustomer.Any(st => st.Id == id);
        }

        public IEnumerable<Customer> GetALLCustomers()
        {
            List<Customer> listCustomer = XMLTools.LoadListFromXMLSerializer<Customer>(customerPath);
            return from st in listCustomer select st;
        }

        public void UpdCustomer(Customer st)
        {

            List<Customer> listCustomer = XMLTools.LoadListFromXMLSerializer<Customer>(customerPath);
            int count = listCustomer.RemoveAll(st => st.Id == st.Id);

            if (count == 0)
                throw new MissingIdException(st.Id, "Customer");

            listCustomer.Add(st);
            XMLTools.SaveListToXMLSerializer(listCustomer, customerPath);
        }

        public void DelCustomer(int id)
        {
            List<Customer> listCustomer = XMLTools.LoadListFromXMLSerializer<Customer>(customerPath);
            int count = listCustomer.RemoveAll(st => st.Id == id);

            if (count == 0)
                throw new MissingIdException(id, "Customer");
            XMLTools.SaveListToXMLSerializer(listCustomer, customerPath);//need?
        }

        public IEnumerable<Customer> GetCustomertsByPerdicate(Predicate<Customer> predicate)
        {
            List<Customer> listCustomer = XMLTools.LoadListFromXMLSerializer<Customer>(customerPath);
            return from st in listCustomer
                   where predicate(st)
                   select st;
        }

        public Customer GetCustomer(int id)
        {
            List<Customer> listCustomer = XMLTools.LoadListFromXMLSerializer<Customer>(customerPath);
            if (!CheckCusromer(id))
                throw new MissingIdException(id, "Customer");

            Customer st = listCustomer.Find(st => st.Id == id);
            return st;
        }

        public void AddStation(Station s)
        {
            List<Station> listStation = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            if (CheckStation(s.Id))

                throw new DO.DuplicateIdException(s.Id, "Station");

            listStation.Add(s);
            XMLTools.SaveListToXMLSerializer(listStation, stationPath);

        }

        public IEnumerable<Station> GetALLStations()
        {
            List<Station> listStation = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            return from st in listStation select st;
        }

        public bool CheckStation(int id)
        {
            List<Station> listStation = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            return listStation.Any(st => st.Id == id);
        }

        public void UpdStation(Station st)
        {
            List<Station> listStation = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            int count = listStation.RemoveAll(st => st.Id == st.Id);

            if (count == 0)
                throw new MissingIdException(st.Id, "Station");

            listStation.Add(st);
            XMLTools.SaveListToXMLSerializer(listStation, stationPath);

        }

        public void DelStation(int id)
        {
            List<Station> listStation = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            int count = listStation.RemoveAll(st => st.Id == id);

            if (count == 0)
                throw new MissingIdException(id, "Station");
            XMLTools.SaveListToXMLSerializer(listStation, stationPath);
        }

        public IEnumerable<Station> GetStationByPerdicate(Predicate<Station> predicate)
        {
            List<Station> listStation = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            return from st in listStation
                   where predicate(st)
                   select st;
        }

        public Station GetStation(int s)
        {
            List<Station> listStation = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            if (!CheckStation(s))
                throw new MissingIdException(s, "Station");

            Station st = listStation.Find(st => st.Id == s);
            return st;
        }

        public void AddDrone(Drone c)
        {
            List<Drone> listDrone = XMLTools.LoadListFromXMLSerializer<Drone>(dronePath);
            if (CheckDrone(c.id))

                throw new DO.DuplicateIdException(c.id, "Drone");

            listDrone.Add(c);
            XMLTools.SaveListToXMLSerializer(listDrone, dronePath);
        }

        public bool CheckDrone(int id)
        {
            List<Drone> listDrone = XMLTools.LoadListFromXMLSerializer<Drone>(dronePath);
            return listDrone.Any(st => st.id == id);
        }

        public void UpdDrone(Drone st)
        {
            List<Drone> listDrone = XMLTools.LoadListFromXMLSerializer<Drone>(dronePath);
            int count = listDrone.RemoveAll(st => st.id == st.id);

            if (count == 0)
                throw new MissingIdException(st.id, "Drone");

            listDrone.Add(st);
            XMLTools.SaveListToXMLSerializer(listDrone, dronePath);
        }

        public void DelDrone(int id)
        {
            List<Drone> listDrone = XMLTools.LoadListFromXMLSerializer<Drone>(dronePath);
            int count = listDrone.RemoveAll(st => st.id == id);

            if (count == 0)
                throw new MissingIdException(id, "Drone");
            XMLTools.SaveListToXMLSerializer(listDrone, dronePath);
        }

        public IEnumerable<Drone> GetDronetsByPerdicate(Predicate<Drone> predicate)
        {
            List<Drone> listDrone = XMLTools.LoadListFromXMLSerializer<Drone>(dronePath);
            return from st in listDrone
                   where predicate(st)
                   select st;
        }

        public Drone GetDrone(int id)
        {
            List<Drone> listDrone = XMLTools.LoadListFromXMLSerializer<Drone>(dronePath);
            if (!CheckDrone(id))
                throw new MissingIdException(id, "Drone");

            Drone st = listDrone.Find(st => st.id == id);
            return st;
        }

        public IEnumerable<Drone> GetALLDrones()
        {
            List<Drone> listDrone = XMLTools.LoadListFromXMLSerializer<Drone>(dronePath);
            return from st in listDrone select st;
        }

        public void AddParcel(Parcel p)
        {
            List<Parcel> listParcel = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            //p.Id =//לא נותן
            if (CheckParcel(p.Id))
                throw new DO.DuplicateIdException(p.Id, "Parcel");

            listParcel.Add(p);
            XMLTools.SaveListToXMLSerializer(listParcel, parcelPath);
        }

        public bool CheckParcel(int id)
        {
            List<Parcel> listParcel = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            return listParcel.Any(st => st.Id == id);
        }

        public void UpdParcel(Parcel st)
        {
            List<Parcel> listParcel = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            int count = listParcel.RemoveAll(st => st.Id == st.Id);

            if (count == 0)
                throw new MissingIdException(st.Id, "Parcel");

            listParcel.Add(st);
            XMLTools.SaveListToXMLSerializer(listParcel, parcelPath);
        }

        public void DelParcel(int id)
        {
            List<Parcel> listParcel = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            int count = listParcel.RemoveAll(st => st.Id == id);

            if (count == 0)
                throw new MissingIdException(id, "Parcel");
            XMLTools.SaveListToXMLSerializer(listParcel, parcelPath);
        }

        public IEnumerable<Parcel> GetParcelByPerdicate(Predicate<Parcel> predicate)
        {
            List<Parcel> listParcel = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            return from st in listParcel
                   where predicate(st)
                   select st;
        }

        public Parcel GetParcel(int s)
        {
            List<Parcel> listParcel = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            if (!CheckParcel(s))
                throw new MissingIdException(s, "Parcel");

            Parcel st = listParcel.Find(st => st.Id == s);
            return st;

        }

        public IEnumerable<Parcel> GetALLParcel()
        {
            List<Parcel> listParcel = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            return listParcel;
        }

        public void AddDroneCharging(DroneCharge drone)
        {
            XElement DroneChargingRootElem = XMLTools.LoadListFromXMLElement(droneChargingPath);

            XElement dr = (from d in DroneChargingRootElem.Elements()
                           where int.Parse(d.Element("ID").Value) == drone.Droneld
                           select d).FirstOrDefault();

            if (dr != null)
                throw new DO.DuplicateIdException(drone.Droneld, "Duplicate DroneCharge ID");

            XElement droneElem = new XElement("Drone", new XElement("ID", drone.Droneld),
                                 new XElement("Name", drone.StationId));

            DroneChargingRootElem.Add(droneElem);

            XMLTools.SaveListToXMLElement(DroneChargingRootElem, droneChargingPath);
        }

        public bool CheckDroneCharging(int id)
        {
            XElement DroneChargingRootElem = XMLTools.LoadListFromXMLElement(droneChargingPath);
            XElement dr = (from p in DroneChargingRootElem.Elements()
                           where int.Parse(p.Element("ID").Value) == id
                           select p).FirstOrDefault();
            if (dr == null)
                return false;
            return true;
        }

        public void UpdDroneCharging(DroneCharge drone)
        {
            XElement DroneChargingRootElem = XMLTools.LoadListFromXMLElement(droneChargingPath);

            XElement dr = (from p in DroneChargingRootElem.Elements()
                           where int.Parse(p.Element("ID").Value) == drone.Droneld
                           select p).FirstOrDefault();

            if (dr != null)
            {
                dr.Element("ID").Value = drone.Droneld.ToString();
                dr.Element("Name").Value = drone.StationId.ToString();
                //DroneChargingRootElem.Add(drone);
                XMLTools.SaveListToXMLElement(DroneChargingRootElem, droneChargingPath);
            }
            else
                throw new DO.MissingIdException(drone.Droneld, $"Missing Drone Id: {drone.Droneld}");
        }

        public void DelDroneCharge(int droneId)
        {
            XElement DroneChargingRootElem = XMLTools.LoadListFromXMLElement(droneChargingPath);

            XElement dr = (from p in DroneChargingRootElem.Elements()
                           where int.Parse(p.Element("ID").Value) == droneId
                           select p).FirstOrDefault();

            if (dr != null)
            {
                dr.Remove();

                XMLTools.SaveListToXMLElement(DroneChargingRootElem, droneChargingPath);
            }
            else
                throw new DO.MissingIdException(droneId, $"Missing Drone Charge id: {droneId}");
        }

        public IEnumerable<DroneCharge> ByPerdicate(Predicate<DroneCharge> predicate)
        {
            XElement DroneChargingRootElem = XMLTools.LoadListFromXMLElement(droneChargingPath);

            return from p in DroneChargingRootElem.Elements()
                   let p1 = new DroneCharge()
                   {
                       Droneld = Int32.Parse(p.Element("IDdrone").Value),
                       StationId = Int32.Parse(p.Element("Namestation").Value)
                   }
                   where predicate(p1)
                   select p1;
        }

        public DroneCharge GetDroneCharge(int stationId)
        {
            XElement DroneChargingRootElem = XMLTools.LoadListFromXMLElement(droneChargingPath);
            DroneCharge d = (from dr in DroneChargingRootElem.Elements()
                             where int.Parse(dr.Element("ID").Value) == stationId
                             select new DroneCharge()
                             {
                                 Droneld = Int32.Parse(dr.Element("IDdrone").Value),
                                 StationId = Int32.Parse(dr.Element("Namestation").Value)
                             }
                        ).FirstOrDefault();
            if (!CheckDroneCharging(stationId))
                throw new MissingIdException(stationId, "DroneCharge");
            return d;
        }

        public IEnumerable<DroneCharge> GetALLDroneCharge()
        {
            XElement DroneChargingRootElem = XMLTools.LoadListFromXMLElement(droneChargingPath);

            return (from p in DroneChargingRootElem.Elements()
                    select new DroneCharge()
                    {
                        Droneld = Int32.Parse(p.Element("IDdrone").Value),
                        StationId = Int32.Parse(p.Element("Namestation").Value)
                    }
                   );
        }

        public void SendDroneTpCharge(int StationId, int DroneId)
        {
            try
            {
                XElement DroneChargingRootElem = XMLTools.LoadListFromXMLElement(droneChargingPath);
                List<Station> listStation = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
                List<Drone> listDrone = XMLTools.LoadListFromXMLSerializer<Drone>(dronePath);

                Station tempStation = instance.GetStation(StationId);
                Drone tempDrone = instance.GetDrone(DroneId);
                tempStation.ChargeSlots--;///עידכון עמדות טעינה
                listDrone.Add(tempDrone);
                listDrone.Remove(instance.GetDrone(DroneId));
                listStation.Add(tempStation);///הוספת התחנה המעודכנת
                listStation.Remove(instance.GetStation(StationId));
                DroneCharge tempDronecharge = new DroneCharge();
                tempDronecharge.Droneld = DroneId;
                tempDronecharge.StationId = StationId;
                DroneChargingRootElem.Add(tempDronecharge);
                XMLTools.SaveListToXMLElement(DroneChargingRootElem, droneChargingPath);
                XMLTools.SaveListToXMLSerializer(listDrone, dronePath);
                XMLTools.SaveListToXMLSerializer(listStation, stationPath);

            }
            catch (DO.MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }
        }

        public void ConnectParcelToDron(int ParcelId, int DronId)
        {
            try
            {
                List<Drone> listDrone = XMLTools.LoadListFromXMLSerializer<Drone>(dronePath);
                List<Parcel> listParcel = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
                Parcel p = new Parcel();
                Drone d = new Drone();
                int i = listParcel.Count() + 1;
                p = instance.GetParcel(ParcelId);
                d = instance.GetDrone(DronId);
                p.DroneId = d.id;
                p.Scheduled = DateTime.Now;
                listParcel[listParcel.FindIndex(x => x.Id == p.Id)] = p;//לבדוק איך עושים את הפיינד אינדקס
                XMLTools.SaveListToXMLSerializer(listParcel, parcelPath);
                XMLTools.SaveListToXMLSerializer(listDrone, dronePath);
            }
            catch (MissingIdException)
            {
                throw new MissingIdException(ParcelId, "parcel");
            }
        }

        public void ReleaseDroneFromChargeStation(int DroneId)
        {
            try
            {
                XElement DroneChargingRootElem = XMLTools.LoadListFromXMLElement(droneChargingPath);
                List<Station> listStation = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
               
                Drone tempDrone1 = instance.GetDrone(DroneId);
                int save = 0;
                //for (int i = 0; i < Dal.DataSource.DronesCharge.Count; i++)
                //{
                //    if (Dal.DataSource.DronesCharge[i].Droneld == DroneId)
                //    {
                //        save = Dal.DataSource.DronesCharge[i].StationId;
                //        Dal.DataSource.DronesCharge.Remove(Dal.DataSource.DronesCharge[i]);

                //    }
                //}

                XElement dr = (from p in DroneChargingRootElem.Elements()//לשנות
                               where int.Parse(p.Element("ID").Value) == DroneId
                               select p).FirstOrDefault();
                Station s = instance.GetStation(save);
                s.ChargeSlots++;
                listStation.Add(s);
                listStation.Remove(instance.GetStation(save));
                XMLTools.SaveListToXMLElement(DroneChargingRootElem, droneChargingPath);
                XMLTools.SaveListToXMLSerializer(listStation, stationPath);
            }
            catch (DO.MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }
        }

        public void collection(int ParcelId)
        {
            List<Parcel> listParcel = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            Parcel p = new Parcel();
            p = instance.GetParcel(ParcelId);
            listParcel.Remove(p);
            p.PickedUp = DateTime.Now;
            listParcel.Add(p);
            XMLTools.SaveListToXMLSerializer(listParcel, parcelPath);
        }

        public void PackageDalvery(int ParcelId)
        {
            List<Parcel> listParcel = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            Parcel p = new Parcel();
            p = instance.GetParcel(ParcelId);
            listParcel.Remove(p);
            p.Delivered = DateTime.Now;
            listParcel.Add(p);
            XMLTools.SaveListToXMLSerializer(listParcel, parcelPath);

        }

        public List<Station> ShowStationAvailable()
        {
            List<Station> listStation = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            List<Station> run = listStation;
            List<Station> temp = new List<Station>();
            for (int i = 0; i < run.Count; i++)
            {
                if (run[i].ChargeSlots > 0)
                    temp.Add(run[i]);
            }
            XMLTools.SaveListToXMLSerializer(listStation, stationPath);
            return temp;
        }

        public int GetChargingRate()
        {
            XElement aa = XMLTools.LoadListFromXMLElement(configPath);
            int codd = XMLTools.LoadListFromXMLElement(configPath).Element("RowNumbers").Elements().Select(e => Convert.ToInt32(e.Value)).FirstOrDefault();//יש תמספר חבילה
            return codd;
        }

        public double[] batteryArr()
        {
            // return DataSource.config.returnArrBattery();
            double[] arr = new double[5] { 0.2, 0.1, 0.4, 0.4, 0.4 };
            return arr;
        }

        public int GetrunNumberPackage()
        {
            return 20;
        }

        #endregion



    }
}


