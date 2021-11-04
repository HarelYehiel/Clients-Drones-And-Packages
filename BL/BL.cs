using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{

    public class BL : IBL
    {
        List<BO.Drone> listDrons = new List<BO.Drone>();
        IDAL.DO.IDal temp = new  IDAL.DalObject.DalObject();


        public void Insert_options() { }

        /* In function Insert_options*/
        public void Adding_a_base_station() { }
        public void Adding_a_drone() { }
        public void Absorption_of_a_new_customer() { }
        public void Receipt_of_package_for_delivery() { }
        /* Until here */

        public void Update_options() { }

        /* In function Update_options*/
        public void Update_drone_data() { }
        public void Update_station_data() { }
        public void Update_customer_data() { }
        public void Sending_a_drone_for_charging() { }
        public void Release_drone_from_charging() { }
        public void Assign_a_package_to_a_drone() { }
        public void Collection_of_a_package_by_drone() { }
        public void Delivery_of_a_package_by_drone() { }
        /* Until here */

        public void Entity_display_options() { }

        /* In function Entity_display_options*/
        public void base_station_view() { }
        public void drone_view() { }
        public void customer_view() { }
        public void package_view() { }
        /* Until here */

        public void List_view_options() { }

        /* In function List_view_options*/
        public void Displays_a_list_of_base_stations() { }
        public void Displays_the_list_of_drones() { }
        public void Displays_a_list_of_information() { }
        public void Displays_the_list_of_packages() { }
        public void Displays_a_list_of_packages_not_yet_associated_with_the_drone() { }
        public void Display_of_base_stations_with_available_charging_stations(){ }
        /* Until here */
    }
}
