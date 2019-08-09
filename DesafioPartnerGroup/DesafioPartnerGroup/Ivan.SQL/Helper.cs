using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ivan.Service;
using Ivan.Dependency;

namespace Ivan.SQL
{
    public static class Helper
    {
        #region "Convert a list of values in the SQL IN list of values"
        /// <summary>
        /// Convert a list of values in the SQL IN list of values
        /// </summary>
        /// <typeparam name="T">Desired type to create the list</typeparam>
        /// <param name="list">List of values</param>
        /// <param name="defautValue">Default value to be used if there is no values in the list</param>
        /// <param name="addParentheses">If true parentheses will be added in the start and end of the in list</param>
        /// <returns>A string with the IN values formatted</returns>
        public static string ToSqlIn<T>(IList<T> list, T defautValue, bool addParentheses)
        {
            StringBuilder sb = new StringBuilder("");
            string quotas = string.Empty;

            if (defautValue is int || defautValue is long || defautValue is Single || defautValue is double)
                quotas = string.Empty;
            else
                quotas = "'";

            // The passed list is empty, then just return the default
            if (list == null || list.Count == 0)
            {
                if (addParentheses)
                {
                    return string.Format("({0}{1}{2})", quotas, defautValue, quotas);
                }
                else
                {
                    return string.Format("{0}{1}{2}", quotas, defautValue, quotas);
                }
            }

            if (addParentheses) sb.Append("(");

            foreach (T item in list)
            {
                sb.Append(string.Format("{0}{1}{2}, ", quotas, item, quotas));
            }

            sb.Append(string.Format("{0}{1}{2}", quotas, defautValue, quotas));

            if (addParentheses) sb.Append(")");

            return sb.ToString();
        }
        #endregion



        /// <summary>
        /// Convert one System.Collections.Generic.ICollection to an integer array.
        /// </summary>
        /// <typeparam name="TKeys">System.Collections.Generic.ICollection</typeparam>
        /// <param name="keys">The collection to be converted.</param>
        /// <returns>An integer array.</returns>
        public static int[] ToIntArray<TKeys>(ICollection<TKeys> keys)
        {
            //System.Collections.Generic.KeyCollection
            int[] arr = new int[keys.Count];
            int count = 0;

            foreach (TKeys key in keys)
            {
                arr[count] = Convert.ToInt32(key);
                count++;
            }

            return arr;
        }


    }
}
