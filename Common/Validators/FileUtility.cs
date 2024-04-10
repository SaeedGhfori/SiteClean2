using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Validators
{
    public static class FileUtility
    {
        public static byte[] ReadFileAsBytes(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }
    }
}
