using System.ComponentModel;
using System.Reflection;

namespace Sample.Identity.Domain.Extensions
{
    public static class EnumExtensions
    {
        public static string Description(this IConvertible value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            try
            {
                DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

                if (attributes?.Length > 0)
                    return attributes[0].Description;
                else
                    return value.ToString();
            }
            catch (Exception)
            {
                return value.ToString();
            }
        }

        public static string Name(this Enum value)
        {
            return value.ToString();
        }

        public static int ValueAsInt(this Enum value)
        {
            return Convert.ToInt32(value);
        }
    }
}