using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    public partial class BL : IBL
    {
        public void Update_drone_data(int ID, string newModel)
        {
            IDAL.DO.Drone drone = temp.GetDrone(ID);
            drone.Model = newModel;
            temp.
        
        }
        public void Update_station_data() { }
        public void Update_customer_data() { }
        public void Sending_a_drone_for_charging() { }
        public void Release_drone_from_charging(int ID,int min)
        {
            
            foreach(BO.Drone drone in listDrons)
                if (drone.uniqueID == ID)
                    if(drone.Status != BO.Enum.DroneStatus.Baintenance)
                        throw ""
                    else
                    {
                        drone.Battery = 
                    }
            temp.setFreeStation(ID);
        }
        public void Assign_a_package_to_a_drone(int droneId) { }
        public void Collection_of_a_package_by_drone(int droneId) { }
        public void Delivery_of_a_package_by_drone(int droneId) { }
    }
}
