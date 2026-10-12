using System;
using System.ComponentModel;


namespace Utility
{
    public class Helper
    {
        public static string ValIfNullOrEmpty(string input, bool trim, string valToReturn)
        {
            if (String.IsNullOrEmpty(input) || String.IsNullOrWhiteSpace(input))
                return valToReturn;

            if (trim)
                return input.Trim();
            else
                return input;
        }

        public static string RandomColor()
        {
            var random = new Random();
            string color = String.Format("#{0:X6}", random.Next(0x1000000));
            return color;
        }

        public static string GetEnumDescription(System.Type enumType_, object value_)
        {
            string value = Enum.GetName(enumType_, value_);
            var memInfo = enumType_.GetMember(value);
            var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            return ((DescriptionAttribute)attributes[0]).Description;
        }


    }
}
