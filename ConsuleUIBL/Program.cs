using System;
using System.Collections.Generic;
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
                Console.WriteLine(IBL.BO.MyExeption_BO.Only_numbers_should_be_type_to + "\nGive number");
            } while (true);
        }public static int giveNumberRang()
        {
            string s;
            IBL.BL temp = new IBL.BL();

            do
            {
                s = Console.ReadLine();
                if (temp.IsDigitsOnly(s) && (s == "0" || s == "1" || s == "2"))
                    return Convert.ToInt32(s);
                Console.WriteLine(IBL.BO.MyExeption_BO.Only_numbers_should_be_type_to + "\nGive number");
            } while (true);
        }
        static void Main(string[] args)
        {
            IBL.BL temp = new IBL.BL();
            int ch = 0, ch1, ch2, ch3, ch4;
            temp.InitializeAndUpdateTheListsInIBL(); // Do initialize if data sourse and update the list listDrons of IBL.

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
                    ch = giveNumber();
                    switch (ch)
                    {
                        case 1:
                            Console.WriteLine("Choose one of the following:");
                            do
                            {
                                temp.InsertOptions();
                                ch1 = Convert.ToInt32(Console.ReadLine());
                                int ID = 0;
                                switch (ch1)
                                {
                                    case 1:
                                        Console.WriteLine("enter station ID(5 digit):");
                                        ID = giveNumber();
                                        Console.WriteLine("enter name of station:");
                                        string name = Console.ReadLine();
                                        Console.WriteLine("enter locaition:");
                                        double Latitude = Convert.ToDouble(Console.ReadLine());
                                        double Longitude = Convert.ToDouble(Console.ReadLine());
                                        Console.WriteLine("enter number of charge slots:");
                                        int numSlots = Convert.ToInt32(Console.ReadLine());
                                        temp.AddingBaseStation(ID, name, Latitude, Longitude, numSlots);
                                        break;

                                    case 2:
                                        Console.WriteLine("enter drone ID(5 digit):");
                                        ID = giveNumber();
                                        Console.WriteLine("enter model of drone:");
                                        string model = Console.ReadLine();
                                        Console.WriteLine("enter weight: \n1 = Light, 2 = Medium, 3 = Heavy");
                                        int maxWeight = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("enter number of station you wnat to put the drone:");
                                        int staId = Convert.ToInt32(Console.ReadLine());
                                        temp.AddingDrone(ID, model, maxWeight, staId);
                                        break;

                                    case 3:
                                        Console.WriteLine("enter customer ID(5 digit):");
                                        ID = giveNumber();
                                        Console.WriteLine("enter name of customer:");
                                        string nameCu = Console.ReadLine();
                                        Console.WriteLine("enter phone number:");
                                        string phoneNumber = Console.ReadLine();
                                        Console.WriteLine("enter customer location:");
                                        Latitude = Convert.ToDouble(Console.ReadLine());
                                        Longitude = Convert.ToDouble(Console.ReadLine());
                                        temp.AbsorptionNewCustomer(ID, nameCu, phoneNumber, Latitude, Longitude);
                                        break;

                                    case 4:
                                        Console.WriteLine("enter parcel id(ID - 5 digit):");
                                        int parcelID = giveNumber();
                                        Console.WriteLine("enter sender id(ID - 5 digit):");
                                        int senderID = giveNumber();
                                        Console.WriteLine("enter target id(ID  - 5 digit):");
                                        int targetID = giveNumber();
                                        Console.WriteLine("enter weight: \n0 = Light, 1 = Medium, 2 = Heavy");
                                        maxWeight = giveNumberRang();
                                        Console.WriteLine("enter prioerity: \n 0 = Normal, 1 = Fast, 2 = Emergency");
                                        int prioerity = giveNumberRang();
                                        temp.ReceiptOfPackageForDelivery(parcelID,senderID, targetID, maxWeight, prioerity);
                                        break;
                                }
                                break;
                            } while (ch1 != 0);
                            break;
                        case 2:
                            Console.WriteLine("Choose one of the following:");

                            do
                            {
                                temp.UpdateOptions();
                                ch2 = Convert.ToInt32(Console.ReadLine());
                                int ID = 0;
                                switch (ch2)
                                {
                                    case 1:
                                        Console.WriteLine("witch drone you want to update?(ID)");
                                        ID = giveNumber();
                                        Console.WriteLine("what is the new name?");
                                        string newModel = Console.ReadLine();
                                        temp.UpdateDroneData(ID, newModel);
                                        break;
                                    case 2:
                                        Console.WriteLine("witch station you want to update?(ID)");
                                        ID = giveNumber();
                                        Console.WriteLine("do you want to update the name of station? if not press enter");
                                        string newName = Console.ReadLine();
                                        Console.WriteLine("do you want to update the number of slots? if not press 0");
                                        int numSlots = giveNumber();
                                        temp.UpdateStationData(ID, newName, numSlots);
                                        break;
                                    case 3:
                                        Console.WriteLine("witch customer you want to update?(ID)");
                                        ID = giveNumber();
                                        Console.WriteLine("do you want to update customer name? if not press enter");
                                        string custName = Console.ReadLine();
                                        Console.WriteLine("do you want to update customer phone number? if not press enter");
                                        string phoneNumber = Console.ReadLine();
                                        temp.UpdateCustomerData(ID, custName, phoneNumber);
                                        break;
                                    case 4:
                                        Console.WriteLine("witch drone you want to charge?(ID)");
                                        ID = giveNumber();
                                        temp.SendingDroneToCharging(ID);
                                        break;
                                    case 5:
                                        Console.WriteLine("witch drone you want to release from charge?(ID)");
                                        Console.WriteLine("how many time?(minuets)");
                                        ID = giveNumber();
                                        int min = Convert.ToInt32(Console.ReadLine());
                                        temp.ReleaseDroneFromCharging(ID, min);
                                        break;
                                    case 6:
                                        Console.WriteLine("witch drone you want to get the parcel?(ID)");
                                        ID = giveNumber();
                                        temp.AssignPackageToDrone(ID);
                                        break;
                                    case 7:
                                        Console.WriteLine("witch drone pickedUp the parcel?(ID)");
                                        ID = giveNumber();
                                        temp.CollectionOfPackageByDrone(ID);
                                        break;
                                    case 8:
                                        Console.WriteLine("witch drone delivered the parcel?(ID)");
                                        ID = giveNumber();
                                        temp.DeliveryOfPackageByDrone(ID);
                                        break;
                                }
                                break;
                            } while (ch2 != 0);
                            break;
                        case 3:
                            Console.WriteLine("Choose one of the following:");
                            do
                            {
                                temp.EntityDisplayOptions();
                                ch3 = giveNumber();
                                int ID = 0;

                                switch (ch3)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        Console.WriteLine("witch station you want to view?");
                                        ID = giveNumber();
                                        Console.WriteLine(temp.BaseStationView(ID).ToString());
                                        ch3 = 0;
                                        break;
                                    case 2:
                                        Console.WriteLine("witch drone you want to view?");
                                        ID = giveNumber();
                                        Console.WriteLine(temp.DroneView(ID).ToString());
                                        ch3 = 0;
                                        break;
                                    case 3:
                                        Console.WriteLine("witch customer you want to view?");
                                        ID = giveNumber();
                                        Console.WriteLine(temp.CustomerView(ID).ToString());
                                        ch3 = 0;
                                        break;
                                    case 4:
                                        Console.WriteLine("witch parcel you want to view?");
                                        ID = giveNumber();
                                        Console.WriteLine(temp.ParcelView(ID).ToString());
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
                                temp.ListViewOptions();

                                ch4 = giveNumber();
                                switch (ch4)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        foreach (IBL.BO.StationForTheList item in temp.DisplaysListOfBaseStations())
                                        {
                                            Console.WriteLine(item.ToString());
                                        }
                                        break;
                                    case 2:
                                        foreach (IBL.BO.DroneToList item in temp.DisplaysTheListOfDrones())
                                    {
                                            Console.WriteLine(item.ToString());
                                        }
                                        break;
                                    case 3:
                                        IEnumerable<IBL.BO.CustomerToList> customersToList = new List<IBL.BO.CustomerToList>();
                                        customersToList = temp.DisplaysListOfCustomers();
                                        foreach (IBL.BO.CustomerToList item in customersToList)
                                        {
                                            Console.WriteLine(item.ToString());
                                        }
                                        break;
                                    case 4:
                                        foreach (IBL.BO.ParcelToList item in temp.DisplaysTheListOfParcels())
                                        {
                                            Console.WriteLine(item.ToString());
                                        }
                                        break;
                                    case 5:
                                        foreach (IBL.BO.ParcelToList item in temp.DisplaysListOfParcelsNotYetAssociatedWithDrone())
                                        {
                                            Console.WriteLine(item.ToString());
                                        }
                                        break;
                                    case 6:
                                        foreach (IBL.BO.StationForTheList item in temp.DisplayBaseStationsWithAvailableChargingStations())
                                        {
                                            Console.WriteLine(item.ToString());
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
/*


 */
