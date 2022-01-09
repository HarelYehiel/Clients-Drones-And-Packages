using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace BlApi
{
    public partial class BL : IBL
    {
        //public delegate BO.DroneToList Converter<in IDAL.DO.Drone, out BO.DroneToList>(IDAL.DO.Drone input);
        public void AddingBaseStation(int ID,string name,double Latitude,double Longitude, int numSlots)
        {
          /*  BO.station station = new BO.station();
            station.uniqueID = ID; // update the Data source
            station.name = name;
            station.location.latitude = Latitude;
            station.location.longitude = Longitude;
            station.availableChargingStations = numSlots;
          */
            Station station1 = new Station();
            station1.id = ID; // update the drones list at BL
            station1.name = name;
            Point loc = new Point();
            loc.latitude = Latitude;
            loc.longitude = Longitude;
            station1.Location = loc;
            station1.ChargeSlots = numSlots;

            accessDal.InputTheStation(station1);
        } 

        public void AddingDrone(int ID,string model,int maxWeight,int staId)
        {
            Station sta = new Station();
            sta = accessDal.GetStation(staId);

            BO.DroneToList drone = new BO.DroneToList();
            drone.uniqueID = ID;
            drone.Model = model;
            drone.weight = (BO.EnumBO.WeightCategories)maxWeight;

            BO.Location locationBO = new BO.Location();
            locationBO.latitude = sta.Location.latitude;
            locationBO.longitude = sta.Location.longitude;
            drone.location = locationBO;
            drone.packageDelivered = 0;
            
            drone.status = BO.EnumBO.DroneStatus.Baintenance;
            var rand = new Random();
            drone.Battery = rand.Next(20, 40);

            Drone drone1 = new Drone();
            drone1.Id = ID;
            drone1.Model = model;
            drone1.MaxWeight = (DO.Enum.WeightCategories)maxWeight;
            accessDal.InputTheDrone(drone1);
            this.ListDroneToList.Add(drone);

        }
        public void AbsorptionNewCustomer(int ID, string nameCu, string phoneNumber, double Latitude, double Longitude)
        {
            /*BO.Customer customer = new BO.Customer();
            customer.uniqueID = ID;
            customer.name = nameCu;
            customer.phone = phoneNumber;
            customer.location.latitude = Latitude;
            customer.location.longitude = Longitude0;*/
    

            Customer customer1 = new Customer();

            customer1.Id = ID;
            customer1.name = nameCu;
            customer1.phone = phoneNumber;
            Point point = new Point();
            point.latitude = Latitude;
            point.longitude = Longitude;
            customer1.location = point;
            accessDal.InputTheCustomer(customer1);
        }
        public void ReceiptOfPackageForDelivery(int parcelID, int senderName, int targetName, int maxWeight, int prioerity)
        {
           /* BO.Parcel parcel = new BO.Parcel();
            parcel.customerInDelivery_Sender.name = senderName;
            parcel.customerInDelivery_Target.name = targetName;
            parcel.weight = (BO.Enum_BO.WeightCategories)maxWeight;
            parcel.priority = (BO.Enum_BO.Priorities)prioerity;
            parcel.requested = DateTime.Now;
            parcel.droneInParcel.uniqueID = 0;
            */
            Parcel parcel1 = new Parcel();
            parcel1.Id = parcelID;
            parcel1.SenderId = senderName;
            parcel1.TargetId = targetName;
            parcel1.weight = (DO.Enum.WeightCategories)maxWeight;
            parcel1.priority = (DO.Enum.Priorities)prioerity;
            parcel1.Requested = DateTime.Now;
            parcel1.DroneId = 0;
            DateTime def = new DateTime();
            parcel1.PickedUp = def;
            parcel1.Scheduled = def;
            parcel1.Delivered = def;

            accessDal.InputTheParcel(parcel1);
        }

        public void Simulator(int droneId, Func<bool> f1, Action action)
        {
            throw new NotImplementedException();
        }
    }
}
