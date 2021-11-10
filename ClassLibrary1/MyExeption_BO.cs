using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    {
        public class MyExeption_BO : Exception
        {
           public static Exception There_is_no_variable_with_this_ID = new Exception( "There is no variable with this ID.");
            public static Exception An_empty_list = new Exception("An empty list.");
            public static Exception Only_numbers_should_be_type_to = new Exception("Only numbers should be type to.");
           // public static Exception
            //public static Exception 
            //public static Exception
            //public static Exception
            public MyExeption_BO(Exception e) : base(e.ToString()) { }

        }
    
    }
}
