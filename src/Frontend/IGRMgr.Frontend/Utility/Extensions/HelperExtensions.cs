using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IGRMgr.Frontend.Utility.Extensions
{
    public static class HelperExtensions
    {
        public static string ExtractString(this string source, string pattern)
        {
            var result = string.Empty;
            Match match = Regex.Match(source, pattern);
            if (match.Success)
                result = match.Value;
            return result;
        }

        public static byte[] ConvertToByteArray(this Stream stream)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                stream.Position = 0;
                stream.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public static DateTime UnixTimeStampToDateTime(this double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp);
            return dtDateTime;
        }
    }
}
