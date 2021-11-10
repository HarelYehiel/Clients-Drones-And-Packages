using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
         public class Parcel
        {
            public int uniqueID { get; set; }
            public CustomerInDelivery namrSender { get; set; }
            public CustomerInDelivery nameTarget { get; set; }
            public DroneInPackage droneInParcel { get; set; }
            public Enum.Priorities priority { get; set; }
            public Enum.WeightCategories weight { get; set; }
            public DateTime requested { get; set; }
            public DateTime scheduled { get; set; }
            public DateTime pickedUp { get; set; }
            public DateTime delivered { get; set; }
            public override string ToString()
            {
                return $"Parcel ID = {uniqueID}, sender = {namrSender}, target = {nameTarget}, drone ID = {drone}\n requested = {requested}, scheduled = {scheduled}, picked up = {pickedUp}, delivered = {delivered} ";
            }
        }
    }
}
