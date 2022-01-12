using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace BlApi
{
    public partial class BL : IBL
    {
        // DalApi.DalObject.UpdateClass updateDataSourceFun = new DalApi.DalObject.UpdateClass();

         private delegate bool Predicate<in T>(T obj);
         private delegate BO.Parcel Converter<in ParcelToList, out Parcel>(BO.ParcelToList input);


        private static bool findEmergency(Parcel parcel) { return (parcel.Scheduled == null && parcel.priority == DO.Enum.Priorities.Emergency); }
        private static bool findFast(Parcel parcel) { return (parcel.Scheduled == null && parcel.priority == DO.Enum.Priorities.Fast); }
        private static bool findNormal(Parcel parcel) { return (parcel.Scheduled == null && parcel.priority == DO.Enum.Priorities.Normal); }

        [MethodImpl(MethodImplOptions.Synchronized)] public void UpdateDroneData(int ID, string newModel)
        {
            try
            {
                Drone drone = accessDal.GetDrone(ID);
                drone.Model = newModel;
                accessDal.updateDrone(drone);
                BO.DroneToList droneToList_BO;
                droneToList_BO = GetDroneToListBO(ID);
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
        [MethodImpl(MethodImplOptions.Synchronized)] public void UpdateStationData(int ID, string name, int numSlots)
        {
            try
            {
                if (name == "" && numSlots == 0)
                    return; // Don't do nathing.

                Station station = accessDal.GetStation(ID);
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
                accessDal.updateStation(station);
            }
            catch (Exception e)
            {

                throw new BO.MyExeption_BO("Exception from function 'Update_station_data", e);
            }

        }
        [MethodImpl(MethodImplOptions.Synchronized)] public void RemoveAllSkimmersFromTheStation(int ID)
        {
            List<BO.DroneInCharging> droneInChargings = new List<BO.DroneInCharging>();
            droneInChargings = GetAllDronesInCharging(d => d.staitionId == ID).ToList();

            foreach (var item in droneInChargings)
            {
                double timestamp = DateTime.UtcNow.Ticks;

                var converted = DateTime.Now.ToOADate();

                double d = 1;//= ( DateTime.Now - item.startCharge)
                DateTime dateTime = new DateTime();
                dateTime = DateTime.Now;
                ReleaseDroneFromCharging(item.uniqueID, dateTime);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)] public void UpdateCustomerData(int ID, string name, string phoneNumber)
        {
            try
            {
                Customer customer = accessDal.GetCustomer(ID);
                if (name != "")
                    customer.name = name;
                if (phoneNumber != "")
                    customer.phone = phoneNumber;
                accessDal.updateCustomer(customer);
            }
            catch (Exception e)
            {

                throw new BO.MyExeption_BO("Exception from function 'UpdateCustomerData", e);
            }

        }
        BO.Location ReturnLoctionOfCloslyStation(BO.Location location1)
        {
            ///----------find the most close station---------
            List<BO.station> stations = GetAllStaionsBy(s => s.ChargeSlots > 0).ToList().ConvertAll(convertToStationNotList);
            BO.Location point2 = new BO.Location();
            point2 = stations[0].location;
            double min = location1.distancePointToPoint(point2);
            foreach (var station in stations)
            {
                double dis = location1.distancePointToPoint(station.location);
                if (dis < min)
                {
                    point2 = station.location;
                }
            }

            return point2;
        }
        [MethodImpl(MethodImplOptions.Synchronized)] public void SendingDroneToCharging(int ID)
        {
            try
            {
                Drone drone = accessDal.GetDrone(ID);
                BO.DroneToList droneToListBo = GetDroneToListBO(ID);
                //-----check drone status, only if he is free check the next condition-----
                if (drone.droneStatus == DO.Enum.DroneStatus.Avilble)
                {
                    BO.Location mostCloseStation = ReturnLoctionOfCloslyStation(droneToListBo.location);
                    //--------if drone's battary can survive up to the station-------------
                    if (droneToListBo.Battery - colculateBatteryBO(droneToListBo.location, mostCloseStation, ID) > 0)
                    {
                        //update drone data in BO
                        droneToListBo.Battery -= colculateBatteryBO(droneToListBo.location, mostCloseStation, ID);
                        droneToListBo.location = mostCloseStation;
                        droneToListBo.status = BO.EnumBO.DroneStatus.Baintenance;
                        for (int i = 0; i < ListDroneToList.Count; i++)
                        {
                            if (ListDroneToList[i].uniqueID == droneToListBo.uniqueID)
                            {
                                ListDroneToList[i] = droneToListBo;
                                break;
                            }
                        }

                        //update the change at the station
                        foreach (var station in GetAllStaionsBy(s => s.ChargeSlots > 0).ToList().ConvertAll(convertToStationNotList))
                        {
                            if (station.location.latitude == mostCloseStation.latitude && station.location.longitude == mostCloseStation.longitude)
                            {
                                station.availableChargingStations--;
                                //update station data in DataSource
                                UpdateStationData(station.uniqueID, station.name, station.availableChargingStations);
                                //update all the changes data
                                DroneCharge droneCharge = new DroneCharge
                                {
                                    DroneId = ID,
                                    staitionId = station.uniqueID,
                                    startCharge = DateTime.Now,
                                };
                                accessDal.InputTheDroneCharge(droneCharge);
                                break;
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
        [MethodImpl(MethodImplOptions.Synchronized)] public void ReleaseDroneFromCharging(int ID, DateTime updateTime)
        {
            try
            {
                Drone drone = accessDal.GetDrone(ID);
                BO.DroneToList droneBo = GetDroneToListBO(ID);// new BO.DroneToList();

                if (droneBo.status == BO.EnumBO.DroneStatus.Baintenance)
                {
                    //------gett data of this dron from BL drone list-----------
                    List<double> getConfig = accessDal.PowerConsumptionBySkimmer();
                    BO.DroneInCharging droneCharge = GetDroneInCharging(ID);
                    double timInCharge = (updateTime - droneCharge.startCharge).TotalSeconds;
                    //update drone in BL list
                    droneBo.Battery = droneBo.Battery + (timInCharge * getConfig[4]);//update the battery according to config
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
                    accessDal.updateRelaseDroneFromCharge(ID, point.longitude, point.latitude, timInCharge);

                    //update all the changes data at the stations list
                    //Station sta = new Station();
                    //List<BO.station> stations = GetListOfBaseStations().ToList().ConvertAll(convertToStationNotList);
                    //foreach (var station in stations)
                    //{
                    //    if (station.location == point)
                    //    {
                    //        sta.ChargeSlots++;
                    //        accessDal.updateStation(sta);
                    //    }

                    //}
                }
                else
                    throw new BO.MyExeption_BO("The drone is not maintained at all");
            }

            catch (Exception e)
            {
                throw new BO.MyExeption_BO("Exception from function 'ReleaseDroneFromCharging", e);
            }

        }
        [MethodImpl(MethodImplOptions.Synchronized)] public void AssignPackageToDrone(int droneId)
        {
            try
            {
                //get the data of the specific drone from DAL(data source)
                Drone drone = accessDal.GetDrone(droneId);
                //get the data of the specific drone at BO
                BO.DroneToList droneBo = GetDroneToListBO(droneId);
                Parcel parcelDO = new Parcel();


                bool flag = true;
                //---------------first of all - check if the drone is avilble----------------------
                if (droneBo.status == BO.EnumBO.DroneStatus.Avilble)
                {
                    //-----------we always prefere to take care by priority order---------------
                    List<BO.ParcelToList> newList = GetAllParcelsBy(findEmergency).ToList();
                    newList.AddRange(GetAllParcelsBy(findFast).ToList());
                    newList.AddRange(GetAllParcelsBy(findNormal).ToList());
                    foreach (var parcel in newList.ConvertAll(convertToParcelNotList))
                        if ((int)parcel.weight <= (int)drone.MaxWeight)
                        {
                            //newList now have all the aviable and unScheluled parcels by priority order 
                            flag = serchForRelevantParcel(parcel, droneBo);
                            if (!flag)
                            {
                                parcelDO = accessDal.GetParcel(parcel.uniqueID);
                                break;
                            }
                        }
                    if (flag)//After all the search, this drone cant take any parecl
                        throw new BO.MyExeption_BO("This drone can't take any parecl");
                    else // We found a relavante parcel! update the data. 
                    {
                        parcelDO.Scheduled = DateTime.Now;
                        parcelDO.DroneId = drone.Id;
                        accessDal.updateParcel(parcelDO);

                        drone.droneStatus = DO.Enum.DroneStatus.Delivery;
                        accessDal.updateDrone(drone);

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
        [MethodImpl(MethodImplOptions.Synchronized)] public void CollectionOfPackageByDrone(int ID)
        {
            try
            {
                Drone drone = accessDal.GetDrone(ID);
                Parcel parcel = new Parcel();
                if (drone.droneStatus == DO.Enum.DroneStatus.Delivery)
                {
                    List<BO.Parcel> parcels = GetTheListOfParcels().ToList().ConvertAll(convertToParcelNotList);
                    for (int i = 0; i < parcels.Count; i++)
                    {
                        if (parcels[i].droneInParcel != null && parcels[i].droneInParcel.uniqueID == ID)
                        {
                            //if the parcel not picked up yet the PickUp time will be defult
                            if (parcels[i].pickedUp == null)
                            {
                                BO.DroneToList droneToListeBo = GetDroneToListBO(ID);
                                BO.Customer sender = GetCustomer(parcels[i].customerInParcelSender.uniqueID);
                                double minus = colculateBatteryBO(sender.location, droneToListeBo.location, ID);

                                //update list drone in BL, and time of picked up at parcel
                                droneToListeBo.Battery -= minus;
                                droneToListeBo.location.latitude = sender.location.latitude;
                                droneToListeBo.location.longitude = sender.location.longitude;

                                //update changes at DataSource
                                //if (parcelFromUser == 0)
                                //    parcel = accessDal.GetParcel(parcelFromUser);
                                //else
                                parcel = accessDal.GetParcel(parcels[i].uniqueID);
                                parcel.PickedUp = DateTime.Now;
                                accessDal.updateParcel(parcel);

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
        [MethodImpl(MethodImplOptions.Synchronized)] public void DeliveryOfPackageByDrone(int ID)
        {
            try
            {
                Drone drone = accessDal.GetDrone(ID);
                if (drone.droneStatus == DO.Enum.DroneStatus.Delivery)
                {
                    List<BO.Parcel> parcels = GetTheListOfParcels().ToList().ConvertAll(convertToParcelNotList);//GetAllParcelsBy(p => true).ToList();
                    for (int i = 0; i < parcels.Count; i++)
                    {
                        if (parcels[i].droneInParcel.uniqueID == ID)
                        {
                            //if this drone is picked up so the pickedUp time isn't defult and not yet deliverd
                            if (parcels[i].pickedUp != null && parcels[i].delivered == null)
                            {
                                BO.DroneToList droneToList_Bo = GetDroneToListBO(ID);
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
                                Parcel parcel = accessDal.GetParcel(parcels[i].uniqueID);
                                parcel.Delivered = DateTime.Now;
                                drone.droneStatus = DO.Enum.DroneStatus.Avilble;

                                //update changes in DataSource
                                accessDal.updateDrone(drone);
                                accessDal.updateParcel(parcel);
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
        [MethodImpl(MethodImplOptions.Synchronized)] public void UpdateBatteryInReelTime(int idDrone, double distance,
    char AddOrSubtractToBattery /*'-' or '+'*/)
        {

            BO.DroneToList droneToList_BO = GetDroneToListBO(idDrone);
            List<double> configStatus = accessDal.PowerConsumptionBySkimmer();

            if (AddOrSubtractToBattery == '-')
                switch (droneToList_BO.weight)
                {
                    case BO.EnumBO.WeightCategories.Light:
                        droneToList_BO.Battery -= distance / configStatus[1];
                        break;
                    case BO.EnumBO.WeightCategories.Medium:
                        droneToList_BO.Battery -= distance / configStatus[2];

                        break;
                    case BO.EnumBO.WeightCategories.Heavy:
                        droneToList_BO.Battery -= distance / configStatus[3];

                        break;

                }
            else if (AddOrSubtractToBattery == '+')
            {
                droneToList_BO.Battery += configStatus[4];
            }

            if (droneToList_BO.Battery < 0) droneToList_BO.Battery = 0;
            if (droneToList_BO.Battery > 100) droneToList_BO.Battery = 100;

            for (int i = 0; i < ListDroneToList.Count; i++)
            {
                if (ListDroneToList[i].uniqueID == idDrone)
                {
                    ListDroneToList[i] = droneToList_BO;
                    break;
                }
            }

        }
        [MethodImpl(MethodImplOptions.Synchronized)] public void updateBatteryBySimultor(int droneId, double updateBattery)
        {
            for (int i = 0; i < ListDroneToList.Count; i++)
            {
                if (ListDroneToList[i].uniqueID == droneId)
                {
                    ListDroneToList[i].Battery = updateBattery;
                    break;
                }
            }
        }
        private BO.DroneToList GetDroneToListBO(int id)
        {
            for (int i = 0; i < ListDroneToList.Count; i++)
            {
                if (ListDroneToList[i].uniqueID == id) return ListDroneToList[i];
            }
            throw new BO.MyExeption_BO("Exception from function 'GetDroneBO", BO.MyExeption_BO.There_is_no_variable_with_this_ID);
        }
        bool serchForRelevantParcel(BO.Parcel parcel, BO.DroneToList droneBo)
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
                double d = -colculateBatteryBO(point1, point2, droneBo.uniqueID) - colculateBatteryBO(point1, point3, droneBo.uniqueID)
                    - colculateBatteryBO(point3, ReturnLoctionOfCloslyStation(point3), droneBo.uniqueID);
                //check if drone have enough battery to get up to the sender and than go up to target with the parcel
                double f = droneBo.Battery + d;
                if (f > 0)
                {
                    //we found a parcel! return false for searching.

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
            accessDal.GetListOfStations().ToList().ForEach(delegate (Station stationDO)
            {
                if (station.uniqueID == stationDO.id)
                {
                    BO.Location point = new BO.Location();
                    point.latitude = stationDO.Location.latitude;
                    point.longitude = stationDO.Location.longitude;
                    station.location = point;
                }
            });
            return station;
        }
        BO.Parcel convertToParcelNotList(BO.ParcelToList parcelToList)
        {
            BO.Parcel parcel = new BO.Parcel();
            BO.CustomerInParcel tempCust = new BO.CustomerInParcel();
            parcel.uniqueID = parcelToList.uniqueID;
            //tempCust.name = parcelToList.namrSender;

            parcel.customerInParcelSender = GetCustomerInParcel(GetParcel(parcelToList.uniqueID).customerInParcelSender.uniqueID);
            parcel.customerInParcel_Target = GetCustomerInParcel(GetParcel(parcelToList.uniqueID).customerInParcel_Target.uniqueID);
            // tempCust.name = parcelToList.nameTarget; ;
            parcel.priority = parcelToList.priority;
            parcel.weight = parcelToList.weight;
            accessDal.GetListOfParcels().ToList().ForEach(delegate (Parcel parcelDO)
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
        [MethodImpl(MethodImplOptions.Synchronized)] public double distance(BO.Location p1, BO.Location p2)
        {
            return 100 * Math.Sqrt((Math.Pow(p1.latitude - p2.latitude, 2) + Math.Pow(p1.longitude - p2.longitude, 2)));
        }
        [MethodImpl(MethodImplOptions.Synchronized)] public double colculateBatteryBO(BO.Location point1, BO.Location point2, int ID)
        {
            BO.Drone drone = GetDrone(ID);
            double minus = 0;
            List<double> configStatus = accessDal.PowerConsumptionBySkimmer();
            double distance = point1.distancePointToPoint(point2);
            if (drone.weight == BO.EnumBO.WeightCategories.Light)
            {
                //all 10 KM is minus 1% battery
                minus = distance / configStatus[1];
            }
            if (drone.weight == BO.EnumBO.WeightCategories.Medium)
            {
                //all 8 KM is minus 1% battery
                minus = distance / configStatus[2];
            }
            if (drone.weight == BO.EnumBO.WeightCategories.Heavy)
            {
                //All 5 KM is minus 1% battery
                minus = distance / configStatus[3];
            }
            return minus;
        }
        [MethodImpl(MethodImplOptions.Synchronized)] public void updateParcel(int parcelID, int choise)
        {
            // In the object "ParcelToList" no have drone Id paraneter, so we take it from DO.Parcel
            DO.Parcel parcel1 = accessDal.GetParcel(parcelID);
            if (choise == 1)
                // After that we have droneID update collection parcel by this drone 
                CollectionOfPackageByDrone(parcel1.DroneId);
            if (choise == 2)
                DeliveryOfPackageByDrone(parcel1.DroneId);
        }
    }

}