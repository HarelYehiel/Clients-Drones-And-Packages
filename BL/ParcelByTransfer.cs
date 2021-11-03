using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class ParcelByTransfer
        {
            public int uniqueID { get; set; }
            public Enum.WeightCategories weight { get; set; }
            public Enum.Priorities priority { get; set; }
            
            public Enum.DeliveryStatus parcelStatus { get; set; }
            public Location collectionLocation { get; set; }
            public Location destinationLocation { get; set; }
            public double transportDistance { get; set; }

            public override string ToString()
            {
                return $"Parcel ID = {uniqueID}, weight = {weight}, priority = {priority}, parcel status = {parcelStatus}, collection location = {collectionLocation}, destination location = {destinationLocation}, transport distance = {transportDistance}";
            }

        }
    }
    
}
