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


        int fun_parce_lsituation(IDAL.DO.Parcel p)
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
            List<DroneCharge> droneCharges_DataS = new List<DroneCharge>();

            droneCharges_DataS = DataSource.droneCharge.ToList();
            stations_DO = DataSource.stations.ToList<IDAL.DO.station>();

            BO.station bs = new BO.station();

            foreach (IDAL.DO.station item in stations_DO)
            {
                stationForTheList.uniqueID = item.Id;
                stationForTheList.name = item.name;
                stationForTheList.availableChargingStations = item.ChargeSlots;

                // Counter the unavailable charging stations.
                int counter_unAvailableChargingStations = 0;
                foreach (DroneCharge item_DroneCharge in droneCharges_DataS)
                {
                    if (item_DroneCharge.staitionId == item.Id)
                        counter_unAvailableChargingStations++;
                }
                stationForTheList.unAvailableChargingStations = counter_unAvailableChargingStations;

                stationsForTheList_BO.Add(stationForTheList);
            }
            return stationsForTheList_BO;
        }
        public IEnumerable<DroneToList> Displays_the_list_of_drones()
        {
            if (DataSource.drones.Count == 0)
                throw new MyExeption_BO(MyExeption_BO.An_empty_list);

            return List_droneToList;

            /* //List<IDAL.DO.Drone> drones_DO = new List<IDAL.DO.Drone>();
            //List<DroneToList> DronesToList_BO = new List<DroneToList>();
            //drones_DO = DataSource.drones.ToList<IDAL.DO.Drone>();
            //DroneToList droneToList = new DroneToList();
            //foreach (IDAL.DO.Drone item in drones_DO)
            //{
            //    droneToList.uniqueID = item.Id;
            //    droneToList.Model = item.Model;
            //    droneToList.weight = (BO.Enum.WeightCategories)(int)item.MaxWeight;
            //    droneToList.status = (BO.Enum.DroneStatus)(int)item.droneStatus;
            //    foreach (DroneToList item_droneToList in List_droneToList)
            //    {
            //        if(item_droneToList.uniqueID == droneToList.uniqueID)
            //        // Can take this informtion just from the list in IBL.BL
            //        {
            //            droneToList.location.latitude = item_droneToList.location.latitude;
            //            droneToList.location.longitude = item_droneToList.location.longitude;
            //            droneToList.packageDelivered = item_droneToList.packageDelivered;
            //            droneToList.Battery = item_droneToList.Battery;
            //        }
            //    }
            //    DronesToList_BO.Add(droneToList);
            //}
            //return DronesToList_BO; 
            */
        }
        public IEnumerable<CustomerToList> Displays_a_list_of_customers()
        {
            if (DataSource.customers.Count == 0)
                throw new MyExeption_BO(MyExeption_BO.An_empty_list);

            List<IDAL.DO.Customer> customers_DO = new List<IDAL.DO.Customer>();
            List<CustomerToList> CustomersToList_BO = new List<CustomerToList>();
            
            customers_DO = DataSource.customers.ToList<IDAL.DO.Customer>();

            CustomerToList customerToList = new CustomerToList();

            foreach (IDAL.DO.Customer item in customers_DO)
            {
                customerToList.uniqueID = item.Id;
                customerToList.Name = item.Name;
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

            ParcelToList ParceslToList_BO = new ParcelToList();
            List<IDAL.DO.Parcel> parcels_DO = new List<IDAL.DO.Parcel>();
            List<ParcelToList> parcels_BO = new List<ParcelToList>();

            parcels_DO = DataSource.parcels.ToList<IDAL.DO.Parcel>();

            foreach (IDAL.DO.Parcel item in parcels_DO)
            {
                ParceslToList_BO.uniqueID = item.Id;
                ParceslToList_BO.nameTarget = dalO.GetCustomer(item.TargetId).Name;
                ParceslToList_BO.namrSender = dalO.GetCustomer(item.SenderId).Name;
                ParceslToList_BO.priority = (BO.Enum.Priorities)(int)item.Priority;
                ParceslToList_BO.weight = (BO.Enum.WeightCategories)(int)item.Weight;
                ParceslToList_BO.parcelsituation = (BO.Enum.Situations)fun_parce_lsituation(item);

                parcels_BO.Add(ParceslToList_BO);
            }
            return parcels_BO;
        }
        public IEnumerable<ParcelToList> Displays_a_list_of_Parcels_not_yet_associated_with_the_drone()
        {
            if (DataSource.parcels.Count == 0)
                throw new MyExeption_BO(MyExeption_BO.An_empty_list);

            ParcelToList ParceslToList_BO = new ParcelToList();
            List<IDAL.DO.Parcel> parcels_DO = new List<IDAL.DO.Parcel>();
            List<ParcelToList> parcels_BO = new List<ParcelToList>();

            parcels_DO = DataSource.parcels.ToList<IDAL.DO.Parcel>();
            
            foreach (IDAL.DO.Parcel item in DataSource.parcels)
            {
                if (item.DroneId == 0)
                {
                    ParceslToList_BO.uniqueID = item.Id;
                    ParceslToList_BO.nameTarget = dalO.GetCustomer(item.TargetId).Name;
                    ParceslToList_BO.namrSender = dalO.GetCustomer(item.SenderId).Name;
                    ParceslToList_BO.priority = (BO.Enum.Priorities)(int)item.Priority;
                    ParceslToList_BO.weight = (BO.Enum.WeightCategories)(int)item.Weight;
                    ParceslToList_BO.parcelsituation = BO.Enum.Situations.created;

                    parcels_BO.Add(ParceslToList_BO);
                }
            }
            return parcels_BO;
        }
        public IEnumerable<StationForTheList> Display_of_base_stations_with_available_charging_stations()
        {

            if (DataSource.stations.Count == 0)
                throw new MyExeption_BO(MyExeption_BO.An_empty_list);

            List<IDAL.DO.station> stations_DO = new List<IDAL.DO.station>();
            List<StationForTheList> stationsForTheList_BO = new List<StationForTheList>();
            StationForTheList stationForTheList = new StationForTheList();

            stations_DO = DataSource.stations.ToList<IDAL.DO.station>();

            BO.station bs = new BO.station();

            foreach (IDAL.DO.station item in stations_DO)
            {
                if (item.ChargeSlots > 0)
                {
                    stationForTheList.uniqueID = item.Id;
                    stationForTheList.name = item.name;
                    stationForTheList.availableChargingStations = item.ChargeSlots;

                    foreach (IDAL.DO.DroneCharge item_droneCarge in IDAL.DalObject.DataSource.droneCharge)
                    {
                        if (item_droneCarge.staitionId == item.Id)
                            stationForTheList.unAvailableChargingStations +;
                    }

                    stationsForTheList_BO.Add(stationForTheList);
                }
            }
            return stationsForTheList_BO;
        }
    }
}
