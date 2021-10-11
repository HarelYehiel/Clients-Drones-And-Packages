using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public struct Parcel
    {
        public int Id {get;set;}
        public int SenderId {get;set;}
        public int TargetId {get;set;}
        //public ??  Weight {get;set;}
        //public ?? Priority {get;set;}
        //public ??  Requested {get;set;} 
        public int DroneId {get;set;}
        //public ?? Scheduled {get;set;} 
        //public ?? PickedUp {get;set;} 
        //public ?? Delivered {get;set;} 


    }
}
