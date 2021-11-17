using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    
    public interface IBL
    {
        public void InsertOptions();

        /* In function Insert_options*/
        public void AddingBaseStation(int ID, string name, double Latitude, double Longitude, int numSlots);
        public void AddingDrone(int ID, string model, int maxWeight, int staId);
        public void AbsorptionNewCustomer(int ID, string nameCu, string phoneNumber, double Latitude, double Longitude);
        public void ReceiptOfPackageForDelivery(int parcelID, int senderName, int targetName, int maxWeight, int prioerity);

        /* Until here */

        public void UpdateOptions();

        /* In function Update_options*/
        public void UpdateDroneData(int ID, string newModel);
        public void UpdateStationData(int ID, string name, int numSlots);
        public void UpdateCustomerData(int ID, string custName, string phoneNumber);
        public void SendingDroneToCharging(int ID);
        public void ReleaseDroneFromCharging(int ID,int min);
        public void AssignPackageToDrone(int droneId);
        public void CollectionOfPackageByDrone(int droneId);
        public void DeliveryOfPackageByDrone(int droneId);
        /* Until here */

        public void EntityDisplayOptions();

        /* In function Entity_display_options*/
        public BO.station BaseStationView(int ID);
        public BO.Drone DroneView(int ID);
        public BO.Customer CustomerView(int ID);
        public BO.Parcel ParcelView(int ID);
        /* Until here */

        public void ListViewOptions();

        /* In function List_view_options*/
        public IEnumerable<BO.StationForTheList> DisplaysListOfBaseStations();
        public IEnumerable<BO.DroneToList> DisplaysTheListOfDrones();
        public IEnumerable<BO.CustomerToList> DisplaysListOfCustomers();
        public IEnumerable<BO.ParcelToList> DisplaysTheListOfParcels();
        public IEnumerable<BO.ParcelToList> DisplaysListOfParcelsNotYetAssociatedWithDrone();
        public IEnumerable<BO.StationForTheList> DisplayBaseStationsWithAvailableChargingStations();
        /* Until here */

    }
}
