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
        public class DataSource
        {
            private DO.Drone[] droens { get; set; } = new DO.Drone[10];
            public DO.station[] stations { get; set; } = new DO.station[5];
            public DO.Customer[] customers { get; set; } = new DO.Customer[100];
            public DO.Parcel[] parcels { get; set; } = new DO.Parcel[1000];

           

            public static void Initialize()
            {
            
            }
        }
        public struct Cofing
            {
            
        }
    }
}
