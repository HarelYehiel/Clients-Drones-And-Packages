using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public partial class BL : IBL
    {
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

                customerDO = accessIdal.GetCustomer(id);

                customerInParcelBO.uniqueID = customerDO.Id;
                customerInParcelBO.name = customerDO.name;

                return customerInParcelBO;
            }
            catch (Exception e)
            {

                throw new BO.MyExeption_BO("Exception from function 'getCustomerInDelivery'", e);
            }

        }
        BO.parcelAtCustomer GetParcelAtCustomer(int id_parcel, int id_customer)
        // 'id': the id of parcel
        // 'id_customer': the id of customer that open this function.
        // If 'id_customer' = parcel.SenderId, so the id of 'customerInDelivery' is parcel.TargetId.
        // If 'id_customer' = parcel.TargetId, so the id of 'customerInDelivery' is parcel.SenderId.
        {
            try
            {
                DO.Parcel parcelDO = new DO.Parcel();
                BO.parcelAtCustomer parcelToCustomerBO = new BO.parcelAtCustomer();

                parcelDO = accessIdal.GetParcel(id_parcel);

                parcelToCustomerBO.uniqueID = parcelDO.Id;
                parcelToCustomerBO.weight = (BO.EnumBO.WeightCategories)parcelDO.weight;
                parcelToCustomerBO.priority = (BO.EnumBO.Priorities)parcelDO.priority;
                parcelToCustomerBO.situation = (BO.EnumBO.Situations)FunParcelSituation(parcelDO);

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
        public BO.station getBaseStation(int id)
        {
            try
            {
                DO.Station stationDO = new DO.Station();
                BO.station stationBO = new BO.station();

                stationDO = accessIdal.GetStation(id);

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
                foreach (DO.DroneCharge item in accessIdal.GetListOfDroneCharge()) // IDAL.DalObject.DataSource.dronesCharge
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
        BO.ParcelByTransfer getParcelByTransfer(int id)
        {

            try
            {
                BO.ParcelByTransfer parcelByTransfer = new BO.ParcelByTransfer();

                foreach (var item in accessIdal.GetListOfParcels())
                {
                    if (item.Id == id)
                    {
                        parcelByTransfer.uniqueID = item.Id;
                        parcelByTransfer.priority = (BO.EnumBO.Priorities)item.priority;
                        parcelByTransfer.weight = (BO.EnumBO.WeightCategories)item.weight;
                        
                        DO.Customer customer_DO = accessIdal.GetCustomer(item.SenderId);
                        BO.Location l = new BO.Location();
                        l.latitude = customer_DO.location.latitude;
                        l.longitude = customer_DO.location.longitude;
                        parcelByTransfer.collectionLocation = l;

                        customer_DO = accessIdal.GetCustomer(item.TargetId);
                        l = new BO.Location();
                        l.latitude = customer_DO.location.latitude;
                        l.longitude = customer_DO.location.longitude;
                        parcelByTransfer.destinationLocation = l;

                        parcelByTransfer.theSander = GetCustomerInParcel(item.SenderId);
                        parcelByTransfer.theTarget = GetCustomerInParcel(item.TargetId);

                        // Transport distance from sender to target.
                        parcelByTransfer.transportDistance = parcelByTransfer.collectionLocation.distancePointToPoint(parcelByTransfer.destinationLocation);


                        // Is wait for collection ?
                        if (accessIdal.GetParcel(id).PickedUp != null) // The parcel PickedUp
                            parcelByTransfer.isWaitForCollection = false; // The parcel don't wait to PickedUp, it in transfer
                        else
                            parcelByTransfer.isWaitForCollection = true; // The parcel wait to PickedUp

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
        public BO.Drone GetDrone(int id)
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
                        droneBO.parcelByTransfer = getParcelByTransfer(item.packageDelivered);


                    return droneBO;
                }
            }

            throw new BO.MyExeption_BO("Exception from function 'drone_view'", BO.MyExeption_BO.There_is_no_variable_with_this_ID);
        }
        public BO.Customer GetCustomer(int id)
        {
            try
            {
                DO.Customer customerDO = new DO.Customer();
                BO.Customer customerBO = new BO.Customer();

                customerDO = accessIdal.GetCustomer(id);

                customerBO.uniqueID = customerDO.Id;
                customerBO.name = customerDO.name;
                customerBO.phone = customerDO.phone;

                BO.Location location = new BO.Location();
                location.latitude =  customerDO.location.latitude;
                location.longitude =  customerDO.location.longitude;
                customerBO.location = location;

                customerBO.fromTheCustomer = new List<BO.parcelAtCustomer>();
                customerBO.toTheCustomer = new List<BO.parcelAtCustomer>();
                foreach (DO.Parcel item in accessIdal.GetListOfParcels())
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
        public BO.Parcel GetParcel(int id)
        {
            try
            {
                DO.Parcel parcelDO = new DO.Parcel();
                BO.Parcel parcelBO = new BO.Parcel();

                parcelDO = accessIdal.GetParcel(id);

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
    }
}
