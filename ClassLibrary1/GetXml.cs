using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using DO;

namespace DalXml
{
    public class GetXml : IDal
    {
        // make the access to DalObject by singelton way
        static readonly GetXml instance = new GetXml();
        internal static GetXml Instance { get { return instance; } }

        string dirPath = @"..\..\..\..\DalXML\";
        string configDataPath = @"configXml.xml";
        string stationsPath = @"stationsXml.xml";
        string customersPath = @"customersXml.xml";
        string parcelsPath = @"parcelsXml.xml";
        string dronesPath = @"dronesXml.xml";
        string dronesChargePath = @"dronesXml.xml";

        public List<double> PowerConsumptionBySkimmer()
        {

            FileStream stream = File.OpenRead(dirPath + configDataPath);
            XmlSerializer serializer = new XmlSerializer(typeof(List<double>));

            List<double> dou = (List<double>)serializer.Deserialize(stream);
            return dou;

        }
        public void AddParcel(Parcel par)

        {
            par.runNumber++;
        }
        #region get personal item
        public Station GetStation(int stationId)
        // Return the station with stationId
        {
            FileStream stream = File.OpenRead(dirPath + stationsPath);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Station>));

            List<Station> stations = (List<Station>)serializer.Deserialize(stream);
            stream.Close();

            // IEnumerable<station> stations  = DataSource.stations;

            foreach (Station station in stations)
            {
                if (station.id == stationId)
                    return station;
            }
            throw new myExceptionDO("Exception from function GetStation", myExceptionDO.There_is_no_variable_with_this_ID);
        }



        public Drone GetDrone(int droneId)

        {
            FileStream stream = File.OpenRead(dirPath + dronesPath);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Drone>));

            List<Drone> drones = (List<Drone>)serializer.Deserialize(stream);
            // IEnumerable<station> stations  = DataSource.stations;

            stream.Close();
            foreach (Drone drone in drones)
            {
                if (drone.Id == droneId)
                    return drone;
            }

            throw new myExceptionDO("Exception from function GetDrone", myExceptionDO.There_is_no_variable_with_this_ID);
        }
        DroneCharge GetDroneCharge(int droneId)

        {
            FileStream stream = File.OpenRead(dirPath + dronesChargePath);
            XmlSerializer serializer = new XmlSerializer(typeof(List<DroneCharge>));

            List<DroneCharge> droneCharges = (List<DroneCharge>)serializer.Deserialize(stream);
            // IEnumerable<station> stations  = DataSource.stations;

            stream.Close();
            foreach (DroneCharge ob in droneCharges)
            {
                if (ob.DroneId == droneId)
                    return ob;
            }

            throw new myExceptionDO("Exception from function GetDrone", myExceptionDO.There_is_no_variable_with_this_ID);
        }


        public Customer GetCustomer(int CustomerId)

        {
            FileStream stream = File.OpenRead(dirPath + customersPath);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Customer>));

            List<Customer> customers = (List<Customer>)serializer.Deserialize(stream);
            stream.Close();
            foreach (Customer customer in customers)
            {
                if (customer.Id == CustomerId)
                    return customer;
            }
            throw new myExceptionDO("Exception from function GetCustomer", myExceptionDO.There_is_no_variable_with_this_ID);
        }
        public Parcel GetParcel(int ParcelId)

        // Return the parcel with parcelId
        {
            FileStream stream = File.OpenRead(dirPath + parcelsPath);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Parcel>));

            List<Parcel> parcels = (List<Parcel>)serializer.Deserialize(stream);
            stream.Close();

            foreach (Parcel parcel in parcels)
            {
                if (parcel.Id == ParcelId)
                    return parcel;
            }
            throw new myExceptionDO("Exception from function GetParcel", myExceptionDO.There_is_no_variable_with_this_ID);
        }
        #endregion
        #region delete personal item
        public void DelParcel(int ID)
        {
            Parcel parcel = GetParcel(ID);
            if (parcel.Requested != null) //if the parcel is exist
            {
                List<Parcel> tempList = GetListOfParcels().ToList();
                tempList.Remove(parcel);
                SaveListToXmlSerializer<Parcel>(tempList, dirPath + parcelsPath);

            }


        }

        public void DelStation(int ID)
        {
            throw new Exception();
        }

        public void DelCustomer(int ID)
        {
            throw new Exception();
        }

        public void DelDrone(int ID)
        {
            try
            {
                Drone drone = GetDrone(ID);
                List<Drone> tempList = GetListOfDrones().ToList();
                tempList.Remove(drone);
                SaveListToXmlSerializer<Drone>(tempList, dirPath + dronesPath);

            }
            catch
            {
                //nothing
            }
        }
        #endregion
        #region add item to xml file
        public void InputTheStation(Station station)
        {
            List<Station> tempList = GetListOfStations().ToList();
            tempList.Add(station);
            SaveListToXmlSerializer<Station>(tempList, dirPath + stationsPath);
        }
        public void InputTheDroneCharge(DroneCharge droneCharge)
        {
            List<DroneCharge> tempList = GetListOfDronesInCharging().ToList();
            tempList.Add(droneCharge);
            SaveListToXmlSerializer<DroneCharge>(tempList, dirPath + dronesChargePath);
        }
        public void InputTheParcel(Parcel par)
        {
            List<Parcel> tempList = GetListOfParcels().ToList();
            tempList.Add(par);
            SaveListToXmlSerializer<Parcel>(tempList, dirPath + parcelsPath);
        }
        public void InputTheCustomer(Customer cust)
        {

            List<Customer> templist = GetListOfCustmers().ToList();
            templist.Add(cust);
            SaveListToXmlSerializer<Customer>(templist, dirPath + customersPath);
        }
        public void InputTheDrone(Drone drone)
        {
            //XDocument dronesFile = new XDocument(dirPath + dronesPath);
            //dronesFile.Add(drone);
            //dronesFile.Save(dirPath + dronesPath);
            List<Drone> lst = GetListOfDrones().ToList();
            lst.Add(drone);
            SaveListToXmlSerializer<Drone>(lst, dirPath + dronesPath);
        }
        #endregion
        #region get list of items
        public IEnumerable<DroneCharge> GetListOfDroneCharge()
        {
            return LoadListFromXmlSerializer<DroneCharge>(dirPath + dronesChargePath);
        }

        public IEnumerable<Station> GetListOfStations()
        //return all the station from DataSource.stations

        {
            return LoadListFromXmlSerializer<Station>(dirPath + stationsPath);
        }

        public IEnumerable<Customer> GetListOfCustmers()
        //return all the customer from DataSource.customers
        {
            return LoadListFromXmlSerializer<Customer>(dirPath + customersPath);
        }
        public IEnumerable<DroneCharge> GetListOfDronesInCharging()
        {
            return LoadListFromXmlSerializer<DroneCharge>(dirPath + dronesChargePath);
        }

        public IEnumerable<Parcel> GetListOfParcels()
        //print all the Parcel from DataSource.parcels

        {
            return LoadListFromXmlSerializer<Parcel>(dirPath + parcelsPath);
        }
        public IEnumerable<Drone> GetListOfDrones()
        //print all the Drone from DataSource.drones
        {
            return LoadListFromXmlSerializer<Drone>(dirPath + dronesPath);

        }
        #region specific filter of list
        public IEnumerable<Parcel> DisplaysParcelsDontHaveDrone()

        // Print the details of all the parcels don't have An associated skimmer (Selected_drone == 0).
        {
            try
            {
                IEnumerable<Parcel> parcels = GetListOfParcels();
                List<Parcel> parcelsWithoutDrone = new List<Parcel>();
                foreach (Parcel parcel in parcels)
                {
                    if (parcel.Id != 0 && parcel.DroneId == 0)
                        parcelsWithoutDrone.Add(parcel);
                }
                return parcelsWithoutDrone;
            }
            catch
            {
                throw new myExceptionDO("Exception from function displaysParcelsDontHaveDrone", myExceptionDO.Dont_have_any_parcel_in_the_list);
            }
        }
        #endregion
        #endregion
        #region implementaion of intarface IDal
        public IEnumerable<Station> AvailableChargingStations()

        //Print the all stations that have DroneCharge available
        {
            try
            {
                List<Station> stat = new();
                foreach (Station station in GetListOfStations())
                {
                    if (station.ChargeSlots != 0)
                        stat.Add(station);
                }
                return stat;
            }
            catch
            {
                throw new myExceptionDO("Exception from function AvailableChargingStations", myExceptionDO.Dont_have_any_station_in_the_list);

            }
        }
        public string MinimumFromCustomer(double minDistance, Point p)

        {
            try
            {

                int id;
                foreach (Customer customer in GetListOfCustmers())
                {
                    double distance = customer.location.distancePointToPoint(p);
                    if (minDistance > distance)
                    {
                        id = customer.Id;
                        minDistance = distance;
                    }
                }
                return ("The minimum distancefrom the point is: " + minDistance +
                   "\nThe id of customer is: ");
            }
            catch
            {
                throw new myExceptionDO("Exception from function MinimumFromCustomer", myExceptionDO.Dont_have_any_customer_in_the_list);
            }
        }
        public string MinimumFromStation(double minDistance, Point p)
        {
            try
            {

                int id = 0;
                foreach (Station station in GetListOfStations())
                {
                    double distance = station.Location.distancePointToPoint(p);
                    if (minDistance > distance)
                    {
                        id = station.id;
                        minDistance = distance;
                    }
                }
                return ("The minimum distance from the point is: " + minDistance +
                    "\nThe id of station is: " + id.ToString());
            }
            catch
            {
                throw new myExceptionDO("Exception from function MinimumFromStation", myExceptionDO.Dont_have_any_station_in_the_list);

            }
        }
        #endregion
        #region update methods
        public void AffiliationDroneToParcel(int parcelID, int droneID)
        {
            if (GetListOfParcels().Count() == 0)
                throw new myExceptionDO("Exception from function AffiliationDroneToParcel", myExceptionDO.Dont_have_any_parcel_in_the_list);
            try
            {
                Parcel tempParcel = new Parcel();
                foreach (Parcel parcel in GetListOfParcels())
                {
                    if (parcel.Id == parcelID)
                    {
                        tempParcel = parcel;
                        tempParcel.DroneId = droneID;
                        updateParcel(tempParcel);
                        break;
                    }
                }
            }
            catch
            {
                throw new myExceptionDO("Exception from function AffiliationDroneToParcel", myExceptionDO.There_is_no_variable_with_this_ID);
            }
        }
        public void PickUp(int parcelId)
        {
            if (GetListOfParcels().Count() == 0)
                throw new myExceptionDO("Exception from function pickUp", myExceptionDO.Dont_have_any_parcel_in_the_list);

            try
            {
                Parcel tempParcel = new Parcel();
                foreach (Parcel parcel in GetListOfParcels())
                {
                    if (parcel.Id == parcelId)
                    {
                        tempParcel = parcel;
                        tempParcel.PickedUp = DateTime.Now;
                        updateParcel(tempParcel);
                        break;
                    }
                }
            }
            catch
            {
                throw new myExceptionDO("Exception from function pickUp", myExceptionDO.There_is_no_variable_with_this_ID); ;
            }
        }
        public void Delivered(int parcelID)
        {
            if (GetListOfParcels().Count() == 0)
                throw new myExceptionDO("Exception from function delivered", myExceptionDO.Dont_have_any_parcel_in_the_list);

            try
            {
                Parcel tempParcel = new Parcel();
                foreach (Parcel parcel in GetListOfParcels())
                {
                    if (parcel.Id == parcelID)
                    {
                        tempParcel = parcel;
                        tempParcel.Delivered = DateTime.Now;
                        updateParcel(tempParcel);
                        break;
                    }
                }
            }
            catch
            {
                throw new myExceptionDO("Exception from function delivered", myExceptionDO.There_is_no_variable_with_this_ID);
            }
        }
        public void SetFreeStation(int droneId)
        {
            if (GetListOfStations().Count() == 0)
                throw new myExceptionDO("Exception from function setFreeStation", myExceptionDO.Dont_have_any_station_in_the_list);
            if (GetListOfDrones().Count() == 0)
                throw new myExceptionDO("Exception from function setFreeStation", myExceptionDO.Dont_have_any_drone_in_the_list);
            try
            {
                foreach (DroneCharge droneCharge in GetListOfDronesInCharging())
                {
                    if (droneCharge.DroneId == droneId)
                    {
                        foreach (Station station1 in GetListOfStations())
                        {
                            if (station1.id == droneCharge.staitionId)
                            {
                                Station station = new Station();
                                station = station1;
                                station.ChargeSlots++;
                                updateStation(station);
                            }
                        }

                        updateDroneToCharge(droneCharge);
                        break;
                    }
                }
            }
            catch
            {
                throw new myExceptionDO("Exception from function setFreeStation", myExceptionDO.We_ge_to_the_end_of_list_and_dont_find_the_drone);
            }
        }
        public void DroneToCharge(int droneId, int stationId)
        {
            if (GetListOfStations().Count() == 0)
                throw new myExceptionDO("Exception from function droneToCharge", myExceptionDO.Dont_have_any_station_in_the_list);
            if (GetListOfDrones().Count() == 0)
                throw new myExceptionDO("Exception from function droneToCharge", myExceptionDO.Dont_have_any_drone_in_the_list);

            foreach (DroneCharge droneCharging in GetListOfDronesInCharging()) // Check if The drone already is in charge  
            {
                if (droneCharging.DroneId == droneId)
                {
                    throw new myExceptionDO("The drone already is in charge.");
                }
            }

            foreach (Station station1 in GetListOfStations()) // Find if the station exists
            {
                if (station1.id == stationId)
                {
                    DroneCharge droneCharge = new DroneCharge();
                    droneCharge.DroneId = droneId;
                    droneCharge.staitionId = stationId;
                    updateDroneToCharge(droneCharge);

                    Station station = new Station();
                    station = station1;
                    station.ChargeSlots--;
                    updateStation(station);

                }
            }

        }
        public void updateParcel(Parcel updatedParcel)
        {
            // get the new update station, remove the older and save the new
            List<Parcel> parcels = GetListOfParcels().ToList();
            Parcel ParcelToRemove = GetParcel(updatedParcel.Id);
            parcels.Remove(ParcelToRemove);
            parcels.Add(updatedParcel);
            SaveListToXmlSerializer<Parcel>(parcels, dirPath + parcelsPath);

        }
        public void updateStation(Station updatedStation)
        {
            // get the new update station, remove the older and save the new
            List<Station> stations = GetListOfStations().ToList();
            Station stationToRemove = GetStation(updatedStation.id);
            stations.Remove(stationToRemove);
            stations.Add(updatedStation);
            SaveListToXmlSerializer<Station>(stations, dirPath + parcelsPath);
        }
        public void updateDroneToCharge(DroneCharge droneCharge)
        {
            //if the drone is not exist add him
            List<DroneCharge> droneCharges = GetListOfDroneCharge().ToList();
            bool exists = false;
            for (int i = 0; i < droneCharges.Count; i++)
            {
                if (droneCharges[i].DroneId == droneCharge.DroneId)
                {
                    exists = true;
                }

            }
            if (!exists)
                droneCharges.Add(droneCharge);
            SaveListToXmlSerializer<DroneCharge>(droneCharges, dirPath + dronesChargePath);
        }
        public void updateDrone(DO.Drone drone)
        {
            // get the new update drone, remove the older and save the new
            List<Drone> drones = GetListOfDrones().ToList();
            Drone DroneToRemove = GetDrone(drone.Id);
            drones.Remove(DroneToRemove);
            drones.Add(drone);
            SaveListToXmlSerializer<Drone>(drones, dirPath + dronesPath);
        }
        public void updateCustomer(DO.Customer customer)
        {
            // get the new update customer, remove the older and save the new
            List<Customer> customers = GetListOfCustmers().ToList();
            Customer customerToRemove = GetCustomer(customer.Id);
            customers.Remove(customerToRemove);
            customers.Add(customer);
            SaveListToXmlSerializer<Customer>(customers, dirPath + customersPath);
        }
        public void updateRelaseDroneFromCharge(int droneId, double longi, double lati, double min)
        {
            //   DO.Point stationLocation = new DO.Point({ latitude = lati, longitude = longi, });


            List<DroneCharge> droneCharges = GetListOfDroneCharge().ToList();
            foreach (DroneCharge droneCharge in droneCharges)
            {
                if (droneCharge.DroneId == droneId)
                {
                    //the drone is release so the staition have more free slot
                    Station station = GetStation(droneCharge.staitionId);
                    station.ChargeSlots++;
                    updateStation(station);
                    // update the status of drone
                    Drone drone = GetDrone(droneId);
                    drone.droneStatus = (DO.Enum.DroneStatus)0;
                    updateDrone(drone);
                    //remove this object from list
                    droneCharges.Remove(droneCharge);
                    SaveListToXmlSerializer<DroneCharge>(droneCharges, dirPath + dronesChargePath);
                    break;
                }
            }


        }
        #endregion
        void SaveListToXmlSerializer<T>(List<T> list, string filePath)
        {
            try
            {
                FileStream file = new(filePath, FileMode.Create);
                XmlSerializer x = new(list.GetType());
                x.Serialize(file, list);
                file.Close();
            }
            catch
            {
                //dont do anything
            }
        }
        static List<T> LoadListFromXmlSerializer<T>(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    List<T> list;
                    XmlSerializer x = new(typeof(List<T>));
                    FileStream file = new(filePath, FileMode.Open);
                    list = (List<T>)x.Deserialize(file);
                    file.Close();
                    return list;
                }
                else
                    return new List<T>();
            }
            catch
            {
                return new List<T>();
            }
        }
    }
}
