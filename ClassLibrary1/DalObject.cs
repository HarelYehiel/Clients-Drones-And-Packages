using System;
using System.Collections.Generic;
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
                d[0] = DataSource.Cofing.vacant;
                d[1] = DataSource.Cofing.lightWeight;
                d[2] = DataSource.Cofing.mediumWeight;
                d[3] = DataSource.Cofing.heavyWeight;
                d[4] = DataSource.Cofing.droneLoadingRate;
                
                return d;


            }
            public DalObject(int x = 0)
            {
                DataSource.Initialize();
            }
            public int addParcel()
            {
                int parcelNumber = ++DataSource.Cofing.runNumber;
                return parcelNumber;
            }
            public  ref IDAL.DO.station GetStation(int stationId)
            // Return the station with stationId
            {
                for (int i = 0; i < DataSource.Cofing.stationIndex; i++)
                {
                    if (IDAL.DalObject.DataSource.stations[i].Id == stationId)
                        return ref IDAL.DalObject.DataSource.stations[i];

                }
                Exception e11 = new Exception("tt");
                throw e11;
            }
            public  ref IDAL.DO.Drone GetDrone(int droneId)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (IDAL.DalObject.DataSource.drones[i].Id == droneId)
                        return ref IDAL.DalObject.DataSource.drones[i];

                }
                Exception e11 = new Exception("tt");
                throw e11;
            }
        
            public  ref IDAL.DO.Customer GetCustomer(int CustomerId)
            {
                for (int i = 0; i < DataSource.Cofing.customersIndex; i++)
                {
                    if (IDAL.DalObject.DataSource.customers[i].Id == CustomerId)
                        return ref IDAL.DalObject.DataSource.customers[i]; 
                }
                Exception e11 = new Exception("tt");
                throw e11;
            }
            public  ref IDAL.DO.Parcel GetParcel(int ParcelId)
            // Return the parcel with parcelId
            {
                for (int i = 0; i < DataSource.Cofing.parcelsIndex; i++)
                {
                    if (DataSource.parcels[i].Id == ParcelId)
                        return ref IDAL.DalObject.DataSource.parcels[i];

                }
                Exception e11 = new Exception("tt");
                throw e11;
            }
            public  void inputTheStationToArray(station station)
            {
                IDAL.DalObject.DataSource.stations[IDAL.DalObject.DataSource.Cofing.stationIndex] = station;
                IDAL.DalObject.DataSource.Cofing.stationIndex++;//update the num of free cell in the array
            }
            public  void inputTheParcelToArray(Parcel par)
            {
                IDAL.DalObject.DataSource.parcels[IDAL.DalObject.DataSource.Cofing.parcelsIndex] = par;
                DataSource.Cofing.parcelsIndex++;//update the num of free cell in the array
                par.runNumber = IDAL.DalObject.DalObject.addParcel();//update the run-number serial
            }
            public  void inputTheCustomerToArray(Customer cust)
            {
                IDAL.DalObject.DataSource.customers[DataSource.Cofing.customersIndex] = cust;
                DataSource.Cofing.customersIndex++;//update the num of free cell in the array

            }
            public  void inputTheDroneToArray(Drone dro)
            {
                IDAL.DalObject.DataSource.drones[DataSource.Cofing.droneIndex] = dro;
                DataSource.Cofing.droneIndex++;//update the num of free cell in the array
            }
            public  IDAL.DO.station[] Displays_list_of_stations()
            //Copy all the station from DataSource.stations[] to new_array_stations.
            {
                IDAL.DO.station[] new_array_stations = new IDAL.DO.station[IDAL.DalObject.DataSource.Cofing.stationIndex];
                for (int i = 0; i < IDAL.DalObject.DataSource.Cofing.stationIndex; i++)
                {
                    new_array_stations[i] = IDAL.DalObject.DataSource.stations[i];
                }
                return new_array_stations;
            }
            public  IDAL.DO.Customer[] Displays_list_of_custmers()
            //Copy all the customer from DataSource.customers[] to new_array_custmers.
            {
                IDAL.DO.Customer[] new_array_custmers = new IDAL.DO.Customer[IDAL.DalObject.DataSource.Cofing.customersIndex];
                for (int i = 0; i < IDAL.DalObject.DataSource.Cofing.customersIndex; i++)
                {
                    new_array_custmers[i] = IDAL.DalObject.DataSource.customers[i];
                }
                return new_array_custmers;
            }
            public  IDAL.DO.Parcel[] Displays_list_of_Parcels()
            //Copy all the Parcel from DataSource.parcels[] to new_array_parcels.
            {
                IDAL.DO.Parcel[] new_array_Parcels = new IDAL.DO.Parcel[IDAL.DalObject.DataSource.Cofing.parcelsIndex];
                for (int i = 0; i < IDAL.DalObject.DataSource.Cofing.parcelsIndex; i++)
                {
                    new_array_Parcels[i] = IDAL.DalObject.DataSource.parcels[i];
                }
                return new_array_Parcels;
            }
            public  IDAL.DO.Drone[] Displays_list_of_drone()
            //Copy all the Drone from DataSource.drones[] to new_array_drones.
            {
                IDAL.DO.Drone[] new_array_Drones = new IDAL.DO.Drone[IDAL.DalObject.DataSource.Cofing.droneIndex];
                for (int i = 0; i < IDAL.DalObject.DataSource.Cofing.droneIndex; i++)
                {
                    new_array_Drones[i] = IDAL.DalObject.DataSource.drones[i];
                }
                return new_array_Drones;
            }
            public  void displaysParcelsDontHaveDrone()
            // Print the details of all the parcels don't have An associated skimmer (Selected_drone == 0).
            {
                for (int i = 0; i < IDAL.DalObject.DataSource.parcels.Length; i++)
                {
                    if (IDAL.DalObject.DataSource.parcels[i].Id != 0 && IDAL.DalObject.DataSource.parcels[i].DroneId == 0)
                        Console.WriteLine(IDAL.DalObject.DataSource.parcels[i].ToString()
                            );
                }
            }
            public  void AvailableChargingStations()
            //Print the all stations that have DroneCharge available
            {
                for (int i = 0; i < IDAL.DalObject.DataSource.Cofing.stationIndex; i++)
                {
                    if (IDAL.DalObject.DataSource.stations[i].ChargeSlots != 0)
                        Console.WriteLine(IDAL.DalObject.DataSource.stations[i].ToString());
                }
            }
            public  void MinmumFromCustomer(double minDistance,Point p)
            {
                int saveTheI = 0;// save the index with minimum destance from the point p
                for (int i = 1; i < IDAL.DalObject.DataSource.Cofing.customersIndex; i++)
                {
                    double distance = IDAL.DalObject.DataSource.customers[i].Location.distancePointToPoint(p);
                    if (minDistance > distance)
                    {
                        saveTheI = i;
                        minDistance = distance;
                    }
                }
                Console.WriteLine("The minimum distancefrom the point is: {0}" +
                    "\nThe id of customer is: {1}", minDistance, DataSource.customers[saveTheI].Id);
            }
            public  void MinimumFromStation(double minDistance,Point p)
            {
                int saveTheI = 0;// save the index with minimum destance from the point p

                for (int i = 0; i < IDAL.DalObject.DataSource.Cofing.stationIndex; i++)
                {
                    double distance = IDAL.DalObject.DataSource.stations[i].Location.distancePointToPoint(p);
                    if (minDistance > distance)
                    {
                        saveTheI = i;
                        minDistance = distance;
                    }
                }
                Console.WriteLine("The minimum distance from the point is: {0}" +
                    "\nThe id of station is: {1}", minDistance, IDAL.DalObject.DataSource.stations[saveTheI].Id);

            }
        }
    }
}
