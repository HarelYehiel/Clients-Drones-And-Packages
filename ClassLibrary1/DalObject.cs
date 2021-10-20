using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace IDAL
{
    namespace DalObject
    {
        public struct DalObject
        {
            public DalObject(int x = 0)
            {
                DataSource.Initialize();
            }
            public static ref station GetStation(int stationId)
            // Return the station with stationId
            {
                for (int i = 0; i < DataSource.Cofing.stationIndex; i++)
                {
                    if (DataSource.stations[i].Id == stationId)
                        return ref DataSource.stations[i];

                }
                Exception e11 = new Exception("tt");
                throw e11;
            }
            public static ref Drone GetDrone(int droneId)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (DataSource.drones[i].Id == droneId)
                        return ref DataSource.drones[i];

                }
                Exception e11 = new Exception("tt");
                throw e11;
            }
            public static ref Customer GetCustomer(int CustomerId)
            // Return the customer with customerId
            {
                for (int i = 0; i < DataSource.Cofing.customersIndex; i++)
                {
                    if (DataSource.customers[i].Id == CustomerId)
                        return ref DataSource.customers[i];

                }
                Exception e11 = new Exception("tt");
                throw e11;
            }
            public static ref Parcel GetParcel(int ParcelId)
            // Return the parcel with parcelId
            {
                for (int i = 0; i < DataSource.Cofing.parcelsIndex; i++)
                {
                    if (DataSource.parcels[i].Id == ParcelId)
                        return ref DataSource.parcels[i];

                }
                Exception e11 = new Exception("tt");
                throw e11;
            }
            public static int addParcel()
            {
                int parcelNumber = ++DataSource.Cofing.runNumber;
                return parcelNumber;
            }

            public static station[] Displays_list_of_stations()
            //Copy all the station from DataSource.stations[] to new_array_stations.
            {
                station[] new_array_stations = new station[DataSource.Cofing.stationIndex];
                for (int i = 0; i < DataSource.Cofing.stationIndex; i++)
                {
                    new_array_stations[i] = DataSource.stations[i];
                }
                return new_array_stations;
            }
            public static Customer[] Displays_list_of_custmers()
            //Copy all the customer from DataSource.customers[] to new_array_custmers.
            {
                Customer[] new_array_custmers = new Customer[DataSource.Cofing.customersIndex];
                for (int i = 0; i < DataSource.Cofing.customersIndex; i++)
                {
                    new_array_custmers[i] = DataSource.customers[i];
                }
                return new_array_custmers;
            }
            public static Parcel[] Displays_list_of_Parcels()
            //Copy all the Parcel from DataSource.parcels[] to new_array_parcels.
            {
                Parcel[] new_array_Parcels = new Parcel[DataSource.Cofing.parcelsIndex];
                for (int i = 0; i < DataSource.Cofing.parcelsIndex; i++)
                {
                    new_array_Parcels[i] = DataSource.parcels[i];
                }
                return new_array_Parcels;
            }
            public static Drone[] Displays_list_of_drone()
            //Copy all the Drone from DataSource.drones[] to new_array_drones.
            {
                Drone[] new_array_Drones = new Drone[DataSource.Cofing.droneIndex];
                for (int i = 0; i < DataSource.Cofing.droneIndex; i++)
                {
                    new_array_Drones[i] = DataSource.drones[i];
                }
                return new_array_Drones;
            }
            public static void displaysParcelsDontHaveDrone()
            // Print the details of all the parcels don't have An associated skimmer (Selected_drone == 0).
            {
                for (int i = 0; i < DataSource.parcels.Length; i++)
                {
                    if (DataSource.parcels[i].Id != 0 && DataSource.parcels[i].DroneId == 0)
                        Console.WriteLine(DataSource.parcels[i].ToString()
                            );
                }
            }
            public static void AvailableChargingStations()
            //Print the all stations that have DroneCharge available
            {
                for (int i = 0; i < DataSource.Cofing.stationIndex; i++)
                {
                    if (DataSource.stations[i].ChargeSlots != 0)
                        Console.WriteLine(DataSource.stations[i].ToString());
                }
            }

            public static void addStation()
            {
                IDAL.DO.station sta = new IDAL.DO.station();
               
                Console.WriteLine("enter drone-station ID:(5 digits)");
                sta.Id = Convert.ToInt32(Console.ReadLine());//user set id
                
                
                Console.WriteLine("enter drone-station name:");
                sta.name = Console.ReadLine();//user input name

                Point p = new Point();
                Console.WriteLine("enter Latitude:");
                p.Latitude = Convert.ToDouble(Console.ReadLine());//user input Latitude

                Console.WriteLine("enter Longitude:");
                p.Longitude = Convert.ToDouble(Console.ReadLine());//user input Longitude

                sta.Location = p;

                sta.ChargeSlots = 10;//all station have only 10 charge slots 
                DataSource.stations[DataSource.Cofing.stationIndex] = sta;
                DataSource.Cofing.stationIndex++;//update the num of free cell in the array
            }
            public static void addParcel1()
            {

                IDAL.DO.Parcel par = new IDAL.DO.Parcel();
                Console.WriteLine("enter parcel ID(5 digits):");
                int parId = Convert.ToInt32(Console.ReadLine());//user set id
                par.Id = parId;
                Console.WriteLine("enter sender ID(5 digits):");
                int senId = Convert.ToInt32(Console.ReadLine());//user set sendr id
                par.SenderId = senId;
                Console.WriteLine("enter target ID:(5 digits)");
                int tarId = Convert.ToInt32(Console.ReadLine());//user set target id
                par.TargetId = tarId;
                Console.WriteLine("enter drone ID:(5 digits)");
                int dronId = Convert.ToInt32(Console.ReadLine());//user set dron id
                par.DroneId = dronId;
                Console.WriteLine("enter weight:\n Light = 0, Medium = 1, Heavy = 2");//user set weight of parcel
                int parWeight = Convert.ToInt32(Console.ReadLine());
                par.Weight = (IDAL.DO.Enum.WeightCategories)parWeight;
                Console.WriteLine("enter priority:\n Normal = 0, Fast = 1, Emergency =2");
                int parPriority = Convert.ToInt32(Console.ReadLine());
                par.Priority = (IDAL.DO.Enum.Priorities)parPriority;
                par.Requested = DateTime.Now;//the requestsd time is now
                par.Scheduled = par.Requested.AddMinutes(5);//the parcel find drone at 5 minutes
                IDAL.DalObject.DataSource.parcels[DataSource.Cofing.parcelsIndex] = par;
                DataSource.Cofing.parcelsIndex++;//update the num of free cell in the array
                par.runNumber = addParcel();//update the run-number serial
            }
            public static void addCustomer()
            {

                IDAL.DO.Customer cust = new IDAL.DO.Customer();
                Console.WriteLine("enter customer ID:(5 digits)");
                cust.Id =  Convert.ToInt32(Console.ReadLine());//user set id

                Console.WriteLine("enter customer name:");
                cust.Name = Console.ReadLine();//user input name
                
                Console.WriteLine("enter phone number:");
                cust.Phone = Console.ReadLine();//user set phone number
                
                Console.WriteLine("enter Latitude:");
                Point P = new Point();
                P.Latitude = Convert.ToDouble(Console.ReadLine());//user input Latitude
                Console.WriteLine("enter Longitude:");
                P.Longitude = Convert.ToDouble(Console.ReadLine());//user input Longitude

                cust.Location = P;

                IDAL.DalObject.DataSource.customers[DataSource.Cofing.customersIndex] = cust;
                DataSource.Cofing.customersIndex++;//update the num of free cell in the array
            }
            public static void addDrone()
            {
                IDAL.DO.Drone dro = new IDAL.DO.Drone();
                Console.WriteLine("enter drone ID:(5 digits)");
                int droId = Convert.ToInt32(Console.ReadLine());//user set id
                dro.Id = droId;
                Console.WriteLine("enter drone battery status:");
                double battery = Convert.ToDouble(Console.ReadLine());//user set Battery status
                dro.Battery = battery;
                Console.WriteLine("enter drone model:");
                string model = Console.ReadLine();//user input Model
                dro.Model = model;
                Console.WriteLine("enter drone weight:\n Light = 0, Medium = 1, Heavy = 2");
                int weightChoose = Convert.ToInt32(Console.ReadLine());//user input weight
                dro.MaxWeight = (IDAL.DO.Enum.WeightCategories)weightChoose;//convert the choose to WeightCategories
                dro.Status = (IDAL.DO.Enum.DroneStatus)0;//in crate drone the status is avilble
                IDAL.DalObject.DataSource.drones[DataSource.Cofing.droneIndex] = dro;
                DataSource.Cofing.droneIndex++;//update the num of free cell in the array
            }
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
                station statioID = new station();
                statioID.Id = dro.stationOfCharge.staitionId;
                statioID = GetStation(statioID.Id);
                if (statioID.ChargeSlots == 10)//the defolt charge slots is 10, if 10 is free so the station is empty
                    Console.WriteLine("this station is empty, no drone is cherging here:");
                else
                {
                    statioID.ChargeSlots++;
                    DroneCharge charge = new DroneCharge();
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
                IDAL.DO.DroneCharge charge = new DroneCharge();//crate new object type DroneChrge
                charge.DroneId = drId;
                charge.staitionId = statId;
                dron.stationOfCharge = charge;//after update the data of DroneCharge update the informaition in the dron
                ref IDAL.DO.station statioId = ref IDAL.DalObject.DalObject.GetStation(statId);
                if (statioId.ChargeSlots > 0)//if the station that the user choose is  free
                    statioId.ChargeSlots--;
                else
                    Console.WriteLine("this station is not vailable:");
            }
            public static void chooseObjectToconvert()
            {
                Console.WriteLine("witch cordinate you want to convert? ");
                IDAL.DO.Point point = new Point();
                point.Latitude = Convert.ToDouble(Console.ReadLine());
                point.Longitude = Convert.ToDouble(Console.ReadLine());
                string newLat = IDAL.DO.Point.convertLatitudeToDegree(point);
                string newLon = IDAL.DO.Point.convertLongitudeToDegree(point);
                Console.WriteLine("the new Latitude is: " + newLat);
                Console.WriteLine("the new Longitude is: " + newLon);

            }

            public static void distanceFromCustomerOrStation()
            /*Receives coordinates of any point from the user and prints distance
            from any base or client to that point.*/
            {
                Console.WriteLine("Type Latitude and Longitude");
                Point p = new Point();
                p.Latitude = Convert.ToDouble(Console.ReadLine());
                p.Longitude = Convert.ToDouble(Console.ReadLine());
                int choose;
                
                do
                {
                    
                    Console.WriteLine("Choose one of the following:" +
                                        "\n1 = distance from customer" +
                                        "\n2 = distance from station");
                    choose = Convert.ToInt32(Console.ReadLine());
                    double minDistance = DataSource.customers[0].Location.distancePointToPoint(p);
                    int saveTheI = 0;// save the index with minimum destance from the point p
                    switch (choose)
                    {
                        case 1: 
                            for (int i = 1; i < DataSource.Cofing.customersIndex; i++)
                            {
                                double distance = DataSource.customers[i].Location.distancePointToPoint(p);
                                if (minDistance > distance)
                                {
                                    saveTheI = i;
                                    minDistance = distance;
                                }
                            }
                            Console.WriteLine("The minimum distancefrom the point is: {0}" +
                                "\nThe id of customer is: {1}",minDistance, DataSource.customers[saveTheI].Id);

                            return;

                        case 2:
                            for (int i = 0; i < DataSource.Cofing.stationIndex; i++)
                            {
                                double distance = DataSource.stations[i].Location.distancePointToPoint(p);
                                if (minDistance > distance)
                                {
                                    saveTheI = i;
                                    minDistance = distance;
                                }
                            }
                            Console.WriteLine("The minimum distance from the point is: {0}" +
                                "\nThe id of station is: {1}", minDistance, DataSource.stations[saveTheI].Id);
                            return;

                        default:                            
                            break;
                    }
                } while (choose != 1 && choose != 2);
                Exception e11 = new Exception("Error in function distanceFromCustomerOrStation");
                throw e11; 

            }
        }
    }
}
