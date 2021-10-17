using System;
using IDAL.DalObject;
using IDAL.DO;
namespace ConsoleUI
{
    using System;

    namespace ConsoleUI
    {
        class Program
        {
            static void Main(string[] args)

            {
                Console.WriteLine("Choose one of the following:");
                int ch, ch1, ch2;
                do
                {
                    Console.WriteLine("press 1 to add new object:");
                    Console.WriteLine("press 2 to update object properties:");
                    Console.WriteLine("press 3 to see object properties:");
                    Console.WriteLine("press 4 to see lists of  objects:");
                    Console.WriteLine("press 5 to exit:");

                    ch = Convert.ToInt32(Console.ReadLine());
                    switch (ch)
                    {
                        case 1:
                            Console.WriteLine("Choose one of the following:");
                            Console.WriteLine("press 0 to back ");
                            Console.WriteLine("press 1 to add a new drone-staition");
                            Console.WriteLine("press 2 to add a new drone");
                            Console.WriteLine("press 3 to add a new customer");
                            Console.WriteLine("press 4 to add a new parcel");
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
                            Console.WriteLine("Choose one of the following:");
                            Console.WriteLine("press 0 to back ");
                            Console.WriteLine("press 1 to assign a parcel to drone ");
                            Console.WriteLine("press 2 to update pick up time");
                            Console.WriteLine("press 3 to update arrival time");
                            Console.WriteLine("press 4 to send drone from charge in station ");
                            Console.WriteLine("press 5 to send drone to charge at station");
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
                                        if (statioID.ChargeSlots == 10)//the defolt charge slots is 10, if 10 is free so the station is empty
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
                                        if (statioId.ChargeSlots > 0)//if the station that the user choose is  free
                                            statioId.ChargeSlots--;
                                        else
                                            Console.WriteLine("this station is not vailable:\n");
                                        break;
                                }
                            } while (ch2 != 0);
                            break;

                        case 3:
                            Console.WriteLine("1 = Station View" +
                            "\n2 = Drone View" +
                               "\n3 = Customer View" +
                               "\n4 = parcel View" +
                               "\nChoose number");
                            int ch3 = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Type an ID\n");
                            int id = Convert.ToInt32(Console.ReadLine());
                            switch (ch3) //Display
                            {
                                case 1:
                                    DalObject.GetStation(id).ToString();
                                    break;

                                case 2:
                                    DalObject.GetDrone(id).ToString();
                                    break;

                                case 3:
                                    DalObject.GetCustomer(id).ToString();
                                    break;

                                case 4:
                                    DalObject.GetParcel(id).ToString();
                                    break;

                            }
                            break;

                        case 4:

                            Console.WriteLine("1 = Displays a list of base stations" +
                                "\n2 = Displays a list of drones" +
                                "\n3 = Displays a list of customer" +
                                "\n4 = Displays the list of parcels" +
                                "\n5 = Displays a list of packages that have not yet been assigned to the glider" +
                                "\n6 = Display base stations with available charging stations\n");

                            int ch4 = Convert.ToInt32(Console.ReadLine());
                            switch (ch4)
                            {
                                case 1:
                                    station[] s = DalObject.Displays_list_of_stations();
                                    for (int i = 0; i < s.Length; i++)
                                    {
                                        Console.WriteLine(s[i].ToString() + "\n");
                                    }
                                    break;

                                case 2:
                                    Drone[] d = DalObject.Displays_list_of_drone();
                                    for (int i = 0; i < d.Length; i++)
                                    {
                                        Console.WriteLine(d[i].ToString() + "\n");
                                    }
                                    break;

                                case 3:
                                    Customer[] c = DalObject.Displays_list_of_custmers();
                                    for (int i = 0; i < c.Length; i++)
                                    {
                                        Console.WriteLine(c[i].ToString() + "\n");
                                    }
                                    break;

                                case 4:
                                    Parcel[] p = DalObject.Displays_list_of_Parcels();
                                    for (int i = 0; i < p.Length; i++)
                                    {
                                        Console.WriteLine(p[i].ToString() + "\n");
                                    }
                                    break;

                                case 5:
                                    DalObject.displaysParcelsDontHaveDrone();
                                    break;

                                case 6:
                                    DalObject.AvailableChargingStations();
                                    break;
                            }
                            break;


                    }
                } while (ch != 5);

            }

        }
    }
};
