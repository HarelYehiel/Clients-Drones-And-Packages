using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    class functionCase1
    {
        DO.IDal temp = DalApi.DalFactory.GetDal("s");
        public void addStation()
        {
            DO.Station item = new DO.Station();
            Console.WriteLine("enter drone-station ID:(5 digits)");
            item.id = Convert.ToInt32(Console.ReadLine());//user set id
            Console.WriteLine("enter drone-station name:");
            item.name = Console.ReadLine();//user input name
            DO.Point p = new DO.Point();
            Console.WriteLine("enter Latitude:");
            p.latitude = Convert.ToDouble(Console.ReadLine());//user input Latitude
            Console.WriteLine("enter Longitude:");
            p.longitude = Convert.ToDouble(Console.ReadLine());//user input Longitude
            item.Location = p;
            item.ChargeSlots = 10;//all station have only 10 charge slots 
            temp.InputTheStationToArray(item);//this function is using at the data base so she must to be in DAL project
          
        }
        public void addParcel1()
        {

            DO.Parcel par = new DO.Parcel();
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
            par.weight = (DO.Enum.WeightCategories)parWeight;
            Console.WriteLine("enter priority:\nNormal = 0, Fast = 1, Emergency =2");
            int parPriority = Convert.ToInt32(Console.ReadLine());
            par.priority = (DO.Enum.Priorities)parPriority;
            par.Requested = DateTime.Now;//the requestsd time is now
            //par.Scheduled = par.Requested.AddMinutes(5);//the parcel find drone at 5 minutes
            temp.InputTheParcelToArray(par);
        }

        public void addCustomer()
        {

            DO.Customer cust = new DO.Customer();
            Console.WriteLine("enter customer ID:(5 digits)");
            cust.Id = Convert.ToInt32(Console.ReadLine());//user set id

            Console.WriteLine("enter customer name:");
            cust.name = Console.ReadLine();//user input name

            Console.WriteLine("enter phone number:");
            cust.phone = Console.ReadLine();//user set phone number

            Console.WriteLine("enter Latitude:");
            DO.Point P = new DO.Point();
            P.latitude = Convert.ToDouble(Console.ReadLine());//user input Latitude
            Console.WriteLine("enter Longitude:");
            P.longitude = Convert.ToDouble(Console.ReadLine());//user input Longitude
            cust.location = P;
            temp.InputTheCustomerToArray(cust);
        }

        public void addDrone()
        {
            DO.Drone dro = new DO.Drone();
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
            dro.MaxWeight = (DO.Enum.WeightCategories)weightChoose;//convert the choose to WeightCategories
            temp.InputTheDroneToArray(dro);
            
        }
    }
}
