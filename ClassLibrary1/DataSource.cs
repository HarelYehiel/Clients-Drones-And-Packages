using DO;
using System;
using System.Collections.Generic;

namespace DalApi
{
    namespace DalObject
    {
        public struct DataSource
        {
            internal static List<Drone> drones = new List<Drone>();
            internal static List<DroneCharge> dronesCharge = new List<DroneCharge>();
            internal static List<Station> stations = new List<Station>();

            internal static List<Customer> customers = new List<Customer>();
            internal static List<Parcel> parcels = new List<Parcel>();
            //public interface IEnumerable { IEnumerator<DataSource> GetEnumerator(); }
            static DalObject dalO { get; set; }
            internal struct Config
            {

                public static double vacant { get; set; }
                public static double lightWeight { get; set; }
                public static double mediumWeight { get; set; }
                public static double heavyWeight { get; set; }
                public static double droneLoadingRate { get; set; }
            }

            public static void Initialize()
            {
                Config.vacant = 3;
                Config.lightWeight = 10;
                Config.mediumWeight = 8;
                Config.heavyWeight = 5;
                Config.droneLoadingRate = (double) 2;//all 1 second is charge the drone at 2%
                for (int i = 0; i < 30; i++)//create 5 drones with random data
                {
                    Drone drone = new Drone();
                    var rand = new Random();
                    drone.Id = rand.Next(10000, 99999);
                    drone.Model = "dro" + rand.Next(1, 10);
                    drone.MaxWeight = (DO.Enum.WeightCategories)rand.Next(0, 2);
                    drone.droneStatus = DO.Enum.DroneStatus.Avilble;
                    drones.Add(drone);

                }
                for (int i = 0; i < 15; i++)//create 15 staitons with random data
                {
                    Station station = new Station();
                    var rand = new Random();
                    station.id = rand.Next(10000, 99999);
                    station.name = "sta" + rand.Next(1, 10);
                    Point p = new Point();
                    p.latitude = 31 + rand.NextDouble();
                    p.longitude = 34 + rand.NextDouble();
                    station.Location = p;
                    station.ChargeSlots = rand.Next(5, 10);
                    stations.Add(station);
                }
                for (int i = 0; i < 10; i++)//create new 10 random coustomers
                {
                    Customer customer = new Customer();
                    var rand = new Random();
                    customer.Id = rand.Next(11111, 99999);
                    customer.name = "cust" + rand.Next(1, 99);
                    customer.phone = "05" + rand.Next(10000000, 99999999);
                    Point p = new Point();
                    p.latitude = 31 + rand.NextDouble();
                    p.longitude = 34 + rand.NextDouble();
                    customer.location = p;
                    customers.Add(customer);
                }
                for (int i = 0; i < 100; i++)//create new 30 parcels with random data
                {
                    Parcel parcel = new Parcel();
                    var rand = new Random();
                    parcel.Id = rand.Next(11111, 99999);
                    parcel.SenderId = customers[rand.Next(0, customers.Count)].Id;
                    do
                    {
                        parcel.TargetId = customers[rand.Next(0, customers.Count)].Id;

                        // Ckeck that sender don't send to himself parcel.
                        if (parcel.SenderId != parcel.TargetId) break;

                    } while (true);


                    parcel.weight = (DO.Enum.WeightCategories)rand.Next(0, 2);
                    parcel.priority = (DO.Enum.Priorities)rand.Next(0, 2);
                    DateTime start = new DateTime(2021, rand.Next(1, 12), rand.Next(1, 28));//crate random time and colculate all the next properties
                    parcel.Requested = start.AddMinutes(rand.Next(1, 240));
                    parcels.Add(parcel);

                }
            }

        }

    }
}
