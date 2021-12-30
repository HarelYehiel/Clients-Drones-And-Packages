using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace ConsoleUI
{
    class functionCase5
    {
        IDal temp = DalApi.DalFactory.GetDal("DalObject");

        public void chooseObjectToconvert()
        {
            Console.WriteLine("witch cordinate you want to convert? ");
            Point point = new Point();
            point.latitude = giveDouble();
            point.longitude = giveDouble();
            string newLat = Point.convertLatitudeToDegree(point);
            string newLon = Point.convertLongitudeToDegree(point);
            Console.WriteLine("the new Latitude is: " + newLat);
            Console.WriteLine("the new Longitude is: " + newLon);

        }

        public void distanceFromCustomerOrStation()
        /*Receives coordinates of any point from the user and prints distance
        from any base or client to that point.*/
        {
            Console.WriteLine("Type Latitude and Longitude");
            Point p = new Point();
            p.latitude = giveDouble();
            p.longitude = giveDouble();
            int choose;

            do
            {

                Console.WriteLine("Choose one of the following:" + "\n1 = distance from customer" + "\n2 = distance from station");
                choose =giveNumber();
                //double minDistance = IDAL.DalObject.DataSource.customers[0].location.distancePointToPoint(p);
                //switch (choose)
                //{
                //    case 1:
                //        Console.WriteLine(temp.MinimumFromCustomer(minDistance,p));
                //        return;

                //    case 2:
                ////        Console.WriteLine(temp.MinimumFromStation(minDistance,p));
                ////        return;

                //    default:
                //        break;
                //}
            } while (choose != 1 && choose != 2);
            Exception e11 = new Exception("Error in function distanceFromCustomerOrStation");
            throw e11;

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
