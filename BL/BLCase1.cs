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
            BO.station station = new BO.station();
            IDAL.DO.station station1 = new IDAL.DO.station();
            station.uniqueID = ID; // update the Data source
            station1.Id = ID; // update the drones list at BL
            station.name = name;
            station1.name = name;
            station.location.latitude = Latitude;
            station.location.longitude = Longitude;
            IDAL.DO.Point loc = new IDAL.DO.Point();
            loc.Latitude = Latitude;
            loc.Longitude = Longitude;
            station1.Location = loc;
            station.availableChargingStations = numSlots;
            station1.ChargeSlots = numSlots;
            //איך לשלוח מBL לDL///////////////////////////////////////////////////

            temp.inputTheStationToArray(station1);
        }
        public void Adding_a_drone(int ID,string model,int maxWeight,int staId)
        {
            BO.Drone drone = new BO.Drone();
            IDAL.DO.Drone drone1 = new IDAL.DO.Drone();
            drone.uniqueID = ID;
            drone.Model = model;
            drone1.Model = model;
            drone.weight = (BO.Enum.WeightCategories)maxWeight;
            drone1.MaxWeight = (IDAL.DO.Enum.WeightCategories)maxWeight;
            IDAL.DO.station sta = new IDAL.DO.station();
            sta = IDAL.DalObject.DalObject GetStation(staId);//////////////////////////////////////////איך לגשת לקבל את הישות של התחנה הזאת
            drone.location.latitude = sta.Location.Latitude;
            drone.location.longitude = sta.Location.Longitude;
            drone.Status = BO.Enum.DroneStatus.Baintenance;
            var rand = new Random();
            drone.Battery = rand.Next(20,40);
            //איך לשלוח מBL לDL///////////////////////////////////////////////////
            temp.inputTheDroneToArray(drone);
            this.listDrons.Add(station);

        }
        public void Absorption_of_a_new_customer(int ID, string nameCu, string phoneNumber, double Latitude, double Longitude)
        {
            BO.Customer customer = new BO.Customer();
            customer.uniqueID = ID;
            customer.name = nameCu;
            customer.phone = phoneNumber;
            customer.location.latitude = Latitude;
            customer.location.longitude = Longitude;
            //איך לשלוח מBL לDL
            temp.inputTheCustomerToArray(customer);
        }
        public void Receipt_of_package_for_delivery(string senderName, string targetName, int maxWeight, int prioerity)
        {
            BO.Parcel parcel = new BO.Parcel();
            parcel.namrSender = senderName;
            parcel.nameTarget = targetName;
            parcel.weight = (BO.Enum.WeightCategories)maxWeight;
            parcel.priority = (BO.Enum.Priorities)prioerity;
            parcel.requested = DateTime.Now;
            parcel.drone = 0;

            //איך לשלוח מBL לDL
            temp.inputTheParcelToArray(parcel);
        }
    }
}
