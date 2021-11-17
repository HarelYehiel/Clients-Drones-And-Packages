using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    public partial class BL : IBL
    {
        BO.DroneInPackage getDroneInPackage(int id)
        {
            BO.DroneInPackage droneInPackage_BO = new BO.DroneInPackage();

            foreach (BO.DroneToList item in List_droneToList)
            {
                if (item.uniqueID == id)
                {
                    droneInPackage_BO.uniqueID = item.uniqueID;
                    droneInPackage_BO.batteryStatus = item.Battery;
                    droneInPackage_BO.location = item.location;
                    return droneInPackage_BO;
                }
            }

            throw new BO.MyExeption_BO("Exception from function 'getDroneInPackage'", BO.MyExeption_BO.There_is_no_variable_with_this_ID);


        }
        BO.CustomerInParcel getCustomerInParcel(int id)
        {
            try
            {
                IDAL.DO.Customer customer_DO = new IDAL.DO.Customer();
                BO.CustomerInParcel customerInParcel_BO = new BO.CustomerInParcel();

                customer_DO = dalO.GetCustomer(id);

                customerInParcel_BO.uniqueID = customer_DO.Id;
                customerInParcel_BO.name = customer_DO.name;

                return customerInParcel_BO;
            }
            catch (Exception e)
            {

                throw new BO.MyExeption_BO("Exception from function 'getCustomerInDelivery'", e);
            }

        }
        BO.parcelAtCustomer getParcelAtCustomer(int id_parcel, int id_customer)
        // 'id': the id of parcel
        // 'id_customer': the id of customer that open this function.
        // If 'id_customer' = parcel.SenderId, so the id of 'customerInDelivery' is parcel.TargetId.
        // If 'id_customer' = parcel.TargetId, so the id of 'customerInDelivery' is parcel.SenderId.
        {
            try
            {
                IDAL.DO.Parcel parcel_DO = new IDAL.DO.Parcel();
                BO.parcelAtCustomer parcelToCustomer_BO = new BO.parcelAtCustomer();

                parcel_DO = dalO.GetParcel(id_parcel);

                parcelToCustomer_BO.uniqueID = parcel_DO.Id;
                parcelToCustomer_BO.weight = (BO.Enum_BO.WeightCategories)parcel_DO.weight;
                parcelToCustomer_BO.priority = (BO.Enum_BO.Priorities)parcel_DO.priority;
                parcelToCustomer_BO.situation = (BO.Enum_BO.Situations)fun_parcel_situation(parcel_DO);

                // Find the 'customer_In_Delivery' for parcelToCustomer_BO:

                // If 'id_customer' = parcel.SenderId, so the id of 'customerInDelivery' is parcel.TargetId.
                if (parcel_DO.SenderId == id_customer)
                    parcelToCustomer_BO.customer_In_Delivery = getCustomerInParcel(parcel_DO.TargetId);

                // else 'id_customer' = parcel.TargetId, so the id of 'customerInDelivery' is parcel.SenderId.
                else // (parcel_DO.TargetId == id_customer)
                    parcelToCustomer_BO.customer_In_Delivery = getCustomerInParcel(parcel_DO.SenderId);

                return parcelToCustomer_BO;
            }
            catch (Exception e)
            {

                throw new BO.MyExeption_BO("Exception from function 'getParcelAtCustomer'", e);
            }
        }
        double getbatteryStatus(int id)
        {
            foreach (BO.DroneToList item in List_droneToList)
            {
                if (item.uniqueID == id) return item.Battery;
            }
            throw new BO.MyExeption_BO("Exception from function 'getbatteryStatus'", BO.MyExeption_BO.There_is_no_variable_with_this_ID);
        }
        public BO.station base_station_view(int id)
        {
            try
            {
                IDAL.DO.station station_DO = new IDAL.DO.station();
                BO.station station_BO = new BO.station();

                station_DO = dalO.GetStation(id);

                station_BO.uniqueID = station_DO.id;
                station_BO.name = station_DO.name;
                station_BO.availableChargingStations = station_DO.ChargeSlots;

                BO.Location location = new BO.Location();
                location.latitude = station_DO.Location.latitude;
                location.longitude = station_DO.Location.longitude;
                station_BO.location = location;

                //The all drones that charging in this station.
                BO.DroneInCharging droneInCharging_BO = new BO.DroneInCharging();
                station_BO.dronesInCharging = new List<BO.DroneInCharging>();
                foreach (IDAL.DO.DroneCharge item in IDAL.DalObject.DataSource.dronesCharge)
                {
                    if (item.DroneId == id)
                    {
                        droneInCharging_BO.uniqueID = item.DroneId;
                        droneInCharging_BO.batteryStatus = getbatteryStatus(id);

                        station_BO.dronesInCharging.Add(droneInCharging_BO);

                    }
                }

                return station_BO;

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

                foreach (var item in IDAL.DalObject.DataSource.parcels)
                {
                    if (item.Id == id)
                    {
                        parcelByTransfer.uniqueID = item.Id;
                        parcelByTransfer.priority = (BO.Enum_BO.Priorities)item.priority;
                        parcelByTransfer.weight = (BO.Enum_BO.WeightCategories)item.weight;

                        IDAL.DO.Customer customer_DO = dalO.GetCustomer(item.SenderId);
                        parcelByTransfer.collectionLocation.latitude = customer_DO.location.latitude;
                        parcelByTransfer.collectionLocation.longitude = customer_DO.location.longitude;

                        customer_DO = dalO.GetCustomer(item.TargetId);
                        parcelByTransfer.destinationLocation.latitude = customer_DO.location.latitude;
                        parcelByTransfer.destinationLocation.longitude = customer_DO.location.longitude;

                        parcelByTransfer.theSander = getCustomerInParcel(item.SenderId);
                        parcelByTransfer.theTarget = getCustomerInParcel(item.TargetId);

                        // Transport distance from sender to target.
                        parcelByTransfer.transportDistance = parcelByTransfer.collectionLocation.distancePointToPoint(parcelByTransfer.destinationLocation);


                        // Is wait for collection ?
                        if (dalO.GetParcel(id).PickedUp != new DateTime()) // The parcel PickedUp
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
        public BO.Drone drone_view(int id)
        {
            foreach (BO.DroneToList item in List_droneToList)
            {
                if (item.uniqueID == id)
                {
                    BO.Drone drone_BO = new BO.Drone();

                    drone_BO.uniqueID = item.uniqueID;
                    drone_BO.Model = item.Model;
                    drone_BO.Battery = item.Battery;
                    drone_BO.Status = item.status;
                    drone_BO.weight = item.weight;
                    drone_BO.location = item.location;

                    if (drone_BO.Status == BO.Enum_BO.DroneStatus.Delivery)
                        drone_BO.parcelByTransfer = getParcelByTransfer(item.packageDelivered);


                    return drone_BO;
                }
            }

            throw new BO.MyExeption_BO("Exception from function 'drone_view'", BO.MyExeption_BO.There_is_no_variable_with_this_ID);
        }
        public BO.Customer customer_view(int id)
        {
            try
            {
                IDAL.DO.Customer customer_DO = new IDAL.DO.Customer();
                BO.Customer customer_BO = new BO.Customer();

                customer_DO = dalO.GetCustomer(id);

                customer_BO.uniqueID = customer_DO.Id;
                customer_BO.name = customer_DO.name;
                customer_BO.phone = customer_DO.phone;

                BO.Location location = new BO.Location();
                location.latitude =  customer_DO.location.latitude;
                location.longitude =  customer_DO.location.longitude;
                customer_BO.location = location;

                customer_BO.fromTheCustomer = new List<BO.parcelAtCustomer>();
                customer_BO.toTheCustomer = new List<BO.parcelAtCustomer>();
                foreach (IDAL.DO.Parcel item in IDAL.DalObject.DataSource.parcels)
                {
                    if (item.SenderId == id)
                    {
                        customer_BO.fromTheCustomer.Add(getParcelAtCustomer(item.Id, id));
                    }
                    else if (item.TargetId == id)
                    {
                        customer_BO.toTheCustomer.Add(getParcelAtCustomer(item.Id, id));

                    }
                }

                return customer_BO;
            }
            catch (Exception e)
            {

                throw new BO.MyExeption_BO("Exception from function 'customer_view'", e);
            }

        }
        public BO.Parcel parcel_view(int id)
        {
            try
            {
                IDAL.DO.Parcel parcel_DO = new IDAL.DO.Parcel();
                BO.Parcel parcel_BO = new BO.Parcel();

                parcel_DO = dalO.GetParcel(id);

                parcel_BO.uniqueID = parcel_DO.Id;
                parcel_BO.customerInParcelSender = getCustomerInParcel(parcel_DO.SenderId);
                parcel_BO.customerInParcel_Target = getCustomerInParcel(parcel_DO.TargetId);
                if (parcel_DO.DroneId != 0)
                    parcel_BO.droneInParcel = getDroneInPackage(parcel_DO.DroneId);
                parcel_BO.priority = (BO.Enum_BO.Priorities)parcel_DO.priority;
                parcel_BO.weight = (BO.Enum_BO.WeightCategories)parcel_DO.priority;
                parcel_BO.pickedUp = parcel_DO.PickedUp;
                parcel_BO.requested = parcel_DO.Requested;
                parcel_BO.scheduled = parcel_DO.Scheduled;
                parcel_BO.requested = parcel_DO.Requested;

                return parcel_BO;

            }
            catch (Exception e)
            {

                throw new BO.MyExeption_BO("Exception from function 'parcel_view'", e);
            }

        }
    }
}
