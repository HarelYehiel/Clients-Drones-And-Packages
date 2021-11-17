using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DalObject;
using IDAL.DO;
using IBL.BO;
using IBL;

namespace IBL
{
    public partial class BL : IBL
    {
        DalObject dalO = new DalObject();

        int FunParcelSituation(IDAL.DO.Parcel p)
        {
            if (p.Delivered != new DateTime()) return 3;
            else if (p.PickedUp != new DateTime()) return 2;
            else if (p.Scheduled != new DateTime()) return 1;
            return 0;

        }
        public IEnumerable<StationForTheList> DisplaysListOfBaseStations()
        {
            if (DataSource.stations.Count == 0)
                throw new MyExeption_BO("Exception from function 'Displays_a_list_of_base_stations'", MyExeption_BO.An_empty_list);

            List<IDAL.DO.Station> stationsDO = new List<IDAL.DO.Station>();
            List<StationForTheList> stationsForTheListBO = new List<StationForTheList>();
            StationForTheList stationForTheList;
            List<DroneCharge> dronesCharges_DataS = new List<DroneCharge>();

            foreach (IDAL.DO.Station station_DO in DataSource.stations)
            {
                stationForTheList = new StationForTheList();
                stationForTheList.uniqueID = station_DO.id;
                stationForTheList.name = station_DO.name;
                stationForTheList.availableChargingStations = station_DO.ChargeSlots;

                // Counter the unavailable charging stations.
                foreach (DroneCharge item_DroneCharge in DataSource.dronesCharge)
                {
                    if (item_DroneCharge.staitionId == station_DO.id)
                        stationForTheList.unAvailableChargingStations++;
                }

                stationsForTheListBO.Add(stationForTheList);
            }

            return stationsForTheListBO;
        }
        public IEnumerable<DroneToList> DisplaysTheListOfDrones()
        {
            try
            {
                if (ListDroneToList.Count == 0)
                    throw new MyExeption_BO( MyExeption_BO.An_empty_list);

                return ListDroneToList;
            }
            catch (Exception e)
            {

                throw new MyExeption_BO("Exception from function 'Displays_the_list_of_drones'", e);
            }

        }
        public IEnumerable<CustomerToList> DisplaysListOfCustomers()
        {
            if (DataSource.customers.Count == 0)
                throw new MyExeption_BO("Exception from function 'Displays_a_list_of_customers'", MyExeption_BO.An_empty_list);

            List<CustomerToList> CustomersToListBO = new List<CustomerToList>();

            foreach (IDAL.DO.Customer item in DataSource.customers)
            {
                CustomerToList customerToList = new CustomerToList();

                customerToList.uniqueID = item.Id;
                customerToList.name = item.name;
                customerToList.phone = item.phone;

                foreach (IDAL.DO.Parcel parcelFromTheList in DataSource.parcels)
                // Run on the list parcel and find the parcels the related him (the customer).
                {
                    // packages sent and delivered
                    if (parcelFromTheList.SenderId == item.Id && parcelFromTheList.Delivered != new DateTime())
                        customerToList.packagesSentAndDelivered++;
                    // packages sent and not delivered
                    if (parcelFromTheList.SenderId == item.Id && parcelFromTheList.Scheduled != new DateTime() && parcelFromTheList.Delivered == new DateTime())
                        customerToList.packagesSentAndNotDelivered++;
                    // packages he received
                    if (parcelFromTheList.TargetId == item.Id && parcelFromTheList.Delivered != new DateTime())
                        customerToList.packagesHeReceived++;
                    // packages on the way to the customer
                    if (parcelFromTheList.TargetId == item.Id && parcelFromTheList.Scheduled != new DateTime() && parcelFromTheList.Delivered == new DateTime())
                        customerToList.packagesOnTheWayToTheCustomer++;
                }

                CustomersToListBO.Add(customerToList);
            }

            return CustomersToListBO;
        }
        public IEnumerable<ParcelToList> DisplaysTheListOfParcels()
        {
            if (DataSource.parcels.Count == 0)
                throw new MyExeption_BO("Exception from function 'Displays_the_list_of_Parcels'", MyExeption_BO.An_empty_list);

            ParcelToList parcelToListBO;
            List<ParcelToList> ParcelsToList_BO = new List<ParcelToList>();

            try
            {
                foreach (IDAL.DO.Parcel item in DataSource.parcels)
                {
                    parcelToListBO = new ParcelToList();
                    parcelToListBO.uniqueID = item.Id;
                    parcelToListBO.nameTarget = dalO.GetCustomer(item.TargetId).name;
                    parcelToListBO.namrSender = dalO.GetCustomer(item.SenderId).name;
                    parcelToListBO.priority = (BO.EnumBO.Priorities)(int)item.priority;
                    parcelToListBO.weight = (BO.EnumBO.WeightCategories)(int)item.weight;
                    parcelToListBO.parcelsituation = (BO.EnumBO.Situations)FunParcelSituation(item);

                    ParcelsToList_BO.Add(parcelToListBO);
                }

                return ParcelsToList_BO;
            }
            catch (Exception e)
            {

                throw new BO.MyExeption_BO("Exception from function 'Displays_the_list_of_Parcels'", e);
            }

        }
        public IEnumerable<ParcelToList> DisplaysListOfParcelsNotYetAssociatedWithDrone()
        {
            if (DataSource.parcels.Count == 0)
                throw new MyExeption_BO("Exception from function 'Displays_a_list_of_Parcels_not_yet_associated_with_the_drone'", MyExeption_BO.An_empty_list);

            ParcelToList ParceslToListBO;
            List<ParcelToList> ParcelsToListBO = new List<ParcelToList>();

            try
            {


                foreach (IDAL.DO.Parcel item in DataSource.parcels)
                {
                    if (item.DroneId == 0)
                    {
                        ParceslToListBO = new ParcelToList();
                        ParceslToListBO.uniqueID = item.Id;
                        ParceslToListBO.nameTarget = dalO.GetCustomer(item.TargetId).name;
                        ParceslToListBO.namrSender = dalO.GetCustomer(item.SenderId).name;
                        ParceslToListBO.priority = (BO.EnumBO.Priorities)(int)item.priority;
                        ParceslToListBO.weight = (BO.EnumBO.WeightCategories)(int)item.weight;
                        ParceslToListBO.parcelsituation = BO.EnumBO.Situations.created;

                        ParcelsToListBO.Add(ParceslToListBO);
                    }

                }

                return ParcelsToListBO;
            }
            catch (Exception e)
            {

                throw new BO.MyExeption_BO("Exception from function 'Displays_a_list_of_Parcels_not_yet_associated_with_the_drone'", e);
            }
        }
        public IEnumerable<StationForTheList> DisplayBaseStationsWithAvailableChargingStations()
        {

            if (DataSource.stations.Count == 0)
                throw new MyExeption_BO("Exception from function 'Display_of_base_stations_with_available_charging_stations'", MyExeption_BO.An_empty_list);

            List<StationForTheList> stationsForTheListBO = new List<StationForTheList>();
            StationForTheList stationForTheList;

            foreach (IDAL.DO.Station station_DO in DataSource.stations)
            {
                if (station_DO.ChargeSlots > 0)
                {
                    stationForTheList = new StationForTheList();
                    stationForTheList.uniqueID = station_DO.id;
                    stationForTheList.name = station_DO.name;
                    stationForTheList.availableChargingStations = station_DO.ChargeSlots;

                    foreach (IDAL.DO.DroneCharge droneCarge_DO in IDAL.DalObject.DataSource.dronesCharge)
                    {
                        if (droneCarge_DO.staitionId == station_DO.id)
                            stationForTheList.unAvailableChargingStations++;
                    }

                    stationsForTheListBO.Add(stationForTheList);
                }
            }

            return stationsForTheListBO;
        }
    }
}