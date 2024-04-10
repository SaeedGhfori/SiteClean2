using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Common.Validators
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class NationalCodeAttribute : ValidationAttribute
    {
        public NationalCodeAttribute(string ErrorMessage) : base()
        {
            this.ErrorMessage = ErrorMessage;
        }


        public override bool IsValid(object value)
        {
            if (value == null) return false;
            if (IsValidNationalCode(value.ToString()) == false) return false;
            return true;
        }

        public bool IsValidNationalCode(string nationalcode)
        {
            if (string.IsNullOrEmpty(nationalcode)) return false;
            if (!new Regex(@"\d{10}").IsMatch(nationalcode)) return false;
            var array = nationalcode.ToCharArray();

            var allDigitEqual = new[]
            {
                "0000000000", "1111111111", "2222222222", "3333333333", "4444444444", "5555555555", "6666666666",
                "7777777777", "8888888888", "9999999999"
            };
            if (allDigitEqual.Contains(nationalcode)) return false;
            var check = Convert.ToInt32(nationalcode.Substring(9, 1));
            var sum = Enumerable.Range(0, 9)
                          .Select(x => Convert.ToInt32(nationalcode.Substring(x, 1)) * (10 - x))
                          .Sum() % 11;

            return sum < 2 && check == sum || sum >= 2 && check + sum == 11;
            //            var j = 10;
            //            var sum = 0;
            //            for (var i = 0; i < array.Length - 1; i++)
            //            {
            //                sum += Int32.Parse(array[i].ToString(CultureInfo.InvariantCulture)) * j;
            //                j--;
            //            }
            //
            //            var div = sum / 11;
            //            var r = div * 11;
            //            var diff = Math.Abs(sum - r);
            //            if (diff <= 2)
            //            {
            //                return diff == Int32.Parse(array[9].ToString(CultureInfo.InvariantCulture));
            //            }
            //
            //            var temp = Math.Abs(diff - 11);
            //            return temp == Int32.Parse(array[9].ToString(CultureInfo.InvariantCulture));
        }
    }
}