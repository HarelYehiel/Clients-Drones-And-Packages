using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace BO
    {
        public  class CustomerInParcel
        {
            public int uniqueID { get; set; }
            public string name { get; set; }
            public override string ToString()
            {
                return $"Customer ID: {uniqueID}, name: {name}";
            }
        }
    }
    

