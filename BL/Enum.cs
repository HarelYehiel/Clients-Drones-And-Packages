﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {


        public struct Enum
        {

            public enum WeightCategories { Light, Medium, Heavy }

            public enum DroneStatus { Avilble, Baintenance, Delivery }
            public enum Priorities { Normal, Fast, Emergency }
            public enum DeliveryStatus {waitingForCollection, onTheWay }
            public enum Situations { created, associated, collected, provided}

          
        }
    }
}
