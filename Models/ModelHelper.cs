using System.Reflection;

namespace VnStockproxx.Models
{
    public static class ModelHelper
    {
        public static List<PropertyInfo> GetProperties<T>(T model)
        {
            return typeof(T).GetProperties().ToList();
        }
    }
}
