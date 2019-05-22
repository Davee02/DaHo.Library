﻿using System;
using System.Runtime.InteropServices;
using System.Security;

namespace DaHo.Library.Utilities
{
    public static class StringUtilities
    {
        public static bool Contains(this string source, string value, StringComparison comparisonType)
        {
            return !string.IsNullOrEmpty(value)
                   && source.IndexOf(value, comparisonType) != -1;
        }

        public static string ConvertToString(this SecureString value)
        {
            IntPtr bstr = Marshal.SecureStringToBSTR(value);

            try
            {
                return Marshal.PtrToStringBSTR(bstr);
            }
            finally
            {
                Marshal.FreeBSTR(bstr);
            }
        }
    }
}
