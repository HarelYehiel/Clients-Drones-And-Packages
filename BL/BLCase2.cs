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
        public void Update_drone_data(int ID, string newModel)
        {
            try
            {
                IDAL.DO.Drone drone = temp.GetDrone(ID);
                drone.Model = newModel;
                updateDataSourceFun.updateDrone(drone);
            }
            catch (Exception e)
            {

                throw new BO.MyExeption_BO("Exception from function 'Update_drone_data", e);
            }

        }
        public void Update_station_data(int ID, string name, int numSlots)
        {
            try
            {
                IDAL.DO.station station = temp.GetStation(ID);
                if (name[0] != '\n')
                    station.name = name;
                if (numSlots != '\n')
                    station.ChargeSlots = numSlots;
                updateDataSourceFun.updateStation(station);
            }
            catch (Exception e)
            {

                throw new BO.MyExeption_BO("Exception from function 'Update_station_data", e);
            }

        }
        public void Update_customer_data(int ID, string name, string phoneNumber)
        {
            try
            {
                IDAL.DO.Customer customer = temp.GetCustomer(ID);
                if (name[0] != '\n')
                    customer.name = name;
                if (phoneNumber[0] != '\n')
                    customer.phone = phoneNumber;
                updateDataSourceFun.updateCustomer(customer);
            }
            catch (Exception e)
            {

                throw new BO.MyExeption_BO("Exception from function 'Update_customer_data", e);
            }

        }
        public void Sending_a_drone_for_charging(int ID)
        {
            try
            {
                IDAL.DO.Drone drone = temp.GetDrone(ID);
                BO.Drone droneBo = new BO.Drone();
                //-----check drone status, only if he is free check the next condition-----
                if (drone.droneStatus == IDAL.DO.Enum.DroneStatus.Avilble)
                {
                    //------gett data of this dron from BL drone list-----------
                    for (int i = 0; i < listDrons.Count; i++)
                    {
                        if (listDrons[i].uniqueID == ID)
                        {
                            droneBo = listDrons[i];
                        }
                    }
                    ///----------find the most close station---------
                    IDAL.DO.Point point1, point2 = new IDAL.DO.Point();
                    point1.latitude = droneBo.location.latitude;
                    point1.longitude = droneBo.location.longitude;
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
                    if (droneBo.Battery - updateDataSourceFun.colculateBattery(point1, point2, ID) > 0)
                    {
                        //update drone data in BO
                        droneBo.Battery -= updateDataSourceFun.colculateBattery(point1, point2, ID);
                        droneBo.location.latitude = point2.latitude;
                        droneBo.location.longitude = point2.longitude;
                        droneBo.Status = BO.Enum_BO.DroneStatus.Baintenance;


                        //update station data
                        IDAL.DO.station sta = new IDAL.DO.station();
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
        public void Release_drone_from_charging(int ID, int min)
        {
            try
            {
                IDAL.DO.Drone drone = temp.GetDrone(ID);
                if (drone.droneStatus == IDAL.DO.Enum.DroneStatus.Baintenance)
                {
                    //------gett data of this dron from BL drone list-----------
                    BO.Drone droneBo = new BO.Drone();
                    for (int i = 0; i < listDrons.Count; i++)
                    {
                        if (listDrons[i].uniqueID == ID)
                        {
                            droneBo = listDrons[i];
                        }
                    }
                    //update drone in BL list
                    droneBo.Battery = droneBo.Battery + (min);//every minute in charge is 1% more
                    if (droneBo.Battery > 100)
                        droneBo.Battery = 100;
                    droneBo.Status = BO.Enum_BO.DroneStatus.Avilble;
                    for (int i = 0; i < listDrons.Count; i++)
                    {
                        if (listDrons[i].uniqueID == ID)
                        {
                            listDrons[i] = droneBo;
                        }
                    }
                    //update data in dataSource
                    IDAL.DO.Point point = new IDAL.DO.Point();
                    point.latitude = droneBo.location.latitude;
                    point.longitude = droneBo.location.longitude;
                    updateDataSourceFun.updateRelaseDroneFromCharge(ID, point, min);

                }
                else//הרחפן בכלל לא בתחזוקה
                    throw new BO.MyExeption_BO("The skimmer is not maintained at all");
            }

            catch (Exception e)
            {
                throw new BO.MyExeption_BO("Exception from function 'Release_drone_from_charging", e);
            }

        }
        public void Assign_a_package_to_a_drone(int droneId)
        {
            bool serchForRelevantParcel(IDAL.DO.Parcel parcel,IDAL.DO.Drone drone, BO.Drone droneBo)
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
                    droneBo.Status = BO.Enum_BO.DroneStatus.Delivery;
                    IDAL.DO.Parcel par = parcel;
                    par.Scheduled = DateTime.Now;
                    //update to data source
                    Update_drone_data(droneId, drone.Model);
                    updateDataSourceFun.updateParcel(par);
                    return false;
                }
                return true;
            }
            try
            {
                //get the data of the specific drone from DAL(data source)
                IDAL.DO.Drone drone = temp.GetDrone(droneId);
                //get the data of the specific drone at BO
                BO.Drone droneBo = new BO.Drone();
                foreach(var tempDroneOfBL in listDrons)
                {
                    if (tempDroneOfBL.uniqueID == droneId)
                        droneBo = tempDroneOfBL;
                }
               
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
                                flag = serchForRelevantParcel(parcel, drone, droneBo);
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
                                    flag = serchForRelevantParcel(parcel2, drone, droneBo);

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
                                    flag = serchForRelevantParcel(parcel3, drone, droneBo);

                                }
                            }
                        }
                    }

                    if(flag)//after all the search, this drone cant take any parecl
                        throw new BO.MyExeption_BO("This drone cant take any parecl");
                }
                else
                    throw new BO.MyExeption_BO("The drone is not available and can not be called anything else right now");
            }

            catch (Exception e)
            {

                throw new BO.MyExeption_BO("Exception from function 'Assign_a_package_to_a_drone", e);
            }

        }
        public void Collection_of_a_package_by_drone(int ID)
        {
            try
            {
                IDAL.DO.Drone drone = temp.GetDrone(ID);
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
                                BO.Drone droneBo = new BO.Drone();
                                foreach (var dro in listDrons)
                                {
                                    if (dro.uniqueID == ID)
                                        droneBo = dro;
                                }
                                //find sender location
                                senderId = IDAL.DalObject.DataSource.parcels[i].SenderId;
                                IDAL.DO.Point point1, point2 = new IDAL.DO.Point();
                                IDAL.DO.Customer sender = temp.GetCustomer(senderId);
                                point1.latitude = sender.location.latitude;
                                point1.longitude = sender.location.longitude;

                                //find drone location
                                point2.latitude = droneBo.location.latitude;
                                point2.longitude = droneBo.location.longitude;
                                double minus = updateDataSourceFun.colculateBattery(point1, point2, ID);
                                //update list drone in BL, no parametrs to update in IDAL
                                droneBo.Battery -= minus;
                                droneBo.location.latitude = sender.location.latitude;
                                droneBo.location.longitude = sender.location.longitude;

                                for (int j = 0; j < listDrons.Count; j++)
                                {
                                    if (listDrons[j].uniqueID == ID)
                                        listDrons[j] = droneBo;
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
        public void Delivery_of_a_package_by_drone(int ID)
        {
            try
            {
                IDAL.DO.Drone drone = temp.GetDrone(ID);
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
                                BO.Drone droneBo = new BO.Drone();
                                foreach (var dro in listDrons)
                                {
                                    if (dro.uniqueID == ID)
                                        droneBo = dro;
                                }
                                //find target location
                                targetId = IDAL.DalObject.DataSource.parcels[i].TargetId;
                                IDAL.DO.Point point1, point2 = new IDAL.DO.Point();
                                IDAL.DO.Customer target = temp.GetCustomer(targetId);
                                point1.latitude = target.location.latitude;
                                point1.longitude = target.location.longitude;

                                //find drone location
                                point2.latitude = droneBo.location.latitude;
                                point2.longitude = droneBo.location.longitude;

                                //update list drone in BL
                                double minus = updateDataSourceFun.colculateBattery(point1, point2, ID);
                                droneBo.Battery -= minus;
                                droneBo.location.latitude = target.location.latitude;
                                droneBo.location.longitude = target.location.longitude;
                                droneBo.Status = BO.Enum_BO.DroneStatus.Avilble;
                                for (int j = 0; j < listDrons.Count; j++)
                                {
                                    if (listDrons[j].uniqueID == ID)
                                        listDrons[j] = droneBo;
                                }

                                drone.droneStatus = IDAL.DO.Enum.DroneStatus.Avilble;
                                updateDataSourceFun.updateDrone(drone);
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
    }
}
