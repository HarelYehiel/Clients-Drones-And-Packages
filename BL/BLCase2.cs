using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    public partial class BL : IBL
    {
        IDAL.DalObject.UpdateClass updateFun = new IDAL.DalObject.UpdateClass();

        public void Update_drone_data(int ID, string newModel)
        {
            IDAL.DO.Drone drone = temp.GetDrone(ID);
            drone.Model = newModel;
            updateFun.updateDrone(drone);
        }
        public void Update_station_data(int ID, string name, int numSlots)
        {
            IDAL.DO.station station = temp.GetStation(ID);
            if (name[0] != '\n')
                station.name = name;
            if (numSlots != '\n')
                station.ChargeSlots = numSlots;
            updateFun.updateStation(station);
        }
        public void Update_customer_data(int ID, string name, string phoneNumber)
        {
            IDAL.DO.Customer customer = temp.GetCustomer(ID);
            if (name[0] != '\n')
                customer.Name = name;
            if (phoneNumber[0] != '\n')
                customer.Phone = phoneNumber;
            updateFun.updateCustomer(customer);
        }
        public void Sending_a_drone_for_charging(int ID)
        {
            IDAL.DO.Drone drone = temp.GetDrone(ID);
            if (drone.droneStatus == IDAL.DO.Enum.DroneStatus.Avilble)
            {
                if (temp.)//אם הרחפן יכול להגיע עד לתחנה רלוונטית כדי להטען
                {
                    //צריך להשתמש בפןונקציות המרחק ךלא סגור  עליהם
                    IDAL.DO.station station = new IDAL.DO.station();
                    //לעדכן את המיקום של התחנה והמזהה שלה למזהה נכון
                    updateFun.updateDroneToCharge(ID, station.Id);
                    BO.Drone drone1 = new BO.Drone;
                    //ךלעדכן את מצב הסוללה של הרחפן בהתאמה למרחק שהוא עבר
                }
                else//אין לו מספיק סוללה להגיע לתחנה
                    throw ""
            }
            else//הרחפן בכלל לא פנוי אז אי אפשר לשלוח אותו
                throw ""
        }
        public void Release_drone_from_charging(int ID, int min)
        {
            IDAL.DO.Drone drone = temp.GetDrone(ID);
            if (drone.droneStatus == IDAL.DO.Enum.DroneStatus.Baintenance)
            {
                IDAL.DO.station station = new IDAL.DO.station();
                //צריך לחשוב איך למצוא את התחנה שהרחפן נמצא בה עכשיו
                updateFun.updateRelaseDroneFromCharge(ID, station.Id, min);

            }
            else//הרחפן בכלל לא בתחזוקה
                throw ""
        }
    
        public void Assign_a_package_to_a_drone(int droneId) 
        {
            IDAL.DO.Drone drone = temp.GetDrone(droneId);
            if (drone.droneStatus == IDAL.DO.Enum.DroneStatus.Avilble)
            {
                if () {//רק אם הרחפן יכול להגיע מבחינת בטריה עד לחבילה שצריכה איסוף
                    List<IDAL.DO.Parcel> parcels = new List<IDAL.DO.Parcel>();
                    ////////////////////////////////////////////////////////////////צור רשימה חדשה עם הדחופים ביותר
                    for (int i = 0; i < IDAL.DalObject.DataSource.parcels.Count; i++)
                    {
                        if (IDAL.DalObject.DataSource.parcels[i].Priority == IDAL.DO.Enum.Priorities.Emergency)
                            parcels.Add(IDAL.DalObject.DataSource.parcels[i]);
                    }
                    if (parcels.Count == 0)//if no parcel is emergency
                    {
                        for (int i = 0; i < IDAL.DalObject.DataSource.parcels.Count; i++)
                        {
                            if (IDAL.DalObject.DataSource.parcels[i].Priority == IDAL.DO.Enum.Priorities.Fast)
                                parcels.Add(IDAL.DalObject.DataSource.parcels[i]);
                        }
                    }
                    if (parcels.Count == 0)//if no parcel is emergency or fast priority
                    {
                        for (int i = 0; i < IDAL.DalObject.DataSource.parcels.Count; i++)
                        {
                            parcels.Add(IDAL.DalObject.DataSource.parcels[i]);
                        }
                    }
                    List<IDAL.DO.Parcel> filterParcels = new List<IDAL.DO.Parcel>();
                    //////////////////////////////////////////////////////////////////נסנן עוד קצת - המשקל הגבוה ביותר הרלוונטי
                    foreach (var parcel in parcels)
                    {
                        if (drone.MaxWeight == parcel.Weight)
                            filterParcels.Add(parcel);
                    }
                    if (filterParcels.Count == 0)
                    {

                        foreach (var parcel in parcels)
                        {
                            if (drone.MaxWeight > parcel.Weight)
                                filterParcels.Add(parcel);
                        }
                    }
                    ////////////////////////////////צריך להוסיף עוד סינון עם הבטריה אבל לא מבין איך משתמשים הבזה
                }
                else//אין לרחפן מספיק סוללה להגיע לחבילה
                    throw ""
            }
            else//הרחפן לא פנוי ואי אפשר לקרוא לו עכשיו למשהו אחר
                throw ""
        }
        public void Collection_of_a_package_by_drone(int droneId)
        {

        }
        public void Delivery_of_a_package_by_drone(int droneId) { }
    }
}
