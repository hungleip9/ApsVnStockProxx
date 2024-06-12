using System;

namespace VnStockproxx
{
    public static class DateTimeExtensions
    {
        public static string ToCustomFormat(this DateTime? dateTime, string format = "HH:mm dd/MM/yyyy")
        {
            return dateTime.HasValue ? dateTime.Value.ToString(format) : string.Empty;
        }
        public static string ToCustomFormat(this DateTime dateTime, string format = "HH:mm dd/MM/yyyy")
        {
            return dateTime.ToString(format);
        }
    }
}
