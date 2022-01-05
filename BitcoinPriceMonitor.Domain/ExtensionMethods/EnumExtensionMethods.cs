using BitcoinPriceMonitor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using BitcoinPriceMonitor.Domain.Attributes;

namespace BitcoinPriceMonitor.Domain.ExtensionMethods
{
    public static class EnumExtensionMethods
    {
        public static string GetUid(this PriceSourceEnum @enum)
        {
            return @enum.GetAttribute<EnumIdAttribute>()?.Id ?? string.Empty;
        }

        public static TAttribute GetAttribute<TAttribute>(this Enum @enum) where TAttribute : Attribute
        {
            return @enum.GetType()
            .GetMember(@enum.ToString())
            .FirstOrDefault()?.GetCustomAttribute<TAttribute>();
        }
    }
}
