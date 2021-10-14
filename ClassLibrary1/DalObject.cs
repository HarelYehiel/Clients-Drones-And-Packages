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
        public struct DalObject
        {
             public DalObject(int x = 0)
            {
                DataSource.Initialize();
            }
            public static station GetStation(int stationId)
            {
                for (int i = 0; i < 5; i++)
                {
                    if(DataSource.stations[i].Id == stationId)
                        return DataSource.stations[i];

                }
                Exception e11 = new Exception("tt");
                throw e11;
            }
           public static  Drone GetDrone(int droneId) {
                for (int i = 0; i < 5; i++)
                {
                    if (DataSource.drones[i].Id == droneId)
                        return DataSource.drones[i];

                }
                Exception e11 = new Exception("tt");
                throw e11;
            }
            public static Customer GetCustomer(int CustomerId)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (DataSource.drones[i].Id == CustomerId)
                        return DataSource.customers[i];

                }
                Exception e11 = new Exception("tt");
                throw e11;
            }
            public static Parcel GetParcel(int ParcelId)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (DataSource.drones[i].Id == ParcelId)
                        return DataSource.parcels[i];

                }
                Exception e11 = new Exception("tt");
                throw e11;
            }
            public static int addParcel()
            {
                int parcelNumber = ++DataSource.Cofing.runNumber;
                 return parcelNumber;
            }

            public static station[] Displays_list_of_base_stations()
            //Copy all the station from DataSource.stations[] to new_array_stations.
            {
                station[] new_array_stations = new station[DataSource.Cofing.stationIndex];
                for (int i = 0; i < DataSource.Cofing.stationIndex; i++)
                {
                    new_array_stations[i] = DataSource.stations[i];
                }
                return new_array_stations;
            }
            public static Customer[] Displays_list_of_custmers()
            //Copy all the customer from DataSource.customers[] to new_array_custmers.
            {
                Customer[] new_array_custmers = new Customer[DataSource.Cofing.customersIndex];
                for (int i = 0; i < DataSource.Cofing.customersIndex; i++)
                {
                    new_array_custmers[i] = DataSource.customers[i];
                }
                return new_array_custmers;
            }
            public static Parcel[] Displays_list_of_Parcels()
            //Copy all the Parcel from DataSource.parcels[] to new_array_parcels.
            {
                Parcel[] new_array_Parcels = new Parcel[DataSource.Cofing.parcelsIndex];
                for (int i = 0; i < DataSource.Cofing.parcelsIndex; i++)
                {
                    new_array_Parcels[i] = DataSource.parcels[i];
                }
                return new_array_Parcels;
            }
            public static Drone[] Displays_list_of_drone()
            //Copy all the Drone from DataSource.drones[] to new_array_drones.
            {
                Drone[] new_array_Drones = new Drone[DataSource.Cofing.droneIndex];
                for (int i = 0; i < DataSource.Cofing.droneIndex; i++)
                {
                    new_array_Drones[i] = DataSource.drones[i];
                }
                return new_array_Drones;
            }
        }
    }
}
