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
            public Enum_BO.WeightCategories weight { get; set; }
            public Enum_BO.Priorities priority { get; set; }
            
            public bool isWaitForCollection { get; set; }
            public BO.CustomerInParcel theSander { get; set; }
            public BO.CustomerInParcel theTarget { get; set; }

            public Location collectionLocation { get; set; }
            public Location destinationLocation { get; set; }
            public double transportDistance { get; set; }

            public override string ToString()
            {
                return $"Parcel ID: {uniqueID}, the  sander {theSander.ToString()}, the target {theTarget.ToString()}\n" +
                    $"weight: {Enum.GetName(typeof(Enum_BO.WeightCategories), weight)}," +
                    $" priority: {Enum.GetName(typeof(Enum_BO.Priorities), priority)}" +
                    $" is wait for collection  = parcelStatus\n" +
                    $"collection location = {collectionLocation.ToString()}, destination location = {destinationLocation.ToString()}" +
                    $" transport distance = {transportDistance}";
            }

        }
    }
    
}
