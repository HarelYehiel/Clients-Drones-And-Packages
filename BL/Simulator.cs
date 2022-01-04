using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Threading;
using static BlApi.BL;
using System.Linq;
using System.Diagnostics;

namespace BlApi
{
    public class Simulator
    {
        double speedDrone;
        private Stopwatch stopwatch;
        public Simulator(BlApi.BL bl, int droneId, Func<bool> func)
        {
            stopwatch.Start();
            double? HowMuchTimeMissingToBatteryFull = null;
            Drone drone = bl.GetDrone(droneId);

            while (func())
            {
                switch (drone.Status)
                {
                    case EnumBO.DroneStatus.Avilble:
                        try
                        {
                            bl.AssignPackageToDrone(drone.uniqueID);
                            drone.Status = EnumBO.DroneStatus.Delivery;
                            Thread.Sleep(500); // Wait for drone collection the package.
                            bl.CollectionOfPackageByDrone(drone.uniqueID);
                            Thread.Sleep(500); // Wait for drone delivery the package.
                            bl.DeliveryOfPackageByDrone(drone.uniqueID);
                            drone.Status = EnumBO.DroneStatus.Avilble;
                            drone = bl.GetDrone(drone.uniqueID);
                        }
                        catch (Exception e)
                        {
                            try
                            {
                                if (drone.Battery <= 30 && e.Message.Contains("This drone can't take any parecl"))
                                {
                                    bl.SendingDroneToCharging(drone.uniqueID);
                                    // Get drone with correct battery.
                                    drone = bl.GetDrone(drone.uniqueID);
                                    stopwatch.Start(); // Counting charging time.
                                    //How much time is missing until the battery is full (in milliseaconds).
                                    HowMuchTimeMissingToBatteryFull = (double)((100 - drone.Battery) / 2) / 1000;
                                    drone.Status = EnumBO.DroneStatus.Baintenance;
                                    Thread.Sleep((int)HowMuchTimeMissingToBatteryFull); // Wait for drone finish the charge.
                                }
                                else
                                    Thread.Sleep(1000); // Wait for parcel the drone can to take.

                            }
                            catch (Exception e1)
                            {
                                if (e1.Message.Contains("He does not have enough battery to get to the station"))
                                    Thread.Sleep(1000); // Wait for drone out from charging.

                            }


                        }


                        break;

                    case EnumBO.DroneStatus.Baintenance:

                        if (HowMuchTimeMissingToBatteryFull <= stopwatch.ElapsedMilliseconds)
                        {
                            stopwatch.Stop();
                            bl.ReleaseDroneFromCharging(drone.uniqueID, stopwatch.ElapsedMilliseconds);
                            drone.Status = EnumBO.DroneStatus.Avilble;
                            drone.Battery = 100;
                            Thread.Sleep(1000); // Wait one minute for the good feeling :-) .
                        }
                        break;

                    case EnumBO.DroneStatus.Delivery:
                        Thread.Sleep(500); // Wait half minute for the good feeling :-) .

                        break;

                }
            }
        }
    }
}
