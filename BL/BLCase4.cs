using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    public partial class BL :IBL
    {
        public IDAL.DalObject.DataSource Displays_a_list_of_base_stations()
        {
            if (IDAL.DalObject.DataSource.stations.Count() == 0)
            {
                throw Exception;//לטפל בחריגה
            }
            else
            {
                return IDAL.DalObject.DataSource.stations;
            }
        }
        public void Displays_the_list_of_drones() { }
        public void Displays_a_list_of_information() { }
        public void Displays_the_list_of_packages() { }
        public void Displays_a_list_of_packages_not_yet_associated_with_the_drone() { }
        public void Display_of_base_stations_with_available_charging_stations() { }
    }
}
