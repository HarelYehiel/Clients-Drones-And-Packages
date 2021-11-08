using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    public partial class BL : IBL
    {
        IDAL.DO.IDal temp1 = new IDAL.DalObject.DalObject(); 
        public void Update_drone_data(int ID, string newModel)
        {
            IDAL.DO.Drone drone = temp1.GetDrone(ID);
            drone.Model = newModel;
            temp1.
        
        }
        public void Update_station_data() { }
        public void Update_customer_data() { }
        public void Sending_a_drone_for_charging() { }
        public void Release_drone_from_charging() { }
        public void Assign_a_package_to_a_drone() { }
        public void Collection_of_a_package_by_drone() { }
        public void Delivery_of_a_package_by_drone() { }
    }
}
