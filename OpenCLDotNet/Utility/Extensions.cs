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

        public static string RemoveWhiteSpaces(this string str)
        {
            string result = "";

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] != ' ')
                    result += str[i];
            }
                 
            return result;
        }

        public static bool IsEmpty(this cl_char[] array)
        {
            int size = array.Length;

            for (int i = 0; i < size; i++)
            { 
                char c = array[i];

                if(c != ' ' && c !='\0')
                    return false;
            }

            return true;
        }

        public static string ToText(this cl_char[] array, bool ignore_null = true)
        {
            return ToText(array, array.Length, ignore_null);
        }

        public static string ToText(this cl_char[] array, int size, bool ignore_null = true)
        {
            var str = new StringBuilder(size);
            for (int i = 0; i < size; i++)
            {
                var c = (Char)array[i];

                if (ignore_null && c != '\0')
                    str.Append(c);
            }

            return str.ToString();
        }

    }
}
