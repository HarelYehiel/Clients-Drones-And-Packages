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
            public Enum.Priorities priority { get; set; }
            public Enum.WeightCategories weight { get; set; }

            public Enum.Situations parcelsituation { get; set; }

            public override string ToString()
            {
                return $"Parcel ID = {uniqueID}, sender = {namrSender}, target = {nameTarget}, priority = {priority}, weight = {weight}, parcel situation = {parcelsituation}";
            }
        }
    }
}
