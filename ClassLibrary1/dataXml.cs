using System;
using System.Collections.Generic;
using DO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;

namespace DalXml
{
    public class dataXml
    {
        public dataXml(IDal temp)
        {
            initilaizeXml(temp);
        }
        public static void initilaizeXml(IDal temp)
        {
            string dirPath = @"..\..\..\..\DalXML\";
            string configDataPath = @"configXml.xml";
            string stationsPath = @"stationsXml.xml";
            string customersPath = @"customersXml.xml";
            string parcelsPath = @"parcelsXml.xml";
            string dronesPath = @"dronesXml.xml";
            string dronesChargePath = @"droneChargesXml.xml";

            //  IDal temp = DalApi.DalFactory.GetDal("DalObject");

            #region linq to xml
            // export the drones list to xmlfile
            /* List<Drone> dronesl = temp.GetListOfDrones().ToList();
             XElement Root = new XElement("Drones");
             foreach (Drone drone in dronesl)
             {
                 XElement Id = new XElement("Id", drone.Id);
                 XElement Model = new XElement("Model", drone.Model);
                 XElement MaxWeight = new XElement("MaxWeight", drone.MaxWeight);
                 XElement status = new XElement("droneStatus", drone.droneStatus);
                 XElement Drone = new XElement("Drone", Id, Model, MaxWeight, status);
                 Root.Add(Drone);
             }
             Root.Save(dirPath + dronesPath);*/
            #endregion

            #region sreilaize xml
            // export the DronesCharge list to xml file

            List<DroneCharge> droneCharges = new List<DroneCharge>();// = temp.GetListOfDronesInCharging();
            SaveListToXmlSerializer<DroneCharge>(droneCharges, dirPath + dronesChargePath);

            // export the DronesCharge list to xml file
            List<Drone> drones = temp.GetListOfDrones().ToList();
            SaveListToXmlSerializer<Drone>(drones, dirPath + dronesPath);


            // export the parcels list to xml file
            List<Parcel> parcels = temp.GetListOfParcels().ToList();
            SaveListToXmlSerializer<Parcel>(parcels, dirPath + parcelsPath);


            // export the customers list to xml file
            List<Customer> customers = temp.GetListOfCustmers().ToList();
            SaveListToXmlSerializer<Customer>(customers, dirPath + customersPath);

            // export the stations list to xml file

            List<Station> stations = temp.GetListOfStations().ToList();
            SaveListToXmlSerializer<Station>(stations, dirPath + stationsPath);

        }
                    #endregion
public static void SaveListToXmlSerializer<T>(List<T> list, string filePath)
        {
            try
            {
                FileStream file = new(filePath, FileMode.Create);
                XmlSerializer x = new(list.GetType());
                x.Serialize(file, list);
                file.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("The export To Xml file failed");
            }
        }
    }

}
