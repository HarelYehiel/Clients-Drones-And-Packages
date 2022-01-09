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
using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;


namespace BlApi
{
    public class Simulator
    {
        double speedDrone = 15; // 3 KM per 1 second. 
        private Stopwatch stopwatch;
        BlApi.BL bl;
        public Simulator(BlApi.BL bl1, int droneId, Func<bool> func, Action action)
        {
            try
            {
                bl = bl1;
                stopwatch = new Stopwatch();
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
                                lock (bl) { bl.AssignPackageToDrone(drone.uniqueID); }
                                f(droneId, action);


                                lock (bl)
                                {
                                    bl.CollectionOfPackageByDrone(drone.uniqueID);
                                }
                                Thread.Sleep(500); // Wait for drone delivery the package.
                                bl.DeliveryOfPackageByDrone(drone.uniqueID);
                                drone.Status = EnumBO.DroneStatus.Avilble;
                                drone = bl.GetDrone(drone.uniqueID);


                            }
                            catch (Exception e)
                            {
                                try
                                {
                                    if (drone.Battery <= 30 && e.ToString().Contains("This drone can't take any parecl"))
                                    {
                                        lock (bl)
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
                                bl.ReleaseDroneFromCharging(drone.uniqueID,DateTime.Now/* stopwatch.ElapsedMilliseconds*/);
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
            catch (Exception)
            {

            }

        }

        void f(int idDrone, Action action)
        {
            double distance; // Dictance from the client oe station.
            double HowLongToArrive; // How long to arrive to collect/ delivered parcel or arrive to station. (in second)

            Drone drone = bl.GetDrone(idDrone);
            distance = bl.distance(drone.location, drone.parcelByTransfer.collectionLocation);
            HowLongToArrive = distance / speedDrone;
            updateInRealTime(drone.uniqueID, HowLongToArrive, action);
        }
        public void updateInRealTime(int droneId, double HowLongToArrive, Action action)
        // How long to arrive to collect / delivered parcel or arrive to station (in second).
        // Dictance from the client oe station (meter).
        {

            for (int i = 1; i <= HowLongToArrive; i++)
            {
                lock (bl) { bl.UpdateBatteryInReelTime(droneId, speedDrone); }
                action();
                Thread.Sleep(1000);
            }
        }
    }
}
