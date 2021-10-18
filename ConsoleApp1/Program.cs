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
                int ch, ch1, ch2,ch3,ch4;
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
                                        IDAL.DalObject.DalObject.AffiliationDroneToParcel();
                                        break;
                                    case 2:
                                        IDAL.DalObject.DalObject.pickUp();
                                        break;
                                    case 3://update at the Parcel odbject delivered time
                                        IDAL.DalObject.DalObject.delivered();
                                        break;
                                    case 4:
                                        IDAL.DalObject.DalObject.setFreeStation();
                                        break;
                                    case 5:
                                        IDAL.DalObject.DalObject.droneToCharge();
                                        break;
                                }
                            } while (ch2 != 0);
                            break;

                        case 3:
                            Console.WriteLine("Choose one of the following:");
                            Console.WriteLine("press 0 to back ");
                            Console.WriteLine("press 1 to Station View");
                            Console.WriteLine("press 2 to drone View");
                            Console.WriteLine("press 3 to Customer View");
                            Console.WriteLine("press 4 to parcel View ");
                            ch3 = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Type an ID\n");
                            int id = Convert.ToInt32(Console.ReadLine());
                            switch (ch3) //Display
                            {
                                case 1:
                                    Console.WriteLine(DalObject.GetStation(id).ToString());
                                    break;

                                case 2:
                                    Console.WriteLine( DalObject.GetDrone(id).ToString());
                                    break;

                                case 3:
                                    Console.WriteLine(DalObject.GetCustomer(id).ToString());
                                    break;

                                case 4:
                                    Console.WriteLine(DalObject.GetParcel(id).ToString());
                                    break;

                            }
                            break;

                        case 4:
                            Console.WriteLine("Choose one of the following:");
                            Console.WriteLine("press 0 to back ");
                            Console.WriteLine("press 1 to displays a list of base stations");
                            Console.WriteLine("press 2 to displays a list of drones");
                            Console.WriteLine("press 3 to displays a list of customer");
                            Console.WriteLine("press 4 to Displays the list of parcels ");
                            Console.WriteLine("press 5 to displays a list of packages that have not yet been assigned to the glider");
                            Console.WriteLine("press 6 to base stations with available charging stations\n");
                            ch4 = Convert.ToInt32(Console.ReadLine());
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
/*
1
3
12345
harel
052333555
31.456789
34.789456
1
2
11111
88.5
HAR
1
1
1
22222
Har
34.123456
31.321654
1
4
33333
12345
12345
11111
0
0
3
1
22222
3
2
11111
3
3
12345
3
4
33333
4
1
4
2
4
3
4
4
4
5
1
4
22222
12345
12345
0
1
1
4
5
4
6
2
1
22222
11111
0
3
4
22222
2
2
22222
0
3
4
22222
2
3
22222


 */