using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class parcelAtCustomer
        {
            public int uniqueID { get; set; }
            public EnumBO.WeightCategories weight { get; set; }
            public EnumBO.Priorities priority { get; set; }
            public EnumBO.Situations situation { get; set; }
            public CustomerInParcel customer_In_Delivery { get; set; }
            public override string ToString()
            {
                return $"Parcel ID = {uniqueID}, weight: {Enum.GetName(typeof(EnumBO.WeightCategories), weight)}," +
                    $" priority: {Enum.GetName(typeof(EnumBO.Priorities), priority)}, situation: {Enum.GetName(typeof(EnumBO.Situations),situation)}" +
                    $", customer in delivery: {customer_In_Delivery.ToString()}";
            }
        }
    }
}
