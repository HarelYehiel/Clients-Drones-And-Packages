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
            public static station GetStation(int stationId)
            {
                for (int i = 0; i < 5; i++)
                {
                    if(DataSource.stations[i].Id == stationId)
                        return DataSource.stations[stationId];

                }
                throw "Don't have station with this id station";
                try
                {

                }
                catch (Exception)
                {

                    throw;
                }
            }
           public static  Drone GetDrone(int id) {return DataSource.droens[id];}
            public static Customer GetCustomer(int id) { return DataSource.customers[id]; }
            public static Parcel GetParcel(int id) { return DataSource.parcels[id]; }
        }
    }
}
