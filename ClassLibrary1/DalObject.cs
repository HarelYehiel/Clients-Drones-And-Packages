using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace IDAL
{
    namespace DalObject
    {

        public struct DalObject : IDal
        {
            public double[] powerConsumptionBySkimmer()
            {
                double[] d = new double[5];
                d[0] = DataSource.Config.vacant;
                d[1] = DataSource.Config.lightWeight;
                d[2] = DataSource.Config.mediumWeight;
                d[3] = DataSource.Config.heavyWeight;
                d[4] = DataSource.Config.droneLoadingRate;

                return d;


            }
            public DalObject(int x = 0)
            {
                DataSource.Initialize();
            }
            public void addParcel(Parcel par)

            {
                par.runNumber++;
            }

            public IDAL.DO.station GetStation(int stationId)
            // Return the station with stationId
            {
               // IEnumerable<station> stations  = DataSource.stations;
               foreach(station station in DataSource.stations)
                {
                    if (station.id == stationId)
                        return station;
                }
                throw new myException_DO("Exception from function GetStation", myException_DO.There_is_no_variable_with_this_ID);
            }
            public IDAL.DO.Drone GetDrone(int droneId)

            {
                foreach(Drone drone in DataSource.drones)
                {
                    if (drone.Id == droneId)
                        return drone;
                }
                throw new myException_DO("Exception from function GetDrone", myException_DO.There_is_no_variable_with_this_ID);
            }

            public IDAL.DO.Customer GetCustomer(int CustomerId)

            {
                foreach (Customer customer in DataSource.customers)
                {
                    if (customer.Id == CustomerId)
                        return customer;
                }
                throw new myException_DO("Exception from function GetCustomer", myException_DO.There_is_no_variable_with_this_ID);

            }

            public IDAL.DO.Parcel GetParcel(int ParcelId)

            // Return the parcel with parcelId
            {
                foreach (Parcel parcel in DataSource.parcels)
                {
                    if (parcel.Id == ParcelId)
                        return parcel;

                }
                throw new myException_DO("Exception from function GetParcel", myException_DO.There_is_no_variable_with_this_ID);
            }
            public void inputTheStationToArray(station station)
            {
                List<station> new_stations = new List<station>();
                foreach (station station1 in DataSource.stations)
                {
                    new_stations.Add(station1);
                }
                DataSource.stations = new_stations;
            }
            public void inputTheParcelToArray(Parcel par)
            {
                List<Parcel> parcels = new List<Parcel>();
                foreach (Parcel parcel in DataSource.parcels)
                {
                    parcels.Add(parcel);
                }
                DataSource.parcels = parcels;
                addParcel(par);//update the run-number serial
            }
            public void inputTheCustomerToArray(Customer cust)
            {
                List<Customer> customers = new List<Customer>();
                foreach (Customer customer in DataSource.customers)
                {
                    customers.Add(customer);
                }
                DataSource.customers = customers;
            }
            public void inputTheDroneToArray(Drone dro)
            {
                List<Drone> drones = new List<Drone>();
                foreach (Drone drone in DataSource.drones)
                {
                    drones.Add(drone);
                }
                DataSource.drones = drones;
            }

            public IEnumerable<station> Displays_list_of_stations()
            //return all the station from DataSource.stations

            {
                if (DataSource.parcels.Count == 0)
                    throw new myException_DO("Exception from function Displays_list_of_stations", myException_DO.Dont_have_any_station_in_the_list);

                List<station> stations = new List<station>();
                foreach (station station in DataSource.stations)
                    stations.Add(station);
                return stations;
            }

            public IEnumerable<Customer> Displays_list_of_custmers()
            //return all the customer from DataSource.customers
            {
                if (DataSource.parcels.Count == 0)
                    throw new myException_DO("Exception from function Displays_list_of_custmers", myException_DO.Dont_have_any_customer_in_the_list);

                List<Customer> customers = new List<Customer>();
                foreach (Customer customer in DataSource.customers)
                    customers.Add(customer);
                return customers;
            }
            public IEnumerable<Parcel> Displays_list_of_Parcels()
            //print all the Parcel from DataSource.parcels

            {
                if (DataSource.parcels.Count == 0)
                    throw new myException_DO("Exception from function Displays_list_of_Parcels", myException_DO.Dont_have_any_parcel_in_the_list);

                List<Parcel> parcels = new List<Parcel>();
                foreach (Parcel parcel in DataSource.parcels)
                {
                    parcels.Add(parcel);
                }
                return parcels;
            }
            public IEnumerable<Drone> Displays_list_of_drone()
            //print all the Drone from DataSource.drones
            {
                if (DataSource.parcels.Count == 0)
                    throw new myException_DO("Exception from function Displays_list_of_drone", myException_DO.Dont_have_any_drone_in_the_list);

                List<Drone> drones = new List<Drone>();
                foreach (Drone drone in DataSource.drones)
                {
                    drones.Add(drone);
                }
                return drones;
            }
            public IEnumerable<Parcel> displaysParcelsDontHaveDrone()

            // Print the details of all the parcels don't have An associated skimmer (Selected_drone == 0).
            {
                if (DataSource.parcels.Count == 0)
                    throw new myException_DO("Exception from function displaysParcelsDontHaveDrone", myException_DO.Dont_have_any_parcel_in_the_list);

                List<Parcel> par = new();
                foreach (Parcel parcel in DataSource.parcels)
                {
                    if (parcel.Id != 0 && parcel.DroneId == 0)
                        par.Add(parcel);
                }
                return par;
            }

            public IEnumerable<station> AvailableChargingStations()

            //Print the all stations that have DroneCharge available
            {
                if (DataSource.stations.Count == 0)
                    throw new myException_DO("Exception from function AvailableChargingStations", myException_DO.Dont_have_any_station_in_the_list);

                List<station> stat = new();
                IEnumerator iter = DataSource.stations.GetEnumerator();
                foreach (station station in DataSource.stations)
                {
                    if (station.ChargeSlots != 0)
                        stat.Add(station);
                }
                return stat;
            }
            public string MinimumFromCustomer(double minDistance, Point p)

            {
                if(DataSource.customers.Count == 0)
                    throw new myException_DO("Exception from function MinimumFromCustomer", myException_DO.Dont_have_any_customer_in_the_list);

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
                    throw new myException_DO("Exception from function MinimumFromStation", myException_DO.Dont_have_any_station_in_the_list);

            int saveTheI = 0;// save the index with minimum destance from the point p
                IEnumerator iter = DataSource.stations.GetEnumerator();
                foreach (station station in DataSource.stations)
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
                    throw new myException_DO("Exception from function AffiliationDroneToParcel", myException_DO.Dont_have_any_parcel_in_the_list);

                IDAL.DO.Parcel parcel = new IDAL.DO.Parcel();
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

                throw new myException_DO("Exception from function AffiliationDroneToParcel", myException_DO.There_is_no_variable_with_this_ID);
            }
            public void pickUp(int parcelId)
            {
                if (DataSource.parcels.Count == 0)
                    throw new myException_DO("Exception from function pickUp", myException_DO.Dont_have_any_parcel_in_the_list);

                IDAL.DO.Parcel parcel = new IDAL.DO.Parcel();
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

                throw new myException_DO("Exception from function pickUp", myException_DO.There_is_no_variable_with_this_ID);;
            }
            public void delivered(int deliId)
            {
                if (DataSource.parcels.Count == 0)
                    throw new myException_DO("Exception from function delivered", myException_DO.Dont_have_any_parcel_in_the_list);

                IDAL.DO.Parcel tempParcel = new IDAL.DO.Parcel();
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

                throw new myException_DO("Exception from function delivered", myException_DO.There_is_no_variable_with_this_ID);

            }
            public void setFreeStation(int droneId)
            {
                if (DataSource.stations.Count == 0)
                    throw new myException_DO("Exception from function setFreeStation", myException_DO.Dont_have_any_station_in_the_list);
                if (DataSource.drones.Count == 0)
                    throw new myException_DO("Exception from function setFreeStation", myException_DO.Dont_have_any_drone_in_the_list);

                IDAL.DO.Drone drone = new IDAL.DO.Drone();
                for (int i = 0; i < DataSource.drones.Count(); i++)
                {
                    if (DataSource.drones[i].Id == droneId)
                    {
                        drone = DataSource.drones[i];
                        station station = new station();
                        station.id = drone.stationOfCharge.staitionId;
                        for (int j = 0; j < DataSource.stations.Count(); j++)
                        {
                            if (DataSource.stations[j].id == station.id)
                            {
                                station = DataSource.stations[i];
                                station.ChargeSlots++;
                                DataSource.stations[i] = station;
                                break;
                            }

                            //We get to the end of list and don't find the station
                            else if (j == DataSource.stations.Count())
                                throw new myException_DO("Exception from function setFreeStation", myException_DO.Dont_have_any_station_in_the_list);

                            /////////////////לטפל בחריגה שמשחררים יותררחפנים מה שיש
                        }

                        break;
                    }
                    else if (i == DataSource.drones.Count())
                                throw new myException_DO("Exception from function setFreeStation", myException_DO.Dont_have_any_drone_in_the_list);

                }
            }
            public void droneToCharge(int droneId, int stationId)
            {
                if (DataSource.stations.Count == 0)
                    throw new myException_DO("Exception from function droneToCharge", myException_DO.Dont_have_any_station_in_the_list);
                if (DataSource.drones.Count == 0)
                    throw new myException_DO("Exception from function droneToCharge", myException_DO.Dont_have_any_drone_in_the_list);

                Drone drone = new Drone();
                for (int i = 0; i < DataSource.drones.Count(); i++)
                {
                    if (DataSource.drones[i].Id == droneId)
                    {
                        drone = DataSource.drones[i];
                        IDAL.DO.DroneCharge charge = new IDAL.DO.DroneCharge();//crate new object type DroneChrge
                        charge.DroneId = droneId;
                        charge.staitionId = stationId;
                        drone.stationOfCharge = charge;//after update the data of DroneCharge update the informaition in the dron
                        for (int j = 0; j < DataSource.stations.Count(); j++)
                        {
                            if (DataSource.stations[j].id == stationId)
                            {
                                station station = new station();
                                station = DataSource.stations[j];
                                station.ChargeSlots--;
                                DataSource.stations[j] = station;
                                break;
                            }
                            else if (j == DataSource.stations.Count())
                                throw new myException_DO("Exception from function droneToCharge", myException_DO.Dont_have_any_station_in_the_list);
                        }
                        DataSource.drones[i] = drone;
                        break;
                    }
                    else if (i == DataSource.drones.Count())
                                throw new myException_DO("Exception from function droneToCharge", myException_DO.Dont_have_any_drone_in_the_list);
                }



            }
        }
    }


}
