using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BitCoinPriceMonitor.Infrastructure.ExtensionMethods
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string source) => string.IsNullOrEmpty(source); 

        public static Maybe<TObject> Deserialize<TObject>(this string json)
        {
            try
            {

                if (json.IsNullOrEmpty())
                    return Maybe<TObject>.None;
                var desObject = JsonSerializer.Deserialize<TObject>(json);
                return desObject != null ? desObject : Maybe<TObject>.None;
            }
            catch 
            {
                return Maybe<TObject>.None;
            }
        }
    }
}
