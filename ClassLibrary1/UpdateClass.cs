using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace DalApi
{
    namespace DalObject
    {
        public class UpdateClass
        {
            IDal temp = DalFactory.GetDal("DalObject");
            public void updateDrone(DO.Drone drone)
            {
                for (int i = 0; i < DataSource.drones.Count; i++)
                {
                    if (DataSource.drones[i].Id == drone.Id)
                    {
                        DataSource.drones[i] = drone;
                    }
                }
            }
            public void updateStation(DO.Station station)
            {
                for (int i = 0; i < DataSource.stations.Count; i++)
                {
                    if (DataSource.stations[i].id == station.id)
                    {
                        DataSource.stations[i] = station;
                    }
                }
            }
            public void updateCustomer(DO.Customer customer)
            {
                for (int i = 0; i < DataSource.customers.Count; i++)
                {
                    if (DataSource.customers[i].Id == customer.Id)
                    {
                        DataSource.customers[i] = customer;
                    }
                }
            }
            public void updateDroneToCharge(int droneId, int stationId)
            {
                for (int i = 0; i < DataSource.drones.Count; i++)
                {
                    if (DataSource.drones[i].Id == droneId)
                    {
                        DO.Drone drone = temp.GetDrone(droneId);
                        drone.droneStatus = DO.Enum.DroneStatus.Baintenance;
                        DataSource.drones[i] = drone;
                    }
                }

                DO.DroneCharge droneCharge = new DO.DroneCharge();
                droneCharge.DroneId = droneId;
                droneCharge.staitionId = stationId;
                droneCharge.startCharge = DateTime.Now;
                temp.InputTheDroneCharge(droneCharge);
            }
            public void updateRelaseDroneFromCharge(int droneId, double longi, double lati, double min)
            {
                DO.Point stationLocation = new DO.Point();
                stationLocation.latitude = lati;
                stationLocation.longitude = longi;
                for (int i = 0; i < DataSource.drones.Count; i++)
                {
                    if (DataSource.drones[i].Id == droneId)
                    {
                        DO.Drone drone = temp.GetDrone(droneId);
                        drone.droneStatus = DO.Enum.DroneStatus.Avilble;
                        DataSource.drones[i] = drone;
                    }
                }
                for (int i = 0; i < DataSource.stations.Count; i++)
                {
                    if (DataSource.stations[i].Location.latitude == stationLocation.latitude && DataSource.stations[i].Location.longitude == stationLocation.longitude)
                    {
                        DO.Station station = DataSource.stations[i];
                        station.ChargeSlots++;
                        DataSource.stations[i] = station;
                    }
                }
                for (int i = 0; i < DataSource.dronesCharge.Count; i++)
                {
                    if (DataSource.dronesCharge[i].DroneId == droneId)
                        DataSource.dronesCharge.RemoveAt(i);

                }
            }

            public void updateDeliveryParcelByDrone(int ID)
            {

            }
            public double colculateBattery(DO.Point point1, DO.Point point2, int ID)
            {
                Drone drone = temp.GetDrone(ID);
                double minus = 0;
                List<double> configStatus = temp.PowerConsumptionBySkimmer();
                double distance = point1.distancePointToPoint(point2);
                if (drone.MaxWeight == DO.Enum.WeightCategories.Light)
                {
                    //all 1500 meters is minus 1% battery
                    minus = distance / configStatus[1];
                }
                if (drone.MaxWeight == DO.Enum.WeightCategories.Medium)
                {
                    //all 1000 meters is minus 1% battery
                    minus = distance / configStatus[2];
                }
                if (drone.MaxWeight == DO.Enum.WeightCategories.Heavy)
                {
                    //all 850 meters is minus 1% battery
                    minus = distance / configStatus[3];
                }
                return minus;
            }
            public void updateParcel(DO.Parcel parcel)
            {
                for (int i = 0; i < DataSource.parcels.Count; i++)
                {
                    if (DataSource.parcels[i].Id == parcel.Id)
                    {
                        DataSource.parcels[i] = parcel;
                    }
                }
            }


        }
    }
}
