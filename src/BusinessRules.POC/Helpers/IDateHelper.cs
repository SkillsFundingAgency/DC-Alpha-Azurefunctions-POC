using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.POC.Helpers
{
    public interface IDateHelper
    {
        DateTime GetLastFridayInMonth(DateTime date);

        int GetAge(DateTime reference, DateTime? doB);

        int GetYearInWhichPersonTurnsTo(int ageTurningTo, DateTime? doB);

        DateTime GetLastFridayInJuneOfAcademicYear(DateTime referenceDate);
    }

    public class DateHelper : IDateHelper
    {
        public int GetAge(DateTime reference, DateTime? doB)
        {
            if (doB == null) return 0;
            int age = reference.Year - doB.Value.Year;
            if (reference < doB.Value.AddYears(age)) age--;

            return age;
        }

        public DateTime GetLastFridayInMonth(DateTime date)
        {
            var firstDayOfNextMonth = new DateTime(date.Year, date.Month, 1).AddMonths(1);
            int vector = (((int)firstDayOfNextMonth.DayOfWeek + 1) % 7) + 1;
            return firstDayOfNextMonth.AddDays(-vector);
        }

        public int GetYearInWhichPersonTurnsTo(int ageTurningTo, DateTime? doB)
        {
            if (doB == null) return 0;
            return doB.Value.Year + ageTurningTo;
        }

        public DateTime GetLastFridayInJuneOfAcademicYear(DateTime referenceDate)
        {
            //before 31/aug return current year last friday date
            if(referenceDate.Month <= 8 && referenceDate.Day <= 31)
            {
                return GetLastFridayInMonth(new DateTime(referenceDate.Year, 6, 1));
            }

            return GetLastFridayInMonth(new DateTime(referenceDate.Year+1, 6, 1));
        }

    }


}
