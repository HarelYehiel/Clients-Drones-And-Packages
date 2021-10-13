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
                ++DataSource.Cofing.parcelsIndex;
                Random ra = new Random();
                int parcelId = ra.Next(100, 200);
                return parcelId;
            }
        }
    }
}
