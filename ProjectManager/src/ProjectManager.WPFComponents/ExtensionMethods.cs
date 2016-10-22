﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.WPFComponents
{
    public static class ExtentionMethods
    {
        public static string NullToString(this object o)
        {
            return o == null ? string.Empty : o.ToString();
        }

        public static Task Try(this Task task, string errorMsg = "Error", bool isFatal = true)
        {
            task.ContinueWith(t =>
            {

            }, TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);
            return task;
        }

        public static Task<T> Try<T>(this Task<T> task, string errorMsg = "Error", bool isFatal = true)
        {
            task.ContinueWith(t =>
            {

            }, TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);
            return task;
        }


        public static List<KeyValuePair<int, string>> ToKeyValuePair(this Enum eenum)
        {
            List<KeyValuePair<int, string>> result = new List<KeyValuePair<int, string>>();

            foreach (var e in Enum.GetValues(eenum.GetType()))
                result.Add(new KeyValuePair<int, string>(Convert.ToInt32(e), e.ToString()));

            return result;
        }
    }
}
