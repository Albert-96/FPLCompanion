using System.Reflection;

namespace PrimeNGTableExtension.Utils
{
    public static class CastPropertyValue
    {
        public static object CastPropertiesType(PropertyInfo property, string value)
        {

            if (property?.PropertyType == typeof(int))
                return Convert.ToInt32(value);
            else if (property?.PropertyType == typeof(int?))
                return Convert.ToInt32(value);
            else if (property?.PropertyType == typeof(double))
                return Convert.ToDouble(value);
            else if (property?.PropertyType == typeof(double?))
                return Convert.ToDouble(value);
            else if (property?.PropertyType == typeof(DateTime))
                return Convert.ToDateTime(value);
            else if (property?.PropertyType == typeof(DateTime?))
                return Convert.ToDateTime(value);
            else if (property?.PropertyType == typeof(bool))
                return Convert.ToBoolean(value);
            else if (property?.PropertyType == typeof(bool?))
                return Convert.ToBoolean(value);
            else if (property?.PropertyType == typeof(short))
                return Convert.ToInt16(value);
            else if (property?.PropertyType == typeof(short?))
                return Convert.ToInt16(value);
            else if (property?.PropertyType == typeof(long))
                return Convert.ToInt64(value);
            else if (property?.PropertyType == typeof(long?))
                return Convert.ToInt64(value);
            else if (property?.PropertyType == typeof(float))
                return Convert.ToSingle(value);
            else if (property?.PropertyType == typeof(float?))
                return Convert.ToSingle(value);
            else if (property?.PropertyType == typeof(decimal))
                return Convert.ToDecimal(value);
            else if (property?.PropertyType == typeof(decimal?))
                return Convert.ToDecimal(value);
            else if (property?.PropertyType == typeof(byte))
                return Convert.ToByte(value);
            else if (property?.PropertyType == typeof(byte?))
                return Convert.ToByte(value);

            return value.ToString();
        }
    }
}
