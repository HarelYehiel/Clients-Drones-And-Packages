using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    
    public interface IBL
    {
        public void Insert_options();

        /* In function Insert_options*/
        public void Adding_a_base_station(int ID, string name, double Latitude, double Longitude, int numSlots);
        public void Adding_a_drone(int ID, string model, int maxWeight, int staId);
        public void Absorption_of_a_new_customer(int ID, string nameCu, string phoneNumber, double Latitude, double Longitude);
        public void Receipt_of_package_for_delivery(string senderName, string targetName, int maxWeight, int prioerity);

        /* Until here */

        public void Update_options();

        /* In function Update_options*/
        public void Update_drone_data(int ID, string newModel);
        public void Update_station_data();
        public void Update_customer_data();
        public void Sending_a_drone_for_charging();
        public void Release_drone_from_charging(int ID,int min);
        public void Assign_a_package_to_a_drone(int droneId);
        public void Collection_of_a_package_by_drone(int droneId);
        public void Delivery_of_a_package_by_drone(int droneId);
        /* Until here */

        public void Entity_display_options();

        /* In function Entity_display_options*/
        public void base_station_view();
        public void drone_view();
        public void customer_view();
        public void package_view();
        /* Until here */

        public void List_view_options();

        /* In function List_view_options*/
        public IEnumerable<BO.StationForTheList> Displays_a_list_of_base_stations();
            public void Displays_a_list_of_information();
        public IEnumerable<BO.DroneToList> Displays_the_list_of_drones();
        public IEnumerable<BO.CustomerToList> Displays_a_list_of_customers();
        public IEnumerable<BO.ParcelToList> Displays_the_list_of_Parcels();
        public void Displays_the_list_of_packages();
        public IEnumerable<BO.ParcelToList> Displays_a_list_of_Parcels_not_yet_associated_with_the_drone();
        public IEnumerable<BO.StationForTheList> Display_of_base_stations_with_available_charging_stations();
        /* Until here */

    }
}
