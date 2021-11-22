//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using IBL.BO;

//namespace IBL
//{
//    public class InitializeBO
//    {
//        public InitializeBO()
//        {
//            IDAL.DalObject.DataSource.Initialize();
//            BO.DroneToList droneToListBO;

//            foreach (var item in IDAL.DalObject.DataSource.drones) // Update the list in listDrons of IBL
//            {
//                var rand = new Random();
//                droneToListBO = new BO.DroneToList();

//                droneToListBO.uniqueID = item.Id;
//                droneToListBO.Model = item.Model;
//                droneToListBO.Battery = rand.Next(20, 80);
//                droneToListBO.weight = (BO.EnumBO.WeightCategories)item.MaxWeight;
//                droneToListBO.status = (BO.EnumBO.DroneStatus)item.droneStatus;

//                BO.Location l = new BO.Location();
//                l.latitude = 31 + (double)rand.Next(0, 1);
//                l.longitude = 34 + (double)rand.Next(0, 1);
//                droneToListBO.location = l;
//                droneToListBO.packageDelivered = 0;

//                 BL.ListDroneToList.Add(droneToListBO);
//            }
//        }
//    }
//}

