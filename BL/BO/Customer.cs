using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace BO
    {
       public class Customer
        {
            public int uniqueID { get; set; }
            public string name { get; set; }
            public string phone { get; set; }
            public Location location { get; set; }
            public List<parcelAtCustomer> fromTheCustomer { get; set; }
            public List<parcelAtCustomer> toTheCustomer { get; set; }

            public override string ToString()
            {
                string s = $"Customer ID: {uniqueID}, name: {name}, phone: {phone}, location: {location.ToString()}\n";

                s += "The parcels from the customer:\n";
                foreach (parcelAtCustomer item in fromTheCustomer)
                {
                    s += item.ToString() + "\n";
                }

                s += "The parcels to the customer:\n";
                foreach (parcelAtCustomer item in toTheCustomer)
                {
                    s += item.ToString() + "\n";
                }

                return s;
            }
        }
    }

