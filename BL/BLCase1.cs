using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    public partial class BL : IBL
    {
        public void Adding_a_base_station(int ID,string name,double Latitude,double Longitude, int numSlots)
        {
          /*  BO.station station = new BO.station();
            station.uniqueID = ID; // update the Data source
            station.name = name;
            station.location.latitude = Latitude;
            station.location.longitude = Longitude;
            station.availableChargingStations = numSlots;
          */
            IDAL.DO.station station1 = new IDAL.DO.station();
            station1.Id = ID; // update the drones list at BL
            station1.name = name;
            IDAL.DO.Point loc = new IDAL.DO.Point();
            loc.latitude = Latitude;
            loc.longitude = Longitude;
            station1.Location = loc;
            station1.ChargeSlots = numSlots;
            //איך לשלוח מBL לDL///////////////////////////////////////////////////

            temp.inputTheStationToArray(station1);
        }
        public void Adding_a_drone(int ID,string model,int maxWeight,int staId)
        {
            IDAL.DO.station sta = new IDAL.DO.station();
            sta = temp.GetStation(staId);//////////////////////////////////////////איך לגשת לקבל את הישות של התחנה הזאת

            BO.Drone drone = new BO.Drone();
            drone.uniqueID = ID;
            drone.Model = model;
            drone.weight = (BO.Enum_BO.WeightCategories)maxWeight;
            drone.location.latitude = sta.Location.latitude;
            drone.location.longitude = sta.Location.longitude;
            drone.Status = BO.Enum_BO.DroneStatus.Baintenance;
            var rand = new Random();
            drone.Battery = rand.Next(20, 40);

            IDAL.DO.Drone drone1 = new IDAL.DO.Drone();
            drone1.Id = ID;
            drone1.Model = model;
            drone1.MaxWeight = (IDAL.DO.Enum.WeightCategories)maxWeight;
            temp.inputTheDroneToArray(drone1);
            this.listDrons.Add(drone);

        }
        public void Absorption_of_a_new_customer(int ID, string nameCu, string phoneNumber, double Latitude, double Longitude)
        {
            /*BO.Customer customer = new BO.Customer();
            customer.uniqueID = ID;
            customer.name = nameCu;
            customer.phone = phoneNumber;
            customer.location.latitude = Latitude;
            customer.location.longitude = Longitude0;*/
    

            IDAL.DO.Customer customer1 = new IDAL.DO.Customer();
            customer1.Id = ID;
            customer1.name = nameCu;
            customer1.Phone = phoneNumber;
            IDAL.DO.Point point = new IDAL.DO.Point();
            point.latitude = Latitude;
            point.longitude = Longitude;
            customer1.location = point;
            temp.inputTheCustomerToArray(customer1);
        }
        public void Receipt_of_package_for_delivery(int senderName, int targetName, int maxWeight, int prioerity)
        {
           /* BO.Parcel parcel = new BO.Parcel();
            parcel.customerInDelivery_Sender.name = senderName;
            parcel.customerInDelivery_Target.name = targetName;
            parcel.weight = (BO.Enum_BO.WeightCategories)maxWeight;
            parcel.priority = (BO.Enum_BO.Priorities)prioerity;
            parcel.requested = DateTime.Now;
            parcel.droneInParcel.uniqueID = 0;
            //איך לשלוח מBL לDL
            */
            IDAL.DO.Parcel parcel1 = new IDAL.DO.Parcel();
            parcel1.SenderId = senderName;
            parcel1.TargetId = targetName;
            parcel1.weight = (IDAL.DO.Enum.WeightCategories)maxWeight;
            parcel1.priority = (IDAL.DO.Enum.Priorities)prioerity;
            parcel1.Requested = DateTime.Now;
            parcel1.DroneId = 0;
            DateTime def = new DateTime();
            parcel1.PickedUp = def;
            parcel1.Scheduled = def;
            parcel1.Delivered = def;

            temp.inputTheParcelToArray(parcel1);
        }
    }
}
