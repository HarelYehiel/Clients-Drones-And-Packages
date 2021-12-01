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
        public delegate bool Predicate<in T>(T obj);

        public delegate BO.Parcel Converter<in ParcelToList , out Parcel>(BO.ParcelToList input);


        public static bool findEmergency(IDAL.DO.Parcel parcel) { return (parcel.Scheduled == null && parcel.priority == IDAL.DO.Enum.Priorities.Emergency); }
        public static bool findFast(IDAL.DO.Parcel parcel) { return (parcel.Scheduled == null && parcel.priority == IDAL.DO.Enum.Priorities.Fast); }
        public static bool findNormal(IDAL.DO.Parcel parcel) { return (parcel.Scheduled == null && parcel.priority == IDAL.DO.Enum.Priorities.Normal); }

        public void UpdateDroneData(int ID, string newModel)
        {
            try
            {
                IDAL.DO.Drone drone = accessIdal.GetDrone(ID);
                drone.Model = newModel;
                updateDataSourceFun.updateDrone(drone);
                BO.DroneToList droneToList_BO;
                droneToList_BO = GetDroneBO(ID);
                droneToList_BO.Model = newModel;
                for (int i = 0; i < ListDroneToList.Count; i++)
                {
                    if (ListDroneToList[i].uniqueID == ID)
                    {
                        ListDroneToList[i] = droneToList_BO;
                    }
                }

            }
            catch (Exception e)
            {

                throw new BO.MyExeption_BO("Exception from function 'UpdateDroneData", e);
            }

        }
        public void UpdateStationData(int ID, string name, int numSlots)
        {
            try
            {
                if (name == "" && numSlots == 0)
                    return; // Don't do nathing.

                IDAL.DO.Station station = accessIdal.GetStation(ID);
                if (name != "")
                    station.name = name;
                if (numSlots != 0)
                {
                    BO.station tempStation = new BO.station();
                    tempStation = getBaseStation(ID);
                    if (numSlots < tempStation.dronesInCharging.Count)
                        throw new BO.MyExeption_BO("There are more drones in the station than the number you have chosen");
                    else
                        station.ChargeSlots = numSlots;
                }
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
                IDAL.DO.Customer customer = accessIdal.GetCustomer(ID);
                if (name != "")
                    customer.name = name;
                if (phoneNumber != "")
                    customer.phone = phoneNumber;
                updateDataSourceFun.updateCustomer(customer);
            }   
            catch (Exception e)
            {

                throw new BO.MyExeption_BO("Exception from function 'UpdateCustomerData", e);
            }

        }
        public void SendingDroneToCharging(int ID)
        {
            try
            {
                IDAL.DO.Drone drone = accessIdal.GetDrone(ID);
                BO.DroneToList droneToListBo = GetDroneBO(ID);
                //-----check drone status, only if he is free check the next condition-----
                if (drone.droneStatus == IDAL.DO.Enum.DroneStatus.Avilble)
                {

                    ///----------find the most close station---------
                    List<BO.station> stations = GetListOfBaseStations().ToList().ConvertAll(convertToStationNotList);
                    BO.Location point1, point2 = new BO.Location();
                    point1 = droneToListBo.location;
                    point2 = stations[0].location;
                    double min = point1.distancePointToPoint(point2);
                    foreach (var station in stations)
                    {
                        double dis = point1.distancePointToPoint(station.location);
                        if (dis < min)
                        {
                            point2 = station.location;
                        }
                    }
                    //--------if drone's battary can survive up to the station-------------
                    if (droneToListBo.Battery - colculateBatteryBO(point1, point2, ID) > 0)
                    {
                        //update drone data in BO
                        droneToListBo.Battery -= colculateBatteryBO(point1, point2, ID);
                        droneToListBo.location = point2;
                        droneToListBo.status = BO.EnumBO.DroneStatus.Baintenance;
                        for(int i =0;i<ListDroneToList.Count;i++)
                        {
                            if (ListDroneToList[i].uniqueID == droneToListBo.uniqueID)
                                ListDroneToList[i] = droneToListBo;
                        }
   
                        //update the change at the station
                        foreach (var station in stations)
                        {
                            if (station.location == point2)
                            {
                                if (station.availableChargingStations > 0)
                                {
                                    station.availableChargingStations--;
                                    //update station data in DataSource
                                    UpdateStationData(station.uniqueID,station.name, station.availableChargingStations);
                                    //update all the changes data
                                    updateDataSourceFun.updateDroneToCharge(ID, station.uniqueID);
                                }
                                else
                                    throw new BO.MyExeption_BO("There is no free slot for this drone!");
                            }
                        }

                    }
                    else
                        throw new BO.MyExeption_BO("He does not have enough battery to get to the station");
                }
                else
                    throw new BO.MyExeption_BO("The skimmer is not available at all so it is not possible to send it");
            }
            catch (Exception e)
            {

                throw new BO.MyExeption_BO("Exception from function 'SendingDroneToCharging", e);
            }

        }
        public void ReleaseDroneFromCharging(int ID, int min)
        {
            try
            {
                IDAL.DO.Drone drone = accessIdal.GetDrone(ID);
                if (drone.droneStatus == IDAL.DO.Enum.DroneStatus.Baintenance)
                {
                    //------gett data of this dron from BL drone list-----------
                    BO.DroneToList droneBo = GetDroneBO(ID);// new BO.DroneToList();
                    List<double> getConfig = accessIdal.PowerConsumptionBySkimmer();
                    //update drone in BL list
                    droneBo.Battery = droneBo.Battery + (min*getConfig[4]);//every minute in charge is 1% more
                    if (droneBo.Battery > 100)
                        droneBo.Battery = 100;
                    droneBo.status = BO.EnumBO.DroneStatus.Avilble;

                    //update list - Drone to list
                    for (int i = 0; i < ListDroneToList.Count; i++)
                    {
                        if (ListDroneToList[i].uniqueID == ID)
                        {
                            ListDroneToList[i] = droneBo;
                        }
                    }
                    //update data in dataSource
                    BO.Location point = droneBo.location;
                    updateDataSourceFun.updateRelaseDroneFromCharge(ID, point.longitude, point.latitude, min);

                    //update all the changes data at the stations list
                    IDAL.DO.Station sta = new IDAL.DO.Station();
                    List < BO.station > stations = GetListOfBaseStations().ToList().ConvertAll(convertToStationNotList);
                    foreach (var station in stations)
                    {
                        if (station.location == point)
                            sta.ChargeSlots++;                       
                    }
                    updateDataSourceFun.updateStation(sta);
                }
                else
                    throw new BO.MyExeption_BO("The drone is not maintained at all");
            }

            catch (Exception e)
            {
                throw new BO.MyExeption_BO("Exception from function 'ReleaseDroneFromCharging", e);
            }

        }
        public void AssignPackageToDrone(int droneId)
        {

            try
            {
                //get the data of the specific drone from DAL(data source)
                IDAL.DO.Drone drone = accessIdal.GetDrone(droneId);
                //get the data of the specific drone at BO
                BO.DroneToList droneBo = GetDroneBO(droneId);
                IDAL.DO.Parcel parcelDO = new IDAL.DO.Parcel();


                bool flag = true;
                //---------------first of all - check if the drone is avilble----------------------
                if (drone.droneStatus == IDAL.DO.Enum.DroneStatus.Avilble)
                {
                    //-----------we always prefere to take care by priority order---------------
                    List<BO.ParcelToList> newList = GetAllParcelsBy(findEmergency).ToList();
                    newList.AddRange(GetAllParcelsBy(findFast).ToList());
                    newList.AddRange(GetAllParcelsBy(findNormal).ToList());
                    foreach (var parcel in newList.ConvertAll(convertToParcelNotList))
                        if ((int)parcel.weight <= (int)drone.MaxWeight)
                        {
                            //newList now have all the aviable and unScheluled drones by priority order 
                            flag = serchForRelevantParcel(parcel, drone, droneBo, droneId);
                            if (!flag)
                            {
                                parcelDO = accessIdal.GetParcel(parcel.uniqueID);
                                break;
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
                throw new BO.MyExeption_BO("Exception from function 'AssignPackageToDrone'", e);
            }

        }
        public void CollectionOfPackageByDrone(int ID)
        {
            try
            {
                IDAL.DO.Drone drone = accessIdal.GetDrone(ID);
                IDAL.DO.Parcel parcel = new IDAL.DO.Parcel();
                if (drone.droneStatus == IDAL.DO.Enum.DroneStatus.Delivery)
                {
                    List<BO.Parcel> parcels = DisplaysTheListOfParcels().ToList().ConvertAll(convertToParcelNotList);
                    for (int i = 0; i < parcels.Count; i++)
                    {
                        if (parcels[i].droneInParcel.uniqueID == ID)
                        {
                            //if the parcel not picked up yet the PickUp time will be defult
                            if (parcels[i].pickedUp == null)
                            {
                                BO.DroneToList droneToListeBo = GetDroneBO(ID);                              
                                BO.Customer sender = GetCustomer(parcels[i].customerInParcelSender.uniqueID);
                                double minus = colculateBatteryBO(sender.location, droneToListeBo.location, ID);

                                //update list drone in BL, and time of picked up at parcel
                                droneToListeBo.Battery -= minus;
                                droneToListeBo.location.latitude = sender.location.latitude;
                                droneToListeBo.location.longitude = sender.location.longitude;

                                //update changes at DataSource
                                parcel = accessIdal.GetParcel(parcels[i].uniqueID);
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

                throw new BO.MyExeption_BO("Exception from function 'CollectionOfPackageByDrone", e);
            }

        }
        public void DeliveryOfPackageByDrone(int ID)
        {
            try
            {
                IDAL.DO.Drone drone = accessIdal.GetDrone(ID);
                if (drone.droneStatus == IDAL.DO.Enum.DroneStatus.Delivery)
                {
                    List<BO.Parcel> parcels = DisplaysTheListOfParcels().ToList().ConvertAll(convertToParcelNotList);//GetAllParcelsBy(p => true).ToList();
                    for (int i = 0; i < parcels.Count; i++)
                    {
                        if (parcels[i].droneInParcel.uniqueID == ID)
                        {
                            //if this drone is picked up so the pickedUp time isn't defult and not yet deliverd
                            if (parcels[i].pickedUp != null && parcels[i].delivered == null)
                            {
                                BO.DroneToList droneToList_Bo = GetDroneBO(ID);                              
                                BO.Customer target = GetCustomer(parcels[i].customerInParcel_Target.uniqueID);
                              
                                //update list drone in BL
                                double minus = colculateBatteryBO(target.location, droneToList_Bo.location, ID);
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
                                IDAL.DO.Parcel parcel = accessIdal.GetParcel(parcels[i].uniqueID);
                                parcel.Delivered = DateTime.Now;
                                drone.droneStatus = IDAL.DO.Enum.DroneStatus.Avilble;
                                
                                //update changes in DataSource
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

                throw new BO.MyExeption_BO("Exception from function 'DeliveryOfPackageByDrone", e);
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
        bool serchForRelevantParcel(BO.Parcel parcel, IDAL.DO.Drone drone, BO.DroneToList droneBo, int droneId)
        {
            try
            {
                BO.Location point1, point2, point3 = new BO.Location();
                BO.Customer sender, target = new BO.Customer();
                //get the location of the parcel sender
                sender = GetCustomer(parcel.customerInParcelSender.uniqueID);
                point1 = sender.location;
                //get the location of our drone
                point2 = droneBo.location;
                //get the location of the parcel tgrget
                target = GetCustomer(parcel.customerInParcel_Target.uniqueID);
                point3 = target.location;
                //check if drone have enough battery to get up to the sender and than go up to target with the parcel
                if (droneBo.Battery - colculateBatteryBO(point1, point2, droneId) - colculateBatteryBO(point1, point3, droneId) > 0)
                {
                    //we found a parcel! return false for 
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                throw new BO.MyExeption_BO("Exception from function 'serchForRelevantParcel", e);
            }

        }
        BO.station convertToStationNotList(BO.StationToTheList stationToTheList)
        {
            BO.station station = new BO.station();
            station.uniqueID = stationToTheList.uniqueID;
            station.name = stationToTheList.name;
            station.availableChargingStations = stationToTheList.availableChargingStations;
            accessIdal.GetListOfStations().ToList().ForEach(delegate (IDAL.DO.Station stationDO)
            {
                if (station.uniqueID == stationDO.id)
                {
                    station.location.latitude = stationDO.Location.latitude;
                    station.location.longitude = stationDO.Location.longitude;

                }
            });
            return station;
        }
        BO.Parcel convertToParcelNotList(BO.ParcelToList parcelToList)
        {
            BO.Parcel parcel = new BO.Parcel();
            BO.CustomerInParcel tempCust= new BO.CustomerInParcel();
            parcel.uniqueID = parcelToList.uniqueID;
            tempCust.name = parcelToList.namrSender;
            parcel.customerInParcelSender = tempCust;
            tempCust.name = parcelToList.nameTarget; ;
            parcel.customerInParcel_Target = tempCust;
            parcel.priority = parcelToList.priority;
            parcel.weight = parcelToList.weight;
            accessIdal.GetListOfParcels().ToList().ForEach(delegate (IDAL.DO.Parcel parcelDO)
            {
                if (parcel.uniqueID == parcelDO.Id)
                {
                    parcel.delivered = parcelDO.Delivered;
                    parcel.scheduled = parcelDO.Scheduled;
                    parcel.pickedUp = parcelDO.PickedUp;
                    parcel.requested = parcelDO.Requested;
                    BO.DroneInPackage droneInPackage = new BO.DroneInPackage();
                    droneInPackage.uniqueID = parcelDO.DroneId;
                    parcel.droneInParcel = droneInPackage;


                }
            });
            return parcel;
        }
        public double distance(BO.Location p1, BO.Location p2)
        {
            return 1000 * Math.Sqrt((Math.Pow(p1.latitude - p2.latitude, 2) + Math.Pow(p1.longitude - p2.longitude, 2)));
        }
        public double colculateBatteryBO(BO.Location point1, BO.Location point2, int ID)
        {
            BO.Drone drone = GetDrone(ID);
            double minus = 0;
            List<double> configStatus = accessIdal.PowerConsumptionBySkimmer();
            double distance = point1.distancePointToPoint(point2);
            if (drone.weight == BO.EnumBO.WeightCategories.Light)
            {
                //all 1500 meters is minus 1% battery
                minus = distance / configStatus[1];
            }
            if (drone.weight == BO.EnumBO.WeightCategories.Medium)
            {
                //all 1000 meters is minus 1% battery
                minus = distance / configStatus[2];
            }
            if (drone.weight == BO.EnumBO.WeightCategories.Heavy)
            {
                //all 850 meters is minus 1% battery
                minus = distance / configStatus[3];
            }
            return minus;
        }
    }

}
