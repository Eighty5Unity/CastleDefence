using UnityEngine;

namespace Code
{
    public static class MyExtensions
    {
        public static string ToJson(this object obj)
        {
            return JsonUtility.ToJson(obj);
        }

        public static T ToDeserialize<T>(this string json)
        {
            return JsonUtility.FromJson<T>(json);
        }
    }
}