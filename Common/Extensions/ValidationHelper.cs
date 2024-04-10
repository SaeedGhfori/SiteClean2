using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Common.Extensions
{
    public static class ValidationHelper
    {
        public static bool IsValidPhoneNumber(this string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                throw new ArgumentNullException();
            }
            var isNumeric = long.TryParse(phoneNumber, out _);
            if (!isNumeric)
                throw new ArgumentNullException();
            if (phoneNumber.Length != 11)
                throw new ArgumentOutOfRangeException();
            return true;
        }

        public static bool IsValidEmail(this string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidUsername(this string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return false;

            // نام کاربری باید حداقل 3 کاراکتر و حداکثر 20 کاراکتر باشد و می‌تواند شامل حروف، اعداد و _ باشد
            return Regex.IsMatch(username, @"^[a-zA-Z0-9_]{3,20}$");
        }

        public static bool IsValidPassword(this string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            // رمز عبور باید حداقل 8 کاراکتر باشد و شامل حروف بزرگ، حروف کوچک، اعداد و کاراکترهای خاص باشد
            return Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
        }

        public static bool IsValidUrl(this string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            // بررسی می‌کند که آیا رشته ورودی یک URL معتبر با پروتکل HTTP یا HTTPS است
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

    }
}
