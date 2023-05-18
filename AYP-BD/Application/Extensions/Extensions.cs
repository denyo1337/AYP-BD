using AutoMapper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extensions
{
    public static class Extensions
    {
        public static T Parse<T>(this string input, IFormatProvider provider = null) where T : IParsable<T>
        {
            return T.Parse(input, provider);
        }
    }
}
