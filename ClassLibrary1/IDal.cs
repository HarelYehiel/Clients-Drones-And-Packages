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
            
            public List<double> PowerConsumptionBySkimmer();
            public void AddParcel(Parcel par);

            public Station GetStation(int stationId);
            // Return the station with stationId

            public Drone GetDrone(int droneId);

            public Customer GetCustomer(int CustomerId);

            public IDAL.DO.Parcel GetParcel(int ParcelId);
            // Return the parcel with parcelId

            public void InputTheStationToArray(Station station);

            public void InputTheParcelToArray(Parcel par);

            public void InputTheCustomerToArray(Customer cust);

            public void InputTheDroneToArray(Drone dro);
            public IEnumerable<Station> GetListOfStations();

            public IEnumerable<Customer> DisplaysListOfCustmers();

            public IEnumerable<Parcel> DisplaysListOfParcels();
            //Copy all the Parcel from DataSource.parcels[] to new_array_parcels.
            public IEnumerable<Drone> DisplaysListOfDrones();
            //Copy all the Drone from DataSource.drones[] to new_array_drones.
            public IEnumerable<Parcel> DisplaysParcelsDontHaveDrone();
            // Print the details of all the parcels don't have An associated skimmer (Selected_drone == 0).
            public IEnumerable<Station> AvailableChargingStations();
            public void AffiliationDroneToParcel(int parcelID, int droneID);
            public void PickUp(int PickId);

            public void Delivered(int deliId);
            public void SetFreeStation(int droneId);
            public void DroneToCharge(int droneId, int stationId);



            //Print the all stations that have DroneCharge available
            public string MinimumFromCustomer(double minDistance, Point p);

            public string MinimumFromStation(double minDistance, Point p);
        }

    }
}
