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
            public Enum.WeightCategories weight { get; set; }

            public Enum.DroneStatus Status { get; set; }
            public double Battery { get; set; }

            public Location location { get; set; }

            public override string ToString()
            {
                return $"Drone ID = {uniqueID}, model = {Model}, status = {Status}, weight = {weight}, battery = {Battery}, location = {location}";
            }            
        }        

    }
}
