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
        public class DalObject
        {
            public DalObject(int x = 0)
            {
                DataSource.Initialize();
            }
            public static void addParcel(Parcel par)
            {
                par.runNumber++;
            }

            public static  IDAL.DO.station GetStation(int stationId)
            // Return the station with stationId
            {
                for (int i = 0; i < DataSource.stations.Count(); i++)
                {
                    if (IDAL.DalObject.DataSource.stations[i].Id == stationId)
                        return  DataSource.stations[i];

                }
                Exception e11 = new Exception("tt");
                throw e11;
            }
            public static  IDAL.DO.Drone GetDrone(int droneId)
            {
                for (int i = 0; i < DataSource.drones.Count(); i++)
                {
                    if (IDAL.DalObject.DataSource.drones[i].Id == droneId)
                        return DataSource.drones[i];

                }
                Exception e11 = new Exception("tt");
                throw e11;
            }
        
            public static IDAL.DO.Customer GetCustomer(int CustomerId)
            {
                for (int i = 0; i < DataSource.customers.Count(); i++)
                {
                    if (IDAL.DalObject.DataSource.customers[i].Id == CustomerId)
                        return DataSource.customers[i]; 
                }
                Exception e11 = new Exception("tt");
                throw e11;
            }
            public static IDAL.DO.Parcel GetParcel(int ParcelId)
            // Return the parcel with parcelId
            {
                for (int i = 0; i < DataSource.parcels.Count(); i++)
                {
                    if (DataSource.parcels[i].Id == ParcelId)
                        return DataSource.parcels[i];

                }
                Exception e11 = new Exception("tt");
                throw e11;
            }
            public static void inputTheStationToArray(station station)
            {
                DataSource.stations.Add(station);
            }
            public static void inputTheParcelToArray(Parcel par)
            {
                DataSource.parcels.Add(par);
                addParcel(par);//update the run-number serial
            }
            public static void inputTheCustomerToArray(Customer cust)
            {
                DataSource.customers.Add(cust);
            }
            public static void inputTheDroneToArray(Drone dro)
            {
                DataSource.drones.Add(dro);
            }
            public static List<station> Displays_list_of_stations()
            //return all the station from DataSource.stations
            {
                return DataSource.stations;
            }
            public static List<Customer> Displays_list_of_custmers()
            //return all the customer from DataSource.customers
            {
                return DataSource.customers;
            }
            public static List<Parcel> Displays_list_of_Parcels()
            //print all the Parcel from DataSource.parcels
            {
                return DataSource.parcels;
            }
            public static List<Drone> Displays_list_of_drone()
            //print all the Drone from DataSource.drones
            {
                return DataSource.drones;
            }
            public static List<Parcel> displaysParcelsDontHaveDrone()
            // Print the details of all the parcels don't have An associated skimmer (Selected_drone == 0).
            {
                List<Parcel> par = new(); 
                foreach(Parcel parcel in DataSource.parcels)
                {
                    if (parcel.Id != 0 && parcel.DroneId == 0)
                        par.Add(parcel);
                }
                return par;
            }
            public static List<station> AvailableChargingStations()
            //Print the all stations that have DroneCharge available
            {
                List<station> stat = new();
                IEnumerator iter = DataSource.stations.GetEnumerator();
                foreach (station station in DataSource.stations)                {
                    if (station.ChargeSlots != 0)
                        stat.Add(station);
                }
                return stat;
            }
            public static string MinmumFromCustomer(double minDistance,Point p)
            {
                int saveTheI = 0;// save the index with minimum destance from the point p
                IEnumerator iter = DataSource.customers.GetEnumerator();
                foreach (Customer customer in DataSource.customers)// (int i = 1; i < IDAL.DalObject.DataSource.Cofing.customersIndex; i++)
                {
                    double distance = customer.Location.distancePointToPoint(p);
                    if (minDistance > distance)
                    {
                        saveTheI++;
                        minDistance = distance;
                    }
                }
                return ("The minimum distancefrom the point is: " + minDistance +
                   "\nThe id of customer is: " + DataSource.customers[saveTheI].Id);
            }
            public static string MinimumFromStation(double minDistance,Point p)
            {
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
                    "\nThe id of station is: " + DataSource.stations[saveTheI].Id);

            }
            public static void AffiliationDroneToParcel(int parcelID, int droneID)
            {

                IDAL.DO.Parcel parcel = new IDAL.DO.Parcel();
                for (int i = 0; i < DataSource.parcels.Count(); i++)
                {
                    if(DataSource.parcels[i].Id == parcelID)
                    {
                        parcel = DataSource.parcels[i];
                        parcel.DroneId = droneID;
                        DataSource.parcels[i] = parcel;
                    }
                }
            }
            public static void pickUp(int PickId)
            {
                IDAL.DO.Parcel parcel = new IDAL.DO.Parcel();
                for(int i = 0; i < DataSource.parcels.Count(); i++)
                {
                    if (DataSource.parcels[i].Id == PickId)
                    {
                        parcel = DataSource.parcels[i];
                        parcel.PickedUp = DateTime.Now;
                        DataSource.parcels[i] = parcel;
                    }


                }  
            }
            public static void delivered(int deliId)
            {

                IDAL.DO.Parcel parcel = new IDAL.DO.Parcel();
                for(int i =0;i< DataSource.parcels.Count(); i++)
                {
                    if (DataSource.parcels[i].Id == deliId)
                    {
                        parcel = DataSource.parcels[i];
                        parcel.Delivered = DateTime.Now;
                        DataSource.parcels[i] = parcel;
                    }
                }          
            }
            public static void setFreeStation(int droneId)
            {
                IDAL.DO.Drone drone = new IDAL.DO.Drone();
                for(int i = 0; i < DataSource.drones.Count(); i++)
                {
                    if (DataSource.drones[i].Id == droneId)
                    {
                        drone = DataSource.drones[i];
                        station station = new station();
                        station.Id = drone.stationOfCharge.staitionId;
                        for( int j=0; j<DataSource.stations.Count(); j++)
                        {
                            if(DataSource.stations[j].Id == station.Id)
                            {
                                station = DataSource.stations[i];
                                station.ChargeSlots++;
                                DataSource.stations[i] = station;
                            }
                            /////////////////לטפל בחריגה שמשחררים יותררחפנים מה שיש
                        }
                    }
                }
            }
            public static void droneToCharge(int droneId, int stationId)
            {
                Drone drone = new Drone();
                for(int i =0; i < DataSource.drones.Count(); i++)
                {
                    if(DataSource.drones[i].Id == droneId)
                    {
                        drone = DataSource.drones[i];
                        IDAL.DO.DroneCharge charge = new IDAL.DO.DroneCharge();//crate new object type DroneChrge
                        charge.DroneId = droneId;
                        charge.staitionId = stationId;
                        drone.stationOfCharge = charge;//after update the data of DroneCharge update the informaition in the dron
                        for(int j =0; j < DataSource.stations.Count(); j++)
                        {
                            if(DataSource.stations[j].Id == stationId)
                            {
                                station station = new station();
                                station = DataSource.stations[j];
                                station.ChargeSlots--;
                                DataSource.stations[j] = station;
                            }
                        }
                        DataSource.drones[i] = drone;
                    }
                }
                
                
         
            }
        }
    }
        
    
}
