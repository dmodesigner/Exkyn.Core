using System;

namespace Exkyn.Core.Helpers
{
    public static class HourHelpers
    {
        public static DateTime Initial(DateTime date) => Convert.ToDateTime(String.Format("{0:yyyy-MM-dd 00:00:00}", date));

        public static DateTime Final(DateTime date) => Convert.ToDateTime(String.Format("{0:yyyy-MM-dd 23:59:59}", date));
    }
}