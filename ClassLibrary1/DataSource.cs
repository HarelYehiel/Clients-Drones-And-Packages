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
                for(int i = 0; i < 5; i++)//crate 5 drones withe random data
                {
                    drones[i] = new Drone();
                    var rand = new Random();
                    drones[i].Id = rand.Next(10000, 99999);
                    drones[i].Battery = rand.Next(1, 100);
                    drones[i].MaxWeight = (WeightCategories)rand.Next(0, 2);
                    drones[i].Status = (DroneStatus)rand.Next(0,2);
                    Cofing.droneIndex++;//elert the index of the free cells at drones arry
                }
                for (int i = 0; i < 2; i++)//crate 2 staitons with random data
                {
                    stations[i] = new station();
                    var rand = new Random();
                    stations[i].Id = rand.Next(10000,99999);
                    stations[i].name = "sta" + rand.Next(1, 99);
                    stations[i].latitude = 31 + rand.Next(0, 1);
                    stations[i].longitude = 34 + rand.Next(0, 1);
                    Cofing.stationIndex++; //elert the index of the free cells at staions arrry
                }
                for(int i = 0; i < 10; i++)//crate new 10 random coustomers
                {
                    customers[i] = new Customer();
                    var rand = new Random();
                    customers[i].Id = rand.Next(11111, 99999);
                    customers[i].Name = "cust" + rand.Next(1, 99);
                    customers[i].Phone = "05" + rand.Next(10000000, 99999999);
                    customers[i].latitude = 31 + rand.Next(0, 1);
                    customers[i].longitude = 34 + rand.Next(0, 1);
                    Cofing.customersIndex++;//elert the index of the free cell at customers array
                }
                for(int i=0;i<10; i++)//crate new 10  parcels with random data
                {
                    parcels[i] = new Parcel();
                    var rand = new Random();
                    parcels[i].Id = rand.Next(11111, 99999);
                    parcels[i].SenderId = rand.Next(11111, 99999);
                    parcels[i].TargetId = rand.Next(11111, 99999);
                    parcels[i].DroneId = rand.Next(11111, 99999);
                    parcels[i].Weight = (WeightCategories)rand.Next(0, 2);
                    parcels[i].Priority = (Parcel.Priorities)rand.Next(0, 2);
                    DateTime start = new DateTime(2021, rand.Next(1, 12), rand.Next(1, 31));//crate random time and colculate all the next properties
                    parcels[i].Requested = start.AddMinutes(rand.Next(1, 240));
                    parcels[i].Scheduled = parcels[i].Requested.AddMinutes(rand.Next(20, 60));
                    parcels[i].PickedUp = parcels[i].Requested.AddMinutes(rand.Next(5, 20));
                    parcels[i].Delivered = parcels[i].Requested.AddMinutes(rand.Next(30, 90));
                    Cofing.parcelsIndex++;//elert the index of the free cell at parcels array
                }
            }

        }

    }
}
