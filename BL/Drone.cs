using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class Drone
        {//all the properties
            public int uniqueID { get; set; }

            public string Model { get; set; }
            public Enum_BO.WeightCategories weight { get; set; }

            public Enum_BO.DroneStatus Status { get; set; }
            public DateTime chargingTime { get; set; }
            public double Battery { get; set; }

            public Location location { get; set; }

            public override string ToString()
            {
                return $"Drone ID: {uniqueID}, model: {Model},  Status: {Enum.GetName(typeof(Enum_BO.Situations), Status)}, battery = {Battery}\n" +
                    $"weight: {Enum.GetName(typeof(Enum_BO.WeightCategories),weight)}, location: {location.ToString()}, chargingTime: {chargingTime}";
            }            
        }        

    }
}