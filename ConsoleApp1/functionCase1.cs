using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    class functionCase1
    {

        public static void addStation()
        {
            IDAL.DO.station item = new IDAL.DO.station();
            Console.WriteLine("enter drone-station ID:(5 digits)");
            item.Id = Convert.ToInt32(Console.ReadLine());//user set id
            Console.WriteLine("enter drone-station name:");
            item.name = Console.ReadLine();//user input name
            IDAL.DO.Point p = new IDAL.DO.Point();
            Console.WriteLine("enter Latitude:");
            p.Latitude = Convert.ToDouble(Console.ReadLine());//user input Latitude
            Console.WriteLine("enter Longitude:");
            p.Longitude = Convert.ToDouble(Console.ReadLine());//user input Longitude
            item.Location = p;
            item.ChargeSlots = 10;//all station have only 10 charge slots 
            IDAL.DalObject.DalObject.inputTheStationToArray(item);//this function is using at the data base so she must to be in DAL project
          
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
            Console.WriteLine("enter weight:\nLight = 0, Medium = 1, Heavy = 2");//user set weight of parcel
            int parWeight = Convert.ToInt32(Console.ReadLine());
            par.Weight = (IDAL.DO.Enum.WeightCategories)parWeight;
            Console.WriteLine("enter priority:\nNormal = 0, Fast = 1, Emergency =2");
            int parPriority = Convert.ToInt32(Console.ReadLine());
            par.Priority = (IDAL.DO.Enum.Priorities)parPriority;
            par.Requested = DateTime.Now;//the requestsd time is now
            par.Scheduled = par.Requested.AddMinutes(5);//the parcel find drone at 5 minutes
            IDAL.DalObject.DalObject.inputTheParcelToArray(par);
        }

        public static void addCustomer()
        {

            IDAL.DO.Customer cust = new IDAL.DO.Customer();
            Console.WriteLine("enter customer ID:(5 digits)");
            cust.Id = Convert.ToInt32(Console.ReadLine());//user set id

            Console.WriteLine("enter customer name:");
            cust.Name = Console.ReadLine();//user input name

            Console.WriteLine("enter phone number:");
            cust.Phone = Console.ReadLine();//user set phone number

            Console.WriteLine("enter Latitude:");
            IDAL.DO.Point P = new IDAL.DO.Point();
            P.Latitude = Convert.ToDouble(Console.ReadLine());//user input Latitude
            Console.WriteLine("enter Longitude:");
            P.Longitude = Convert.ToDouble(Console.ReadLine());//user input Longitude
            cust.Location = P;
            IDAL.DalObject.DalObject.inputTheCustomerToArray(cust);
        }

        public static void addDrone()
        {
            IDAL.DO.Drone dro = new IDAL.DO.Drone();
            Console.WriteLine("enter drone ID:(5 digits)");
            int droId = Convert.ToInt32(Console.ReadLine());//user set id
            dro.Id = droId;
            Console.WriteLine("enter drone battery status:");
            double battery = Convert.ToDouble(Console.ReadLine());//user set Battery status
            Console.WriteLine("enter drone model:");
            string model = Console.ReadLine();//user input Model
            dro.Model = model;
            Console.WriteLine("enter drone weight:\nLight = 0, Medium = 1, Heavy = 2");
            int weightChoose = Convert.ToInt32(Console.ReadLine());//user input weight
            dro.MaxWeight = (IDAL.DO.Enum.WeightCategories)weightChoose;//convert the choose to WeightCategories
            IDAL.DalObject.DalObject.inputTheDroneToArray(dro);
            
        }
    }
}
