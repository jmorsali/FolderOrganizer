using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace MMS.Framework.Util_Extension.String.Extensions
{
    public static partial class Utils
    {
        public static string Money3Dispaly(this long InputValue)
        {
            if (InputValue == 0)
                return "0";
            return $"{InputValue:#,###}";
        }

        public static bool IsHexDigit(this string InputValue)
        {
            var hexdigits = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
            return InputValue.All(hexdigits.Contains);
        }

        public static string ToPersinDigit(this string InputValue)
        {
            string UniCodeStr = "";
            foreach (char c in InputValue)
            {
                if (char.IsDigit(c))
                    UniCodeStr += ((char)(c + 1728));
                else
                    UniCodeStr += c;
            }
            return UniCodeStr;
        }
        public static string PersianUnify(this string InputValue)
        {
            return InputValue.Replace("ي", "ی").Replace("ئ", "ی").Replace("ك", "ک").Replace(" ", "").Replace("ء","");
        }

        public static string ToPersianPhrase(this string InputValue)
        {
            InputValue = InputValue.Replace("ي", "ی").Replace("ئ", "ی").Replace("ك", "ک");

            var result = string.Empty;
            foreach (var pchar in InputValue)
            {
                if (IsPersian(pchar.ToString()))
                    result += pchar;
            }
            return result;
        }

        public static bool IsPersian(this string InputValue)
        {
            return Regex.IsMatch(InputValue, "^[ \\-ئةي,،آابپتثجچحخدذرزژسشصضطظعغفقکگلمنوهیكئ()/]+$");
        }

        public static bool IsPersianAndNumeric(this string InputValue)
        {
            return Regex.IsMatch(InputValue, "^[0-9 \\-ئةي,،آابپتثجچحخدذرزژسشصضطظعغفقکگلمنوهیكئ()/]*$");
        }

        public static string ToLatinDigit(this string InputValue)
        {
            string UniCodeStr = "";
            foreach (char c in InputValue)
            {
                if (char.IsDigit(c) && c >= 1776 && c < 1786)
                    UniCodeStr += ((char)(c - 1728));
                else if (char.IsDigit(c) && c >= 1632 && c < 1642)
                    UniCodeStr += ((char)(c - 1584));
                else
                    UniCodeStr += c;
            }
            return UniCodeStr;
        }
        public static string ToMoney3Display(this string InputValue)
        {
            try
            {
                long _InputValue = Convert.ToInt64(InputValue);
                return Money3Dispaly(_InputValue);
            }
            catch
            {
                return InputValue;
            }
        }

        public static string ToMoney3Display(this int? InputValue)
        {
            try
            {
                long _InputValue = Convert.ToInt64(InputValue);
                return Money3Dispaly(_InputValue);
            }
            catch
            {
                return InputValue.ToString();
            }
        }
        public static string ToMoney3Display(this long? InputValue)
        {
            try
            {
                long _InputValue = Convert.ToInt64(InputValue);
                return Money3Dispaly(_InputValue);
            }
            catch
            {
                return InputValue.ToString();
            }
        }

        public static string ToMoney3Display(this long InputValue)
        {
            try
            {
                long _InputValue = Convert.ToInt64(InputValue);
                return Money3Dispaly(_InputValue);
            }
            catch
            {
                return InputValue.ToString();
            }
        }

    }
}
