using System;
using IDAL.DalObject;
using IDAL.DO;
namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
            
        {
            Console.WriteLine("1 = Station View" +
                "\n2 = Drone View" +
                "\n3 = Customer View" +
                "\n4 = parcel View\n");
            int num;
            num = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Type an ID\n");
            int id = Convert.ToInt32(Console.ReadLine());
            switch (num) //Display
            {
                case  1:
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

            
            Console.WriteLine("1 = Displays a list of base stations" +
                "\n2 = Displays a list of drones" +
                "\n3 = Displays a list of customer" +
                "\n4 = Displays the list of parcels" +
                "\n5 = Displays a list of packages that have not yet been assigned to the glider" +
                "\n6 = Display base stations with available charging stations\n");
            id = Convert.ToInt32(Console.ReadLine());
            switch (num) 
            {
                case 1:
                   station[] s =  DalObject.Displays_list_of_stations();
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
        }
    }
}
