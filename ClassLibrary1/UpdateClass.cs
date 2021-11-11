using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DalObject
    {
        public class UpdateClass
        {
            public void updateDrone(DO.Drone drone)
            {
                for(int i = 0; i < DataSource.drones.Count; i++)
                {
                    if(DataSource.drones[i].Id == drone.Id)
                    {
                        DataSource.drones[i] = drone;
                    }
                }
            }
            public void updateStation(DO.station station)
            {
                for (int i = 0; i < DataSource.stations.Count; i++)
                {
                    if (DataSource.stations[i].Id == station.Id)
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
            public void updateDroneToCharge(int droneId,int stationId)
            {
                for (int i = 0; i < DataSource.drones.Count; i++)
                {
                    if (DataSource.drones[i].Id == droneId)
                    {
                        DO.Drone drone = DalObject.GetDrone(droneId);
                        drone.droneStatus = DO.Enum.DroneStatus.Baintenance;
                        DO.DroneCharge droneCharge = new DO.DroneCharge();
                        droneCharge.DroneId = droneId;
                        droneCharge.staitionId = stationId;
                        drone.stationOfCharge = droneCharge;
                        DataSource.drones[i] = drone;
                    }
                }
                for(int i =0;i< DataSource.stations.Count; i++)
                {
                    if(DataSource.stations[i].Id == stationId)
                    {
                        DO.station station = DalObject.GetStation(stationId);
                        station.ChargeSlots--;
                        DataSource.stations[i] = station;
                    }
                }
                
            }
            public void updateRelaseDroneFromCharge(int droneId, int stationId, int min)
            {
                for (int i = 0; i < DataSource.drones.Count; i++)
                {
                    if (DataSource.drones[i].Id == droneId)
                    {
                        DO.Drone drone = DalObject.GetDrone(droneId);
                        drone.droneStatus = DO.Enum.DroneStatus.Avilble;
                        //אי אפשר ךידכן פה מצב סוללה - זה כנראה BL
                        DataSource.drones[i] = drone;
                    }
                }
                for (int i = 0; i < DataSource.stations.Count; i++)
                {
                    if (DataSource.stations[i].Id == stationId)
                    {
                        DO.station station = DalObject.GetStation(stationId);
                        station.ChargeSlots--;
                        DataSource.stations[i] = station;
                    }
                }
                for(int i = 0; i < DataSource.droneCharge.Count; i++)
                {
                    if (DataSource.droneCharge[i].DroneId == droneId && DataSource.droneCharge[i].staitionId == stationId)
                        DataSource.droneCharge.RemoveAt(i);

                }
            }
        }
    }
}
