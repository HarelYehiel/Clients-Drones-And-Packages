using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace BO
    {
       public class DroneToList
        {
            public int uniqueID { get; set; }

            public string Model { get; set; }
            public EnumBO.WeightCategories weight { get; set; }
            public EnumBO.DroneStatus status { get; set; }
            public double Battery { get; set; }
            public Location location { get; set; }
            public int packageDelivered { get; set; }

            public override string ToString()
            {
                return $"Drone ID: {uniqueID}, model: {Model}, weight: {Enum.GetName(typeof(EnumBO.WeightCategories),weight)}\n," +
                    $" status: {Enum.GetName(typeof( EnumBO.DroneStatus),status)}, Battery: {Battery}, location:  {location.ToString()}," +
                    $" package delivered: {packageDelivered}";
            }

        }
    }

