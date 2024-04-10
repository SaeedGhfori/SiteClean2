using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Utilities
{
    public static class Generator
    {
        private static Random random = new Random();
        public static string GenerateDisposableCode(IEnumerable<DisposableCode> disposableCodes)
        {
            string generateDisposableCode = string.Empty;
            bool anyDisposableCode = false;
            while (!anyDisposableCode)
            {
                var generator = new Random();
                int random = generator.Next(1, 1000000);
                generateDisposableCode = random.ToString().PadLeft(6, '0');
                var disposableCode = disposableCodes.FirstOrDefault(f => f.Code == generateDisposableCode);
                if (disposableCode == null)
                    anyDisposableCode = true;
            }
            return generateDisposableCode;
        }
        public static string GenerateUniqCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
        public static string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
    public class DisposableCode
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Code { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsUsed { get; set; }
        public DateTime? UsedDate { get; set; }
    }
}