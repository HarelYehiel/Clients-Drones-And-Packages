using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DalApi
{
    namespace DalObject
    {

        internal sealed class DalObject : IDal
        {
            // make the access to DalObject by singelton way
            static readonly DalObject instance = new DalObject();
            internal static DalObject Instance{ get{ return instance; } }
            public List<double> PowerConsumptionBySkimmer()
            {
                List<double> dou = new List<double>();

                dou.Add( DataSource.Config.vacant);
                dou.Add(DataSource.Config.lightWeight);
                dou.Add(DataSource.Config.mediumWeight);
                dou.Add(DataSource.Config.heavyWeight);
                dou.Add(DataSource.Config.droneLoadingRate);
                return dou;


            }
            private DalObject()
            {
                DataSource.Initialize();
            }
            public void AddParcel(Parcel par)

            {
                par.runNumber++;
            }

            public Station GetStation(int stationId)
            // Return the station with stationId
            {
               // IEnumerable<station> stations  = DataSource.stations;
               foreach(Station station in DataSource.stations)
                {
                    if (station.id == stationId)
                        return station;
                }
                throw new myExceptionDO("Exception from function GetStation", myExceptionDO.There_is_no_variable_with_this_ID);
            }
            public Drone GetDrone(int droneId)

            {
                foreach(Drone drone in DataSource.drones)
                {
                    if (drone.Id == droneId)
                        return drone;
                }
                throw new myExceptionDO("Exception from function GetDrone", myExceptionDO.There_is_no_variable_with_this_ID);
            }

            public Customer GetCustomer(int CustomerId)

            {
                foreach (Customer customer in DataSource.customers)
                {
                    if (customer.Id == CustomerId)
                        return customer;
                }
                throw new myExceptionDO("Exception from function GetCustomer", myExceptionDO.There_is_no_variable_with_this_ID);

            }

            public Parcel GetParcel(int ParcelId)

            // Return the parcel with parcelId
            {
                for (int i = 0; i < DataSource.parcels.Count; i++)
                {
                    if (DataSource.parcels[i].Id == ParcelId)
                        return DataSource.parcels[i];

                }
                throw new myExceptionDO("Exception from function GetParcel", myExceptionDO.There_is_no_variable_with_this_ID);
            }
            public void DelParcel(int ID) 
            {
                Parcel parcelToRemove = new Parcel();
            foreach(var par in DataSource.parcels)
                {
                    if (par.Id == ID)
                        parcelToRemove = par;
                }
                if (parcelToRemove.Requested != null)
                     DataSource.parcels.Remove(parcelToRemove);

            }

            public void DelStation(int ID)
            {
            foreach(var station in DataSource.stations)
                {
                    if (station.id == ID)
                        DataSource.stations.Remove(station);
                }
            }

            public void DelCustomer(int ID) 
            {
            foreach(var customer in DataSource.customers)
                {
                    if (customer.Id == ID)
                        DataSource.customers.Remove(customer);
                }
            }

            public void DelDrone(int ID)
            {
            foreach(var drone in DataSource.drones)
                {
                    if (drone.Id == ID)
                        DataSource.drones.Remove(drone);
                }
            }
            public void InputTheStationToArray(Station station)
            {
                DataSource.stations.Add(station);
            }
            public void InputTheParcelToArray(Parcel par)
            {
                DataSource.parcels.Add(par);
                AddParcel(par);//update the run-number serial
            }
            public void InputTheCustomerToArray(Customer cust)
            {
                DataSource.customers.Add(cust);
            }
            public void InputTheDroneToArray(Drone drone)
            {             
                DataSource.drones.Add(drone);
            }

            public IEnumerable<DroneCharge> GetListOfDroneCharge()
            {
                List<DroneCharge> droneCharges = new List<DroneCharge>();
                
                foreach (var item in DataSource.dronesCharge)
                {
                    droneCharges.Add(item);
                }
                return droneCharges;
            }

            public IEnumerable<Station> GetListOfStations()
            //return all the station from DataSource.stations

            {
                if (DataSource.parcels.Count == 0)
                    throw new myExceptionDO("Exception from function Displays_list_of_stations", myExceptionDO.Dont_have_any_station_in_the_list);

                List<Station> stations = new List<Station>();
                foreach (Station station in DataSource.stations)
                    stations.Add(station);
                return stations;
            }

            public IEnumerable<Customer> DisplaysListOfCustmers()
            //return all the customer from DataSource.customers
            {
                if (DataSource.parcels.Count == 0)
                    throw new myExceptionDO("Exception from function Displays_list_of_custmers", myExceptionDO.Dont_have_any_customer_in_the_list);

                List<Customer> customers = new List<Customer>();
                foreach (Customer customer in DataSource.customers)
                    customers.Add(customer);
                return customers;
            }
            public IEnumerable<Parcel> GetListOfParcels()
            //print all the Parcel from DataSource.parcels

            {
                if (DataSource.parcels.Count == 0)
                    throw new myExceptionDO("Exception from function Displays_list_of_Parcels", myExceptionDO.Dont_have_any_parcel_in_the_list);

                List<Parcel> parcels = new List<Parcel>();
                foreach (Parcel parcel in DataSource.parcels)
                {
                    parcels.Add(parcel);
                }
                return parcels;
            }
            public IEnumerable<Drone> GetListOfDrones()
            //print all the Drone from DataSource.drones
            {
                if (DataSource.parcels.Count == 0)
                    throw new myExceptionDO("Exception from function Displays_list_of_drone", myExceptionDO.Dont_have_any_drone_in_the_list);

                List<Drone> drones = new List<Drone>();
                foreach (Drone drone in DataSource.drones)
                {
                    drones.Add(drone);
                }
                return drones;
            }
            public IEnumerable<Parcel> DisplaysParcelsDontHaveDrone()

            // Print the details of all the parcels don't have An associated skimmer (Selected_drone == 0).
            {
                if (DataSource.parcels.Count == 0)
                    throw new myExceptionDO("Exception from function displaysParcelsDontHaveDrone", myExceptionDO.Dont_have_any_parcel_in_the_list);

                List<Parcel> par = new();
                foreach (Parcel parcel in DataSource.parcels)
                {
                    if (parcel.Id != 0 && parcel.DroneId == 0)
                        par.Add(parcel);
                }
                return par;
            }

            public IEnumerable<Station> AvailableChargingStations()

            //Print the all stations that have DroneCharge available
            {
                if (DataSource.stations.Count == 0)
                    throw new myExceptionDO("Exception from function AvailableChargingStations", myExceptionDO.Dont_have_any_station_in_the_list);

                List<Station> stat = new();
                IEnumerator iter = DataSource.stations.GetEnumerator();
                foreach (Station station in DataSource.stations)
                {
                    if (station.ChargeSlots != 0)
                        stat.Add(station);
                }
                return stat;
            }
            public string MinimumFromCustomer(double minDistance, Point p)

            {
                if(DataSource.customers.Count == 0)
                    throw new myExceptionDO("Exception from function MinimumFromCustomer", myExceptionDO.Dont_have_any_customer_in_the_list);

                int saveTheI = 0;// save the index with minimum destance from the point p
                foreach (Customer customer in DataSource.customers)// (int i = 1; i < IDAL.DalObject.DataSource.Config.customersIndex; i++)
                {
                    double distance = customer.location.distancePointToPoint(p);
                    if (minDistance > distance)
                    {
                        saveTheI++;
                        minDistance = distance;
                    }
                }
                return ("The minimum distancefrom the point is: " + minDistance +
                   "\nThe id of customer is: " + DataSource.customers[saveTheI].Id);
            }
            public string MinimumFromStation(double minDistance, Point p){
                
                  if(DataSource.customers.Count == 0) 
                    throw new myExceptionDO("Exception from function MinimumFromStation", myExceptionDO.Dont_have_any_station_in_the_list);

            int saveTheI = 0;// save the index with minimum destance from the point p
                IEnumerator iter = DataSource.stations.GetEnumerator();
                foreach (Station station in DataSource.stations)
                {
                    double distance = station.Location.distancePointToPoint(p);
                    if (minDistance > distance)
                    {
                        saveTheI++;
                        minDistance = distance;
                    }
                }
                return ("The minimum distance from the point is: " + minDistance +
                    "\nThe id of station is: " + DataSource.stations[saveTheI].id);

            }
            public void AffiliationDroneToParcel(int parcelID, int droneID)
            {
                if (DataSource.parcels.Count == 0)
                    throw new myExceptionDO("Exception from function AffiliationDroneToParcel", myExceptionDO.Dont_have_any_parcel_in_the_list);

                Parcel parcel = new Parcel();
                for (int i = 0; i < DataSource.parcels.Count(); i++)
                {
                    if (DataSource.parcels[i].Id == parcelID)
                    {
                        parcel = DataSource.parcels[i];
                        parcel.DroneId = droneID;
                        DataSource.parcels[i] = parcel;
                        break;
                    }
                }

                throw new myExceptionDO("Exception from function AffiliationDroneToParcel", myExceptionDO.There_is_no_variable_with_this_ID);
            }
            public void PickUp(int parcelId)
            {
                if (DataSource.parcels.Count == 0)
                    throw new myExceptionDO("Exception from function pickUp", myExceptionDO.Dont_have_any_parcel_in_the_list);

                Parcel parcel = new Parcel();
                for (int i = 0; i < DataSource.parcels.Count(); i++)
                {
                    if (DataSource.parcels[i].Id == parcelId)
                    {
                        parcel = DataSource.parcels[i];
                        parcel.PickedUp = DateTime.Now;
                        DataSource.parcels[i] = parcel;
                        break;
                    }
                }

                throw new myExceptionDO("Exception from function pickUp", myExceptionDO.There_is_no_variable_with_this_ID);;
            }
            public void Delivered(int deliId)
            {
                if (DataSource.parcels.Count == 0)
                    throw new myExceptionDO("Exception from function delivered", myExceptionDO.Dont_have_any_parcel_in_the_list);

                Parcel tempParcel = new Parcel();
                for (int i = 0; i < DataSource.parcels.Count(); i++)
                {
                    if (DataSource.parcels[i].Id == deliId)
                    {
                        tempParcel = DataSource.parcels[i];
                        tempParcel.Delivered = DateTime.Now;
                        DataSource.parcels[i] = tempParcel;
                        break;
                    }
                }

                throw new myExceptionDO("Exception from function delivered", myExceptionDO.There_is_no_variable_with_this_ID);

            }
            public void SetFreeStation(int droneId)
            {
                if (DataSource.stations.Count == 0)
                    throw new myExceptionDO("Exception from function setFreeStation", myExceptionDO.Dont_have_any_station_in_the_list);
                if (DataSource.drones.Count == 0)
                    throw new myExceptionDO("Exception from function setFreeStation", myExceptionDO.Dont_have_any_drone_in_the_list);

                for (int i = 0; i < DataSource.dronesCharge.Count(); i++)
                {
                    if (DataSource.dronesCharge[i].DroneId == droneId)
                    {
                       
                        for (int j = 0; j < DataSource.stations.Count; j++)
                        { 
                            if (DataSource.stations[j].id == DataSource.dronesCharge[i].staitionId)
                            {
                                Station station = new Station();
                                station = DataSource.stations[j];
                                station.ChargeSlots++;
                                DataSource.stations[j] = station;
                            }
                            else if (j == DataSource.stations.Count() - 1)
                                throw new myExceptionDO(myExceptionDO.We_ge_to_the_end_of_list_and_dont_find_the_station);
                        }

                        DataSource.dronesCharge.RemoveAt(i); 

                        break;
                    }
                    else if (i == DataSource.drones.Count() - 1)
                           throw new myExceptionDO("Exception from function setFreeStation", myExceptionDO.We_ge_to_the_end_of_list_and_dont_find_the_drone);
                }
            }
            public void DroneToCharge(int droneId, int stationId)
            {
                if (DataSource.stations.Count == 0)
                    throw new myExceptionDO("Exception from function droneToCharge", myExceptionDO.Dont_have_any_station_in_the_list);
                if (DataSource.drones.Count == 0)
                    throw new myExceptionDO("Exception from function droneToCharge", myExceptionDO.Dont_have_any_drone_in_the_list);

                foreach (var droneCharging in DataSource.dronesCharge) // Check if The drone already is in charge  
                {
                    if(droneCharging.DroneId == droneId)
                    {
                        throw new myExceptionDO("The drone already is in charge.");
                    }
                }

                for (int i = 0; i < DataSource.stations.Count; i++) // Find if the station exists
                {
                    if(DataSource.stations[i].id == stationId)
                    {
                        DroneCharge droneCharge = new DroneCharge();
                        droneCharge.DroneId = droneId;
                        droneCharge.staitionId = stationId;
                        DataSource.dronesCharge.Add(droneCharge);

                        Station station = new Station();
                        station = DataSource.stations[i];
                        station.ChargeSlots--;
                        DataSource.stations[i] = station;

                    }
                }

            }
        }
    }


}
