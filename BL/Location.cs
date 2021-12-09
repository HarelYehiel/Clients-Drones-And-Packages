using System;


    namespace BO
    {
        public class Location
        {
            public double distancePointToPoint(Location p)
            {
                //the distance is view by centimeter
                return Math.Sqrt((Math.Pow(p.latitude - latitude, 2) + Math.Pow(p.longitude - longitude, 2)))*10000;
            }
            public double longitude { get; set; }
            public double latitude { get; set; }

            public override string ToString()
            {
                return $"longitude: {longitude}, latitude: {latitude}";
             }
        }
    }
    

