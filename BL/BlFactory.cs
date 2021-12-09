using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    static public class BlFactory
    {
     //**********************************************לחזור להבין מה הסינון הנדרש ומה החריגה הרלוונטית
        static public IBL GetBl(string s)
        {
            if (s == "")
            {
                return new BL();
            }
            else
                throw new Exception();
        }
    }
}
