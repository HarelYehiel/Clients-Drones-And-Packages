using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace IDAL
{
    namespace DO
    {
        interface IDal
        {
            public double[] powerConsumptionBySkimmer();
            public int addParcel();

            public ref IDAL.DO.station GetStation(int stationId);
            // Return the station with stationId

            public ref IDAL.DO.Drone GetDrone(int droneId);

            public ref IDAL.DO.Customer GetCustomer(int CustomerId);

            public ref IDAL.DO.Parcel GetParcel(int ParcelId);
            // Return the parcel with parcelId

            public void inputTheStationToArray(station station);

            public void inputTheParcelToArray(Parcel par);

            public void inputTheCustomerToArray(Customer cust);

            public void inputTheDroneToArray(Drone dro);
            public IDAL.DO.station[] Displays_list_of_stations();
            //Copy all the station from DataSource.stations[] to new_array_stations.
            public IDAL.DO.Customer[] Displays_list_of_custmers();
            //Copy all the customer from DataSource.customers[] to new_array_custmers.
            public IDAL.DO.Parcel[] Displays_list_of_Parcels();
            //Copy all the Parcel from DataSource.parcels[] to new_array_parcels.
            public IDAL.DO.Drone[] Displays_list_of_drone();
            //Copy all the Drone from DataSource.drones[] to new_array_drones.
            public void displaysParcelsDontHaveDrone();
            // Print the details of all the parcels don't have An associated skimmer (Selected_drone == 0).
            public void AvailableChargingStations();
            //Print the all stations that have DroneCharge available
            public void MinmumFromCustomer(double minDistance, Point p);

            public void MinimumFromStation(double minDistance, Point p);
        }

    }
}
