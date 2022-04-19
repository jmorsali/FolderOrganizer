using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using MMS.Entities.BaseEntities;
using MMS.Framework.Iban;
using MMS.Framework.Util_Extension.StringSimilarityHelper;

namespace MMS.Framework.Util_Extension.String.Extensions
{
    public static partial class Utils
    {
        /// <summary>
        /// Replaces a given character with another character in a string. 
        /// The replacement is case insensitive
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <param name="charToReplace">The character to replace</param>
        /// <param name="replacement">The character by which to be replaced</param>
        /// <returns>Copy of string with the characters replaced</returns>
        public static string CaseInsenstiveReplace(this string val, char charToReplace, char replacement)
        {
            Regex regEx = new Regex(charToReplace.ToString(), RegexOptions.IgnoreCase | RegexOptions.Multiline);
            return regEx.Replace(val, replacement.ToString());
        }
        /// <summary>
        /// Replaces a given string with another string in a given string. 
        /// The replacement is case insensitive
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <param name="stringToReplace">The string to replace</param>
        /// <param name="replacement">The string by which to be replaced</param>
        /// <returns>Copy of string with the string replaced</returns>
        public static string CaseInsenstiveReplace(this string val, string stringToReplace, string replacement)
        {
            Regex regEx = new Regex(stringToReplace, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            return regEx.Replace(val, replacement);
        }
        /// <summary>
        /// Replaces the first occurrence of a string with another string in a given string
        /// The replacement is case insensitive
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <param name="stringToReplace">The string to replace</param>
        /// <param name="replacement">The string by which to be replaced</param>
        /// <returns>Copy of string with the string replaced</returns>
        public static string ReplaceFirst(this string val, string stringToReplace, string replacement)
        {
            Regex regEx = new Regex(stringToReplace, RegexOptions.Multiline);
            return regEx.Replace(val, replacement, 1);
        }
        /// <summary>
        /// Replaces the first occurrence of a character with another character in a given string
        /// The replacement is case insensitive
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <param name="charToReplace">The character to replace</param>
        /// <param name="replacement">The character by which to replace</param>
        /// <returns>Copy of string with the character replaced</returns>
        public static string ReplaceFirst(this string val, char charToReplace, char replacement)
        {
            Regex regEx = new Regex(charToReplace.ToString(), RegexOptions.Multiline);
            return regEx.Replace(val, replacement.ToString(), 1);
        }
        /// <summary>
        /// Replaces the last occurrence of a character with another character in a given string
        /// The replacement is case insensitive
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <param name="charToReplace">The character to replace</param>
        /// <param name="replacement">The character by which to replace</param>
        /// <returns>Copy of string with the character replaced</returns>
        public static string ReplaceLast(this string val, char charToReplace, char replacement)
        {
            int index = val.LastIndexOf(charToReplace);
            if (index < 0)
            {
                return val;
            }
            else
            {
                StringBuilder sb = new StringBuilder(val.Length - 2);
                sb.Append(val.Substring(0, index));
                sb.Append(replacement);
                sb.Append(val.Substring(index + 1,
                   val.Length - index - 1));
                return sb.ToString();
            }
        }
        /// <summary>
        /// Replaces the last occurrence of a string with another string in a given string
        /// The replacement is case insensitive
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <param name="stringToReplace">The string to replace</param>
        /// <param name="replacement">The string by which to be replaced</param>
        /// <returns>Copy of string with the string replaced</returns>
        public static string ReplaceLast(this string val, string stringToReplace, string replacement)
        {
            int index = val.LastIndexOf(stringToReplace);
            if (index < 0)
            {
                return val;
            }
            else
            {
                StringBuilder sb = new StringBuilder(val.Length - stringToReplace.Length + replacement.Length);
                sb.Append(val.Substring(0, index));
                sb.Append(replacement);
                sb.Append(val.Substring(index + stringToReplace.Length,
                   val.Length - index - stringToReplace.Length));

                return sb.ToString();
            }
        }
        /// <summary>
        /// Removes occurrences of words in a string
        /// The match is case sensitive
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <param name="filterWords">Array of words to be removed from the string</param>
        /// <returns>Copy of the string with the words removed</returns>
        public static string RemoveWords(this string val, params string[] filterWords)
        {
            return MaskWords(val, char.MinValue, filterWords);
        }
        /// <summary>
        /// Masks the occurence of words in a string with a given character
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <param name="mask">The character mask to apply</param>
        /// <param name="filterWords">The words to be replaced</param>
        /// <returns>The copy of string with the mask applied</returns>
        public static string MaskWords(this string val, char mask, params string[] filterWords)
        {
            string stringMask = mask == char.MinValue ?
               string.Empty : mask.ToString();
            string totalMask = stringMask;

            foreach (string s in filterWords)
            {
                Regex regEx = new Regex(s, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                if (stringMask.Length > 0)
                {
                    for (int i = 1; i < s.Length; i++)
                        totalMask += stringMask;
                }

                val = regEx.Replace(val, totalMask);
                totalMask = stringMask;
            }
            return val;
        }

        /// <summary>
        /// Left pads the passed string using the passed pad string for the total number of spaces. 
        /// </summary>
        /// <param name="val"></param>
        /// <param name="pad">The pad string</param>
        /// <param name="totalWidth">The total width of the resulting string</param>
        /// <param name="cutOff">True to cut off the characters if exceeds the specified width</param>
        /// <returns>Copy of string with the padding applied</returns>
        public static string PadLeft(this string val, string pad, int totalWidth, bool cutOff = false)
        {
            if (val.Length >= totalWidth)
                return val;

            int padCount = pad.Length;
            string paddedString = val;

            while (paddedString.Length < totalWidth)
            {
                paddedString += pad;
            }

            if (cutOff)
                paddedString = paddedString.Substring(0, totalWidth);
            return paddedString;
        }
        /// <summary>
        /// Right pads the passed string using the passed pad string for the total number of spaces. 
        /// It will not cut-off the pad even if it causes the string to exceed the total width.
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <param name="pad">The pad string</param>
        /// <param name="totalWidth">The total width of the resulting string</param>
        /// <returns>Copy of string with the padding applied</returns>
        public static string PadRight(this string val, string pad, int totalWidth)
        {
            return PadRight(val, pad, totalWidth, false);
        }
        /// <summary>
        /// Right pads the passed string using the passed pad string for the total number of spaces. 
        /// </summary>
        /// <param name="val"></param>
        /// <param name="pad">The pad string</param>
        /// <param name="totalWidth">The total width of the resulting string</param>
        /// <param name="cutOff">True to cut off the characters if exceeds the specified width</param>
        /// <returns>Copy of string with the padding applied</returns>
        public static string PadRight(this string val, string pad, int totalWidth, bool cutOff)
        {
            if (val.Length >= totalWidth)
                return val;

            string paddedString = string.Empty;

            while (paddedString.Length < totalWidth - val.Length)
            {
                paddedString += pad;
            }

            if (cutOff)
                paddedString = paddedString.Substring(0, totalWidth - val.Length);
            paddedString += val;
            return paddedString;
        }
        /// <summary>
        /// Removes new line characters from a string
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <returns>Returns copy of string with the new line characters removed</returns>
        public static string RemoveNewLines(this string val)
        {
            return RemoveNewLines(val, false);
        }
        /// <summary>
        /// Removes new line characters from a string
        /// Test Coverage: Included
        /// </summary>
        /// <param name="input"></param>
        /// <param name="addSpace">True to add a space after removing a new line character</param>
        /// <returns>Returns a copy of the string after removing the new line character</returns>
        public static string RemoveNewLines(this string input, bool addSpace)
        {
            string replace = addSpace ? " " : string.Empty;
            const string pattern = @"[\r|\n]";
            Regex regEx = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            return regEx.Replace(input, replace);
        }
        /// <summary>
        /// Removes a non numeric character from a string
        /// Test Coverage: Included
        /// </summary>
        /// <param name="s"></param>
        /// <returns>Copy of the string after removing non numeric characters</returns>
        public static string RemoveNonNumeric(this string s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
                if (Char.IsNumber(s[i]))
                    sb.Append(s[i]);
            return sb.ToString();
        }
        /// <summary>
        /// Removes numeric characters from a given string
        /// Test Coverage: Included
        /// </summary>
        /// <param name="s"></param>
        /// <returns>Copy of the string after removing the numeric characters</returns>
        public static string RemoveNumeric(this string s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
                if (!Char.IsNumber(s[i]))
                    sb.Append(s[i]);
            return sb.ToString();
        }
        /// <summary>
        /// Reverses a string
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <returns>Copy of the reversed string</returns>
        public static string Reverse(this string val)
        {
            char[] reverse = new char[val.Length];
            for (int i = 0, k = val.Length - 1; i < val.Length; i++, k--)
            {
                if (char.IsSurrogate(val[k]))
                {
                    reverse[i + 1] = val[k--];
                    reverse[i++] = val[k];
                }
                else
                {
                    reverse[i] = val[k];
                }
            }
            return new string(reverse);
        }
        /// <summary>
        /// Changes the string as sentence case.
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <returns>Copy of string with the sentence case applied</returns>
        public static string SentenceCase(this string val)
        {
            if (val.Length < 1)
                return val;

            string sentence = val.ToLower();
            return sentence[0].ToString().ToUpper() +
               sentence.Substring(1);
        }
        /// <summary>
        /// Changes the string as title case.
        /// Ignores short words in the string.
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <returns>Copy of string with the title case applied</returns>
        public static string TitleCase(this string val)
        {
            if (val.Length == 0) return string.Empty;
            return TitleCase(val, true);
        }
        /// <summary>
        /// Changes the string as title case.
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <param name="ignoreShortWords">true to ignore short words</param>
        /// <returns>Copy of string with the title case applied</returns>
        public static string TitleCase(this string val, bool ignoreShortWords)
        {
            if (val.Length == 0) return string.Empty;

            IList<string> ignoreWords = null;
            if (ignoreShortWords)
            {

                ignoreWords = new List<string>();
                ignoreWords.Add("a");
                ignoreWords.Add("is");
                ignoreWords.Add("was");
                ignoreWords.Add("the");
            }

            string[] tokens = val.Split(' ');
            StringBuilder sb = new StringBuilder(val.Length);
            foreach (string s in tokens)
            {
                if (ignoreShortWords == true
                    && s != tokens[0]
                    && ignoreWords.Contains(s.ToLower()))
                {
                    sb.Append(s + " ");
                }
                else
                {
                    sb.Append(s[0].ToString().ToUpper());
                    sb.Append(s.Substring(1).ToLower());
                    sb.Append(" ");
                }
            }

            return sb.ToString().Trim();
        }
        /// <summary>
        /// Removes multiple spaces between words
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <returns>Returns a copy of the string after removing the extra spaces</returns>
        public static string TrimIntraWords(this string val)
        {
            Regex regEx = new Regex(@"[\s]+");
            return regEx.Replace(val, " ");
        }
        /// <summary>
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <param name="charCount">The number of characters after which it should wrap the text</param>
        /// <returns>The copy of the string after applying the Wrap</returns>
        public static string WordWrap(this string val, int charCount)
        {
            return WordWrap(val, charCount, false, Environment.NewLine);
        }
        /// <summary>
        /// Wraps the passed string at the passed total number of characters (if cuttOff is true)
        /// or at the next whitespace (if cutOff is false). 
        /// Uses the environment new line symbol for the break text
        /// </summary>
        /// <param name="val"></param>
        /// <param name="charCount">The number of characters after which to break</param>
        /// <param name="cutOff">true to break at specific</param>
        /// <returns></returns>
        public static string WordWrap(this string val, int charCount, bool cutOff)
        {
            return WordWrap(val, charCount, cutOff, Environment.NewLine);
        }
        private static string WordWrap(this string val, int charCount, bool cutOff, string breakText)
        {
            StringBuilder sb = new StringBuilder(val.Length + 100);
            int counter = 0;

            if (cutOff)
            {
                while (counter < val.Length)
                {
                    if (val.Length > counter + charCount)
                    {
                        sb.Append(val.Substring(counter, charCount));
                        sb.Append(breakText);
                    }
                    else
                    {
                        sb.Append(val.Substring(counter));
                    }
                    counter += charCount;
                }
            }
            else
            {
                string[] strings = val.Split(' ');
                for (int i = 0; i < strings.Length; i++)
                {
                    // added one to represent the space.
                    counter += strings[i].Length + 1;
                    if (i != 0 && counter > charCount)
                    {
                        sb.Append(breakText);
                        counter = 0;
                    }

                    sb.Append(strings[i] + ' ');
                }
            }
            // to get rid of the extra space at the end.
            return sb.ToString().TrimEnd();
        }
        /// <summary>
        /// Converts an list of string to CSV string representation.
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <param name="insertSpaces">True to add spaces after each comma</param>
        /// <returns>CSV representation of the data</returns>
        public static string ToCSV(this IEnumerable<string> val, bool insertSpaces)
        {
            if (insertSpaces)
                return System.String.Join(", ", val.ToArray());
            else
                return System.String.Join(",", val.ToArray());
        }
        /// <summary>
        /// Converts an list of characters to CSV string representation.
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <param name="insertSpaces">True to add spaces after each comma</param>
        /// <returns>CSV representation of the data</returns>
        public static string ToCSV(this IEnumerable<char> val, bool insertSpaces)
        {
            List<string> casted = new List<string>();
            foreach (var item in val)
                casted.Add(item.ToString());

            if (insertSpaces)
                return System.String.Join(", ", casted.ToArray());
            else
                return System.String.Join(",", casted.ToArray());
        }
        /// <summary>
        /// Converts CSV to list of string.
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <returns>IEnumerable collection of string</returns>
        public static IEnumerable<string> ListFromCSV(this string val)
        {
            string[] split = val.Split(',');
            foreach (string item in split)
            {
                item.Trim();
            }
            return new List<string>(split);
        }
        /// <summary>
        /// Binary Serialization to a file
        /// </summary>
        /// <param name="val"></param>
        /// <param name="filePath">The file where serialized data has to be stored</param>
        public static void Serialize(this string val, string filePath)
        {
            try
            {
                Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, val);
                stream.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string Right(this string val, int length)
        {
            return val.Substring(val.Length - length);
        }
        public static int RightDigits(this string val, int length)
        {
            return Convert.ToInt32(Right(val, length));
        }
        public static string StripHTML(this string input)
        {
            return Regex.Replace(input, "<.*?>", System.String.Empty);
        }
        public static string AddSplitter(this string InputString, string Splitter = "-", int Seed = 4)
        {
            int i = Seed;
            while (i < InputString.Length)
            {
                InputString = InputString.Insert(i, Splitter);
                i += (Seed + 1);
            }
            return InputString;
        }
        public static string AddSplitter(this decimal Inputnumber, string Splitter = "-", int Seed = 4)
        {
            return AddSplitter(Inputnumber.ToString(CultureInfo.InvariantCulture), Splitter, Seed);
        }
        public static string MaskCardNo(this string CardNo)
        {
            if (CardNo != null && CardNo.IsValidCardNo())
            {
                return CardNo.Substring(0, 6) + "******" + CardNo.Substring(12, 4);
            }
            return null;
        }



        public static bool IsValidCardNo(this string card_num)
        {
            if (card_num.Length != 16)
                return false;

            return checkCardNoDigits(card_num);
            //return (Crypt.GetCheckMode10(card_num.Substring(0, 15)) == card_num.Substring(15, 1));
        }

        private static bool checkCardNoDigits(string val)
        {
            var sum = 0;
            var i = 0;
            while (i < val.Length)
            {
                var intVal = val.Substring(i, 1).ToInt32();
                if (i % 2 == 0)
                {
                    intVal *= 2;
                    if (intVal > 9)
                    {
                        intVal = 1 + intVal % 10;
                    }
                }
                sum += intVal;
                i++;
            }
            return sum % 10 == 0;


        }

       
        public static bool IsValidAYNAccountNoForShaparak(this string ac_num)
        {
            if (ac_num.IsValidAYNAccountNo())
            {
                if (ac_num.StartsWith("04") || ac_num.StartsWith("08"))
                    return false;
            }
            return true;
        }

        public static bool IsValidAYNAccountNo(this string ac_num)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ac_num) || ac_num.Length != 13)
                    return false;

                string ac_s = ac_num.Substring(0, 12);
                string check_digit = ac_num.Substring(12, 1);

                int check_dig = 5 * Convert.ToInt32(ac_s.Substring(11, 1)) + 7 * Convert.ToInt32(ac_s.Substring(10, 1));

                check_dig = check_dig + 13 * Convert.ToInt32(ac_s.Substring(9, 1)) +
                            17 * Convert.ToInt32(ac_s.Substring(8, 1));

                check_dig = check_dig + 19 * Convert.ToInt32(ac_s.Substring(7, 1)) +
                            23 * Convert.ToInt32(ac_s.Substring(6, 1));

                check_dig = check_dig + 29 * Convert.ToInt32(ac_s.Substring(5, 1)) +
                            31 * Convert.ToInt32(ac_s.Substring(4, 1));

                check_dig = check_dig + 37 * Convert.ToInt32(ac_s.Substring(3, 1)) +
                            41 * Convert.ToInt32(ac_s.Substring(2, 1));

                check_dig = check_dig + 43 * Convert.ToInt32(ac_s.Substring(1, 1)) +
                            47 * Convert.ToInt32(ac_s.Substring(0, 1));

                check_dig = check_dig % 11;
                if (check_dig != 1)
                {
                    check_dig = 11 - check_dig;
                    if (check_dig == 11)
                    {
                        check_dig = 0;
                    }
                    if (check_dig.ToString() == check_digit)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        //public static bool IsValidPostCode(this string postcode)
        //{
        //    return (postcode.IsDecimal() && postcode.Trim().Length == 10);
        //}

        public static bool IsValidPostalCode(this string postalCode)
        {
            postalCode = postalCode.Trim();
            if (postalCode.Length < 10 && !postalCode.IsDecimal()) return false;

            switch (postalCode)
            {
                case "1111111111":
                    return false;
                case "2222222222":
                    return false;
                case "3333333333":
                    return false;
                case "4444444444":
                    return false;
                case "5555555555":
                    return false;
                case "6666666666":
                    return false;
                case "7777777777":
                    return false;
                case "8888888888":
                    return false;
                case "9999999999":
                    return false;
                case "0000000000":
                    return false;
            }
            return true;
        }


        public static bool IsValidMelliCode(this string mellicode)
        {
            try
            {
                string[] InvalidPattern =
                   {
                        "0000000000",
                        "1111111111",
                        "2222222222",
                        "3333333333",
                        "4444444444",
                        "5555555555",
                        "6666666666",
                        "7777777777",
                        "8888888888",
                        "9999999999"
                    };
                if (InvalidPattern.Contains(mellicode.Trim()))
                    return false;
                return ValidateNIN(mellicode);
            }
            catch
            {
                return false;
            }
        }

        private static bool ValidateNIN(string NIN)
        {
            int num3;
            int a = 0;
            bool flag = Regex.IsMatch(NIN, @"^\d{10}$");
            if (!flag)
            {
                return flag;
            }
            for (int i = 2; i < 11; i++)
            {
                char ch = NIN[i - 2];
                a += int.Parse(ch.ToString()) * (12 - i);
            }
            Math.DivRem(a, 11, out num3);
            int num4 = int.Parse(NIN[9].ToString());
            return ((((num3 == 0) && (num4 == 0)) || ((num3 == 1) && (num4 == 1))) || (num4 == (11 - num3)));
        }
        public static bool IsValidMobileNumber(this string mobileNo)
        {
            return (mobileNo.IsInt64() && mobileNo.Trim().Length == 11);
        }
        public static bool IsValidMCINumber(this string mobileNo)
        {
            return (mobileNo.IsInt64() && mobileNo.Length == 11 && mobileNo.StartsWith("091"));
        }

        public static bool IsValidIranCellPhonenumber(this string mobileNo)
        {
            return (mobileNo.IsInt64() && mobileNo.Length == 11 && mobileNo.StartsWith("093"));
        }
        public static bool IsValidPhonenumber(this string PhoneNo)
        {
            if (string.IsNullOrWhiteSpace(PhoneNo) || !PhoneNo.Contains('-'))
                return false;
            PhoneNo = PhoneNo.Trim();
            var PrePhoneNo = PhoneNo.Split('-')[0];
            PhoneNo = PhoneNo.Split('-')[1];
            return PhoneNo.IsInt32() && PrePhoneNo.IsInt32() &&
                    PrePhoneNo.StartsWith("0") && PrePhoneNo.Length == 3 &&
                    PhoneNo.Length == 8;
        }
        public static bool IsValidIrancellWimaxNumber(this string mobileNo)
        {
            return (mobileNo.IsInt64() && mobileNo.Length == 11 && mobileNo.StartsWith("0941"));
        }

        public static string ToNonInternationalPhoneNumber(this string mobileNo)
        {
            if (mobileNo.StartsWith("+98"))
            {
                return mobileNo.ReplaceFirst("+98", "0");
            }
            if (mobileNo.StartsWith("98"))
            {
                return mobileNo.ReplaceFirst("98", "0");
            }
            if (mobileNo.StartsWith("0098"))
            {
                return mobileNo.ReplaceFirst("0098", "0");
            }
            return mobileNo;
        }
        public static PersianDateTime ToPersianDateTime(this string strDateTime)
        {
            if (!strDateTime.IsNullOrWhiteSpace())
                return new PersianDateTime(strDateTime);
            return null;
        }

        public static string ToInternationalPhoneNumber(this string mobileNo)
        {
            if (mobileNo.StartsWith("093"))
            {
                return mobileNo.ReplaceFirst("093", "9893");
            }
            if (mobileNo.StartsWith("091"))
            {
                return mobileNo.ReplaceFirst("091", "9891");
            }
            return mobileNo;
        }
        public static bool IsNullOrWhiteSpace(this string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }
        public static float Similarity(this string str1, string str2)
        {
            str1 = str1.Replace("ي", "ی").Replace("ك", "ک");
            str2 = str2.Replace("ي", "ی").Replace("ك", "ک");
            MatchsMaker match = new MatchsMaker(str1, str2);
            return match.Score;
        }

        public static string JoinPart(this string[] input, int Start, int? End = null, char Seperator = ' ')
        {
            int _End = End ?? (input.Length - 1);
            string Temp = "";
            for (int i = Start; i <= _End; i++)
                Temp += input[i] + Seperator;

            Temp = Temp.TrimEnd(Seperator);
            return Temp;
        }

        public static PersianDateTime CreationDateTime(this string url)
        {
            try
            {
                var physicalPath = HttpContext.Current.Server.MapPath(url);
                if (File.Exists(physicalPath))
                {
                    FileInfo file = new FileInfo(physicalPath);
                    return file.LastWriteTime.ToPersianDateTime();
                }

                return null;
            }
            catch{ return null;}
        }

        public static string GetMimeType(this string extension)
        {
            if (extension == null)
                throw new ArgumentNullException(nameof(extension));

            if (extension.StartsWith("."))
                extension = extension.Substring(1);


            switch (extension.ToLower())
            {
                #region Big freaking list of mime types
                case "323": return "text/h323";
                case "3g2": return "video/3gpp2";
                case "3gp": return "video/3gpp";
                case "3gp2": return "video/3gpp2";
                case "3gpp": return "video/3gpp";
                case "7z": return "application/x-7z-compressed";
                case "aa": return "audio/audible";
                case "aac": return "audio/aac";
                case "aaf": return "application/octet-stream";
                case "aax": return "audio/vnd.audible.aax";
                case "ac3": return "audio/ac3";
                case "aca": return "application/octet-stream";
                case "accda": return "application/msaccess.addin";
                case "accdb": return "application/msaccess";
                case "accdc": return "application/msaccess.cab";
                case "accde": return "application/msaccess";
                case "accdr": return "application/msaccess.runtime";
                case "accdt": return "application/msaccess";
                case "accdw": return "application/msaccess.webapplication";
                case "accft": return "application/msaccess.ftemplate";
                case "acx": return "application/internet-property-stream";
                case "addin": return "text/xml";
                case "ade": return "application/msaccess";
                case "adobebridge": return "application/x-bridge-url";
                case "adp": return "application/msaccess";
                case "adt": return "audio/vnd.dlna.adts";
                case "adts": return "audio/aac";
                case "afm": return "application/octet-stream";
                case "ai": return "application/postscript";
                case "aif": return "audio/x-aiff";
                case "aifc": return "audio/aiff";
                case "aiff": return "audio/aiff";
                case "air": return "application/vnd.adobe.air-application-installer-package+zip";
                case "amc": return "application/x-mpeg";
                case "application": return "application/x-ms-application";
                case "art": return "image/x-jg";
                case "asa": return "application/xml";
                case "asax": return "application/xml";
                case "ascx": return "application/xml";
                case "asd": return "application/octet-stream";
                case "asf": return "video/x-ms-asf";
                case "ashx": return "application/xml";
                case "asi": return "application/octet-stream";
                case "asm": return "text/plain";
                case "asmx": return "application/xml";
                case "aspx": return "application/xml";
                case "asr": return "video/x-ms-asf";
                case "asx": return "video/x-ms-asf";
                case "atom": return "application/atom+xml";
                case "au": return "audio/basic";
                case "avi": return "video/x-msvideo";
                case "axs": return "application/olescript";
                case "bas": return "text/plain";
                case "bcpio": return "application/x-bcpio";
                case "bin": return "application/octet-stream";
                case "bmp": return "image/bmp";
                case "c": return "text/plain";
                case "cab": return "application/octet-stream";
                case "caf": return "audio/x-caf";
                case "calx": return "application/vnd.ms-office.calx";
                case "cat": return "application/vnd.ms-pki.seccat";
                case "cc": return "text/plain";
                case "cd": return "text/plain";
                case "cdda": return "audio/aiff";
                case "cdf": return "application/x-cdf";
                case "cer": return "application/x-x509-ca-cert";
                case "chm": return "application/octet-stream";
                case "class": return "application/x-java-applet";
                case "clp": return "application/x-msclip";
                case "cmx": return "image/x-cmx";
                case "cnf": return "text/plain";
                case "cod": return "image/cis-cod";
                case "config": return "application/xml";
                case "contact": return "text/x-ms-contact";
                case "coverage": return "application/xml";
                case "cpio": return "application/x-cpio";
                case "cpp": return "text/plain";
                case "crd": return "application/x-mscardfile";
                case "crl": return "application/pkix-crl";
                case "crt": return "application/x-x509-ca-cert";
                case "cs": return "text/plain";
                case "csdproj": return "text/plain";
                case "csh": return "application/x-csh";
                case "csproj": return "text/plain";
                case "css": return "text/css";
                case "csv": return "text/csv";
                case "cur": return "application/octet-stream";
                case "cxx": return "text/plain";
                case "dat": return "application/octet-stream";
                case "datasource": return "application/xml";
                case "dbproj": return "text/plain";
                case "dcr": return "application/x-director";
                case "def": return "text/plain";
                case "deploy": return "application/octet-stream";
                case "der": return "application/x-x509-ca-cert";
                case "dgml": return "application/xml";
                case "dib": return "image/bmp";
                case "dif": return "video/x-dv";
                case "dir": return "application/x-director";
                case "disco": return "text/xml";
                case "dll": return "application/x-msdownload";
                case "dll.config": return "text/xml";
                case "dlm": return "text/dlm";
                case "doc": return "application/msword";
                case "docm": return "application/vnd.ms-word.document.macroenabled.12";
                case "docx": return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                case "dot": return "application/msword";
                case "dotm": return "application/vnd.ms-word.template.macroenabled.12";
                case "dotx": return "application/vnd.openxmlformats-officedocument.wordprocessingml.template";
                case "dsp": return "application/octet-stream";
                case "dsw": return "text/plain";
                case "dtd": return "text/xml";
                case "dtsconfig": return "text/xml";
                case "dv": return "video/x-dv";
                case "dvi": return "application/x-dvi";
                case "dwf": return "drawing/x-dwf";
                case "dwp": return "application/octet-stream";
                case "dxr": return "application/x-director";
                case "eml": return "message/rfc822";
                case "emz": return "application/octet-stream";
                case "eot": return "application/octet-stream";
                case "eps": return "application/postscript";
                case "etl": return "application/etl";
                case "etx": return "text/x-setext";
                case "evy": return "application/envoy";
                case "exe": return "application/octet-stream";
                case "exe.config": return "text/xml";
                case "fdf": return "application/vnd.fdf";
                case "fif": return "application/fractals";
                case "filters": return "application/xml";
                case "fla": return "application/octet-stream";
                case "flr": return "x-world/x-vrml";
                case "flv": return "video/x-flv";
                case "fsscript": return "application/fsharp-script";
                case "fsx": return "application/fsharp-script";
                case "generictest": return "application/xml";
                case "gif": return "image/gif";
                case "group": return "text/x-ms-group";
                case "gsm": return "audio/x-gsm";
                case "gtar": return "application/x-gtar";
                case "gz": return "application/x-gzip";
                case "h": return "text/plain";
                case "hdf": return "application/x-hdf";
                case "hdml": return "text/x-hdml";
                case "hhc": return "application/x-oleobject";
                case "hhk": return "application/octet-stream";
                case "hhp": return "application/octet-stream";
                case "hlp": return "application/winhlp";
                case "hpp": return "text/plain";
                case "hqx": return "application/mac-binhex40";
                case "hta": return "application/hta";
                case "htc": return "text/x-component";
                case "htm": return "text/html";
                case "html": return "text/html";
                case "htt": return "text/webviewhtml";
                case "hxa": return "application/xml";
                case "hxc": return "application/xml";
                case "hxd": return "application/octet-stream";
                case "hxe": return "application/xml";
                case "hxf": return "application/xml";
                case "hxh": return "application/octet-stream";
                case "hxi": return "application/octet-stream";
                case "hxk": return "application/xml";
                case "hxq": return "application/octet-stream";
                case "hxr": return "application/octet-stream";
                case "hxs": return "application/octet-stream";
                case "hxt": return "text/html";
                case "hxv": return "application/xml";
                case "hxw": return "application/octet-stream";
                case "hxx": return "text/plain";
                case "i": return "text/plain";
                case "ico": return "image/x-icon";
                case "ics": return "application/octet-stream";
                case "idl": return "text/plain";
                case "ief": return "image/ief";
                case "iii": return "application/x-iphone";
                case "inc": return "text/plain";
                case "inf": return "application/octet-stream";
                case "inl": return "text/plain";
                case "ins": return "application/x-internet-signup";
                case "ipa": return "application/x-itunes-ipa";
                case "ipg": return "application/x-itunes-ipg";
                case "ipproj": return "text/plain";
                case "ipsw": return "application/x-itunes-ipsw";
                case "iqy": return "text/x-ms-iqy";
                case "isp": return "application/x-internet-signup";
                case "ite": return "application/x-itunes-ite";
                case "itlp": return "application/x-itunes-itlp";
                case "itms": return "application/x-itunes-itms";
                case "itpc": return "application/x-itunes-itpc";
                case "ivf": return "video/x-ivf";
                case "jar": return "application/java-archive";
                case "java": return "application/octet-stream";
                case "jck": return "application/liquidmotion";
                case "jcz": return "application/liquidmotion";
                case "jfif": return "image/pjpeg";
                case "jnlp": return "application/x-java-jnlp-file";
                case "jpb": return "application/octet-stream";
                case "jpe": return "image/jpeg";
                case "jpeg": return "image/jpeg";
                case "jpg": return "image/jpeg";
                case "js": return "application/x-javascript";
                case "jsx": return "text/jscript";
                case "jsxbin": return "text/plain";
                case "latex": return "application/x-latex";
                case "library-ms": return "application/windows-library+xml";
                case "lit": return "application/x-ms-reader";
                case "loadtest": return "application/xml";
                case "lpk": return "application/octet-stream";
                case "lsf": return "video/x-la-asf";
                case "lst": return "text/plain";
                case "lsx": return "video/x-la-asf";
                case "lzh": return "application/octet-stream";
                case "m13": return "application/x-msmediaview";
                case "m14": return "application/x-msmediaview";
                case "m1v": return "video/mpeg";
                case "m2t": return "video/vnd.dlna.mpeg-tts";
                case "m2ts": return "video/vnd.dlna.mpeg-tts";
                case "m2v": return "video/mpeg";
                case "m3u": return "audio/x-mpegurl";
                case "m3u8": return "audio/x-mpegurl";
                case "m4a": return "audio/m4a";
                case "m4b": return "audio/m4b";
                case "m4p": return "audio/m4p";
                case "m4r": return "audio/x-m4r";
                case "m4v": return "video/x-m4v";
                case "mac": return "image/x-macpaint";
                case "mak": return "text/plain";
                case "man": return "application/x-troff-man";
                case "manifest": return "application/x-ms-manifest";
                case "map": return "text/plain";
                case "master": return "application/xml";
                case "mda": return "application/msaccess";
                case "mdb": return "application/x-msaccess";
                case "mde": return "application/msaccess";
                case "mdp": return "application/octet-stream";
                case "me": return "application/x-troff-me";
                case "mfp": return "application/x-shockwave-flash";
                case "mht": return "message/rfc822";
                case "mhtml": return "message/rfc822";
                case "mid": return "audio/mid";
                case "midi": return "audio/mid";
                case "mix": return "application/octet-stream";
                case "mk": return "text/plain";
                case "mmf": return "application/x-smaf";
                case "mno": return "text/xml";
                case "mny": return "application/x-msmoney";
                case "mod": return "video/mpeg";
                case "mov": return "video/quicktime";
                case "movie": return "video/x-sgi-movie";
                case "mp2": return "video/mpeg";
                case "mp2v": return "video/mpeg";
                case "mp3": return "audio/mpeg";
                case "mp4": return "video/mp4";
                case "mp4v": return "video/mp4";
                case "mpa": return "video/mpeg";
                case "mpe": return "video/mpeg";
                case "mpeg": return "video/mpeg";
                case "mpf": return "application/vnd.ms-mediapackage";
                case "mpg": return "video/mpeg";
                case "mpp": return "application/vnd.ms-project";
                case "mpv2": return "video/mpeg";
                case "mqv": return "video/quicktime";
                case "ms": return "application/x-troff-ms";
                case "msi": return "application/octet-stream";
                case "mso": return "application/octet-stream";
                case "mts": return "video/vnd.dlna.mpeg-tts";
                case "mtx": return "application/xml";
                case "mvb": return "application/x-msmediaview";
                case "mvc": return "application/x-miva-compiled";
                case "mxp": return "application/x-mmxp";
                case "nc": return "application/x-netcdf";
                case "nsc": return "video/x-ms-asf";
                case "nws": return "message/rfc822";
                case "ocx": return "application/octet-stream";
                case "oda": return "application/oda";
                case "odc": return "text/x-ms-odc";
                case "odh": return "text/plain";
                case "odl": return "text/plain";
                case "odp": return "application/vnd.oasis.opendocument.presentation";
                case "ods": return "application/oleobject";
                case "odt": return "application/vnd.oasis.opendocument.text";
                case "one": return "application/onenote";
                case "onea": return "application/onenote";
                case "onepkg": return "application/onenote";
                case "onetmp": return "application/onenote";
                case "onetoc": return "application/onenote";
                case "onetoc2": return "application/onenote";
                case "orderedtest": return "application/xml";
                case "osdx": return "application/opensearchdescription+xml";
                case "p10": return "application/pkcs10";
                case "p12": return "application/x-pkcs12";
                case "p7b": return "application/x-pkcs7-certificates";
                case "p7c": return "application/pkcs7-mime";
                case "p7m": return "application/pkcs7-mime";
                case "p7r": return "application/x-pkcs7-certreqresp";
                case "p7s": return "application/pkcs7-signature";
                case "pbm": return "image/x-portable-bitmap";
                case "pcast": return "application/x-podcast";
                case "pct": return "image/pict";
                case "pcx": return "application/octet-stream";
                case "pcz": return "application/octet-stream";
                case "pdf": return "application/pdf";
                case "pfb": return "application/octet-stream";
                case "pfm": return "application/octet-stream";
                case "pfx": return "application/x-pkcs12";
                case "pgm": return "image/x-portable-graymap";
                case "pic": return "image/pict";
                case "pict": return "image/pict";
                case "pkgdef": return "text/plain";
                case "pkgundef": return "text/plain";
                case "pko": return "application/vnd.ms-pki.pko";
                case "pls": return "audio/scpls";
                case "pma": return "application/x-perfmon";
                case "pmc": return "application/x-perfmon";
                case "pml": return "application/x-perfmon";
                case "pmr": return "application/x-perfmon";
                case "pmw": return "application/x-perfmon";
                case "png": return "image/png";
                case "pnm": return "image/x-portable-anymap";
                case "pnt": return "image/x-macpaint";
                case "pntg": return "image/x-macpaint";
                case "pnz": return "image/png";
                case "pot": return "application/vnd.ms-powerpoint";
                case "potm": return "application/vnd.ms-powerpoint.template.macroenabled.12";
                case "potx": return "application/vnd.openxmlformats-officedocument.presentationml.template";
                case "ppa": return "application/vnd.ms-powerpoint";
                case "ppam": return "application/vnd.ms-powerpoint.addin.macroenabled.12";
                case "ppm": return "image/x-portable-pixmap";
                case "pps": return "application/vnd.ms-powerpoint";
                case "ppsm": return "application/vnd.ms-powerpoint.slideshow.macroenabled.12";
                case "ppsx": return "application/vnd.openxmlformats-officedocument.presentationml.slideshow";
                case "ppt": return "application/vnd.ms-powerpoint";
                case "pptm": return "application/vnd.ms-powerpoint.presentation.macroenabled.12";
                case "pptx": return "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                case "prf": return "application/pics-rules";
                case "prm": return "application/octet-stream";
                case "prx": return "application/octet-stream";
                case "ps": return "application/postscript";
                case "psc1": return "application/powershell";
                case "psd": return "application/octet-stream";
                case "psess": return "application/xml";
                case "psm": return "application/octet-stream";
                case "psp": return "application/octet-stream";
                case "pub": return "application/x-mspublisher";
                case "pwz": return "application/vnd.ms-powerpoint";
                case "qht": return "text/x-html-insertion";
                case "qhtm": return "text/x-html-insertion";
                case "qt": return "video/quicktime";
                case "qti": return "image/x-quicktime";
                case "qtif": return "image/x-quicktime";
                case "qtl": return "application/x-quicktimeplayer";
                case "qxd": return "application/octet-stream";
                case "ra": return "audio/x-pn-realaudio";
                case "ram": return "audio/x-pn-realaudio";
                case "rar": return "application/x-rar-compressed";
                case "ras": return "image/x-cmu-raster";
                case "rat": return "application/rat-file";
                case "rc": return "text/plain";
                case "rc2": return "text/plain";
                case "rct": return "text/plain";
                case "rdlc": return "application/xml";
                case "resx": return "application/xml";
                case "rf": return "image/vnd.rn-realflash";
                case "rgb": return "image/x-rgb";
                case "rgs": return "text/plain";
                case "rm": return "application/vnd.rn-realmedia";
                case "rmi": return "audio/mid";
                case "rmp": return "application/vnd.rn-rn_music_package";
                case "roff": return "application/x-troff";
                case "rpm": return "audio/x-pn-realaudio-plugin";
                case "rqy": return "text/x-ms-rqy";
                case "rtf": return "application/rtf";
                case "rtx": return "text/richtext";
                case "ruleset": return "application/xml";
                case "s": return "text/plain";
                case "safariextz": return "application/x-safari-safariextz";
                case "scd": return "application/x-msschedule";
                case "sct": return "text/scriptlet";
                case "sd2": return "audio/x-sd2";
                case "sdp": return "application/sdp";
                case "sea": return "application/octet-stream";
                case "searchconnector-ms": return "application/windows-search-connector+xml";
                case "setpay": return "application/set-payment-initiation";
                case "setreg": return "application/set-registration-initiation";
                case "settings": return "application/xml";
                case "sgimb": return "application/x-sgimb";
                case "sgml": return "text/sgml";
                case "sh": return "application/x-sh";
                case "shar": return "application/x-shar";
                case "shtml": return "text/html";
                case "sit": return "application/x-stuffit";
                case "sitemap": return "application/xml";
                case "skin": return "application/xml";
                case "sldm": return "application/vnd.ms-powerpoint.slide.macroenabled.12";
                case "sldx": return "application/vnd.openxmlformats-officedocument.presentationml.slide";
                case "slk": return "application/vnd.ms-excel";
                case "sln": return "text/plain";
                case "slupkg-ms": return "application/x-ms-license";
                case "smd": return "audio/x-smd";
                case "smi": return "application/octet-stream";
                case "smx": return "audio/x-smd";
                case "smz": return "audio/x-smd";
                case "snd": return "audio/basic";
                case "snippet": return "application/xml";
                case "snp": return "application/octet-stream";
                case "sol": return "text/plain";
                case "sor": return "text/plain";
                case "spc": return "application/x-pkcs7-certificates";
                case "spl": return "application/futuresplash";
                case "src": return "application/x-wais-source";
                case "srf": return "text/plain";
                case "ssisdeploymentmanifest": return "text/xml";
                case "ssm": return "application/streamingmedia";
                case "sst": return "application/vnd.ms-pki.certstore";
                case "stl": return "application/vnd.ms-pki.stl";
                case "sv4cpio": return "application/x-sv4cpio";
                case "sv4crc": return "application/x-sv4crc";
                case "svc": return "application/xml";
                case "swf": return "application/x-shockwave-flash";
                case "t": return "application/x-troff";
                case "tar": return "application/x-tar";
                case "tcl": return "application/x-tcl";
                case "testrunconfig": return "application/xml";
                case "testsettings": return "application/xml";
                case "tex": return "application/x-tex";
                case "texi": return "application/x-texinfo";
                case "texinfo": return "application/x-texinfo";
                case "tgz": return "application/x-compressed";
                case "thmx": return "application/vnd.ms-officetheme";
                case "thn": return "application/octet-stream";
                case "tif": return "image/tiff";
                case "tiff": return "image/tiff";
                case "tlh": return "text/plain";
                case "tli": return "text/plain";
                case "toc": return "application/octet-stream";
                case "tr": return "application/x-troff";
                case "trm": return "application/x-msterminal";
                case "trx": return "application/xml";
                case "ts": return "video/vnd.dlna.mpeg-tts";
                case "tsv": return "text/tab-separated-values";
                case "ttf": return "application/octet-stream";
                case "tts": return "video/vnd.dlna.mpeg-tts";
                case "txt": return "text/plain";
                case "u32": return "application/octet-stream";
                case "uls": return "text/iuls";
                case "user": return "text/plain";
                case "ustar": return "application/x-ustar";
                case "vb": return "text/plain";
                case "vbdproj": return "text/plain";
                case "vbk": return "video/mpeg";
                case "vbproj": return "text/plain";
                case "vbs": return "text/vbscript";
                case "vcf": return "text/x-vcard";
                case "vcproj": return "application/xml";
                case "vcs": return "text/plain";
                case "vcxproj": return "application/xml";
                case "vddproj": return "text/plain";
                case "vdp": return "text/plain";
                case "vdproj": return "text/plain";
                case "vdx": return "application/vnd.ms-visio.viewer";
                case "vml": return "text/xml";
                case "vscontent": return "application/xml";
                case "vsct": return "text/xml";
                case "vsd": return "application/vnd.visio";
                case "vsi": return "application/ms-vsi";
                case "vsix": return "application/vsix";
                case "vsixlangpack": return "text/xml";
                case "vsixmanifest": return "text/xml";
                case "vsmdi": return "application/xml";
                case "vspscc": return "text/plain";
                case "vss": return "application/vnd.visio";
                case "vsscc": return "text/plain";
                case "vssettings": return "text/xml";
                case "vssscc": return "text/plain";
                case "vst": return "application/vnd.visio";
                case "vstemplate": return "text/xml";
                case "vsto": return "application/x-ms-vsto";
                case "vsw": return "application/vnd.visio";
                case "vsx": return "application/vnd.visio";
                case "vtx": return "application/vnd.visio";
                case "wav": return "audio/wav";
                case "wave": return "audio/wav";
                case "wax": return "audio/x-ms-wax";
                case "wbk": return "application/msword";
                case "wbmp": return "image/vnd.wap.wbmp";
                case "wcm": return "application/vnd.ms-works";
                case "wdb": return "application/vnd.ms-works";
                case "wdp": return "image/vnd.ms-photo";
                case "webarchive": return "application/x-safari-webarchive";
                case "webtest": return "application/xml";
                case "wiq": return "application/xml";
                case "wiz": return "application/msword";
                case "wks": return "application/vnd.ms-works";
                case "wlmp": return "application/wlmoviemaker";
                case "wlpginstall": return "application/x-wlpg-detect";
                case "wlpginstall3": return "application/x-wlpg3-detect";
                case "wm": return "video/x-ms-wm";
                case "wma": return "audio/x-ms-wma";
                case "wmd": return "application/x-ms-wmd";
                case "wmf": return "application/x-msmetafile";
                case "wml": return "text/vnd.wap.wml";
                case "wmlc": return "application/vnd.wap.wmlc";
                case "wmls": return "text/vnd.wap.wmlscript";
                case "wmlsc": return "application/vnd.wap.wmlscriptc";
                case "wmp": return "video/x-ms-wmp";
                case "wmv": return "video/x-ms-wmv";
                case "wmx": return "video/x-ms-wmx";
                case "wmz": return "application/x-ms-wmz";
                case "wpl": return "application/vnd.ms-wpl";
                case "wps": return "application/vnd.ms-works";
                case "wri": return "application/x-mswrite";
                case "wrl": return "x-world/x-vrml";
                case "wrz": return "x-world/x-vrml";
                case "wsc": return "text/scriptlet";
                case "wsdl": return "text/xml";
                case "wvx": return "video/x-ms-wvx";
                case "x": return "application/directx";
                case "xaf": return "x-world/x-vrml";
                case "xaml": return "application/xaml+xml";
                case "xap": return "application/x-silverlight-app";
                case "xbap": return "application/x-ms-xbap";
                case "xbm": return "image/x-xbitmap";
                case "xdr": return "text/plain";
                case "xht": return "application/xhtml+xml";
                case "xhtml": return "application/xhtml+xml";
                case "xla": return "application/vnd.ms-excel";
                case "xlam": return "application/vnd.ms-excel.addin.macroenabled.12";
                case "xlc": return "application/vnd.ms-excel";
                case "xld": return "application/vnd.ms-excel";
                case "xlk": return "application/vnd.ms-excel";
                case "xll": return "application/vnd.ms-excel";
                case "xlm": return "application/vnd.ms-excel";
                case "xls": return "application/vnd.ms-excel";
                case "xlsb": return "application/vnd.ms-excel.sheet.binary.macroenabled.12";
                case "xlsm": return "application/vnd.ms-excel.sheet.macroenabled.12";
                case "xlsx": return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                case "xlt": return "application/vnd.ms-excel";
                case "xltm": return "application/vnd.ms-excel.template.macroenabled.12";
                case "xltx": return "application/vnd.openxmlformats-officedocument.spreadsheetml.template";
                case "xlw": return "application/vnd.ms-excel";
                case "xml": return "text/xml";
                case "xmta": return "application/xml";
                case "xof": return "x-world/x-vrml";
                case "xoml": return "text/plain";
                case "xpm": return "image/x-xpixmap";
                case "xps": return "application/vnd.ms-xpsdocument";
                case "xrm-ms": return "text/xml";
                case "xsc": return "application/xml";
                case "xsd": return "text/xml";
                case "xsf": return "text/xml";
                case "xsl": return "text/xml";
                case "xslt": return "text/xml";
                case "xsn": return "application/octet-stream";
                case "xss": return "application/xml";
                case "xtp": return "application/octet-stream";
                case "xwd": return "image/x-xwindowdump";
                case "z": return "application/x-compress";
                case "zip": return "application/x-zip-compressed";
                #endregion
                default: return "application/octet-stream";
            }
        }
    }
}
