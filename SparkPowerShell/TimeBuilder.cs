using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkPowerShell
{
    public static class TimeBuilder
    {
        public static string BuildString()
        {
            string strDateString = "";

            strDateString += "set-date \"";
            strDateString += DateTime.Now.DayOfWeek;
            strDateString += ", ";
            strDateString += CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);
            strDateString += " ";
            strDateString += DateTime.Now.Day;
            strDateString += ", ";
            strDateString += DateTime.Now.Year;
            strDateString += " ";
            strDateString += DateTime.Now.TimeOfDay.ToString();
            strDateString += "\"";

            return strDateString;
        }
       

        
    }
}
