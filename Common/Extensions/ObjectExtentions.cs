using Newtonsoft.Json;

namespace Common.Extensions
{
    public static class ObjectExtentions
    {
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static TType FromJson<TType>(this string str)
        {
            return JsonConvert.DeserializeObject<TType>(str);
        }
    }
}