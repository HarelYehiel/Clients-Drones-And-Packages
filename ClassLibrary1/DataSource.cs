﻿using System;
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
            public static IEnumerable<Drone> drones = new List<Drone>();

            public static IEnumerable<station> stations = new List<station>();

            public static IEnumerable<Customer> customers = new List<Customer>();
            public static IEnumerable<Parcel> parcels = new List<Parcel>();
            //public interface IEnumerable { IEnumerator<DataSource> GetEnumerator(); }

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
                for (int i = 0; i < 5; i++)//crate 5 drones withe random data
                {
                    Drone drone = new Drone();
                    var rand = new Random();
                    drone.Id = rand.Next(10000, 99999);
                    drone.MaxWeight = (IDAL.DO.Enum.WeightCategories)rand.Next(0, 2);
                    drones.Add(drone);
                }
                for (int i = 0; i < 2; i++)//crate 2 staitons with random data
                {
                    station station = new station();
                    var rand = new Random();
                    station.Id = rand.Next(10000, 99999);
                    station.name = "sta" + rand.Next(1, 99);
                    Point p = new Point();
                    p.Latitude = 31 + rand.Next(0, 1);
                    p.Longitude = 34 + rand.Next(0, 1);
                    station.Location = p;
                    stations.Add(station);
                }
                for (int i = 0; i < 10; i++)//crate new 10 random coustomers
                {
                    Customer customer = new Customer();
                    var rand = new Random();
                    customer.Id = rand.Next(11111, 99999);
                    customer.Name = "cust" + rand.Next(1, 99);
                    customer.Phone = "05" + rand.Next(10000000, 99999999);
                    Point p = new Point();
                    p.Latitude = 31 + rand.Next(0, 1);
                    p.Longitude = 34 + rand.Next(0, 1);
                    customer.location = p;
                    IEnumerable<Customer> list = customers.ToList<Customer>();
                    IEnumerable<Customer> iter = list.GetEnumerator();
                    list.(customer);
                }
                for (int i = 0; i < 10; i++)//crate new 10  parcels with random data
                {
                    Parcel parcel = new Parcel();
                    var rand = new Random();
                    parcel.Id = rand.Next(11111, 99999);
                    parcel.SenderId = rand.Next(11111, 99999);
                    parcel.TargetId = rand.Next(11111, 99999);
                    parcel.Weight = (DO.Enum.WeightCategories)rand.Next(0, 2);
                    parcel.Priority = (DO.Enum.Priorities)rand.Next(0, 2);
                    DateTime start = new DateTime(2021, rand.Next(1, 12), rand.Next(1, 31));//crate random time and colculate all the next properties
                    parcel.Requested = start.AddMinutes(rand.Next(1, 240));
                    parcels.Add(parcel);
                }
            }

        }

    }
}
