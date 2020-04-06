using System.Web;

namespace Aliera.Utilities.Encoding
{
    public static class HtmlEncodeDecode
    {
        /// <summary>
        /// Html Encode 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string HtmlEncode(string value)
        {
           return HttpUtility.HtmlEncode(value);
        }

        /// <summary>
        /// Html Decode
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string HtmlDecode(string value)
        {
            return HttpUtility.HtmlDecode(value);
        }
    }
}
