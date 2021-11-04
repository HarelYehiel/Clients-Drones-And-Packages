using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    class functionCase5
    {
        public static void chooseObjectToconvert()
        {
            Console.WriteLine("witch cordinate you want to convert? ");
            IDAL.DO.Point point = new IDAL.DO.Point();
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
            IDAL.DO.Point p = new IDAL.DO.Point();
            p.Latitude = Convert.ToDouble(Console.ReadLine());
            p.Longitude = Convert.ToDouble(Console.ReadLine());
            int choose;

            do
            {

                Console.WriteLine("Choose one of the following:" + "\n1 = distance from customer" + "\n2 = distance from station");
                choose = Convert.ToInt32(Console.ReadLine());
                double minDistance = IDAL.DalObject.DataSource.customers[0].Location.distancePointToPoint(p);
                switch (choose)
                {
                    case 1:
                        Console.WriteLine(IDAL.DalObject.DalObject.MinmumFromCustomer(minDistance,p));
                        return;

                    case 2:
                        Console.WriteLine(IDAL.DalObject.DalObject.MinimumFromStation(minDistance,p));
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
