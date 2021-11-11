using System;

namespace IBL
{
    namespace BO
    {
        public class Location
        {
             public double longitude { get; set; }
            public double latitude { get; set; }

            public override string ToString()
            {
                return $"longitude: {longitude}, latitude: {latitude}";
             }
        }
    }
    
}
