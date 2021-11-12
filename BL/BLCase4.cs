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
        List<DroneToList> List_droneToList = new List<DroneToList>();

        int fun_parcel_situation(IDAL.DO.Parcel p)
        {
            if (p.Delivered != new DateTime()) return 3;
            else if (p.PickedUp != new DateTime()) return 2;
            else if (p.Scheduled != new DateTime()) return 1;
            return 0;

        }
        public IEnumerable<StationForTheList> Displays_a_list_of_base_stations()
        {
            if (DataSource.stations.Count == 0)
                throw new MyExeption_BO(MyExeption_BO.An_empty_list);

            List<IDAL.DO.station> stations_DO = new List<IDAL.DO.station>();
            List<StationForTheList> stationsForTheList_BO = new List<StationForTheList>();
            StationForTheList stationForTheList = new StationForTheList();
            List<DroneCharge> dronesCharges_DataS = new List<DroneCharge>();

            //dronesCharges_DataS = DataSource.dronesCharge.ToList();
            //stations_DO = DataSource.stations.ToList<IDAL.DO.station>();

            BO.station bs = new BO.station();

            foreach (IDAL.DO.station station_DO in DataSource.stations)
            {
                stationForTheList.uniqueID = station_DO.id;
                stationForTheList.name = station_DO.name;
                stationForTheList.availableChargingStations = station_DO.ChargeSlots;

                // Counter the unavailable charging stations.
                foreach (DroneCharge item_DroneCharge in DataSource.dronesCharge)
                {
                    if (item_DroneCharge.staitionId == station_DO.id)
                        stationForTheList.unAvailableChargingStations++;
                }

                stationsForTheList_BO.Add(stationForTheList);
            }

            return stationsForTheList_BO;
        }
        public IEnumerable<DroneToList> Displays_the_list_of_drones()
        {
            if (DataSource.drones.Count == 0)
                throw new MyExeption_BO(MyExeption_BO.An_empty_list);

            return List_droneToList;
        }   
        public IEnumerable<CustomerToList> Displays_a_list_of_customers()
        {
            if (DataSource.customers.Count == 0)
                throw new MyExeption_BO(MyExeption_BO.An_empty_list);

            List<CustomerToList> CustomersToList_BO = new List<CustomerToList>();
            CustomerToList customerToList = new CustomerToList();

            foreach (IDAL.DO.Customer item in DataSource.customers)
            {
                customerToList.uniqueID = item.Id;
                customerToList.Name = item.name;
                customerToList.Phone = item.Phone;

                foreach (IDAL.DO.Parcel parcel_fromTheList in DataSource.parcels)
                // Run on the list parcel and find the parcels the related him (the customer).
                {
                    // packages sent and delivered
                    if (parcel_fromTheList.SenderId == item.Id && parcel_fromTheList.Delivered != new DateTime())
                        customerToList.packagesSentAndDelivered++;
                    // packages sent and not delivered
                    if (parcel_fromTheList.SenderId == item.Id && parcel_fromTheList.Scheduled != new DateTime() && parcel_fromTheList.Delivered == new DateTime())
                        customerToList.packagesSentAndNotDelivered++;
                    // packages he received
                    if (parcel_fromTheList.TargetId == item.Id && parcel_fromTheList.Delivered != new DateTime())
                        customerToList.packagesHeReceived++;
                    // packages on the way to the customer
                    if (parcel_fromTheList.TargetId == item.Id && parcel_fromTheList.Scheduled != new DateTime() && parcel_fromTheList.Delivered == new DateTime())
                        customerToList.packagesOnTheWayToTheCustomer++;
                }

                CustomersToList_BO.Add(customerToList);
            }

            return CustomersToList_BO;
        }
        public IEnumerable<ParcelToList> Displays_the_list_of_Parcels()
        {
            if (DataSource.parcels.Count == 0)
                throw new MyExeption_BO(MyExeption_BO.An_empty_list);

            ParcelToList ParcelToList_BO = new ParcelToList();
            List<ParcelToList> ParcelsToList_BO = new List<ParcelToList>();

            try
            {
                foreach (IDAL.DO.Parcel item in DataSource.parcels)
                {
                    ParcelToList_BO.uniqueID = item.Id;
                    ParcelToList_BO.nameTarget = dalO.GetCustomer(item.TargetId).name;
                    ParcelToList_BO.namrSender = dalO.GetCustomer(item.SenderId).name;
                    ParcelToList_BO.priority = (BO.Enum_BO.Priorities)(int)item.priority;
                    ParcelToList_BO.weight = (BO.Enum_BO.WeightCategories)(int)item.weight;
                    ParcelToList_BO.parcelsituation = (BO.Enum_BO.Situations)fun_parcel_situation(item);

                    ParcelsToList_BO.Add(ParcelToList_BO);
                }
                
                return ParcelsToList_BO;
            }
            catch (Exception e)
            {

                throw new BO.MyExeption_BO("Exception from function 'Displays_the_list_of_Parcels'",e);
            }

        }
        public IEnumerable<ParcelToList> Displays_a_list_of_Parcels_not_yet_associated_with_the_drone()
        {
            if (DataSource.parcels.Count == 0)
                throw new MyExeption_BO(MyExeption_BO.An_empty_list);

            ParcelToList ParceslToList_BO = new ParcelToList();
            List<ParcelToList> ParcelsToList_BO = new List<ParcelToList>();

            try
            {


                foreach (IDAL.DO.Parcel item in DataSource.parcels)
                {
                    if (item.DroneId == 0)
                    {
                        ParceslToList_BO.uniqueID = item.Id;
                        ParceslToList_BO.nameTarget = dalO.GetCustomer(item.TargetId).name;
                        ParceslToList_BO.namrSender = dalO.GetCustomer(item.SenderId).name;
                        ParceslToList_BO.priority = (BO.Enum_BO.Priorities)(int)item.priority;
                        ParceslToList_BO.weight = (BO.Enum_BO.WeightCategories)(int)item.weight;
                        ParceslToList_BO.parcelsituation = BO.Enum_BO.Situations.created;

                        ParcelsToList_BO.Add(ParceslToList_BO);
                    }

                }

                return ParcelsToList_BO;
            }
            catch (Exception e)
            {

                throw new BO.MyExeption_BO("Exception from function 'Displays_a_list_of_Parcels_not_yet_associated_with_the_drone'", e);
            }
        }
        public IEnumerable<StationForTheList> Display_of_base_stations_with_available_charging_stations()
        {

            if (DataSource.stations.Count == 0)
                throw new MyExeption_BO(MyExeption_BO.An_empty_list);

            List<StationForTheList> stationsForTheList_BO = new List<StationForTheList>();
            StationForTheList stationForTheList = new StationForTheList();
            BO.station bs = new BO.station();

            foreach (IDAL.DO.station station_DO in DataSource.stations)
            {
                if (station_DO.ChargeSlots > 0)
                {
                    stationForTheList.uniqueID = station_DO.id;
                    stationForTheList.name = station_DO.name;
                    stationForTheList.availableChargingStations = station_DO.ChargeSlots;

                    foreach (IDAL.DO.DroneCharge droneCarge_DO in IDAL.DalObject.DataSource.dronesCharge)
                    {
                        if (droneCarge_DO.staitionId == station_DO.id)
                            stationForTheList.unAvailableChargingStations++;
                    }

                    stationsForTheList_BO.Add(stationForTheList);
                }
            }

            return stationsForTheList_BO;
        }
    }
}