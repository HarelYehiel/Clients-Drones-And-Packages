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
        IDal temp = DalApi.DalFactory.GetDal("s");

        public void chooseObjectToconvert()
        {
            Console.WriteLine("witch cordinate you want to convert? ");
            Point point = new Point();
            point.latitude = Convert.ToDouble(Console.ReadLine());
            point.longitude = Convert.ToDouble(Console.ReadLine());
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
            p.latitude = Convert.ToDouble(Console.ReadLine());
            p.longitude = Convert.ToDouble(Console.ReadLine());
            int choose;

            do
            {

                Console.WriteLine("Choose one of the following:" + "\n1 = distance from customer" + "\n2 = distance from station");
                choose = Convert.ToInt32(Console.ReadLine());
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
    }
}
