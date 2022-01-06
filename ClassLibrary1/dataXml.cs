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
            string configDataPath = @"dronesXml.xml";
            string stationsPath = @"stationsXml.xml";
            string customersPath = @"customersXml.xml";
            string parcelsPath = @"parcelsXml.xml";
            string dronesPath = @"dronesXml.xml";
            string dronesChargePath = @"dronesXml.xml";

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
            try
            {
                List<DroneCharge> droneCharges = new List<DroneCharge>();// = temp.GetListOfDronesInCharging();
                XmlSerializer DroneChargeToXml = new XmlSerializer(typeof(List<DroneCharge>));
                FileStream DroneChargeFile = new FileStream(dirPath + dronesChargePath, FileMode.Create);
                DroneChargeToXml.Serialize(DroneChargeFile, droneCharges);
                DroneChargeFile.Close();
            }
            catch 
            {
                int a;
            }

            // export the DronesCharge list to xml file
            List<Drone> drones = temp.GetListOfDrones().ToList();
            XmlSerializer DronesToXml = new XmlSerializer(typeof(List<Drone>));
            FileStream DronesFile = new FileStream(dirPath + dronesPath, FileMode.Create);
            DronesToXml.Serialize(DronesFile, drones);
            DronesFile.Close();

            // export the parcels list to xml file
            List<Parcel> parcels = temp.GetListOfParcels().ToList();
            XmlSerializer parcelsToXml = new XmlSerializer(typeof(List<Parcel>));
            FileStream parfile = new FileStream(dirPath + parcelsPath, FileMode.Create);
            parcelsToXml.Serialize(parfile, parcels);
            parfile.Close();


            // export the customers list to xml file

            List<Customer> customers = temp.GetListOfCustmers().ToList();
            XmlSerializer customersToXml = new XmlSerializer(typeof(List<Customer>));
            FileStream custfile = new FileStream(dirPath + customersPath, FileMode.Create);
            customersToXml.Serialize(custfile, customers);
            custfile.Close();

            // export the stations list to xml file

            List<Station> stations   = temp.GetListOfStations().ToList();
            XmlSerializer stationsToXml = new XmlSerializer(typeof(List<Station>));
            FileStream stafile = new FileStream(dirPath + stationsPath, FileMode.Create);
            stationsToXml.Serialize(stafile, stations);
            stafile.Close();

            #endregion
        }
    }
}
