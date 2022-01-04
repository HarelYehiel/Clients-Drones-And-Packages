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
        public dataXml()
        {
            initilaizeXml();
        }
        public static void initilaizeXml()
        {
            IDal temp = DalApi.DalFactory.GetDal("DalObject");
            #region linq to xml
            // export the drones list to xmlfile
            IEnumerable<Drone> drones = temp.GetListOfDrones();
            string dronesPath = @"C:\Users\POSEIDON\source\repos\mini project\HarelYehiel\Course-mini-project\DalXML\drones.xml";
            XElement Root = new XElement("Drones");
            foreach (Drone drone in drones)
            {
                XElement Id = new XElement("Id", drone.Id);
                XElement Model = new XElement("Model", drone.Model);
                XElement MaxWeight = new XElement("MaxWeight", drone.MaxWeight);
                XElement status = new XElement("droneStatus", drone.droneStatus);
                //get in the atribiute to odject Drone
                XElement Drone = new XElement("Drone", Id, Model, MaxWeight, status);
                Root.Add(Drone);
            }
            Root.Save(dronesPath);
            #endregion
            #region sreilaize xml
            // export the parcels list to xml file
            IEnumerable<Parcel> parcels = temp.GetListOfParcels();
            string parcelPath = @"C:\Users\POSEIDON\source\repos\mini project\HarelYehiel\Course-mini-project\DalXML\parcels.xml";
            XmlSerializer parcelsToXml = new XmlSerializer(typeof(List<Parcel>));
            FileStream parfile = new FileStream(parcelPath, FileMode.Create);
            parcelsToXml.Serialize(parfile, parcels);
            parfile.Close();


            // export the customers list to xml file

            IEnumerable<Customer> customers = temp.DisplaysListOfCustmers();
            string customerPath = @"C:\Users\POSEIDON\source\repos\mini project\HarelYehiel\Course-mini-project\DalXML\customers.xml";
            XmlSerializer customersToXml = new XmlSerializer(typeof(List<Customer>));
            FileStream custfile = new FileStream(customerPath, FileMode.Create);
            customersToXml.Serialize(custfile, customers);
            custfile.Close();

            // export the stations list to xml file

            IEnumerable<Station> stations = temp.GetListOfStations();
            string stationPath = @"C:\Users\POSEIDON\source\repos\mini project\HarelYehiel\Course-mini-project\DalXML\stations.xml";
            XmlSerializer stationsToXml = new XmlSerializer(typeof(List<Station>));
            FileStream stafile = new FileStream(stationPath, FileMode.Create);
            stationsToXml.Serialize(stafile, stations);
            stafile.Close();

            #endregion
        }
    }
}
