using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace BO
    {
        public class ParcelToList
        {
            public int uniqueID { get; set; }
            public string namrSender { get; set; }
            public string nameTarget { get; set; }
            public EnumBO.Priorities priority { get; set; }
            public EnumBO.WeightCategories weight { get; set; }
            public EnumBO.Situations parcelsituation { get; set; }

            public override string ToString()
            {
                return $"Parcel ID = {uniqueID}, sender = {namrSender}, target = {nameTarget}\n" +
                    $"priority: {Enum.GetName(typeof(EnumBO.Priorities), priority)}," +
                    $" weight: {Enum.GetName(typeof(EnumBO.WeightCategories), weight)}," +
                    $" parcel situation = {Enum.GetName(typeof(EnumBO.Situations),parcelsituation)}";
            }
        }
    }
