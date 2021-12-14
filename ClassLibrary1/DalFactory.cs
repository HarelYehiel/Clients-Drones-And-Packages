using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi.DalObject;
using DO;

namespace DalApi
{
    static public class DalFactory
    {
        //לחזור לפה להבין מה המחלקה אמורה להחזיר ומה היא אמורה לזרוק
        static public IDal GetDal(string s)
        {
            if (s == "DalObject")
                return DalObject.DalObject.Instance;
            else
                throw new myExceptionDO("Exception from function DalFactory.GetDal", myExceptionDO.Get_wrong_string_for_geting_access_to_DalObject);
        }
    }
}
