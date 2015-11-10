using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManager.Core.Service
{
    public class DateTimeExtensions
    {
        // Return the first day of the month corresponding to the input date
        public static DateTime FirstDayOfMonth(DateTime dateTimeValue)
        {
            return new DateTime(dateTimeValue.Year, dateTimeValue.Month, 1);
        }

        // Return the last day of the month corresponding to the input date
        public static DateTime LastDayOfMonth(DateTime dateTimeValue)
        {
            return new DateTime(dateTimeValue.Year, dateTimeValue.Month, 
                                DateTime.DaysInMonth(dateTimeValue.Year, dateTimeValue.Month));
        }

        // Return the minimum of the two input dates
        public static DateTime MinDate(DateTime dateTimeValue1, DateTime dateTimeValue2)
        {

            return (dateTimeValue1 < dateTimeValue2) ? dateTimeValue1 : dateTimeValue2;
        }

        // Return the maximum of the two input dates
        // If null is accepted, return the non-null date
        public static DateTime MaxDate(DateTime dateTimeValue1, DateTime dateTimeValue2)
        {           
            return (dateTimeValue1 > dateTimeValue2) ? dateTimeValue1 : dateTimeValue2;
        }
    }
}
