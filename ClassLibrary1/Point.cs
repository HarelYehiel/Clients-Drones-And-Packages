using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct Point
        {
            public double Longitude;
            public double Latitude;

            public double distancePointToPoint(Point p)
            {
               return Math.Sqrt((Math.Pow(p.Latitude - Latitude , 2) + Math.Pow(p.Longitude - Longitude, 2)));
            }

        }
    }

}
