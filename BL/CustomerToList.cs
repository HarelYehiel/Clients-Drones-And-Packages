using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class CustomerToList
        {
            public int uniqueID { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public int packagesSentAndDelivered { get; set; }
            public int packagesSentAndNotDelivered { get; set; }
            public int packagesHeReceived { get; set; }
            public int packagesOnTheWayToTheCustomer { get; set; }

            public override string ToString()
            {
                return $"Customer ID: {uniqueID}, name: {Name}, phone: {Phone}\n" +
                    $" packages sent and delivered: {packagesSentAndDelivered}, packages sent and not delivered: {packagesSentAndNotDelivered}\n" +
                    $" packages he received: {packagesHeReceived}, packages on the way to the customer: {packagesOnTheWayToTheCustomer}";
            }
        }
    }
}
