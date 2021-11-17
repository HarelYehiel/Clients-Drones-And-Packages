using System;

namespace IDAL

{
    namespace DO
    {

        public struct Station
        {
            public int id { get; set; }
            public string name { get; set; }
            public Point Location { get; set; }
            public int ChargeSlots { get; set; }

            
            

            public override string ToString()
            {
                return $"Station ID: {id}, name: {name}, Longitude: {Location.longitude}, Latitude: {Location.latitude}";
            }
            

            /*  public static string func()
              {
                  Console.WriteLine("Do you want sexagesimal or deegre?\n");
                  string ans = Console.ReadLine();
                  if(ans == "sexagesimal") 
                  {
                  set
                  }
                  return
              }
  */
        }

    }
}