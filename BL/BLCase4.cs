using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DalObject;
using IDAL.DO;

namespace IBL
{
    public partial class BL : IBL
    {
        //public void List_view_options() { } לא יודע מה זה

        /* In function List_view_options*/
        public List<station> Displays_a_list_of_base_stations()
        {
            List<station> stations_1 = new List<station>();
            stations_1 = DataSource.stations.ToList<station>();
            return stations_1;
        }
        public List<Drone> Displays_the_list_of_drones()
        {
            List<Drone> drones_1 = new List<Drone>();
            drones_1 = DataSource.drones.ToList<Drone>();
            return drones_1;
        }
        public List<Customer> Displays_a_list_of_customers()
        {
            List<Customer> Customer_1 = new List<Customer>();
            Customer_1 = DataSource.customers.ToList<Customer>();
            return Customer_1;
        }
        public List<Parcel> Displays_the_list_of_Parcels()
        {
            List<Parcel> parcels_1 = new List<Parcel>();
            parcels_1 = DataSource.parcels.ToList<Parcel>();
            return parcels_1;
        }
        public void Displays_a_list_of_Parcels_not_yet_associated_with_the_drone()
        {

            List<Parcel> parcels_1 = new List<Parcel>();

            foreach (Parcel item in DataSource.parcels)
            {
                if (item.Scheduled == DateTime.MinValue)  ???????????????/
            }
        }
        public void Display_of_base_stations_with_available_charging_stations() { }
        /* Until here */
    }
}
