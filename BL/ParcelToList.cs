using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class ParcelToList
        {
            public int uniqueID { get; set; }
            public string namrSender { get; set; }
            public string nameTarget { get; set; }
            public Enum_BO.Priorities priority { get; set; }
            public Enum_BO.WeightCategories weight { get; set; }

            public Enum_BO.Situations parcelsituation { get; set; }

            public override string ToString()
            {
                return $"Parcel ID = {uniqueID}, sender = {namrSender}, target = {nameTarget}\n" +
                    $"priority: {Enum.GetName(typeof(Enum_BO.Priorities), priority)}," +
                    $" weight: {Enum.GetName(typeof(Enum_BO.WeightCategories), weight)}," +
                    $" parcel situation = {Enum.GetName(typeof(Enum_BO.Situations),parcelsituation)}";
            }
        }
    }
}
