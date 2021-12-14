using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace DO
    {
        public class myExceptionDO : Exception
        {

            public static Exception There_is_no_variable_with_this_ID = new Exception("There is no variable with this ID.");
            public static Exception An_empty_list = new Exception("An empty list.");
            public static Exception Only_numbers_should_be_type_to = new Exception("Only numbers should be type to.");
            public static Exception Dont_have_any_customer_in_the_list = new Exception("Don't have any customer in the list.");
            public static Exception Dont_have_any_station_in_the_list = new Exception("Don't have any station in the list.");
            public static Exception Dont_have_any_parcel_in_the_list = new Exception("Don't have any parcel in the list.");
            public static Exception Dont_have_any_drone_in_the_list = new Exception("Don't have any drone in the list.");
            public static Exception We_ge_to_the_end_of_list_and_dont_find_the_station = new Exception("We get to the end of list and don't find the station");
            public static Exception We_ge_to_the_end_of_list_and_dont_find_the_drone = new Exception("We get to the end of list and don't find the drone");
            public static Exception Get_wrong_string_for_geting_access_to_DalObject = new Exception("get wrong string for geting access to DalObject");
            
            public myExceptionDO(Exception e) : base(e.ToString()) { }
            public myExceptionDO(string s, Exception e) : base(s, e) { }
            public myExceptionDO(string s) : base(s) { }


        }
    }


