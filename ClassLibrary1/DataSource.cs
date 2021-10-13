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
        public struct DataSource
        {
            public static DO.Drone[] drones = new DO.Drone[10];
            public static DO.station[] stations = new DO.station[5];
            public static DO.Customer[] customers = new DO.Customer[100];
            public static DO.Parcel[] parcels = new DO.Parcel[1000];
            internal struct Cofing
            {
                public static int droneIndex = 0;
                public static int stationIndex = 0;
                public static int customersIndex = 0;
                public static int parcelsIndex = 0;

            }

            public static void Initialize()
            {

            }
        }

    }
}
