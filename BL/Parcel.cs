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
            public CustomerInParcel customerInParcel_Sender { get; set; }
            public CustomerInParcel customerInParcel_Target { get; set; }
            public DroneInPackage droneInParcel { get; set; }
            public Enum_BO.Priorities priority { get; set; }
            public Enum_BO.WeightCategories weight { get; set; }
            public DateTime requested { get; set; }
            public DateTime scheduled { get; set; }
            public DateTime pickedUp { get; set; }
            public DateTime delivered { get; set; }
            public override string ToString()
            {
                return $"Parcel ID: {uniqueID}\n sender: {customerInParcel_Sender.ToString()}\n" +
                    $" target: {customerInParcel_Target.ToString()}\ndrone: {droneInParcel.ToString()}\n" +
                    $"priority: {Enum.GetName(typeof( Enum_BO.Priorities),priority)}, weight: {Enum.GetName(typeof(Enum_BO.WeightCategories),weight)}\n" +
                    $" requested: {requested}, scheduled = {scheduled}, picked up = {pickedUp}, delivered = {delivered}";
            }
        }
    }
}
