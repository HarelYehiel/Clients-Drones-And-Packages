using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public Point Location { get; set; }
            public override string ToString()
            {
                return $"Customer ID: {Id}, Name: {Name}, Phone: {Phone}, Longitude: {Location.Longitude}, Latitude: {Location.Latitude}";
            }

        }
    }
}
