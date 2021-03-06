// ***********************************************************************
// Assembly         : FCS.Lib
// Author           : FH
// Created          : 27-08-2016
//
// Last Modified By : Frede H.
// Last Modified On : 2020-08-30
// ***********************************************************************
// <copyright file="Mogrifiers.cs" company="FCS">
//     Copyright � FCS 2015-2020
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Text;

namespace FCS.Lib
{
    /// <summary>
    /// Class Converters
    /// </summary>
    public static class Mogrifiers
    {
        /// <summary>
        /// Reverse boolean
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool BoolReverse(bool value)
        {
            return !value;
        }

        /// <summary>
        /// Boolean to integer
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns>System.Int32.</returns>
        public static int BoolToInt(bool value)
        {
            return value ? 1 : 0;
        }

        /// <summary>
        /// Boolean to string
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns>System.String.</returns>
        public static string BoolToString(bool value)
        {
            return value ? "true" : "false";
        }


        /// <summary>
        /// Enum to integer
        /// </summary>
        /// <param name="enumeration">The enumeration.</param>
        /// <returns>System.Int32.</returns>
        public static int EnumToInt(object enumeration)
        {
            return Convert.ToInt32(enumeration, CultureInfo.InvariantCulture);
        }


        /// <summary>
        /// Enum to string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public static string EnumToString(Enum value)
        {
            return value == null ? string.Empty : value.ToString();
        }

        /// <summary>
        /// Integer to boolean.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool IntToBool(int value)
        {
            return value == 1;
        }

        /// <summary>
        /// Integer to enum.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns>T.</returns>
        public static T IntToEnum<T>(int value)
        {
            return (T) Enum.ToObject(typeof(T), value);
        }

        /// <summary>
        /// Integer to letter.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public static string IntToLetter(int value)
        {
            var empty = string.Empty;
            var num = 97;
            var str = "";
            var num1 = 0;
            var num2 = 97;
            for (var i = 0; i <= value; i++)
            {
                num1++;
                empty = string.Concat(str, Convert.ToString(Convert.ToChar(num), CultureInfo.InvariantCulture));
                num++;
                if (num1 != 26) continue;
                num1 = 0;
                str = Convert.ToChar(num2).ToString(CultureInfo.InvariantCulture);
                num2++;
                num = 97;
            }

            return empty;
        }

        /// <summary>
        /// Lists to string using semicolon(;)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <returns>System.String.</returns>
        public static string ListToString<T>(List<T> list)
        {
            return ListToString(list, ";");
        }

        /// <summary>
        /// Lists to string userdefined delimiter
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns>System.String.</returns>
        public static string ListToString<T>(List<T> list, string delimiter)
        {
            var empty = string.Empty;
            if (list == null) return empty;
            var enumerator = (IEnumerator) list.GetType().GetMethod("GetEnumerator")?.Invoke(list, null);
            while (enumerator != null && enumerator.MoveNext())
                if (enumerator.Current != null)
                    empty = string.Concat(empty, enumerator.Current.ToString(), delimiter);

            return empty;
        }


        /// <summary>
        /// String to bool.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "<Pending>")]
        public static bool StringToBool(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;

            var flag = false;
            var upper = value.ToUpperInvariant();
            if (string.Compare(upper, "true", StringComparison.OrdinalIgnoreCase) == 0)
            {
                flag = true;
            }
            else if (string.CompareOrdinal(upper, "false") == 0)
            {
            }
            else if (string.CompareOrdinal(upper, "1") == 0)
            {
                flag = true;
            }

            return flag;
        }

        /// <summary>
        /// String to decimal.
        /// </summary>
        /// <param name="inString">The in string.</param>
        /// <returns>System.Nullable&lt;System.Decimal&gt;.</returns>
        public static decimal? StringToDecimal(string inString)
        {
            if (string.IsNullOrEmpty(inString)) return 0;
            return
                !decimal.TryParse(inString.Replace(",", "").Replace(".", ""), NumberStyles.Number,
                    CultureInfo.InvariantCulture, out var num)
                    ? (decimal?) null
                    : decimal.Divide(num, new decimal((long) 100));
        }

        /// <summary>
        /// String to enum.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns>T.</returns>
        public static T StringToEnum<T>(string value)
        {
            return (T) Enum.Parse(typeof(T), value, true);
        }

        /// <summary>
        /// String to list using semicolon(;).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns>List&lt;T&gt;.</returns>
        public static List<T> StringToList<T>(string value)
        {
            return StringToList<T>(value, ";");
        }

        /// <summary>
        /// String to list userdefined delimiter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns>List&lt;T&gt;.</returns>
        /// <exception cref="System.ArgumentNullException">value</exception>
        /// <exception cref="System.ArgumentNullException">delimiter</exception>
        /// <exception cref="ArgumentNullException">value</exception>
        /// <exception cref="ArgumentNullException">delimiter</exception>
        public static List<T> StringToList<T>(string value, string delimiter)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(value));
            if (string.IsNullOrEmpty(delimiter)) throw new ArgumentNullException(nameof(delimiter));

            var ts = new List<T>();
            var strArrays = value.Split(delimiter.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (var str in strArrays)
            {
                var o = typeof(T).FullName;
                if (o == null) continue;
                var upperInvariant = o.ToUpperInvariant();
                if (string.CompareOrdinal(upperInvariant, "system.string") == 0)
                {
                    ts.Add((T) Convert.ChangeType(str, typeof(T), CultureInfo.InvariantCulture));
                }
                else if (string.CompareOrdinal(upperInvariant, "system.int32") == 0)
                {
                    ts.Add((T) Convert.ChangeType(str, typeof(T), CultureInfo.InvariantCulture));
                }
                else if (string.CompareOrdinal(upperInvariant, "system.guid") == 0)
                {
                    var guid = new Guid(str);
                    ts.Add((T) Convert.ChangeType(guid, typeof(T), CultureInfo.InvariantCulture));
                }
            }

            return ts;
        }

        /// <summary>
        /// String to stream using system default encoding.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Stream.</returns>
        public static Stream StringToStream(string value)
        {
            return StringToStream(value, Encoding.Default);
        }


        /// <summary>
        /// Strings to stream with userdefined encoding.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>Stream.</returns>
        public static Stream StringToStream(string value, Encoding encoding)
        {
            return encoding == null ? null : new MemoryStream(encoding.GetBytes(value ?? ""));
        }

        /// <summary>
        /// Returns timestamp for current date-time object.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public static long CurrentDateTimeToTimeStamp()
        {
            return Convert.ToUInt32(DateTimeToTimeStamp(DateTime.Now));
        }

        /// <summary>
        /// Convert a DateTime object to timestamp
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns>System.Int64.</returns>
        public static long DateTimeToTimeStamp(DateTime dateTime)
        {
            var bigDate = new DateTime(2038, 1, 19, 0, 0, 0, 0);
            var nixDate = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            
            if (dateTime >= bigDate)
                return Convert.ToUInt32((bigDate - nixDate).TotalSeconds) +
                       Convert.ToUInt32((dateTime - bigDate).TotalSeconds);
            
            return Convert.ToUInt32((dateTime - nixDate).TotalSeconds);
        }

        /// <summary>
        /// Convert timestamp to DataTime format
        /// </summary>
        /// <param name="timeStamp">The time stamp.</param>
        /// <returns>DateTime.</returns>
        public static DateTime TimeStampToDateTime(long timeStamp)
        {
            var nixDate = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return nixDate.AddSeconds(timeStamp);
        }

        /// <summary>
        /// Convert timespan to seconds
        /// </summary>
        /// <param name="timespan">The timespan.</param>
        /// <returns>System.Int32.</returns>
        public static long TimeSpanToSeconds(TimeSpan timespan)
        {
            return Convert.ToUInt32(timespan.Ticks / 10000000L);
        }

        /// <summary>
        /// Converts seconds to timespan
        /// </summary>
        /// <param name="seconds">The seconds.</param>
        /// <returns>TimeSpan.</returns>
        public static TimeSpan SecondsToTimeSpan(long seconds)
        {
            return TimeSpan.FromTicks(10000000L * seconds);
        }

        /// <summary>
        /// Converts timespan to minutes
        /// </summary>
        /// <param name="timespan">The timespan.</param>
        /// <returns>System.Int32.</returns>
        public static long TimespanToMinutes(TimeSpan timespan)
        {
            return Convert.ToUInt32(timespan.Ticks / 10000000L) / 60;
        }
    }
}