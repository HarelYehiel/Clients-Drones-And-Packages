using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct Drone
        {//all the properties
            public int Id { get; set; }
            public string Model { get; set; }
            public Enum.WeightCategories MaxWeight{ get; set; }
            public Enum.DroneStatus Status{ get; set; }
            public double Battery { get; set; }

            public override string ToString()
            {
                return $"Drone ID: {Id}, model: {Model}, status: {Status}, maxWeight: {MaxWeight}, battery: {Battery}";
            }            
        }        

    }
}
