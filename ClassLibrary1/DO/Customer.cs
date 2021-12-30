using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace DO
    {
        public struct Customer
        {
            public int Id { get; set; }
            public string name { get; set; }
            public string phone { get; set; }
            public Point location { get; set; }
            public override string ToString()
            {
                return $"Customer ID: {Id}, Name: {name}, Phone: {phone}, Longitude: {location.longitude}, Latitude: {location.latitude}";
            }

        }
    }

