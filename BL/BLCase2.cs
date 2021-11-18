using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    public partial class BL : IBL
    {
        IDAL.DalObject.UpdateClass updateDataSourceFun = new IDAL.DalObject.UpdateClass();
        public void UpdateDroneData(int ID, string newModel)
        {
            try
            {
                IDAL.DO.Drone drone = temp.GetDrone(ID);
                drone.Model = newModel;
                updateDataSourceFun.updateDrone(drone);
                BO.DroneToList droneToList_BO;
                droneToList_BO = GetDroneBO(ID);
                droneToList_BO.Model = newModel;
            }
            catch (Exception e)
            {

                throw new BO.MyExeption_BO("Exception from function 'Update_drone_data", e);
            }

        }
        public void UpdateStationData(int ID, string name, int numSlots)
        {
            try
            {
                if (name == "" && numSlots == 0)
                    return; // Don't do nathing.

                IDAL.DO.Station station = temp.GetStation(ID);
                if (name != "")
                    station.name = name;
                if (numSlots != 0)
                    station.ChargeSlots = numSlots;
                updateDataSourceFun.updateStation(station);
            }
            catch (Exception e)
            {

                throw new BO.MyExeption_BO("Exception from function 'Update_station_data", e);
            }

        }
        public void UpdateCustomerData(int ID, string name, string phoneNumber)
        {
            try
            {
                IDAL.DO.Customer customer = temp.GetCustomer(ID);
                if (name != "")
                    customer.name = name;
                if (phoneNumber != "")
                    customer.phone = phoneNumber;
                updateDataSourceFun.updateCustomer(customer);
            }
            catch (Exception e)
            {

                throw new BO.MyExeption_BO("Exception from function 'Update_customer_data", e);
            }

        }
        public void SendingDroneToCharging(int ID)
        {
            try
            {
                IDAL.DO.Drone drone = temp.GetDrone(ID);
                BO.DroneToList droneToList_Bo = new BO.DroneToList();
                //-----check drone status, only if he is free check the next condition-----
                if (drone.droneStatus == IDAL.DO.Enum.DroneStatus.Avilble)
                {
                    //------gett data of this dron from BL drone list-----------
                    for (int i = 0; i < ListDroneToList.Count; i++)
                    {
                        if (ListDroneToList[i].uniqueID == ID)
                        {
                            droneToList_Bo = ListDroneToList[i];
                        }
                    }
                    ///----------find the most close station---------
                    IDAL.DO.Point point1, point2 = new IDAL.DO.Point();
                    point1.latitude = droneToList_Bo.location.latitude;
                    point1.longitude = droneToList_Bo.location.longitude;
                    point2 = IDAL.DalObject.DataSource.stations[0].Location;
                    double min = point1.distancePointToPoint(point2);
                    foreach (var station in IDAL.DalObject.DataSource.stations)
                    {
                        double dis = point1.distancePointToPoint(station.Location);
                        if (dis < min)
                        {
                            point2 = station.Location;
                        }
                    }
                    //--------if drone's battary can survive up to the station-------------
                    if (droneToList_Bo.Battery - updateDataSourceFun.colculateBattery(point1, point2, ID) > 0)
                    {
                        //update drone data in BO
                        droneToList_Bo.Battery -= updateDataSourceFun.colculateBattery(point1, point2, ID);
                        droneToList_Bo.location.latitude = point2.latitude;
                        droneToList_Bo.location.longitude = point2.longitude;
                        droneToList_Bo.status = BO.EnumBO.DroneStatus.Baintenance;


                        //update station data
                        IDAL.DO.Station sta = new IDAL.DO.Station();
                        foreach (var station in IDAL.DalObject.DataSource.stations)
                        {
                            if (station.Location.latitude == point2.latitude && station.Location.longitude == point2.longitude)
                            {
                                sta = station;
                            }
                        }
                        sta.ChargeSlots--;

                        //update all the changes data
                        updateDataSourceFun.updateStation(sta);
                        updateDataSourceFun.updateDroneToCharge(ID, sta.id);
                    }
                    else
                        throw new BO.MyExeption_BO("He does not have enough battery to get to the station");
                }
                else
                    throw new BO.MyExeption_BO("The skimmer is not available at all so it is not possible to send it");
            }
            catch (Exception e)
            {

                throw new BO.MyExeption_BO("Exception from function 'Sending_a_drone_for_charging", e);
            }

        }
        public void ReleaseDroneFromCharging(int ID, int min)
        {
            try
            {
                IDAL.DO.Drone drone = temp.GetDrone(ID);
                if (drone.droneStatus == IDAL.DO.Enum.DroneStatus.Baintenance)
                {
                    //------gett data of this dron from BL drone list-----------
                    BO.DroneToList droneBo = new BO.DroneToList();
                    for (int i = 0; i < ListDroneToList.Count; i++)
                    {
                        if (ListDroneToList[i].uniqueID == ID)
                        {
                            droneBo = ListDroneToList[i];
                        }
                    }
                    //update drone in BL list
                    droneBo.Battery = droneBo.Battery + (min);//every minute in charge is 1% more
                    if (droneBo.Battery > 100)
                        droneBo.Battery = 100;
                    droneBo.status = BO.EnumBO.DroneStatus.Avilble;
                    for (int i = 0; i < ListDroneToList.Count; i++)
                    {
                        if (ListDroneToList[i].uniqueID == ID)
                        {
                            ListDroneToList[i] = droneBo;
                        }
                    }
                    //update data in dataSource
                    IDAL.DO.Point point = new IDAL.DO.Point();
                    point.latitude = droneBo.location.latitude;
                    point.longitude = droneBo.location.longitude;
                    updateDataSourceFun.updateRelaseDroneFromCharge(ID, point, min);

                }
                else
                    throw new BO.MyExeption_BO("The drone is not maintained at all");
            }

            catch (Exception e)
            {
                throw new BO.MyExeption_BO("Exception from function 'Release_drone_from_charging", e);
            }

        }
        public void AssignPackageToDrone(int droneId)
        {

            try
            {
                //get the data of the specific drone from DAL(data source)
                IDAL.DO.Drone drone = temp.GetDrone(droneId);
                //get the data of the specific drone at BO
                BO.DroneToList droneBo = GetDroneBO(droneId);
                IDAL.DO.Parcel parcelDO = new IDAL.DO.Parcel();


                bool flag = true;
                //---------------first of all - check if the drone is avilble----------------------
                if (drone.droneStatus == IDAL.DO.Enum.DroneStatus.Avilble)
                {
                    foreach (var parcel in IDAL.DalObject.DataSource.parcels)
                    {
                        //-----------we always prefere to take care by priority order---------------
                        if (parcel.priority == IDAL.DO.Enum.Priorities.Emergency)
                        {
                            if (parcel.weight <= drone.MaxWeight)
                            {
                                flag = serchForRelevantParcel(parcel, drone, droneBo, droneId);
                                if (!flag)
                                {
                                    parcelDO = dalO.GetParcel(parcel.Id);
                                    break;
                                }
                            }
                        }
                    }
                    if (flag)//if we not found no emergecncy parcel that the drone can take
                    {
                        foreach (var parcel2 in IDAL.DalObject.DataSource.parcels)
                        {
                            //-----------we always prefere to take care by priority order---------------
                            if (parcel2.priority == IDAL.DO.Enum.Priorities.Fast)
                            {
                                if (parcel2.weight <= drone.MaxWeight)
                                {
                                    flag = serchForRelevantParcel(parcel2, drone, droneBo, droneId);
                                    if (!flag)
                                    {
                                        parcelDO = dalO.GetParcel(parcel2.Id);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    if (flag)//if we not found no emergecncy parcel that the drone can take
                    {
                        foreach (var parcel3 in IDAL.DalObject.DataSource.parcels)
                        {
                            //-----------we always prefere to take care by priority order---------------
                            if (parcel3.priority == IDAL.DO.Enum.Priorities.Fast)
                            {
                                if (parcel3.weight <= drone.MaxWeight)
                                {
                                    flag = serchForRelevantParcel(parcel3, drone, droneBo, droneId);
                                    if (!flag)
                                    {
                                        parcelDO = dalO.GetParcel(parcel3.Id);
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    if (flag)//After all the search, this drone cant take any parecl
                        throw new BO.MyExeption_BO("This drone can't take any parecl");
                    else // We found a relavante parcel! update the data. 
                    {
                        parcelDO.Scheduled = DateTime.Now;
                        parcelDO.DroneId = drone.Id;
                        updateDataSourceFun.updateParcel(parcelDO);

                        drone.droneStatus = IDAL.DO.Enum.DroneStatus.Delivery;
                        updateDataSourceFun.updateDrone(drone);

                        droneBo.status = BO.EnumBO.DroneStatus.Delivery;
                        droneBo.packageDelivered = parcelDO.Id;
                    }
                }
                else
                    throw new BO.MyExeption_BO("The drone is not available and can not be called anything else right now");
            }

            catch (Exception e)
            {
                throw new BO.MyExeption_BO("Exception from function 'AssignPackageToDrone", e);
            }

        }
        public void CollectionOfPackageByDrone(int ID)
        {
            try
            {
                IDAL.DO.Drone drone = temp.GetDrone(ID);
                IDAL.DO.Parcel parcel = new IDAL.DO.Parcel();
                if (drone.droneStatus == IDAL.DO.Enum.DroneStatus.Delivery)
                {
                    for (int i = 0; i < IDAL.DalObject.DataSource.parcels.Count; i++)
                    {
                        if (IDAL.DalObject.DataSource.parcels[i].DroneId == ID)
                        {
                            int senderId;
                            DateTime def = new DateTime();
                            //if the parcel not picked up yet the PickUp time will be defult
                            if (IDAL.DalObject.DataSource.parcels[i].PickedUp == def)
                            {
                                BO.DroneToList droneToListeBo = new BO.DroneToList();
                                for (int j = 0; j < ListDroneToList.Count; j++)
                                {
                                    if (ListDroneToList[j].uniqueID == ID)
                                    {
                                        droneToListeBo = ListDroneToList[j];
                                        parcel = temp.GetParcel(IDAL.DalObject.DataSource.parcels[i].Id);
                                    }
                                }
                                //find sender location
                                senderId = IDAL.DalObject.DataSource.parcels[i].SenderId;
                                IDAL.DO.Point point1, point2 = new IDAL.DO.Point();
                                IDAL.DO.Customer sender = temp.GetCustomer(senderId);
                                point1.latitude = sender.location.latitude;
                                point1.longitude = sender.location.longitude;

                                //find drone location
                                point2.latitude = droneToListeBo.location.latitude;
                                point2.longitude = droneToListeBo.location.longitude;
                                double minus = updateDataSourceFun.colculateBattery(point1, point2, ID);
                                //update list drone in BL, and time of picked up at parcel
                                droneToListeBo.Battery -= minus;
                                droneToListeBo.location.latitude = sender.location.latitude;
                                droneToListeBo.location.longitude = sender.location.longitude;
                                parcel.PickedUp = DateTime.Now;
                                updateDataSourceFun.updateParcel(parcel);

                                for (int j = 0; j < ListDroneToList.Count; j++)
                                {
                                    if (ListDroneToList[j].uniqueID == ID)
                                        ListDroneToList[j] = droneToListeBo;
                                }

                            }
                        }
                    }
                }
                else//the drone is not in delivery
                    throw new BO.MyExeption_BO("The drone is not in delivery");
            }
            catch (Exception e)
            {

                throw new BO.MyExeption_BO("Exception from function 'Release_drone_from_charging", e);
            }

        }
        public void DeliveryOfPackageByDrone(int ID)
        {
            try
            {
                IDAL.DO.Drone drone = temp.GetDrone(ID);
                IDAL.DO.Parcel parcel = new IDAL.DO.Parcel();
                if (drone.droneStatus == IDAL.DO.Enum.DroneStatus.Delivery)
                {
                    for (int i = 0; i < IDAL.DalObject.DataSource.parcels.Count; i++)
                    {
                        if (IDAL.DalObject.DataSource.parcels[i].DroneId == ID)
                        {
                            DateTime def = new DateTime();
                            //if this drone is picked up so the pickedUp time isn't defult and not yet deliverd
                            if (IDAL.DalObject.DataSource.parcels[i].PickedUp != def && IDAL.DalObject.DataSource.parcels[i].Delivered == def)
                            {
                                int targetId;
                                BO.DroneToList droneToList_Bo = new BO.DroneToList();
                                foreach (var dro in ListDroneToList)
                                {
                                    if (dro.uniqueID == ID)
                                        droneToList_Bo = dro;
                                }
                                //find target location
                                targetId = IDAL.DalObject.DataSource.parcels[i].TargetId;
                                IDAL.DO.Point point1, point2 = new IDAL.DO.Point();
                                IDAL.DO.Customer target = temp.GetCustomer(targetId);
                                point1.latitude = target.location.latitude;
                                point1.longitude = target.location.longitude;

                                //find drone location
                                point2.latitude = droneToList_Bo.location.latitude;
                                point2.longitude = droneToList_Bo.location.longitude;

                                //update list drone in BL
                                double minus = updateDataSourceFun.colculateBattery(point1, point2, ID);
                                droneToList_Bo.Battery -= minus;
                                droneToList_Bo.location.latitude = target.location.latitude;
                                droneToList_Bo.location.longitude = target.location.longitude;
                                droneToList_Bo.status = BO.EnumBO.DroneStatus.Avilble;
                                droneToList_Bo.packageDelivered = 0;
                                for (int j = 0; j < ListDroneToList.Count; j++)
                                {
                                    if (ListDroneToList[j].uniqueID == ID)
                                    {
                                        ListDroneToList[j] = droneToList_Bo;
                                    }
                                }

                                parcel = IDAL.DalObject.DataSource.parcels[i];
                                parcel.Delivered = DateTime.Now;
                                //IDAL.DalObject.DataSource.parcels[i] = parcel;

                                drone.droneStatus = IDAL.DO.Enum.DroneStatus.Avilble;
                                updateDataSourceFun.updateDrone(drone);

                                updateDataSourceFun.updateParcel(parcel);
                                break;
                            }
                        }
                    }
                }
                else//the drone is npt in delivery
                    throw new BO.MyExeption_BO("The drone is npt in delivery");
            }
            catch (Exception e)
            {

                throw new BO.MyExeption_BO("Exception from function 'Delivery_of_a_package_by_drone", e);
            }

        }
        private BO.DroneToList GetDroneBO(int id)
        {
            for (int i = 0; i < ListDroneToList.Count; i++)
            {
                if (ListDroneToList[i].uniqueID == id) return ListDroneToList[i];
            }
            throw new BO.MyExeption_BO("Exception from function 'GetDroneBO", BO.MyExeption_BO.There_is_no_variable_with_this_ID);
        }
        bool serchForRelevantParcel(IDAL.DO.Parcel parcel, IDAL.DO.Drone drone, BO.DroneToList droneBo, int droneId)
        {
            try
            {
                IDAL.DO.Point point1, point2, point3 = new IDAL.DO.Point();
                IDAL.DO.Customer sender, target = new IDAL.DO.Customer();
                //get the location of the parcel sender
                sender = temp.GetCustomer(parcel.SenderId);
                point1.latitude = sender.location.latitude;
                point1.longitude = sender.location.longitude;
                //get the location of our drone
                point2.latitude = droneBo.location.latitude;
                point2.longitude = droneBo.location.longitude;
                //get the location of the parcel tgrget
                target = temp.GetCustomer(parcel.TargetId);
                point3.latitude = target.location.latitude;
                point3.longitude = target.location.longitude;
                //check if drone have enough battery to get up to the sender and than go up to target with the parcel
                if (droneBo.Battery - updateDataSourceFun.colculateBattery(point1, point2, droneId) - updateDataSourceFun.colculateBattery(point1, point3, droneId) > 0)
                {
                    //we found a parcel! change accordingly
                    drone.droneStatus = IDAL.DO.Enum.DroneStatus.Delivery;
                    droneBo.status = BO.EnumBO.DroneStatus.Delivery;
                    IDAL.DO.Parcel par = parcel;
                    par.Scheduled = DateTime.Now;
                    //update to data source
                    UpdateDroneData(droneId, drone.Model);
                    updateDataSourceFun.updateParcel(par);
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                throw new BO.MyExeption_BO("Exception from function 'serchForRelevantParcel", e);
            }

        }
    }

}
