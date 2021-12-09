using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace BO
    {
        public class DroneInCharging
        {
            public int uniqueID { get; set; }
            public double batteryStatus { get; set; }

            public override string ToString()
            {
                return $"Drone ID: {uniqueID}, battery status: {batteryStatus}";
            }

        }
    }

