using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IisReader.Utilits
{
    public static class DateTimeConverter
    {
        public static DateTime FileTimeToDateTime(long fileTime)
        {
            return DateTime.FromFileTimeUtc(fileTime);
        }
    }
}
