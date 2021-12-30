using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace DO
    {
        public struct Point
        {
            public double longitude;
            public double latitude;

            public double distancePointToPoint(Point p)
            {
               return 1000 * Math.Sqrt((Math.Pow(p.latitude - latitude , 2) + Math.Pow(p.longitude - longitude, 2)));
            }
            public static string convertLongitudeToDegree(Point p)
            {
                int num = (int)p.longitude;
                int min = (int)((p.longitude - num) * 60);
                short sec = (short)((((p.longitude - num)*60)-min) * 60);
                string sexagesimal = num + "°" + min + "'" + sec;
                return sexagesimal;
            }
            public static string convertLatitudeToDegree(Point p)
            {
                int num = (int)p.latitude;
                int min = (int)((p.latitude - num) * 60);
                short sec = (short)((((p.latitude - num) * 60) - min) * 60);
                string sexagesimal = num + "°" + min + "'" + sec;
                return sexagesimal;
            }

        }
    }

