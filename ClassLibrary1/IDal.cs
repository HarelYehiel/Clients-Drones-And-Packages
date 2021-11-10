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
        public interface IDal
        {
            
            public double[] powerConsumptionBySkimmer();
            public void addParcel(Parcel par);

            public station GetStation(int stationId);
            // Return the station with stationId

            public Drone GetDrone(int droneId);

            public Customer GetCustomer(int CustomerId);

            public IDAL.DO.Parcel GetParcel(int ParcelId);
            // Return the parcel with parcelId

            public void inputTheStationToArray(station station);

            public void inputTheParcelToArray(Parcel par);

            public void inputTheCustomerToArray(Customer cust);

            public void inputTheDroneToArray(Drone dro);
            public IEnumerable<station> Displays_list_of_stations();

            public IEnumerable<Customer> Displays_list_of_custmers();

            public IEnumerable<Parcel> Displays_list_of_Parcels();
            //Copy all the Parcel from DataSource.parcels[] to new_array_parcels.
            public IEnumerable<Drone> Displays_list_of_drone();
            //Copy all the Drone from DataSource.drones[] to new_array_drones.
            public IEnumerable<Parcel> displaysParcelsDontHaveDrone();
            // Print the details of all the parcels don't have An associated skimmer (Selected_drone == 0).
            public List<station> AvailableChargingStations();
            public void AffiliationDroneToParcel(int parcelID, int droneID);
            public void pickUp(int PickId);

            public void delivered(int deliId);
            public void setFreeStation(int droneId);
            public void droneToCharge(int droneId, int stationId);



            //Print the all stations that have DroneCharge available
            public string MinimumFromCustomer(double minDistance, Point p);

            public string MinimumFromStation(double minDistance, Point p);
        }

    }
}
