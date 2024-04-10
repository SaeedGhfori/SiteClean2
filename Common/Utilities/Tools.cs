using System;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Common.Utilities
{
    public static class Tools
    {
        public static void AddFillLog(string message, string fileName, string path = "", string specialDirectory = "ErrorLogs", bool systemControl = true)
        {
            if (systemControl)
            {
                //todo
                var hardDriveLogSave = "0";//ConfigurationManager.AppSettings["DiskLogSave"];
                if (hardDriveLogSave == "0")
                {
                    return;
                }
            }


            if (string.IsNullOrEmpty(path))
            {
                if (!string.IsNullOrEmpty(specialDirectory))
                {
                    path = AppDomain.CurrentDomain.BaseDirectory + @"\" + specialDirectory;
                }
                else
                {
                    path = AppDomain.CurrentDomain.BaseDirectory;
                }
            }
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }



            fileName += "_" + DateTime.Now.ToString("yyyy-MM-dd");

            string fullPath =
                $"{path}\\{fileName}.log";


            if (!File.Exists(fullPath))
            {
                File.WriteAllText(fullPath, message + Environment.NewLine);
            }


            File.AppendAllText(fullPath, message + Environment.NewLine);
        }
        public static string GetEnumDescription(this Enum enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : enumValue.ToString();
        }

        public class MyCategoryAttribute : CategoryAttribute
        {
            public MyCategoryAttribute(string categoryKey) : base(categoryKey) { }

            protected override string GetLocalizedString(string value)
            {
                return value;
            }
        }

        public static void ManualErrorLog(object data, Exception e, string apiName, string title)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(data);
                var message = e.Message;
                message +=
                message += jsonData;
                AddFillLog(message + "_" + e.GetBaseException(), apiName + "_" + title.ToString());
            }
            catch
            {
                try
                {
                    var message = e.Message;
                    message += "*******";
                    AddFillLog(message + "_" + e.GetBaseException(), apiName + "_" + title.ToString());
                }
                catch
                {

                    //ignore
                }
            }

        }
        public static bool ExistsLetter(string identity)
        {
            // بررسی می‌کند که آیا رشته 'identity' شامل حداقل یک حرف الفبا است
            if (Regex.Matches(identity, @"[a-zA-Z]").Count > 0)
                return true;
            return false;
        }
    }
}
