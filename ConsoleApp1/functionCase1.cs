using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    class functionCase1
    {
        DO.IDal temp = DalApi.DalFactory.GetDal("DalObject");
        public void addStation()
        {
            DO.Station item = new DO.Station();
            Console.WriteLine("enter drone-station ID:(5 digits)");
            item.id = giveNumber();//user set id
            Console.WriteLine("enter drone-station name:");
            item.name = Console.ReadLine();//user input name
            DO.Point p = new DO.Point();
            Console.WriteLine("enter Latitude:");
            p.latitude = giveDouble();//user input Latitude
            Console.WriteLine("enter Longitude:");
            p.longitude = giveDouble();//user input Longitude
            item.Location = p;
            item.ChargeSlots = 10;//all station have only 10 charge slots 
            temp.InputTheStationToArray(item);//this function is using at the data base so she must to be in DAL project
          
        }
        public void addParcel1()
        {

            DO.Parcel par = new DO.Parcel();
            Console.WriteLine("enter parcel ID(5 digits):");
            int parId = giveNumber();//user set id
            par.Id = parId;
            Console.WriteLine("enter sender ID(5 digits):");
            int senId = giveNumber();//user set sendr id
            par.SenderId = senId;
            Console.WriteLine("enter target ID:(5 digits)");
            int tarId = giveNumber();//user set target id
            par.TargetId = tarId;
            Console.WriteLine("enter drone ID:(5 digits)");
            int dronId = giveNumber();//user set dron id
            par.DroneId = dronId;
            Console.WriteLine("enter weight:\nLight = 0, Medium = 1, Heavy = 2");//user set weight of parcel
            int parWeight = giveNumber();
            par.weight = (DO.Enum.WeightCategories)parWeight;
            Console.WriteLine("enter priority:\nNormal = 0, Fast = 1, Emergency =2");
            int parPriority = giveNumber();
            par.priority = (DO.Enum.Priorities)parPriority;
            par.Requested = DateTime.Now;//the requestsd time is now
            //par.Scheduled = par.Requested.AddMinutes(5);//the parcel find drone at 5 minutes
            temp.InputTheParcelToArray(par);
        }

        public void addCustomer()
        {

            DO.Customer cust = new DO.Customer();
            Console.WriteLine("enter customer ID:(5 digits)");
            cust.Id = giveNumber();//user set id

            Console.WriteLine("enter customer name:");
            cust.name = Console.ReadLine();//user input name

            Console.WriteLine("enter phone number:");
            cust.phone = Console.ReadLine();//user set phone number

            Console.WriteLine("enter Latitude:");
            DO.Point P = new DO.Point();
            P.latitude = giveDouble();//user input Latitude
            Console.WriteLine("enter Longitude:");
            P.longitude = giveDouble();//user input Longitude
            cust.location = P;
            temp.InputTheCustomerToArray(cust);
        }

        public void addDrone()
        {
            DO.Drone dro = new DO.Drone();
            Console.WriteLine("enter drone ID:(5 digits)");
            int droId = giveNumber();//user set id
            dro.Id = droId;
            Console.WriteLine("enter drone battery status:");
            double battery = giveDouble();//user set Battery status
            Console.WriteLine("enter drone model:");
            string model = Console.ReadLine();//user input Model
            dro.Model = model;
            Console.WriteLine("enter drone weight:\nLight = 0, Medium = 1, Heavy = 2");
            int weightChoose = giveNumber();//user input weight
            dro.MaxWeight = (DO.Enum.WeightCategories)weightChoose;//convert the choose to WeightCategories
            temp.InputTheDroneToArray(dro);
            
        }

        static bool IsDouble(string s)
        {
            bool HaveOnePointInTheNumber = true;


            if (s.Length == 0) return false;
            if (s.Length == 1 && (int)s[0] == (int)'.') return false;

            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] >= (int)'0' && (int)s[i] <= (int)'9')
                    continue;
                else if (HaveOnePointInTheNumber && (int)s[i] == (int)'.')
                {
                    HaveOnePointInTheNumber = false;
                    continue;
                }

                return false;
            }

            return true;
        }
        static public double giveDouble()
        {
            string s;
            do
            {
                s = Console.ReadLine();
                if (IsDouble(s))
                    return Convert.ToDouble(s);
                Console.WriteLine("Only numbers should be type to\nGive number\n");
            } while (true);
        }
        public static int giveNumber()
        {
            string s;
            do
            {
                s = Console.ReadLine();
                if (isNumber(s))
                    return Convert.ToInt32(s);
                Console.WriteLine("Only numbers should be type to\nGive number\n");
            } while (true);
        }
        static bool isNumber(string s)
        {
            if (s.Length == 0) return false;
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] >= (int)'0' && (int)s[i] <= (int)'9')
                    continue;

                return false;
            }

            return true;
        }
    }
}
