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
            public static string convertLongitudeToDegree(Point p)
            {
                int num = (int)p.Longitude;
                int min = (int)((p.Longitude - num) * 60);
                short sec = (short)((((p.Longitude - num)*60)-min) * 60);
                string sexagesimal = num + "°" + min + "'" + sec;
                return sexagesimal;
            }
            public static string convertLatitudeToDegree(Point p)
            {
                int num = (int)p.Latitude;
                int min = (int)((p.Latitude - num) * 60);
                short sec = (short)((((p.Latitude - num) * 60) - min) * 60);
                string sexagesimal = num + "°" + min + "'" + sec;
                return sexagesimal;
            }

        }
    }

}
