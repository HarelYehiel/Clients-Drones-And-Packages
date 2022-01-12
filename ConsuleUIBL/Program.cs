using System;
using System.Collections.Generic;
using BO;
namespace ConsuleUIBL
{
    class Program
    {
        public static int giveNumber()
        {
            string s;
            BlApi.IBL temp = BlApi.BlFactory.GetBl();
            do
            {
                s = Console.ReadLine();
                if (temp.IsDigitsOnly(s))
                    return Convert.ToInt32(s);
                Console.WriteLine(MyExeption_BO.Only_numbers_should_be_type_to + "\nGive number");
            } while (true);
        }
        public static int giveNumberRang()
        {
            string s;
            BlApi.IBL temp = BlApi.BlFactory.GetBl();

            do
            {
                s = Console.ReadLine();
                if (temp.IsDigitsOnly(s) && (s == "0" || s == "1" || s == "2"))
                    return Convert.ToInt32(s);
                Console.WriteLine(MyExeption_BO.Only_numbers_should_be_type_to + "\nGive number");
            } while (true);
        }
        static void Main(string[] args)
        {
            BlApi.IBL temp = BlApi.BlFactory.GetBl();
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
                    Console.WriteLine("press 5 to exit:");
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
                                        temp.AddingCustomer(ID, nameCu, phoneNumber, Latitude, Longitude);
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
                                        temp.AddingParcel(parcelID,senderID, targetID, maxWeight, prioerity);
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
                                        temp.ReleaseDroneFromCharging(ID, DateTime.Now);
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
                                        Console.WriteLine(temp.getBaseStation(ID).ToString());
                                        ch3 = 0;
                                        break;
                                    case 2:
                                        Console.WriteLine("witch drone you want to view?");
                                        ID = giveNumber();
                                        Console.WriteLine(temp.GetDrone(ID).ToString());
                                        ch3 = 0;
                                        break;
                                    case 3:
                                        Console.WriteLine("witch customer you want to view?");
                                        ID = giveNumber();
                                        Console.WriteLine(temp.GetCustomer(ID).ToString());
                                        ch3 = 0;
                                        break;
                                    case 4:
                                        Console.WriteLine("witch parcel you want to view?");
                                        ID = giveNumber();
                                        Console.WriteLine(temp.GetParcel(ID).ToString());
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
                                        foreach (StationToTheList item in temp.GetListOfBaseStations())
                                        {
                                            Console.WriteLine(item.ToString());
                                        }
                                        break;
                                    case 2:
                                        foreach (DroneToList item in temp.GetTheListOfDrones())
                                    {
                                            Console.WriteLine(item.ToString());
                                        }
                                        break;
                                    case 3:
                                        IEnumerable<CustomerToList> customersToList = new List<CustomerToList>();
                                        customersToList = temp.GetListOfCustomers();
                                        foreach (CustomerToList item in customersToList)
                                        {
                                            Console.WriteLine(item.ToString());
                                        }
                                        break;
                                    case 4:
                                        foreach (ParcelToList item in temp.GetTheListOfParcels())
                                        {
                                            Console.WriteLine(item.ToString());
                                        }
                                        break;
                                    case 5:
                                        foreach (ParcelToList item in temp.GetAllParcelsBy(parcel => parcel.DroneId == 0))
                                        {
                                            Console.WriteLine(item.ToString());
                                        }
                                        break;
                                    case 6:
                                        foreach (StationToTheList item in temp.GetAllStaionsBy(station_DO => station_DO.ChargeSlots > 0))
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
press 1 to add new object:
press 2 to update object properties:
press 3 to see object properties:
press 4 to see lists of  objects:
press 5 to exit:
1
Choose one of the following:
press 0 to back
press 1 to add a new drone-staition
press 2 to add a new drone
press 3 to add a new customer
press 4 to add a new parcel
Choose one of the following:
1
enter station ID(5 digit):
11111
enter name of station:
station1
enter locaition:
31.123456
34.123456
enter number of charge slots:
15
press 1 to add new object:
press 2 to update object properties:
press 3 to see object properties:
press 4 to see lists of  objects:
press 5 to exit:
1
Choose one of the following:
press 0 to back
press 1 to add a new drone-staition
press 2 to add a new drone
press 3 to add a new customer
press 4 to add a new parcel
Choose one of the following:
2
enter drone ID(5 digit):
22222
enter model of drone:
DR1
enter weight:
1 = Light, 2 = Medium, 3 = Heavy
1
enter number of station you wnat to put the drone:
11111
press 1 to add new object:
press 2 to update object properties:
press 3 to see object properties:
press 4 to see lists of  objects:
press 5 to exit:
3
Choose one of the following:
press 0 to back
press 1 to Station View
press 2 to drone View
press 3 to Customer View
press 4 to parcel View
2
witch drone you want to view?
22222
Drone ID: 22222, model: DR1,  Status: Baintenance, battery = 26
weight: Medium, location: longitude: 34.123456, latitude: 31.123456
press 1 to add new object:
press 2 to update object properties:
press 3 to see object properties:
press 4 to see lists of  objects:
press 5 to exit:
1
Choose one of the following:
press 0 to back
press 1 to add a new drone-staition
press 2 to add a new drone
press 3 to add a new customer
press 4 to add a new parcel
Choose one of the following:
3
enter customer ID(5 digit):
33333
enter name of customer:
Harel
enter phone number:
0523607900
enter customer location:
31.258963
34.896523
press 1 to add new object:
press 2 to update object properties:
press 3 to see object properties:
press 4 to see lists of  objects:
press 5 to exit:
From Me to Everyone 09:14 PM
System.Exception: Only numbers should be type to.
Give number
31.258963
System.Exception: Only numbers should be type to.
Give number
34.896523
System.Exception: Only numbers should be type to.
Give number
1
Choose one of the following:
press 0 to back
press 1 to add a new drone-staition
press 2 to add a new drone
press 3 to add a new customer
press 4 to add a new parcel
Choose one of the following:
3
enter customer ID(5 digit):
44444
enter name of customer:
yoni
enter phone number:
058258258
enter customer location:
31.147852
34.125478
press 1 to add new object:
press 2 to update object properties:
press 3 to see object properties:
press 4 to see lists of  objects:
press 5 to exit:
1
Choose one of the following:
press 0 to back
press 1 to add a new drone-staition
press 2 to add a new drone
press 3 to add a new customer
press 4 to add a new parcel
Choose one of the following:
4
enter parcel id(ID - 5 digit):
55555
enter sender id(ID - 5 digit):
33333
enter target id(ID  - 5 digit):
44444
enter weight:
0 = Light, 1 = Medium, 2 = Heavy
1
enter prioerity:
 0 = Normal, 1 = Fast, 2 = Emergency
1
press 1 to add new object:
press 2 to update object properties:
press 3 to see object properties:
press 4 to see lists of  objects:
press 5 to exit:
2
Choose one of the following:
Choose one of the following:
press 0 to back
press 1 to update drone
press 2 to update station
press 3 to update customer
press 4 to send drone to charge at station
press 5 to send drone from charge in station
press 6 to assign drone to parcel
press 7 to update picked up parcel by drone
press 8 to update delivered parcel by drone
1
witch drone you want to update?(ID)
22222
what is the new name?
DR2
press 1 to add new object:
press 2 to update object properties:
press 3 to see object properties:
press 4 to see lists of  objects:
press 5 to exit:
2
Choose one of the following:
Choose one of the following:
press 0 to back
press 1 to update drone
press 2 to update station
press 3 to update customer
press 4 to send drone to charge at station
press 5 to send drone from charge in station
press 6 to assign drone to parcel
press 7 to update picked up parcel by drone
press 8 to update delivered parcel by drone
2
witch station you want to update?(ID)
stationNew
System.Exception: Only numbers should be type to.
Give number
12
do you want to update the name of station? if not press enter
2
do you want to update the number of slots? if not press 0
3
IBL.BO.MyExeption_BO: Exception from function 'Update_station_data
 ---> IDAL.DO.myExceptionDO: Exception from function GetStation
 ---> System.Exception: There is no variable with this ID.
   --- End of inner exception stack trace ---
   at IDAL.DalObject.DalObject.GetStation(Int32 stationId) in C:\Users\POSEIDON\source\repos\mini project\HarelYehiel\Course-mini-project\ClassLibrary1\DalObject.cs:line 48
   at IBL.BL.UpdateStationData(Int32 ID, String name, Int32 numSlots) in C:\Users\POSEIDON\source\repos\mini project\HarelYehiel\Course-mini-project\BL\BLCase2.cs:line 37
   --- End of inner exception stack trace ---
   at IBL.BL.UpdateStationData(Int32 ID, String name, Int32 numSlots) in C:\Users\POSEIDON\source\repos\mini project\HarelYehiel\Course-mini-project\BL\BLCase2.cs:line 47
   at ConsuleUIBL.Program.Main(String[] args) in C:\Users\POSEIDON\source\repos\mini project\HarelYehiel\Course-mini-project\ConsuleUIBL\Program.cs:line 138
press 1 to add new object:
press 2 to update object properties:
press 3 to see object properties:
press 4 to see lists of  objects:
press 5 to exit:
33333
press 1 to add new object:
press 2 to update object properties:
press 3 to see object properties:
press 4 to see lists of  objects:
press 5 to exit:
yosi
System.Exception: Only numbers should be type to.
Give number
029245869
press 1 to add new object:
press 2 to update object properties:
press 3 to see object properties:
press 4 to see lists of  objects:
press 5 to exit:
2
Choose one of the following:
Choose one of the following:
press 0 to back
press 1 to update drone
press 2 to update station
press 3 to update customer
press 4 to send drone to charge at station
press 5 to send drone from charge in station
press 6 to assign drone to parcel
press 7 to update picked up parcel by drone
press 8 to update delivered parcel by drone
4
witch drone you want to charge?(ID)
22222
press 1 to add new object:
press 2 to update object properties:
press 3 to see object properties:
press 4 to see lists of  objects:
press 5 to exit:
3
Choose one of the following:
press 0 to back
press 1 to Station View
press 2 to drone View
press 3 to Customer View
press 4 to parcel View
2
witch drone you want to view?
22222
Drone ID: 22222, model: DR2,  Status: Baintenance, battery = 26
weight: Medium, location: longitude: 34.123456, latitude: 31.123456
press 1 to add new object:
press 2 to update object properties:
press 3 to see object properties:
press 4 to see lists of  objects:
press 5 to exit:
2
Choose one of the following:
Choose one of the following:
press 0 to back
press 1 to update drone
press 2 to update station
press 3 to update customer
press 4 to send drone to charge at station
press 5 to send drone from charge in station
press 6 to assign drone to parcel
press 7 to update picked up parcel by drone
press 8 to update delivered parcel by drone
5
witch drone you want to release from charge?(ID)
how many time?(minuets)
22222
30
press 1 to add new object:
press 2 to update object properties:
press 3 to see object properties:
press 4 to see lists of  objects:
press 5 to exit:
2
Choose one of the following:
Choose one of the following:
press 0 to back
press 1 to update drone
press 2 to update station
press 3 to update customer
press 4 to send drone to charge at station
press 5 to send drone from charge in station
press 6 to assign drone to parcel
press 7 to update picked up parcel by drone
press 8 to update delivered parcel by drone
6
witch drone you want to get the parcel?(ID)
22222
press 1 to add new object:
press 2 to update object properties:
press 3 to see object properties:
press 4 to see lists of  objects:
press 5 to exit:
3
Choose one of the following:
press 0 to back
press 1 to Station View
press 2 to drone View
press 3 to Customer View
press 4 to parcel View
2
witch drone you want to view?
22222
Drone ID: 22222, model: DR2,  Status: Delivery, battery = 56
weight: Medium, location: longitude: 34.123456, latitude: 31.123456, parcelId: 81223
press 1 to add new object:
press 2 to update object properties:
press 3 to see object properties:
press 4 to see lists of  objects:
press 5 to exit:
2
Choose one of the following:
Choose one of the following:
press 0 to back
press 1 to update drone
press 2 to update station
press 3 to update customer
press 4 to send drone to charge at station
press 5 to send drone from charge in station
press 6 to assign drone to parcel
press 7 to update picked up parcel by drone
press 8 to update delivered parcel by drone
7
witch drone pickedUp the parcel?(ID)
22222
press 1 to add new object:
press 2 to update object properties:
press 3 to see object properties:
press 4 to see lists of  objects:
press 5 to exit:
2
Choose one of the following:
Choose one of the following:
press 0 to back
press 1 to update drone
press 2 to update station
press 3 to update customer
press 4 to send drone to charge at station
press 5 to send drone from charge in station
press 6 to assign drone to parcel
press 7 to update picked up parcel by drone
press 8 to update delivered parcel by drone
8
witch drone delivered the parcel?(ID)
22222
press 1 to add new object:
press 2 to update object properties:
press 3 to see object properties:
press 4 to see lists of  objects:
press 5 to exit:
3
Choose one of the following:
press 0 to back
press 1 to Station View
press 2 to drone View
press 3 to Customer View
press 4 to parcel View
4
witch parcel you want to view?
81223
Parcel ID: 81223
 sender: Customer ID: 38541, name: cust70
 target: Customer ID: 31259, name: cust30
drone: 22222
priority: Fast, weight: Medium
 requested: 02/05/2021 02:44:00, scheduled = 18/11/2021 14:36:31, picked up = 18/11/2021 14:36:35, delivered = 18/11/2021 14:36:43
press 1 to add new object:
press 2 to update object properties:
press 3 to see object properties:
press 4 to see lists of  objects:
press 5 to exit:
5

C:\Users\POSEIDON\source\repos\mini project\HarelYehiel\Course-mini-project\ConsuleUIBL\bin\Debug\net5.0\ConsuleUI_BL.exe (process 20476) exited with code 0.
To automatically close the console when debugging stops, enable Tools->Options->Debugging->Automatically close the console when debugging stops.
Press any key to close this window . . .

 */
