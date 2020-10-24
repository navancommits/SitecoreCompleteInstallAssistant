using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace SCIA.Common
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string @string)
        {
            return string.IsNullOrEmpty(@string);
        }

        public static string EnsureEnd(this string @this, string end)
        {
            return @this.TrimEnd(end) + end;
        }

        public static string TrimEnd(this string source, string value)
        {
            return source.EndsWith(value) ? source.Substring(0, source.Length - value.Length) : source;
        }

        public static string EmptyToNull(this string source)
        {
            return string.IsNullOrEmpty(source) ? null : source;
        }

        
        public static XmlElement SelectSingleElement(this XmlDocument document, string xpath)
        {
            return document.SelectSingleNode(xpath) as XmlElement;
        }

        public static IEnumerable<XmlElement> SelectElements(this XmlDocument document, string xpath)
        {

            return document.SelectNodes(xpath)?.OfType<XmlElement>() ?? new XmlElement[0];
        }
    }
}
