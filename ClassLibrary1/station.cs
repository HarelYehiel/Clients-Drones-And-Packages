using System;

namespace IDAL

{
    namespace DO
    {

        public struct station
        {
            public int Id { get; set; }
            public string name { get; set; }
            public Point Location { get; set; }
            public int ChargeSlots { get; set; }

            
            

            public override string ToString()
            {
                return $"Station ID: {Id}, name: {name}, Longitude: {Location.Longitude}, Latitude: {Location.Latitude}";
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