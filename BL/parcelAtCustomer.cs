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
            public Enum.WeightCategories weight { get; set; }
            public Enum.Priorities priority { get; set; }
            public Enum.Situations situation { get; set; }
            public CustomerInDelivery customer_In_Delivery { get; set; }
            public override string ToString()
            {
                return $"Parcel ID = {uniqueID}, weight = {weight}, priority = {priority}, situation = {situation}, customer in delivery = {customer_In_Delivery}";
            }
        }
    }
}
