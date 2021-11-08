using System;

namespace ConsuleUIBL
{
    class Program
    {
        static void Main(string[] args)
        {
            IBL.BL temp = new IBL.BL();
            int ch,ch1,ch2,ch3,ch4;
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
                                    temp.Adding_a_base_station(ID, name, Latitude, Longitude,numSlots);
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
                                    temp.Adding_a_drone(ID,model,maxWeight,staId);
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
                                    temp.Absorption_of_a_new_customer(ID,nameCu,phoneNumber, Latitude, Longitude);
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
                                    temp.Receipt_of_package_for_delivery(senderName,targetName,maxWeight,prioerity);
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
                                    temp.Update_drone_data(ID,newModel);
                                    ///////////////////temp. לעדכן את הרחפן הספציפי לשם חדש
                                    break;
                                case 2:
                                    Console.WriteLine("witch station you want to update?(ID)");
                                    ID = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("what do you want to update? \n1 = name of station 2 = charge slots ");
                                    int choise = Convert.ToInt32(Console.ReadLine());
                                    while(choise == 1 || choise ==2)
                                    {
                                        temp.Update_station_data();
                                    }
                                    //////////////////////////לשלוח לפונקציה את הפרמטרים הרלוונטים
                                    break;
                                case 3:
                                    Console.WriteLine("witch customer you want to update?(ID)");
                                    ID = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("what do you want to update? \n1 = name of station 2 = charge slots ");
                                    int choiseC = Convert.ToInt32(Console.ReadLine());
                                    while (choiseC == 1 || choiseC == 2)
                                    {
                                        temp.Update_customer_data();
                                    }
                                    break;
                                case 4:
                                    Console.WriteLine("witch drone you want to charge?(ID)");
                                    ID = Convert.ToInt32(Console.ReadLine());
                                    temp.Sending_a_drone_for_charging();
                                    break;
                                case 5:
                                    Console.WriteLine("witch drone you want to release from charge?(ID)");
                                    Console.WriteLine("how many time?(minuets)");
                                    ID = Convert.ToInt32(Console.ReadLine());
                                    int min = Convert.ToInt32(Console.ReadLine());
                                    temp.Release_drone_from_charging();
                                    break;
                                case 6:
                                    Console.WriteLine("witch drone you want to get the parcel?(ID)");
                                    ID = Convert.ToInt32(Console.ReadLine());
                                    temp.Assign_a_package_to_a_drone();
                                    break;
                                case 7:
                                    Console.WriteLine("witch drone delivered the parcel?(ID)");
                                    ID = Convert.ToInt32(Console.ReadLine());
                                    temp.Delivery_of_a_package_by_drone();
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
                            ch3 = Convert.ToInt32(Console.ReadLine());
                            int ID = 0;
                            
                            switch (ch3)
                            {
                                case 1:
                                    Console.WriteLine("witch station you want to view?");
                                    ID = Convert.ToInt32(Console.ReadLine());
                                    temp.base_station_view();
                                    break;
                                case 2:
                                    Console.WriteLine("witch drone you want to view?");
                                    ID = Convert.ToInt32(Console.ReadLine());
                                    temp.drone_view();
                                    break;
                                case 3:
                                    Console.WriteLine("witch customer you want to view?");
                                    ID = Convert.ToInt32(Console.ReadLine());
                                    temp.customer_view();
                                    break;
                                case 4:
                                    Console.WriteLine("witch parcel you want to view?");
                                    ID = Convert.ToInt32(Console.ReadLine());
                                    temp.package_view();
                                    break;
                            }
                            break;
                        } while (ch3 != 0);
                        break;
                    case 4:
                        Console.WriteLine("Choose one of the following:");
                        do
                        {
                            temp.List_view_options();
                            ch4 = Convert.ToInt32(Console.ReadLine());
                            switch (ch4)
                            {
                                case 1:
                                    temp.Displays_a_list_of_base_stations();
                                    break;
                                case 2:
                                    temp.Displays_the_list_of_drones();
                                    break;
                                case 3:
                                    temp.Displays_a_list_of_information();
                                    break;
                                case 4:
                                    temp.Displays_the_list_of_packages();
                                    break;
                                case 5:
                                    temp.Displays_a_list_of_packages_not_yet_associated_with_the_drone();
                                    break;
                                case 6:
                                    temp.Display_of_base_stations_with_available_charging_stations();
                                    break;
                            }
                            break;
                        } while (ch4 != 0);
                        break;
                }

            } while (ch != 5    );
        }
    }
}
