using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IBL

{
    namespace BO
    {

        public class station
        {
            public int uniqueID { get; set; }
            public string name { get; set; }
            public Location location { get; set; }
            public int availableChargingStations { get; set; }

             public List<DroneInCharging> dronesInCharging { get; set; }

            public override string ToString()
            {
                string s = $"Station ID: {uniqueID}, name: {name}, location: {location.ToString()}\n";

                if (dronesInCharging.Count > 0)
                {
                    s += "The all drone charging in this station:\n";
                    foreach (DroneInCharging item in dronesInCharging)
                    {
                        s += item.ToString() + "\n";
                    }
                }

                return s;
            }
           
        }

    }
}
/*
4
1
0
3
1


 */