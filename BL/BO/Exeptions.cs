using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BL;
using DO;


namespace BO
{

    [Serializable]
    public class MissingIdException : Exception
    {
        public int ID;

        public string EntityName;
        public MissingIdException(int id, string entity) : base() { ID = id; EntityName = entity; }
        public MissingIdException(int id, string entity, string message) :
            base(message)
        { ID = id; EntityName = entity; }
        public MissingIdException(int id, string entity, string message, Exception innerException) :
            base(message, innerException)
        { ID = id; EntityName = entity; }
        public override string ToString() => base.ToString() + $", {EntityName} - missing id: {ID}";
    }

    [Serializable]
    public class DuplicateIdException : Exception
    {
        public int ID;

        public string EntityName;
        public DuplicateIdException(int id, string entity) : base() { ID = id; EntityName = entity; }
        public DuplicateIdException(int id, string entity, string message) :
            base(message)
        { ID = id; EntityName = entity; }
        public DuplicateIdException(int id, string entity, string message, Exception innerException) :
            base(message, innerException)
        { ID = id; EntityName = entity; }
        public override string ToString() => base.ToString() + $", {EntityName} - duplicate id: {ID}";
    }

    [Serializable]
    public class UnsuitableDroneMode : Exception// מצב רחפן לא מתאים
    {
        public DroneStatuses Status;

        public string EntityName;
        public UnsuitableDroneMode(DroneStatuses status, string entity) : base() { Status = status; EntityName = entity; }
        public UnsuitableDroneMode(DroneStatuses status, string entity, string message) :
            base(message)
        { Status = status; EntityName = entity; }
        public UnsuitableDroneMode(DroneStatuses status, string entity, string message, Exception innerException) :
            base(message, innerException)
        { Status = status; EntityName = entity; }
        public override string ToString() => base.ToString() + $", {EntityName} - Unsuitable Drone Mode: {Status}";
    }
    [Serializable]
    public class NotEnoughBattery : Exception// בעיה בסוללה
    {
        public double Baterry;

        public string EntityName;
        public NotEnoughBattery(double baterry, string entity) : base() { Baterry = baterry; EntityName = entity; }
        public NotEnoughBattery(double baterry, string entity, string message) :
            base(message)
        { Baterry = baterry; EntityName = entity; }
        public NotEnoughBattery(double baterry, string entity, string message, Exception innerException) :
            base(message, innerException)
        { Baterry = baterry; EntityName = entity; }
        public override string ToString() => base.ToString() + $", {EntityName} - problem in  Battery: {Baterry}";
    }
    [Serializable]
    public class NoFreeCharging : Exception// אין עמדות טעינה פנויות )
    {

        public string EntityName;
        public NoFreeCharging() : base() { }
        public NoFreeCharging(string entity, string message) :
            base(message)
        { EntityName = entity; }
        public NoFreeCharging(string entity, string message, Exception innerException) :
            base(message, innerException)
        { EntityName = entity; }
        public override string ToString() => base.ToString() + $", {EntityName} - There are no free charging stations ";
    }

    [Serializable]
    public class ErorrValueExceptin : Exception
    {
        public int ID;

        public string EntityName;
        public ErorrValueExceptin(int id, string entity) : base() { ID = id; EntityName = entity; }
        public ErorrValueExceptin(int id, string entity, string message) :
            base(message)
        { ID = id; EntityName = entity; }
        public ErorrValueExceptin(int id, string entity, string message, Exception innerException) :
            base(message, innerException)
        { ID = id; EntityName = entity; }
        public override string ToString() => base.ToString() + $", {EntityName} - duplicate id: {ID}";
    }
    [Serializable]
    public class CanNotRelease : Exception//חריגה חרפן לא יכול להשתחרר מטעינה
    {
        public int ID;

        public string EntityName;
        public CanNotRelease(int id, string entity) : base() { ID = id; EntityName = entity; }
        public CanNotRelease(int id, string entity, string message) :
            base(message)
        { ID = id; EntityName = entity; }
        public CanNotRelease(int id, string entity, string message, Exception innerException) :
            base(message, innerException)
        { ID = id; EntityName = entity; }
    }

}
