using System;
using System.ComponentModel;



namespace Ivan.Models
{
    public class Enumerations
    {
        /// <summary>
        /// Used to know (remember) what command need to be taken for an object like insert, delete, update ... in the data base
        /// this aproach is useful when we are working with one or more lis of objects in memory, some can deleted inserted etc but
        /// the user don't decided if the changes will be saved or discarted, then with that "flag" in each objec we can control the
        /// final user action over one or more collections in his computer memory
        /// </summary>
        public enum SQLCommands : int
        {
            [Description("(Default action) No action to be taken, unknown")]
            NONE = 1,
            [Description("It is a new item and it suposed to be saved in the database if the final user command will be \"save\" for exemple.")]
            INSERT = 2,
            [Description("It is an old object (exists alredy in the database) but the user edits, changed something in this object then it can be saved in the database if the final user command will be \"save\" for exemple.")]
            UPDATE = 3,
            [Description("It is a new or old object and the user wants delete that but its waiting for the final user command like a \"save\" for exemple.")]
            DELETE = 4
        }


        /// <summary>
        /// Enumeration representing the days of the week like:
        /// Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday
        /// 127 Means All!!, it is a trick if you want select all
        /// </summary>
        [System.Flags]
        public enum DaysOfTheWeek
        {
            [Description("No action defined")]
            None = 0,

            [Description("Sunday")]
            Sunday = 1,

            [Description("Monday")]
            Monday = 2,

            [Description("Tuesday")]
            Tuesday = 4,

            [Description("Wednesday")]
            Wednesday = 8,

            [Description("Thursday")]
            Thursday = 16,

            [Description("Friday")]
            Friday = 32,

            [Description("Saturday")]
            Saturday = 64
        }



        /// <summary>
        /// Enumeration representing the Months of the year, like:
        /// January  February  March  April  May  June  July  August  September  October  November  December
        /// 4095 Means All!!, it is a trick if you want select all
        /// </summary>
        [System.Flags]
        public enum MonthsOfTheYear
        {
            [Description("No action defined")]
            None = 0,

            [Description("January")]
            January = 1,

            [Description("February")]
            February = 2,

            [Description("March")]
            March = 4,

            [Description("April")]
            April = 8,

            [Description("May")]
            May = 16,

            [Description("June")]
            June = 32,

            [Description("July")]
            July = 64,

            [Description("August")]
            August = 128,

            [Description("September")]
            September = 256,

            [Description("October")]
            October = 512,

            [Description("November")]
            November = 1024,

            [Description("December")]
            December = 2048
        }



        /// <summary>
        /// Check if "System.DayOfWeek.???" is one of the days inside the enumeration "DaysOfTheWeek"
        /// This function is able to translate the .NET enum System.DayOfWeek to one of DaysOfTheWeek enum values
        /// </summary>
        /// <param name="dayOfWeek">The "System.DayOfWeek" value like: Sunday (0), Monday (1), Tuesday (2), Wednesday (3), Thursday (4), Friday (5) or Saturday (6)</param>
        /// <param name="daysOfTheWeek">An enumeration containing one or many week days values like None (0), Sunday (1), Monday (2), Tuesday (4), Wednesday (8), Thursday (16), Friday (32) or Saturday (64)</param>
        /// <returns>True if there is an equivalent "System.DayOfWeek" inside the "DaysOfTheWeek" enumeration, otherwise False</returns>
        static public bool ContainsDaysOfTheWeek(System.DayOfWeek dayOfWeek, DaysOfTheWeek daysOfTheWeek)
        {
            int dow = (int)dayOfWeek;
            DaysOfTheWeek test = (DaysOfTheWeek)(int)Math.Pow(2, dow);
            if ((test & daysOfTheWeek) != DaysOfTheWeek.None) return true;

            return false;
        }



        /// <summary>
        /// Check if the "month" parameter is one of the months inside the enumeration "MonthsOfTheYear"
        /// This function is able to translate the number of the month to one of MonthsOfTheYear enum values
        /// </summary>
        /// <param name="month">The month as a number e.g. January (1), February (2), March (3), April (4) ...</param>
        /// <param name="monthsOfTheYear">An enumeration containing one or many months values like None (0), January (1), February (2), March (4), April (8) ...</param>
        /// <returns>True if there is an equivalent month inside the "MonthsOfTheYear" enumeration, otherwise False</returns>
        static public bool ContainsMonthsOfTheYear(int month, MonthsOfTheYear monthsOfTheYear)
        {
            if (month < 1 || month > 12) return false;
            MonthsOfTheYear test = (MonthsOfTheYear)(int)Math.Pow(2, month - 1);
            if ((test & monthsOfTheYear) != MonthsOfTheYear.None) return true;

            return false;
        }    

    }
}