using BO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;


namespace BlApi
{
    public class Simulator
    {
        double speedDrone = 15; // 15 KM per 1 second. 
        private Stopwatch stopwatch;
        BlApi.BL bl;
        double HowMuchTimeMissingToBatteryFull = 0;
        Drone drone;
        Action action;
        Func<bool> func;

        public Simulator(BlApi.BL bl1, int droneId, Func<bool> func1, Action action1)
        {


            try
            {
                func = func1;
                action = action1;
                bl = bl1;
                stopwatch = new Stopwatch();
                drone = bl.GetDrone(droneId); 
                action();
                while (func())
                {
                    switch (drone.Status)
                    {
                        case EnumBO.DroneStatus.Avilble:
                            try
                            {
                                lock (bl)
                                {
                                    bl.AssignPackageToDrone(droneId);
                                }
                                action();
                                drone = bl.GetDrone(droneId);


                            }
                            catch (Exception e)
                            {
                                if (drone.Battery <= 30 || e.ToString().Contains("This drone can't take any parecl"))
                                {
                                    sendToCharging(droneId, drone.Battery);
                                    action();
                                }
                                else
                                    Thread.Sleep(1000); // Wait for new parcel.

                            }
                            break;

                        case EnumBO.DroneStatus.Baintenance:

                            try
                            {
                                // The simulter send him to charging.
                                if (stopwatch.IsRunning)
                                {
                                    stopwatch.Stop();
                                    drone.Status = EnumBO.DroneStatus.Avilble;
                                    drone.Battery = 100;
                                    Thread.Sleep(1000); // Wait one minute for the good feeling :-) .
                                    action();
                                }
                                // Arrived that drone was charging.
                                else
                                {
                                    stopwatch.Start(); // Counting charging time.
                                                       //How much time is missing until the battery is full.

                                    List<double> configStatus;
                                    configStatus = bl.accessDal.PowerConsumptionBySkimmer();

                                    HowMuchTimeMissingToBatteryFull = Math.Ceiling((100 - drone.Battery) / configStatus[4]);
                                    updateInRealTime(droneId, HowMuchTimeMissingToBatteryFull, '+', 0);
                                    action();
                                    bl.ReleaseDroneFromCharging(droneId, DateTime.Now);
                                    action();
                                }
                                break;

                            }
                            catch (Exception)
                            {
                                break;
                            }

                        case EnumBO.DroneStatus.Delivery:
                            try
                            {
                                double tempbattery;
                                Parcel parcel;
                                parcel = bl.GetParcel(drone.parcelByTransfer.uniqueID);

                                // Check if drone arrived to simulator in delivery pick up. 
                                if (parcel.pickedUp == null) // Pick up ?
                                {
                                    tempbattery = bl.GetDrone(droneId).Battery; // Save the real battery

                                    // Update the details of drone in real time from drone's location to destination's Location.
                                    startUpdateInRealTime(droneId,
                                       drone.location, drone.parcelByTransfer.collectLocation);


                                    lock (bl)
                                    {
                                        //Raise the drone battery back.
                                        bl.updateBatteryBySimultor(droneId, tempbattery);

                                        bl.CollectionOfPackageByDrone(droneId);
                                        action();
                                    }
                                }

                                tempbattery = bl.GetDrone(droneId).Battery;  // Save the real battery

                                // Update the details of drone in real time from collection's Location to destination's Location.
                                startUpdateInRealTime(droneId,
                                    drone.parcelByTransfer.collectLocation, drone.parcelByTransfer.destinationLocation);

                                lock (bl)
                                {
                                    //Raise the drone battery back.
                                    bl.updateBatteryBySimultor(droneId, tempbattery);

                                    bl.DeliveryOfPackageByDrone(droneId);

                                }
                                action();
                                drone = bl.GetDrone(droneId);

                            }
                            catch (Exception)
                            {

                                throw;
                            }
                            //Thread.Sleep(500); // Wait half minute for the good feeling :-) .

                            break;

                    }
                    action();
                }
            }
            catch (Exception)
            {

            }

        }
        void sendToCharging(int droneId, double droneBattery)
        // Send the drone to charging.
        // If don't have place, try all one second.
        {
            while (func())
            {
                try
                {

                    lock (bl) { bl.SendingDroneToCharging(droneId); }
                    stopwatch.Start(); // Counting charging time.
                                       //How much time is missing until the battery is full.

                    drone.Status = EnumBO.DroneStatus.Baintenance;
                    List<double> configStatus;
                    configStatus = bl.accessDal.PowerConsumptionBySkimmer(); 

                    HowMuchTimeMissingToBatteryFull = Math.Ceiling((100 - droneBattery) / configStatus[4]);
                    updateInRealTime(droneId, HowMuchTimeMissingToBatteryFull, '+', 0);

                    lock (bl) { bl.ReleaseDroneFromCharging(droneId, DateTime.Now); }
                    break;
                }
                catch (Exception e)
                {
                    if (e.ToString().Contains("He does not have enough battery to get to the station"))
                        Thread.Sleep(1000); // Wait for drone out from charging.
                }
            }

        }
        void startUpdateInRealTime(int droneID, Location location1, Location location2)
        {
            // Dictance from the client oe station.
            double distance;
            distance = location1.distancePointToPoint(location2); 

            // How long to arrive to collect/ delivered parcel or arrive to station. (in second)
            double HowLongToArrive = distance / speedDrone;

            updateInRealTime(droneID, HowLongToArrive, '-', distance);
        }
        public void updateInRealTime(int droneId, double HowMantTimes, char AddOrSubtractToBattery, double distance)
        // AddOrSubtractToBattery = what the action you want to do add(+) or subtract(-) to battery.
        // How long to arrive to collect / delivered parcel or arrive to station (in second).
        // Dictance from the client oe station (meter).
        {

            if (AddOrSubtractToBattery == '-')
            {
                for (int i = 1; i <= Math.Ceiling(HowMantTimes) && func(); i++)
                {
                    try
                    {
                        if (distance <= speedDrone)
                            lock (bl) { bl.UpdateBatteryInReelTime(droneId, distance, AddOrSubtractToBattery); }
                        else
                            lock (bl) { bl.UpdateBatteryInReelTime(droneId, speedDrone, AddOrSubtractToBattery); }

                        distance -= speedDrone;
                        action();
                        Thread.Sleep(200);

                        if (distance <= 0) break;
                    }
                    catch (Exception) { }

                }
            }
            else if (AddOrSubtractToBattery == '+')
            {
                for (int i = 1; i <= Math.Ceiling(HowMantTimes) && func(); i++)
                {
                    lock (bl) { bl.UpdateBatteryInReelTime(droneId, 0, AddOrSubtractToBattery); }
                    action();
                    Thread.Sleep(200);
                }

            }
        }

    }
}
