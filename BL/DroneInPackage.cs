using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {

       public class DroneInPackage
        {
            public int uniqueID { get; set; }
            public double batteryStatus { get; set; }
            public Location location { get; set; }
            public override string ToString()
            {
                return $"Drone ID: {uniqueID}, batteryStatus: {batteryStatus}, location: {location.ToString()}";
            }        
            }
    }
}
