using System;

namespace ConsuleUIBL
{
    class Program
    {
        public static int giveNumber()
        {
            string s;
            IBL.BL temp = new IBL.BL();

            do
            {
                s = Console.ReadLine();
                if (temp.IsDigitsOnly(s))
                    return Convert.ToInt32(s);
                Console.WriteLine(IBL.BO.MyExeption_BO.Only_numbers_should_be_type_to);
            } while (true);
        }
        static void Main(string[] args)
        {
            IBL.BL temp = new IBL.BL();
            int ch, ch1, ch2, ch3, ch4;

            do
            {
                try
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
                            do
                            {
                                temp.Insert_options();
                                ch1 = Convert.ToInt32(Console.ReadLine());
                                int ID = 0;
                                switch (ch1)
                                {
                                    case 1:
                                        Console.WriteLine("enter station ID(5 digit):");
                                        ID = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("enter name of station:");
                                        string name = Console.ReadLine();
                                        Console.WriteLine("enter locaition:");
                                        double Latitude = Console.Read();
                                        double Longitude = Console.Read();
                                        Console.WriteLine("enter number of charge slots:");
                                        int numSlots = Convert.ToInt32(Console.ReadLine());
                                        temp.Adding_a_base_station(ID, name, Latitude, Longitude, numSlots);
                                        break;

                                    case 2:
                                        Console.WriteLine("enter drone ID(5 digit):");
                                        ID = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("enter model of drone:");
                                        string model = Console.ReadLine();
                                        Console.WriteLine("enter weight: \n1 = Light, 2 = Medium, 3 = Heavy");
                                        int maxWeight = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("enter number of station you wnat to put the drone:");
                                        int staId = Convert.ToInt32(Console.ReadLine());
                                        temp.Adding_a_drone(ID, model, maxWeight, staId);
                                        break;

                                    case 3:
                                        Console.WriteLine("enter customer ID(5 digit):");
                                        ID = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("enter name of customer:");
                                        string nameCu = Console.ReadLine();
                                        Console.WriteLine("enter phone number:");
                                        string phoneNumber = Console.ReadLine();
                                        Console.WriteLine("enter customer location:");
                                        Latitude = Console.Read();
                                        Longitude = Console.Read();
                                        temp.Absorption_of_a_new_customer(ID, nameCu, phoneNumber, Latitude, Longitude);
                                        break;

                                    case 4:
                                        Console.WriteLine("enter sender name:");
                                        string senderName = Console.ReadLine();
                                        Console.WriteLine("enter target name:");
                                        string targetName = Console.ReadLine();
                                        Console.WriteLine("enter weight: \n1 = Light, 2 = Medium, 3 = Heavy");
                                        maxWeight = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("enter target ID(5 digit): \n 1 = Normal, 2 = Fast, 3 = Emergency");
                                        int prioerity = Convert.ToInt32(Console.ReadLine());
                                        temp.Receipt_of_package_for_delivery(senderName, targetName, maxWeight, prioerity);
                                        break;
                                }
                                break;
                            } while (ch1 != 0);
                            break;
                        case 2:
                            Console.WriteLine("Choose one of the following:");

                            do
                            {
                                temp.Update_options();
                                ch2 = Convert.ToInt32(Console.ReadLine());
                                int ID = 0;
                                switch (ch2)
                                {
                                    case 1:
                                        Console.WriteLine("witch drone you want to update?(ID)");
                                        Console.WriteLine("what is the new name?");
                                        ID = Convert.ToInt32(Console.ReadLine());
                                        string newModel = Console.ReadLine();
                                        temp.Update_drone_data(ID, newModel);
                                        break;
                                    case 2:
                                        Console.WriteLine("witch station you want to update?(ID)");
                                        ID = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("do you want to update the name of station? if not press enter");
                                        string newName = Console.ReadLine();
                                        Console.WriteLine("do you want to update the number of slots? if not press enter");
                                        int numSlots = Convert.ToInt32(Console.ReadLine());
                                        temp.Update_station_data(ID, newName, numSlots);
                                        break;
                                    case 3:
                                        Console.WriteLine("witch customer you want to update?(ID)");
                                        ID = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("do you want to update customer name? if not press enter");
                                        string custName = Console.ReadLine();
                                        Console.WriteLine("do you want to update customer phone number? if not press enter");
                                        string phoneNumber = Console.ReadLine();
                                        temp.Update_customer_data(ID, custName, phoneNumber);
                                        break;
                                    case 4:
                                        Console.WriteLine("witch drone you want to charge?(ID)");
                                        ID = Convert.ToInt32(Console.ReadLine());
                                        temp.Sending_a_drone_for_charging(ID);
                                        break;
                                    case 5:
                                        Console.WriteLine("witch drone you want to release from charge?(ID)");
                                        Console.WriteLine("how many time?(minuets)");
                                        ID = Convert.ToInt32(Console.ReadLine());
                                        int min = Convert.ToInt32(Console.ReadLine());
                                        temp.Release_drone_from_charging(ID, min);
                                        break;
                                    case 6:
                                        Console.WriteLine("witch drone you want to get the parcel?(ID)");
                                        ID = Convert.ToInt32(Console.ReadLine());
                                        temp.Assign_a_package_to_a_drone(ID);
                                        break;
                                    case 7:
                                        Console.WriteLine("witch drone pickedUp the parcel?(ID)");
                                        ID = Convert.ToInt32(Console.ReadLine());
                                        temp.Collection_of_a_package_by_drone(ID);
                                        break;
                                    case 8:
                                        Console.WriteLine("witch drone delivered the parcel?(ID)");
                                        ID = Convert.ToInt32(Console.ReadLine());
                                        temp.Delivery_of_a_package_by_drone(ID);
                                        break;
                                }
                                break;
                            } while (ch2 != 0);
                            break;
                        case 3:
                            Console.WriteLine("Choose one of the following:");
                            do
                            {
                                temp.Entity_display_options();
                                ch3 = giveNumber();
                                int ID = 0;

                                switch (ch3)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        Console.WriteLine("witch station you want to view?");
                                        ID = giveNumber();
                                        temp.base_station_view(ID);
                                        ch3 = 0;
                                        break;
                                    case 2:
                                        Console.WriteLine("witch drone you want to view?");
                                        ID = giveNumber();
                                        temp.drone_view(ID);
                                        ch3 = 0;
                                        break;
                                    case 3:
                                        Console.WriteLine("witch customer you want to view?");
                                        ID = giveNumber();
                                        temp.customer_view(ID);
                                        ch3 = 0;
                                        break;
                                    case 4:
                                        Console.WriteLine("witch parcel you want to view?");
                                        ID = giveNumber();
                                        temp.parcel_view(ID);
                                        ch3 = 0;
                                        break;
                                    default:
                                        Console.WriteLine("Error selecting number");
                                        break;
                                }
                            } while (ch3 != 0);
                            break;
                        case 4:
                            Console.WriteLine("Choose one of the following:");
                            do
                            {
                                temp.List_view_options();

                                ch4 = giveNumber();
                                switch (ch4)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        foreach (IBL.BO.StationForTheList item in temp.Displays_a_list_of_base_stations())
                                        {
                                            Console.WriteLine(item.ToString());
                                        }
                                        break;
                                    case 2:
                                        foreach (IBL.BO.DroneToList item in temp.Displays_the_list_of_drones())
                                    {
                                            Console.WriteLine(temp.ToString());
                                        }
                                        break;
                                    case 3:
                                        foreach (IBL.BO.CustomerToList item in temp.Displays_a_list_of_customers())
                                        {
                                            Console.WriteLine(temp.ToString());
                                        }
                                        break;
                                    case 4:
                                        foreach (IBL.BO.ParcelToList item in temp.Displays_the_list_of_Parcels())
                                        {
                                            Console.WriteLine(temp.ToString());
                                        }
                                        break;
                                    case 5:
                                        foreach (IBL.BO.ParcelToList item in temp.Displays_a_list_of_Parcels_not_yet_associated_with_the_drone())
                                        {
                                            Console.WriteLine(temp.ToString());
                                        }
                                        break;
                                    case 6:
                                        foreach (IBL.BO.StationForTheList item in temp.Display_of_base_stations_with_available_charging_stations())
                                        {
                                            Console.WriteLine(temp.ToString());
                                        }
                                        break;
                                    default:
                                        Console.WriteLine("Error selecting number");
                                        break;
                                }
                            } while (ch4 != 0);
                            break;
                    }

                }
                catch (Exception e)
                {

                    Console.WriteLine(e);
                }
            } while (ch != 5);
        }
    }
}
