using System;
using IDAL.DalObject;
using IDAL.DO;
using System.Collections.Generic;
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
                int ch, ch1, ch2, ch3, ch4;
                do
                {
                    Console.WriteLine("press 1 to add new object:");
                    Console.WriteLine("press 2 to update object properties:");
                    Console.WriteLine("press 3 to see object properties:");
                    Console.WriteLine("press 4 to see lists of  objects:");
                    Console.WriteLine("press 5 to more function:");//the bonus part
                    Console.WriteLine("press 6 to exit");
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
                                        functionCase1.addStation();

                                        break;
                                    case 2:
                                        functionCase1.addDrone();
                                        break;
                                    case 3:
                                        functionCase1.addCustomer();
                                        break;
                                    case 4:
                                        functionCase1.addParcel1();
                                        break;
                                }
                                break;
                            } while (ch1 != 0);
                            break;

                        case 2:

                            do
                            {
                                Console.WriteLine("Choose one of the following:");
                                Console.WriteLine("press 0 to back ");
                                Console.WriteLine("press 1 to assign a parcel to drone ");
                                Console.WriteLine("press 2 to update pick up time");
                                Console.WriteLine("press 3 to update arrival time");
                                Console.WriteLine("press 4 to send drone from charge in station ");
                                Console.WriteLine("press 5 to send drone to charge at station");
                                ch2 = Convert.ToInt32(Console.ReadLine());
                                try
                                {
                                    switch (ch2)
                                    {
                                        case 1://update witch drone is pickUp this parcel
                                            Console.WriteLine("enter parcel ID:");
                                            int parcelID = Convert.ToInt32(Console.ReadLine());
                                            Console.WriteLine("witch drone do you want to take the parcel?(ID)");
                                            int droneID = Convert.ToInt32(Console.ReadLine());
                                            IDAL.DalObject.DalObject.AffiliationDroneToParcel(parcelID,droneID);
                                            break;
                                        case 2:
                                            Console.WriteLine("which parcel is picked up?\n enter parcel ID:");
                                            int PickId = Convert.ToInt32(Console.ReadLine());
                                            IDAL.DalObject.DalObject.pickUp(PickId);
                                            break;
                                        case 3://update at the Parcel odbject delivered time
                                            Console.WriteLine("which parcel is delivered?\n enter parcel ID:");
                                            int deliId = Convert.ToInt32(Console.ReadLine());
                                            IDAL.DalObject.DalObject.delivered(deliId);
                                            break;
                                        case 4:
                                            Console.WriteLine("enter drone ID:");
                                            int droneId = Convert.ToInt32(Console.ReadLine());
                                            IDAL.DalObject.DalObject.setFreeStation(droneId);
                                            break;
                                        case 5:
                                            Console.WriteLine("enter drone ID:");
                                            int drId = Convert.ToInt32(Console.ReadLine());
                                            Console.WriteLine("witch station do you want?\nchoose ID from the list of available charging stations:");
                                            List<IDAL.DO.station> newList = IDAL.DalObject.DalObject.AvailableChargingStations();//print all the available charging stations
                                            int statId = Convert.ToInt32(Console.ReadLine());

                                            foreach (IDAL.DO.station station1 in newList)
                                            {
                                                Console.WriteLine(station1.ToString());
                                            }
                                            IDAL.DalObject.DalObject.droneToCharge(drId,statId);
                                            break;
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
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
                            try
                            {
                                switch (ch3) //Display
                                {
                                    case 1:
                                        Console.WriteLine(IDAL.DalObject.DalObject.GetStation(id).ToString());
                                        break;

                                    case 2:
                                        Console.WriteLine(IDAL.DalObject.DalObject.GetDrone(id).ToString());
                                        break;

                                    case 3:
                                        Console.WriteLine(IDAL.DalObject.DalObject.GetCustomer(id).ToString());
                                        break;

                                    case 4:
                                        Console.WriteLine(IDAL.DalObject.DalObject.GetParcel(id).ToString());
                                        break;

                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
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
                                    List<IDAL.DO.station> tempSta = IDAL.DalObject.DalObject.Displays_list_of_stations();
                                    foreach (station station in DataSource.stations)
                                    {
                                        Console.WriteLine(station.ToString());
                                    }
                                    break;

                                case 2:
                                    List<IDAL.DO.Drone> tempDro = IDAL.DalObject.DalObject.Displays_list_of_drone();
                                    foreach (Drone drone in DataSource.drones)
                                    {
                                        Console.WriteLine(drone.ToString());
                                    }
                                    break;

                                case 3:
                                    List<IDAL.DO.Customer> tempCus = IDAL.DalObject.DalObject.Displays_list_of_custmers();
                                    foreach (Customer customer in DataSource.customers)
                                    {
                                        Console.WriteLine(customer.ToString());
                                    }
                                    break;
                                    

                                case 4:
                                    List<IDAL.DO.Parcel> tempPar = IDAL.DalObject.DalObject.Displays_list_of_Parcels();
                                    foreach (Parcel parcel in DataSource.parcels)
                                    {
                                        Console.WriteLine(parcel.ToString());
                                    }
                                    break;

                                case 5:
                                    List<Parcel> ParcelWithoutDrone = IDAL.DalObject.DalObject.displaysParcelsDontHaveDrone();
                                    foreach(Parcel parcel in ParcelWithoutDrone)
                                    {
                                        Console.WriteLine(parcel.ToString());
                                    }
                                    break;

                                case 6:
                                    List<station> AvailableCH = IDAL.DalObject.DalObject.AvailableChargingStations();
                                    foreach(station station in AvailableCH)
                                    {
                                        Console.WriteLine(station.ToString());

                                    }
                                    break;
                            }
                            break;
                        case 5:
                            Console.WriteLine("Choose one of the following:");
                            Console.WriteLine("press 0 to back ");
                            Console.WriteLine("press 1 to View on 'sexagesimal' (60sex- based on 'sexagesimal') - of the coordinate point.");
                            Console.WriteLine("Press 2 to get the distance from a client or station from point coordinates.");
                            ch4 = Convert.ToInt32(Console.ReadLine());
                            try
                            {
                                switch (ch4)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        functionCase5.chooseObjectToconvert();
                                        break;
                                    case 2:
                                        functionCase5.distanceFromCustomerOrStation();
                                        break;

                                }

                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                            break;




                    }
                } while (ch != 6);

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
0
1
3
11111
1
1
111
111
1
3
22222
2
2
333
333
5
2
222.1
222.1
1

*/