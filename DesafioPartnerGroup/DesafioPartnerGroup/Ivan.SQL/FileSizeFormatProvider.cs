using System;
using System.Collections.Generic;


namespace Ivan.SQL
{
    public class FileSizeFormatProvider : IFormatProvider, ICustomFormatter
    {
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter)) return this;
            return null;
        }

        private const string fileSizeFormat = "fs";
        private const Decimal OneKiloByte = 1024M;
        private const Decimal OneMegaByte = OneKiloByte * 1024M;
        private const Decimal OneGigaByte = OneMegaByte * 1024M;

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (format == null || !format.StartsWith(fileSizeFormat))
            {
                return defaultFormat(format, arg, formatProvider);
            }

            if (arg is string)
            {
                return defaultFormat(format, arg, formatProvider);
            }

            Decimal size;

            try
            {
                size = Convert.ToDecimal(arg);
            }
            catch (InvalidCastException)
            {
                return defaultFormat(format, arg, formatProvider);
            }

            string suffix;
            if (size > OneGigaByte)
            {
                size /= OneGigaByte;
                suffix = "GB";
            }
            else if (size > OneMegaByte)
            {
                size /= OneMegaByte;
                suffix = "MB";
            }
            else if (size > OneKiloByte)
            {
                size /= OneKiloByte;
                suffix = "KB";
            }
            else
            {
                suffix = "B";
            }

            string precision = format.Substring(2);
            if (String.IsNullOrEmpty(precision)) precision = "2";
            return String.Format("{0:N" + precision + "} {1}", size, suffix);

        }

        private static string defaultFormat(string format, object arg, IFormatProvider formatProvider)
        {
            IFormattable formattableArg = arg as IFormattable;
            if (formattableArg != null)
            {
                return formattableArg.ToString(format, formatProvider);
            }
            return arg.ToString();
        }



        /// <summary>
        /// Convert a number to string with the database value formatted
        /// </summary>
        /// <param name="value">The database value to be converted</param>
        /// <param name="addUnit">If true the unit will be added e.g. MB, GB default is True!</param>
        /// <returns>
        /// A string with the database value formatted if the parameter "addUnit" is True
        /// </returns>
        static public string ToStringDatabaseUnit(long value, bool addUnit = true)
        {
            if ((value / (128 * 1024)) < 1) // if it is less than 1 then display size in MB instead of GB
                return (Convert.ToDouble(value) / 128).ToString("0.###") + (addUnit ? " MB" : "");
            else
                return (Convert.ToDouble(value) / (128 * 1024)).ToString("0.###") + (addUnit ? " GB" : "");
        }

    }
}