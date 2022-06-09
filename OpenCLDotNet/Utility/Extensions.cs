using System;
using System.Collections.Generic;
using System.Text;

using OpenCLDotNet.Core;

namespace OpenCLDotNet.Utility
{
    public static class StringExtensions
    {
        public static cl_char[] ToCLCharArray(this string str, bool null_terminate = true)
        {
            int len = str.Length;
            if (null_terminate)
                len++;

            var str_char = new cl_char[len];

            for (int i = 0; i < str.Length; i++)
                str_char[i] = (cl_char)str[i];

            if(null_terminate)
                str_char[str.Length] = (cl_char)('\0');

            return str_char;    
        }

        public static string ToText(this cl_char[] array, bool ignore_null = true)
        {
            int len = array.Length;

            var str = new StringBuilder(len);
            for (int i = 0; i < len; i++)
            {
                var c = (Char)array[i];

                if(ignore_null && c != '\0')
                    str.Append(c);
            }
                
            return str.ToString();
        }

    }
}
