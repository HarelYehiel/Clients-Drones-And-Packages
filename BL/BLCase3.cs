using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    public partial class BL: IBL
    {
        BO.DroneInPackage getDroneInPackage(int id)
        {
            BO.DroneInPackage droneInPackage_BO = new BO.DroneInPackage();

            foreach (BO.Drone item in listDrons)
            {
                if(item.uniqueID == id)
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

                if (customer_DO.Id != id) // Don't find the id in the list data.customers

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
            foreach (BO.Drone item in listDrons)
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
                station_BO.location.latitude = station_DO.Location.latitude;
                station_BO.location.longitude = station_DO.Location.longitude;

                //The all drones that charging in this station.
                BO.DroneInCharging droneInCharging_BO = new BO.DroneInCharging();
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
        public BO.Drone drone_view(int id) 
        {
            foreach (BO.Drone item in listDrons)
            {
                if (item.uniqueID == id) return item;
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
                customer_BO.phone = customer_DO.Phone;
                customer_BO.location.latitude = customer_DO.location.latitude;
                customer_BO.location.longitude = customer_DO.location.longitude;

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
                parcel_BO.customerInParcel_Sender = getCustomerInParcel(parcel_DO.SenderId);
                parcel_BO.customerInParcel_Target = getCustomerInParcel(parcel_DO.TargetId);
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
