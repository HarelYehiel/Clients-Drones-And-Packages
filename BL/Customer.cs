using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
       public class Customer
        {
            public int uniqueID { get; set; }
            public string name { get; set; }
            public string phone { get; set; }
            public Location location { get; set; }

            public List<ParcelToCustomer> fromTheCustome { get; set; }
            public List<ParcelToCustomer> toTheCustome { get; set; }

            public override string ToString()
            {
                return $"Customer ID = {uniqueID}, name = {name}, phone = {phone}, location = {location}";
            }
        }
    }
}
