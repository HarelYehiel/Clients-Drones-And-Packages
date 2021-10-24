using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    class functionCase2
    {
        public static void AffiliationDroneToParcel()
        {
            Console.WriteLine("enter parcel ID:");
            int ID = Convert.ToInt32(Console.ReadLine());
            ref IDAL.DO.Parcel par = ref IDAL.DalObject.DalObject.GetParcel(ID);
            Console.WriteLine("witch drone do you want to take the parcel?(ID)");
            int droneID = Convert.ToInt32(Console.ReadLine());
            par.DroneId = droneID;
            ref IDAL.DO.Drone drone1 = ref IDAL.DalObject.DalObject.GetDrone(droneID);//update drone status
            drone1.Status = (IDAL.DO.Enum.DroneStatus)2;
        }

        public static void pickUp()
        {
            Console.WriteLine("which parcel is picked up?\n enter parcel ID:");
            int PickId = Convert.ToInt32(Console.ReadLine());
            ref IDAL.DO.Parcel PickPar = ref IDAL.DalObject.DalObject.GetParcel(PickId);//get the parcel from the array
            PickPar.PickedUp = DateTime.Now;//update the time of picked up to now
        }

        public static void delivered()
        {
            Console.WriteLine("which parcel is delivered?\n enter parcel ID:");
            int deliId = Convert.ToInt32(Console.ReadLine());
            ref IDAL.DO.Parcel DeliPar = ref IDAL.DalObject.DalObject.GetParcel(deliId);//get the parcel from the array
            DeliPar.Delivered = DateTime.Now;//update the time of delivered to now
        }
        public static void setFreeStation()
        {

            Console.WriteLine("enter drone ID:");
            int droneId = Convert.ToInt32(Console.ReadLine());
            ref IDAL.DO.Drone dro = ref IDAL.DalObject.DalObject.GetDrone(droneId);
            dro.Status = IDAL.DO.Enum.DroneStatus.Baintenance;
            IDAL.DO.station statioID = new IDAL.DO.station();
            statioID.Id = dro.stationOfCharge.staitionId;
            statioID = IDAL.DalObject.DalObject.GetStation(statioID.Id);
            if (statioID.ChargeSlots == 10)//the defolt charge slots is 10, if 10 is free so the station is empty
                Console.WriteLine("this station is empty, no drone is cherging here:");
            else
            {
                statioID.ChargeSlots++;
                IDAL.DO.DroneCharge charge = new IDAL.DO.DroneCharge();
                charge.staitionId = 0;
                charge.DroneId = droneId;
            }
        }
        public static void droneToCharge()
        {
            Console.WriteLine("enter drone ID:");
            int drId = Convert.ToInt32(Console.ReadLine());
            ref IDAL.DO.Drone dron = ref IDAL.DalObject.DalObject.GetDrone(drId);
            dron.Status = IDAL.DO.Enum.DroneStatus.Baintenance;//update the drone status for not call him for another things
            Console.WriteLine("witch station do you want?\nchoose ID from the list of available charging stations:");
            IDAL.DalObject.DalObject.AvailableChargingStations();//print all the available charging stations
            int statId = Convert.ToInt32(Console.ReadLine());
            IDAL.DO.DroneCharge charge = new IDAL.DO.DroneCharge();//crate new object type DroneChrge
            charge.DroneId = drId;
            charge.staitionId = statId;
            dron.stationOfCharge = charge;//after update the data of DroneCharge update the informaition in the dron
            ref IDAL.DO.station statioId = ref IDAL.DalObject.DalObject.GetStation(statId);
            if (statioId.ChargeSlots > 0)//if the station that the user choose is  free
                statioId.ChargeSlots--;
            else
                Console.WriteLine("this station is not vailable:");
        }
    }
}
