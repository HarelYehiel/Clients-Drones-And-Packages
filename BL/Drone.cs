using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace BO
    {
        public class Drone
        {//all the properties
            public int uniqueID { get; set; }

            public string Model { get; set; }
            public EnumBO.WeightCategories weight { get; set; }

            public EnumBO.DroneStatus Status { get; set; }
            // public DateTime chargingTime { get; set; }
            public double Battery { get; set; }
            public ParcelByTransfer parcelByTransfer { get; set; }

            public Location location { get; set; }

            public override string ToString()
            {
                string s = $"Drone ID: {uniqueID}, model: {Model},  Status: {Enum.GetName(typeof(EnumBO.DroneStatus), Status)}, battery = {Battery}\n" +
                    $"weight: {Enum.GetName(typeof(EnumBO.WeightCategories),weight)}, location: {location.ToString()}";

                if (Status == EnumBO.DroneStatus.Delivery)
                    s += $", parcelId: {parcelByTransfer.uniqueID}";

                return s;
            }            
        }        

    }
