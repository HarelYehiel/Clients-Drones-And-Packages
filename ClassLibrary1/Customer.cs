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
            public double longitude { get; set; }
            public double latitude { get; set; }
            public override string ToString()
            {
                return $"Customer ID: {Id}, Name: {Name}, Phone: {Phone}, longitude: {longitude}, latitude: {latitude}";
            }

        }
    }
}
