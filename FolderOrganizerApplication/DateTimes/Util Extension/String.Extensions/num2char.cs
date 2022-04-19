
using System;

namespace MMS.Framework.Util_Extension.String.Extensions
{
    public static class DigitToPersianLetter
    {
        private static readonly string[] yekan = { "صفر", "یک", "دو", "سه", "چهار", "پنج", "شش", "هفت", "هشت", "نه" };
        private static readonly string[] dahgan =  { "", "", "بیست", "سی", "چهل", "پنجاه", "شصت", "هفتاد", "هشتاد", "نود" };
        private static readonly string[] dahyek =  { "ده", "یازده", "دوازده", "سیزده", "چهارده", "پانزده", "شانزده", "هفده", "هجده", "نوزده" };
        //array[10..19]
        private static readonly string[] sadgan =  { "", "یکصد", "دویست", "سیصد", "چهارصد", "پانصد", "ششصد", "هفتصد", "هشتصد", "نهصد" };
        private static readonly string[] basex =  { "", "هزار", "میلیون", "میلیارد", "تریلیون" };


        private static string getnum3(int num3)
        {
            string s = "";
            int d12 = num3 % 100;
            int d3 = num3 / 100;
            if (d3 != 0)
                s = sadgan[d3] + " و ";
            if ((d12 >= 10) && (d12 <= 19))
            {
                s = s + dahyek[d12 - 10];
            }
            else
            {
                int d2 = d12 / 10;
                if (d2 != 0)
                    s = s + dahgan[d2] + " و ";
                int d1 = d12 % 10;
                if (d1 != 0)
                    s = s + yekan[d1] + " و ";
                s = s.Substring(0, s.Length - 3);
            }
            return s;
        }

        public static string ConvertDigitToPersianLetter(this double number)
        {
            return ConvertDigitToPersianLetter(number.ToString());
        }
        public static string ConvertDigitToPersianLetter(this Int64 number)
        {
            return ConvertDigitToPersianLetter(number.ToString());
        }
        public static string ConvertDigitToPersianLetter(this string number)
        {
            string stotal = "";
            if (number == "0")
            {
                return yekan[0];
            }
            else
            {
                number = number.PadLeft(((number.Length - 1) / 3 + 1) * 3, '0');
                int L = number.Length / 3 - 1;
                for (int i = 0; i <= L; i++)
                {
                    int b = int.Parse(number.Substring(i * 3, 3));
                    if (b != 0)
                        stotal = stotal + getnum3(b) + " " + basex[L - i] + " و ";
                }
                stotal = stotal.Substring(0, stotal.Length - 3);
            }
            return stotal;
        }
    }
}
