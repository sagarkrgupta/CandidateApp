using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateApp.Domain.Shared.Utilities
{
    public static class DateTimeHelper
    {
        // Method to get the current DateTime in the machine's region (local time)
        public static DateTime GetLocalTime()
        {
            // Return the current DateTime based on the system's local time zone
            return DateTime.Now;
        }

        // Method to convert DateTime from UTC to a specific time zone
        public static DateTime ConvertFromUtcToTimeZone(DateTime utcDateTime, string timeZoneId)
        {
            try
            {
                // Find the time zone by its ID
                TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                // Convert UTC to the desired time zone
                return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, timeZone);
            }
            catch (TimeZoneNotFoundException)
            {
                throw new ArgumentException("Invalid time zone ID.");
            }
        }

        // Method to get the current DateTime in UTC
        public static DateTime GetCurrentUtcTime()
        {
            return DateTime.UtcNow;
        }

        // Method to convert a DateTime to a specific time zone
        public static DateTime ConvertToTimeZone(DateTime dateTime, string timeZoneId)
        {
            try
            {
                // Find the time zone by its ID
                TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                // Convert the given DateTime to the specified time zone
                return TimeZoneInfo.ConvertTime(dateTime, timeZone);
            }
            catch (TimeZoneNotFoundException)
            {
                throw new ArgumentException("Invalid time zone ID.");
            }
        }

    }
}
