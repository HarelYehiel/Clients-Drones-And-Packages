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
        // public delegate IEnumerable<StationForTheList> 
        int FunParcelSituation(IDAL.DO.Parcel p)
        {
            if (p.Delivered != null) return 3;
            else if (p.PickedUp != null) return 2;
            else if (p.Scheduled != null) return 1;
            return 0;

        }
        public IEnumerable<StationToTheList> GetListOfBaseStations()
        {
            List<BO.StationToTheList> stations = accessIdal.GetListOfStations().ToList().ConvertAll(convertStaionDoToStaionBo);
            if (stations.Count == 0)
                throw new MyExeption_BO("Exception from function 'Displays_a_list_of_base_stations'", MyExeption_BO.An_empty_list);


            return stations;
        }
        public IEnumerable<DroneToList> GetTheListOfDrones()
        {
            try
            {
                List<BO.DroneToList> dronesToList = GetAllDronesBy(p => true).ToList();

                if (dronesToList.Count == 0)
                    throw new MyExeption_BO(MyExeption_BO.An_empty_list);

                return dronesToList;
            }
            catch (Exception e)
            {

                throw new MyExeption_BO("Exception from function 'Displays_the_list_of_drones'", e);
            }

        }
        public IEnumerable<CustomerToList> GetListOfCustomers()
        {
            
            if (accessIdal.DisplaysListOfCustmers().ToList().Count == 0)
                throw new MyExeption_BO("Exception from function 'Displays_a_list_of_customers'", MyExeption_BO.An_empty_list);

            List<CustomerToList> CustomersToListBO = new List<CustomerToList>();

            CustomersToListBO = accessIdal.DisplaysListOfCustmers().ToList().FindAll()
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
                    if (parcelFromTheList.SenderId == item.Id && parcelFromTheList.Delivered != null)
                        customerToList.packagesSentAndDelivered++;
                    // packages sent and not delivered
                    if (parcelFromTheList.SenderId == item.Id && parcelFromTheList.PickedUp != null && parcelFromTheList.Delivered == null)
                        customerToList.packagesSentAndNotDelivered++;
                    // packages he received
                    if (parcelFromTheList.TargetId == item.Id && parcelFromTheList.Delivered != null)
                        customerToList.packagesHeReceived++;
                    // packages on the way to the customer
                    if (parcelFromTheList.TargetId == item.Id && parcelFromTheList.PickedUp != null && parcelFromTheList.Delivered == null)
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
                    parcelToListBO.nameTarget = accessIdal.GetCustomer(item.TargetId).name;
                    parcelToListBO.namrSender = accessIdal.GetCustomer(item.SenderId).name;
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

        public IEnumerable<ParcelToList> GetAllParcelsBy(System.Predicate<IDAL.DO.Parcel> P)
        {
            List<ParcelToList> parcelsToLists = new List<ParcelToList>();
            List<IDAL.DO.Parcel> parcels = new List<IDAL.DO.Parcel>();

            parcels = accessIdal.DisplaysListOfParcels().ToList();
            parcelsToLists.AddRange(parcels.FindAll(P)
                .ConvertAll(convertParcelDoToParcelBo));// convert parcel_do to parcel_bo

            return parcelsToLists;
        }
        public IEnumerable<StationToTheList> GetAllStaionsBy(System.Predicate<IDAL.DO.Station> P)
        {
            List<StationToTheList> StationsToTheList = new List<StationToTheList>();

            StationsToTheList.AddRange(accessIdal.GetListOfStations().ToList()
                .FindAll(P) // Get the list of stations from DO.
                .ConvertAll(convertStaionDoToStaionBo));// convert parcel_do to parcel_bo

            return StationsToTheList;
        }
        public IEnumerable<CustomerToList> GetAllCustomersBy(System.Predicate<IDAL.DO.Customer> P)
        {
            List<CustomerToList> StationsToTheList = new List<CustomerToList>();

            StationsToTheList.AddRange(accessIdal.DisplaysListOfCustmers().ToList()
                .FindAll(P) // Get the list of stations from DO.
                .ConvertAll(convertCustomerDoToCustomerBo));// convert parcel_do to parcel_bo

            return StationsToTheList;
        }
        public IEnumerable<DroneToList> GetAllDronesBy(System.Predicate<IDAL.DO.Drone> P)
        {
            List<DroneToList> DronesToList = new List<DroneToList>();

            DronesToList.AddRange(accessIdal.DisplaysListOfDrones().ToList()
                .FindAll(P) // Get the list of stations from DO.
                .ConvertAll(convertDroneDoToDroneBo));// convert parcel_do to parcel_bo

            return DronesToList;
        }

        public IEnumerable<ParcelToList> DisplaysListOfParcelsNotYetAssociatedWithDrone()
        {
            if (DataSource.parcels.Count == 0)
                throw new MyExeption_BO("Exception from function 'DisplaysListOfParcelsNotYetAssociatedWithDrone'", MyExeption_BO.An_empty_list);

            List<ParcelToList> ParcelsToListBO = new List<ParcelToList>();
            try
            {
                ParcelsToListBO.AddRange(DataSource.parcels.FindAll(parcel => parcel.DroneId == 0)
                    .ConvertAll(convertParcelDoToParcelBo));// convert parcel_do to parcel_bo

                return ParcelsToListBO;
            }
            catch (Exception e)
            {
                throw new BO.MyExeption_BO("Exception from function 'Displays_a_list_of_Parcels_not_yet_associated_with_the_drone'", e);
            }
        }
        public IEnumerable<StationToTheList> DisplayBaseStationsWithAvailableChargingStations()
        {

            if (DataSource.stations.Count == 0)
                throw new MyExeption_BO("Exception from function 'Display_of_base_stations_with_available_charging_stations'", MyExeption_BO.An_empty_list);

            List<StationToTheList> stationsForTheListBO = DataSource.stations.
                // Return list with all the station_DO.ChargeSlots > 0.
                FindAll(station_DO => station_DO.ChargeSlots > 0).
                // Retuen list with StaionBo.
                ConvertAll(convertStaionDoToStaionBo);

            return stationsForTheListBO;
        }
        ParcelToList convertParcelDoToParcelBo(IDAL.DO.Parcel item)
        {
            try
            {
                ParcelToList ParcelToListBO = new ParcelToList();

                ParcelToListBO = new ParcelToList();
                ParcelToListBO.uniqueID = item.Id;
                ParcelToListBO.nameTarget = accessIdal.GetCustomer(item.TargetId).name;
                ParcelToListBO.namrSender = accessIdal.GetCustomer(item.SenderId).name;
                ParcelToListBO.priority = (BO.EnumBO.Priorities)(int)item.priority;
                ParcelToListBO.weight = (BO.EnumBO.WeightCategories)(int)item.weight;
                ParcelToListBO.parcelsituation = BO.EnumBO.Situations.created;

                return ParcelToListBO;

            }
            catch (Exception e)
            {

                throw new BO.MyExeption_BO("Exception from function 'convertParcelDoToParcelBo'", e);
            }


        }
        StationToTheList convertStaionDoToStaionBo(IDAL.DO.Station staion)
        {
            StationToTheList stationForTheList = new StationToTheList();

            stationForTheList.uniqueID = staion.id;
            stationForTheList.name = staion.name;
            stationForTheList.availableChargingStations = staion.ChargeSlots;

            stationForTheList.unAvailableChargingStations = DataSource.dronesCharge.
                // Return list with all the droneCarge_DO == staion.id 
                FindAll(droneCarge_DO => droneCarge_DO.staitionId == staion.id).
                // Return count of item in the list.
                Count();

            return stationForTheList;
        }
        DroneToList convertDroneDoToDroneBo(IDAL.DO.Drone customer)
        {
            DroneToList droneToList = new DroneToList();



            return droneToList;
        }
        CustomerToList convertCustomerDoToCustomerBo(IDAL.DO.Customer customer)
        {
            CustomerToList customerToList = new CustomerToList();

            customerToList.uniqueID = customer.Id;
            customerToList.name = customer.name;
            customerToList.phone = customer.phone;


             // Run on the list parcel and find the parcels the related him (the customer).
            accessIdal.DisplaysListOfParcels().ToList().ForEach(delegate (IDAL.DO.Parcel parcel) {

                // packages sent and delivered
                if (parcel.SenderId == customerToList.uniqueID && parcel.Delivered != null)
                    customerToList.packagesSentAndDelivered++;
                // packages sent and not delivered
                if (parcel.SenderId == customerToList.uniqueID && parcel.PickedUp != null && parcel.Delivered == null)
                    customerToList.packagesSentAndNotDelivered++;
                // packages he received
                if (parcel.TargetId == customerToList.uniqueID && parcel.Delivered != null)
                    customerToList.packagesHeReceived++;
                // packages on the way to the customer
                if (parcel.TargetId == customerToList.uniqueID && parcel.PickedUp != null && parcel.Delivered == null)
                    customerToList.packagesOnTheWayToTheCustomer++;

            });

            return customerToList;
        }
}