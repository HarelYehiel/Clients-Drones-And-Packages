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
                return $"Station ID = {uniqueID}, name = {name}, {location} , available Charging Stations = {availableChargingStations}";
            }
           
        }

    }
}