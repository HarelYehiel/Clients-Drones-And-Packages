using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{

    public partial class BL : IBL
    {
        List<BO.Drone> listDrons = new List<BO.Drone>();
        IDAL.DO.IDal temp = new  IDAL.DalObject.DalObject();
 
        public bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        public void Insert_options() //case 1
        {
            Console.WriteLine("press 0 to back ");
            Console.WriteLine("press 1 to add a new drone-staition");
            Console.WriteLine("press 2 to add a new drone");
            Console.WriteLine("press 3 to add a new customer");
            Console.WriteLine("press 4 to add a new parcel");
            Console.WriteLine("Choose one of the following:");
        }

   
        public void Update_options() //case 2
        {
            Console.WriteLine("Choose one of the following:");
            Console.WriteLine("press 0 to back ");
            Console.WriteLine("press 1 to update drone");
            Console.WriteLine("press 2 to update station");
            Console.WriteLine("press 3 to update customer");
            Console.WriteLine("press 4 to send drone from charge in station ");
            Console.WriteLine("press 5 to send drone to charge at station");
            Console.WriteLine("press 6 to assign drone to parcel");
            Console.WriteLine("press 7 to update picked up parcel by drone");
            Console.WriteLine("press 8 to update delivered parcel by drone");
        }
        public void Entity_display_options() //case 3
        {
            Console.WriteLine("press 0 to back ");
            Console.WriteLine("press 1 to Station View");
            Console.WriteLine("press 2 to drone View");
            Console.WriteLine("press 3 to Customer View");
            Console.WriteLine("press 4 to parcel View ");
        }

        public void List_view_options()//case 4
        {
            Console.WriteLine("press 0 to back.");
            Console.WriteLine("press 1 to displays a list of base stations.");
            Console.WriteLine("press 2 to displays a list of drones.");
            Console.WriteLine("press 3 to displays a list of customer.");
            Console.WriteLine("press 4 to Displays the list of parcels.");
            Console.WriteLine("press 5 to displays a list of packages that have not yet been assigned to the drone.");
            Console.WriteLine("press 6 to base stations with available charging stations.\n");
        }       
    }
}
