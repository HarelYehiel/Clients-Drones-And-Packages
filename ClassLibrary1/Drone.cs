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
            public WeightCategories MaxWeight{ get; set; }
            public DroneStatus Status{ get; set; }
            public double Battery { get; set; };

            public override string ToString()
            {
                return $"Drone ID: {Id}, model: {Model}, status: {Status}, maxWeight: {MaxWeight}, battery: {Battery}";
            }            
        }
        public enum WeightCategories { }
        {
            //I am not sure that this is the way to do it
        }
        public struct DroneStatus
        {
            //I am not sure that this is the way to do it
        }

    }
}
