using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace BO
    {
        public class ParcelByTransfer
        {
            public int uniqueID { get; set; }
            public EnumBO.WeightCategories weight { get; set; }
            public EnumBO.Priorities priority { get; set; }
            
            public bool isWaitForCollection { get; set; }
            public BO.CustomerInParcel theSander { get; set; }
            public BO.CustomerInParcel theTarget { get; set; }

            public Location collectLocation { get; set; }
            public Location destinationLocation { get; set; }
            public double transportDistance { get; set; }

            public override string ToString()
            {
                return $"Parcel ID: {uniqueID}, the  sander {theSander.ToString()}, the target {theTarget.ToString()}\n" +
                    $"weight: {Enum.GetName(typeof(EnumBO.WeightCategories), weight)}," +
                    $" priority: {Enum.GetName(typeof(EnumBO.Priorities), priority)}" +
                    $" is wait for collection  = parcelStatus\n" +
                    $"collection location = {collectLocation.ToString()}, destination location = {destinationLocation.ToString()}" +
                    $" transport distance = {transportDistance}";
            }

        }
    }
    

