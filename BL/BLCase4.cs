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

        /* In function List_view_options*/
        public List<station> Displays_a_list_of_base_stations()
        {
            if (DataSource.stations.Count == 0)
                throw new MyExeption(MyExeption.An_empty_list);

            List<station> stations_1 = new List<station>();
            stations_1 = DataSource.stations.ToList<station>();
            return stations_1;
        }
        public List<Drone> Displays_the_list_of_drones()
        {
            if (DataSource.drones.Count == 0)
                throw new MyExeption(MyExeption.An_empty_list);

            List<Drone> drones_1 = new List<Drone>();
            drones_1 = DataSource.drones.ToList<Drone>();
            return drones_1;
        }
        public List<Customer> Displays_a_list_of_customers()
        {
            if (DataSource.customers.Count == 0)
                throw new MyExeption(MyExeption.An_empty_list);

            List<Customer> Customer_1 = new List<Customer>();
            Customer_1 = DataSource.customers.ToList<Customer>();
            return Customer_1;
        }
        public List<Parcel> Displays_the_list_of_Parcels()
        {
            if (DataSource.parcels.Count == 0)
                throw new MyExeption(MyExeption.An_empty_list);

            List<Parcel> parcels_1 = new List<Parcel>();
            parcels_1 = DataSource.parcels.ToList<Parcel>();
            return parcels_1;
        }
        public List<Parcel> Displays_a_list_of_Parcels_not_yet_associated_with_the_drone()
        {
            if (DataSource.parcels.Count == 0)
                throw new MyExeption(MyExeption.An_empty_list);

            List<Parcel> parcels_1 = new List<Parcel>();
            foreach (Parcel item in DataSource.parcels)
            {
                if (item.DroneId == 0)
                    // Not associated drone to parcel. 
                    parcels_1.Add(item);
            }
            return parcels_1;
        }
        public List<station> Display_of_base_stations_with_available_charging_stations() {

            if (DataSource.stations.Count == 0)
                throw new MyExeption(MyExeption.An_empty_list);
                
            List<station> stations_1 = new List<station>();
            foreach (station item in DataSource.stations)
            {
                if (item.ChargeSlots > 0)
                    // Available place to charging.
                    stations_1.Add(item);
            }

            return stations_1;
        }
        /* Until here */
    }
}
