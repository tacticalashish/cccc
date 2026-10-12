using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class EnumHelper
    {
        public static List<NamedKeyValuePair> GetNamedConstantAndValueOfEnum(System.Type type_, char? charToReplace_, char? charToBeReplacedWith_)
        {
            List<NamedKeyValuePair> listOfNamedKeyValuePair = new List<NamedKeyValuePair>();

            foreach (string value in Enum.GetNames(type_))
            {
                if (charToReplace_ != null && charToBeReplacedWith_ != null)
                    listOfNamedKeyValuePair.Add(new NamedKeyValuePair { Key = (byte)Enum.Parse(type_, value), Value = value.Replace(charToReplace_.Value, charToBeReplacedWith_.Value) });
                else
                    listOfNamedKeyValuePair.Add(new NamedKeyValuePair { Key = (byte)Enum.Parse(type_, value), Value = value });
            }

            return listOfNamedKeyValuePair;
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
