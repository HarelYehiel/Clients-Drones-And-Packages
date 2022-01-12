using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi
{
    public partial class BL : IBL
    {
       
   
        #region Help functions
         BO.DroneInPackage GetDroneInPackage(int id)
        {

            BO.DroneInPackage droneInPackageBO = new BO.DroneInPackage();

            foreach (BO.DroneToList item in ListDroneToList)
            {
                if (item.uniqueID == id)
                {
                    droneInPackageBO.uniqueID = item.uniqueID;
                    droneInPackageBO.batteryStatus = item.Battery;
                    droneInPackageBO.location = item.location;
                    return droneInPackageBO;
                }
            }

            throw new BO.MyExeption_BO("Exception from function 'getDroneInPackage'", BO.MyExeption_BO.There_is_no_variable_with_this_ID);


        }
        BO.CustomerInParcel GetCustomerInParcel(int id)
        {
            try
            {
                DO.Customer customerDO = new DO.Customer();
                BO.CustomerInParcel customerInParcelBO = new BO.CustomerInParcel();

                customerDO = accessDal.GetCustomer(id);

                customerInParcelBO.uniqueID = customerDO.Id;
                customerInParcelBO.name = customerDO.name;

                return customerInParcelBO;
            }
            catch (Exception e)
            {

                throw new BO.MyExeption_BO("Exception from function 'getCustomerInDelivery'", e);
            }

        }

        /// <summary>
        /// 'id_parcel': the id of parcel
        /// 'id_customer': the id of customer that open this function.
        /// If 'id_customer' = parcel.SenderId, so the id of 'customerInDelivery' is parcel.TargetId.
        /// If 'id_customer' = parcel.TargetId, so the id of 'customerInDelivery' is parcel.SenderId.
        /// </summary>
        BO.parcelAtCustomer GetParcelAtCustomer(int id_parcel, int id_customer)        
        {
            try
            {
                DO.Parcel parcelDO = new DO.Parcel();
                BO.parcelAtCustomer parcelToCustomerBO = new BO.parcelAtCustomer();

                parcelDO = accessDal.GetParcel(id_parcel);

                parcelToCustomerBO.uniqueID = parcelDO.Id;
                parcelToCustomerBO.weight = (BO.EnumBO.WeightCategories)parcelDO.weight;
                parcelToCustomerBO.priority = (BO.EnumBO.Priorities)parcelDO.priority;
                parcelToCustomerBO.situation = FunParcelSituation(parcelDO);

                // Find the 'customer_In_Delivery' for parcelToCustomer_BO:

                // If 'id_customer' = parcel.SenderId, so the id of 'customerInDelivery' is parcel.TargetId.
                if (parcelDO.SenderId == id_customer)
                    parcelToCustomerBO.customer_In_Delivery = GetCustomerInParcel(parcelDO.TargetId);

                // else 'id_customer' = parcel.TargetId, so the id of 'customerInDelivery' is parcel.SenderId.
                else // (parcel_DO.TargetId == id_customer)
                    parcelToCustomerBO.customer_In_Delivery = GetCustomerInParcel(parcelDO.SenderId);

                return parcelToCustomerBO;
            }
            catch (Exception e)
            {

                throw new BO.MyExeption_BO("Exception from function 'getParcelAtCustomer'", e);
            }
        }
        double GetBatteryStatus(int id)
        {
            foreach (BO.DroneToList item in ListDroneToList)
            {
                if (item.uniqueID == id) return item.Battery;
            }
            throw new BO.MyExeption_BO("Exception from function 'getbatteryStatus'", BO.MyExeption_BO.There_is_no_variable_with_this_ID);
        }
        BO.ParcelByTransfer getParcelByTransfer(int idParcel, Location locationDrone)
        {

            try
            {
                BO.ParcelByTransfer parcelByTransfer = new BO.ParcelByTransfer();

                foreach (var item in accessDal.GetListOfParcels())
                {
                    if (item.Id == idParcel)
                    {
                        parcelByTransfer.uniqueID = item.Id;
                        parcelByTransfer.priority = (BO.EnumBO.Priorities)item.priority;
                        parcelByTransfer.weight = (BO.EnumBO.WeightCategories)item.weight;

                        DO.Customer customer_DO = accessDal.GetCustomer(item.SenderId);
                        BO.Location l = new BO.Location();
                        l.latitude = customer_DO.location.latitude;
                        l.longitude = customer_DO.location.longitude;
                        parcelByTransfer.collectLocation = l;

                        customer_DO = accessDal.GetCustomer(item.TargetId);
                        l = new BO.Location();
                        l.latitude = customer_DO.location.latitude;
                        l.longitude = customer_DO.location.longitude;
                        parcelByTransfer.destinationLocation = l;

                        parcelByTransfer.theSander = GetCustomerInParcel(item.SenderId);
                        parcelByTransfer.theTarget = GetCustomerInParcel(item.TargetId);

                        // Transport distance from sender to target.
                        if (item.PickedUp != null)  // The parcel PickedUp
                        {
                            parcelByTransfer.transportDistance = parcelByTransfer.collectLocation.
                                distancePointToPoint(parcelByTransfer.destinationLocation);

                            parcelByTransfer.isWaitForCollection = false; // The parcel don't wait to PickedUp, it in transfer
                        }

                        // Transport distance from drone to sender.
                        else if (item.Scheduled != null) // The parcel wait to PickedUp
                        {
                            parcelByTransfer.transportDistance = locationDrone.
                                distancePointToPoint(parcelByTransfer.collectLocation);

                            parcelByTransfer.isWaitForCollection = true; // The parcel wait to PickedUp
                        }

                        return parcelByTransfer;
                    }
                }
                throw new BO.MyExeption_BO(BO.MyExeption_BO.There_is_no_variable_with_this_ID);
            }
            catch (Exception e)
            {

                throw new BO.MyExeption_BO("Exception from function 'getParcelByTransfer'", e);
            }

        }
        [MethodImpl(MethodImplOptions.Synchronized)] public BO.DroneInCharging GetDroneInCharging(int ID)
        {
            try
            {
                BO.DroneInCharging droneCharge = new BO.DroneInCharging();
                List<DO.DroneCharge> droneCharges = accessDal.GetListOfDroneCharge().ToList();
                for (int i = 0; i < droneCharges.Count(); i++)
                {
                    if (droneCharges[i].DroneId == ID)
                    {
                        droneCharge.startCharge = droneCharges[i].startCharge;
                        droneCharge.uniqueID = droneCharges[i].DroneId;
                        droneCharge.batteryStatus = GetBatteryStatus(ID);
                        return droneCharge;
                    }
                }
                throw new BO.MyExeption_BO("Don't have this drone in the list.");
            }
            catch (Exception e)
            {

                throw new BO.MyExeption_BO("Exception from function 'GetDroneInCharging'", e);
            }
        }
        public BO.CustomerToList GetCustomerToTheList(int ID)
        {
            List<CustomerToList> customersToTheLists = GetAllCustomersBy(c => c.Id == ID).ToList();
            if (customersToTheLists.Count == 1)
                return customersToTheLists[0];

            throw MyExeption_BO.There_is_no_variable_with_this_ID;
        }
        #endregion

        #region Functions Get Entity 
        [MethodImpl(MethodImplOptions.Synchronized)] public BO.station getBaseStation(int id)
        {
            try
            {
                DO.Station stationDO = new DO.Station();
                BO.station stationBO = new BO.station();

                stationDO = accessDal.GetStation(id);

                stationBO.uniqueID = stationDO.id;
                stationBO.name = stationDO.name;
                stationBO.availableChargingStations = stationDO.ChargeSlots;

                BO.Location location = new BO.Location();
                location.latitude = stationDO.Location.latitude;
                location.longitude = stationDO.Location.longitude;
                stationBO.location = location;

                //The all drones that charging in this station.
                BO.DroneInCharging droneInCharging_BO = new BO.DroneInCharging();
                stationBO.dronesInCharging = new List<BO.DroneInCharging>();
                foreach (DO.DroneCharge item in accessDal.GetListOfDroneCharge()) // IDAL.DalObject.DataSource.dronesCharge
                {
                    if (item.DroneId == id)
                    {
                        droneInCharging_BO.uniqueID = item.DroneId;
                        droneInCharging_BO.batteryStatus = GetBatteryStatus(id);

                        stationBO.dronesInCharging.Add(droneInCharging_BO);

                    }
                }

                return stationBO;

            }
            catch (Exception e)
            {

                throw new BO.MyExeption_BO("Exception from function 'getbatteryStatus'", e);

            }

        }
        [MethodImpl(MethodImplOptions.Synchronized)] public BO.Drone GetDrone(int id)
        {
            foreach (BO.DroneToList item in ListDroneToList)
            {
                if (item.uniqueID == id)
                {
                    BO.Drone droneBO = new BO.Drone();

                    droneBO.uniqueID = item.uniqueID;
                    droneBO.Model = item.Model;
                    droneBO.Battery = item.Battery;
                    droneBO.Status = item.status;
                    droneBO.weight = item.weight;
                    droneBO.location = item.location;


                    if (droneBO.Status == BO.EnumBO.DroneStatus.Delivery)
                        droneBO.parcelByTransfer = getParcelByTransfer(item.packageDelivered, item.location);


                    return droneBO;
                }
            }

            throw new BO.MyExeption_BO("Exception from function 'drone_view'", BO.MyExeption_BO.There_is_no_variable_with_this_ID);
        }
        [MethodImpl(MethodImplOptions.Synchronized)] public BO.Customer GetCustomer(int id)
        {
            try
            {
                DO.Customer customerDO = new DO.Customer();
                BO.Customer customerBO = new BO.Customer();

                customerDO = accessDal.GetCustomer(id);

                customerBO.uniqueID = customerDO.Id;
                customerBO.name = customerDO.name;
                customerBO.phone = customerDO.phone;

                BO.Location location = new BO.Location();
                location.latitude = customerDO.location.latitude;
                location.longitude = customerDO.location.longitude;
                customerBO.location = location;

                customerBO.fromTheCustomer = new List<BO.parcelAtCustomer>();
                customerBO.toTheCustomer = new List<BO.parcelAtCustomer>();
                foreach (DO.Parcel item in accessDal.GetListOfParcels())
                {
                    if (item.SenderId == id)
                    {
                        customerBO.fromTheCustomer.Add(GetParcelAtCustomer(item.Id, id));
                    }
                    else if (item.TargetId == id)
                    {
                        customerBO.toTheCustomer.Add(GetParcelAtCustomer(item.Id, id));

                    }
                }

                return customerBO;
            }
            catch (Exception e)
            {

                throw new BO.MyExeption_BO("Exception from function 'customer_view'", e);
            }

        }
        [MethodImpl(MethodImplOptions.Synchronized)] public BO.Parcel GetParcel(int id)
        {
            try
            {
                DO.Parcel parcelDO = new DO.Parcel();
                BO.Parcel parcelBO = new BO.Parcel();

                parcelDO = accessDal.GetParcel(id);

                parcelBO.uniqueID = parcelDO.Id;
                parcelBO.customerInParcelSender = GetCustomerInParcel(parcelDO.SenderId);
                parcelBO.customerInParcel_Target = GetCustomerInParcel(parcelDO.TargetId);
                if (parcelDO.DroneId != 0)
                    parcelBO.droneInParcel = GetDroneInPackage(parcelDO.DroneId);
                parcelBO.priority = (BO.EnumBO.Priorities)parcelDO.priority;
                parcelBO.weight = (BO.EnumBO.WeightCategories)parcelDO.priority;
                parcelBO.pickedUp = parcelDO.PickedUp;
                parcelBO.requested = parcelDO.Requested;
                parcelBO.scheduled = parcelDO.Scheduled;
                parcelBO.delivered = parcelDO.Delivered;

                return parcelBO;

            }
            catch (Exception e)
            {

                throw new BO.MyExeption_BO("Exception from function 'parcel_view'", e);
            }

        }
        [MethodImpl(MethodImplOptions.Synchronized)] public BO.StationToTheList GetStationToTheList(int ID)
        {
            List<StationToTheList> stationsToTheLists = GetAllStaionsBy(s => s.id == ID).ToList();
            if (stationsToTheLists.Count == 1)
                return stationsToTheLists[0];

            throw MyExeption_BO.There_is_no_variable_with_this_ID;
        }
        [MethodImpl(MethodImplOptions.Synchronized)] public BO.ParcelToList GetParcelToTheList(int ID)
        {
            List<ParcelToList> parcelsToTheLists = GetAllParcelsBy(p => p.Id == ID).ToList();
            if (parcelsToTheLists.Count == 1)
                return parcelsToTheLists[0];

            throw MyExeption_BO.There_is_no_variable_with_this_ID;
        }
        [MethodImpl(MethodImplOptions.Synchronized)] public BO.DroneToList GetDroneToTheList(int ID)
        {
            List<DroneToList> dronesToTheLists = GetAllDronesBy(d => d.uniqueID == ID).ToList();
            if (dronesToTheLists.Count == 1)
                return dronesToTheLists[0];

            throw MyExeption_BO.There_is_no_variable_with_this_ID;
        }
        #endregion

    }
}
