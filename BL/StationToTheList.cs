using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace BO
    {
        public class StationToTheList
        {
            public int uniqueID { get; set; }
            public string name { get; set; }
            public int availableChargingStations { get; set; }
            public int unAvailableChargingStations { get; set; }

            public override string ToString()
            {
                return $"Station ID: {uniqueID},  station name: {name}," +
                    $" available charging stations: {availableChargingStations}," +
                    $" unavailable charging stations: {unAvailableChargingStations}";
             }

        }
    }

