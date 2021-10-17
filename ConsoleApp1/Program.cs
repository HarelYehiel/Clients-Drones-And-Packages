using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)

        {
            Console.WriteLine("Choose one of the following:\n");
            int ch, ch1, ch2;
            do
            {
                Console.WriteLine("press 0 to exit\n");
                Console.WriteLine("press 1 to add new object\n");
                Console.WriteLine("press 2 to update object properties\n");
                ch = Convert.ToInt32(Console.ReadLine());
                switch (ch)
                {
                    case 1:
                        Console.WriteLine("Choose one of the following:\n");
                        Console.WriteLine("press 0 to back \n");
                        Console.WriteLine("press 1 to add a new drone-staition\n");
                        Console.WriteLine("press 2 to add a new drone\n");
                        Console.WriteLine("press 3 to add a new customer\n");
                        Console.WriteLine("press 4 to add a new parcel\n");
                        do
                        {
                            ch1 = Convert.ToInt32(Console.ReadLine());
                            switch (ch1)
                            {
                                case 1:
                                    IDAL.DalObject.DalObject.addStation();

                                    break;
                                case 2:
                                    IDAL.DalObject.DalObject.addDrone();
                                    break;
                                case 3:
                                    IDAL.DalObject.DalObject.addCustomer();
                                    break;
                                case 4:
                                    IDAL.DalObject.DalObject.addParcel1();
                                    break;
                            }
                            break;
                        } while (ch1 != 0);
                        break;

                    case 2:
                        Console.WriteLine("Choose one of the following:\n");
                        Console.WriteLine("press 0 to back \n");
                        Console.WriteLine("press 1 to assign a parcel to drone \n");
                        Console.WriteLine("press 2 to update pick up time\n");
                        Console.WriteLine("press 3 to update arrival time\n");
                        Console.WriteLine("press 4 to send drone from charge in station \n");
                        Console.WriteLine("press 5 to send drone to charge at station\n");
                        do
                        {
                            ch2 = Convert.ToInt32(Console.ReadLine());
                            switch (ch2)
                            {
                                case 1://update witch drone is pickUp this parcel
                                    Console.WriteLine("enter parcel ID:\n");
                                    int ID = Convert.ToInt32(Console.ReadLine());
                                    IDAL.DO.Parcel par = IDAL.DalObject.DalObject.GetParcel(ID);
                                    Console.WriteLine("witch drone do you want to take the parcel?(ID)\n");
                                    int droneID = Convert.ToInt32(Console.ReadLine());
                                    par.DroneId = droneID;
                                    break;
                                case 2://מקווה שהבנתי נכון את המשימה - עכשיו הגיע איסוף של החבילה ואנחנו שואלים איזה חבילה נאספה כדי לעדכן שעת איסוף
                                    Console.WriteLine("which parcel is picked up?\n enter parcel ID:\n");
                                    int PickId = Convert.ToInt32(Console.ReadLine());
                                    IDAL.DO.Parcel PickPar = IDAL.DalObject.DalObject.GetParcel(PickId);//get the parcel from the array
                                    PickPar.PickedUp = DateTime.Now;//update the time of picked up to now
                                    break;
                                case 3://update at the Parcel odbject delivered time
                                    Console.WriteLine("which parcel is delivered?\n enter parcel ID:\n");
                                    int deliId = Convert.ToInt32(Console.ReadLine());
                                    IDAL.DO.Parcel DeliPar = IDAL.DalObject.DalObject.GetParcel(deliId);//get the parcel from the array
                                    DeliPar.Delivered = DateTime.Now;//update the time of delivered to now
                                    break;
                                case 4:
                                    Console.WriteLine("enter drone ID:\n");
                                    int droneId = Convert.ToInt32(Console.ReadLine());
                                    IDAL.DO.Drone dro = IDAL.DalObject.DalObject.GetDrone(droneId);
                                    dro.Status = IDAL.DO.Enum.DroneStatus.Baintenance;
                                    Console.WriteLine("witch station do you want to set free?\n");
                                    int stationId = Convert.ToInt32(Console.ReadLine());                                          
                                    IDAL.DO.station statioID = IDAL.DalObject.DalObject.GetStation(stationId);
                                    if(statioID.ChargeSlots == 10)//the defolt charge slots is 10, if 10 is free so the station is empty
                                        Console.WriteLine("this station is empty, no drone is cherging here:\n");
                                    else
                                        statioID.ChargeSlots++;
                                    break;
                                case 5:
                                    Console.WriteLine("enter drone ID:\n");
                                    int drId = Convert.ToInt32(Console.ReadLine());
                                    IDAL.DO.Drone dron = IDAL.DalObject.DalObject.GetDrone(drId);
                                    dron.Status = IDAL.DO.Enum.DroneStatus.Baintenance;
                                    Console.WriteLine("witch station do you want?\nchoose ID from the list of available charging stations:\n");
                                    IDAL.DalObject.DalObject.AvailableChargingStations();//print all the available charging stations
                                    int statId = Convert.ToInt32(Console.ReadLine());                                  
                                    IDAL.DO.station statioId = IDAL.DalObject.DalObject.GetStation(statId);
                                    if(statioId.ChargeSlots > 0)//if the station that the user choose is  free
                                        statioId.ChargeSlots--;
                                    else
                                        Console.WriteLine("this station is not vailable:\n");
                                    break;
                            }
                        } while (ch2 != 0);
                        break;
                }

            } while (ch != 0);
        }
    }
};

