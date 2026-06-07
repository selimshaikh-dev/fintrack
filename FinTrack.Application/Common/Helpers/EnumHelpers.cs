using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Common.Helpers
{
    public static class EnumHelpers
    {
        /// <summary>
        /// Retrieve the description on the enum, e.g.
        /// [Description("Bright Pink")]
        /// BrightPink = 2,
        /// Then when you pass in the enum, it will retrieve the description
        /// ex EnumHelper.GetDescription(UserColours.BrightPink);
        /// </summary>
        /// <param name="value">The Enumeration</param>
        /// <returns>A string representing the friendly name</returns>
        public static string GetEnumDescription(this Enum value)

        {

            var enumType = value.GetType();

            var field = enumType.GetField(value.ToString());

            var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length == 0 ? value.ToString() : ((DescriptionAttribute)attributes[0]).Description;

        }

        public static string GetDescription(Enum value)
        {
            return
                value
                    .GetType()
                    .GetMember(value.ToString())
                    .FirstOrDefault()
                    ?.GetCustomAttribute<DescriptionAttribute>()
                    ?.Description
                ?? value.ToString();
        }



        /// <summary>
        /// For this method working Enum Description must be required 
        /// without enum Description this method will throw null refference 
        /// </summary>
        /// <returns>A string representing the friendly name</returns>
        public static List<KeyValuePair<int, string>> GetSelectListItemsWithDescription<T>()
        {
            var enumList = Enum.GetValues(typeof(T)).Cast<Enum>().Select(value => new
            {
                ((DescriptionAttribute)Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute))).Description,
                value
            })
                .OrderBy(item => item.value)
                .ToList();
            var selectList = new List<KeyValuePair<int, string>>();
            foreach (var item in enumList)
            {
                selectList.Add(new KeyValuePair<int, string>(item.value.GetHashCode(), item.Description));

            }

            return selectList;
        }



        /// <summary>
        /// returns list of enum 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>KeyValuePair<text,value></returns>
        public static List<KeyValuePair<string, string>> GetSelectListItemsWithoutDescription<T>()
        {
            var list = Enum.GetNames(
                typeof(T)).Select(name => new KeyValuePair<string, string>(name, name));


            return list.ToList();
        }
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static string GetEnumDescription(object enumValue)
        {
            FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());

            if (null != fi)
            {
                object[] attrs = fi.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }

            return null;
        }
    }
}
