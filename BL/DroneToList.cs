using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
       public class DroneToList
        {
            public int uniqueID { get; set; }

            public string Model { get; set; }
            public Enum.WeightCategories weight { get; set; }
            public Enum.DroneStatus status { get; set; }
            public double Battery { get; set; }
            public Location location { get; set; }
            public int packageDelivered { get; set; }

            public override string ToString()
            {
                return $"Droone ID = {uniqueID}, model = {Model}, weight = {weight}, status = {status}, Battery = {Battery}, location =  {location}, package delivered = {packageDelivered}";
            }

        }
    }
}
