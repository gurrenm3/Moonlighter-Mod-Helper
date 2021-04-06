using UnityEngine;

namespace Moonlighter_Mod_Helper.Extensions
{
    public static class ClassExt
    {
        public static T Duplicate<T>(this T component)
        {
            var json = JsonUtility.ToJson(component);
            return JsonUtility.FromJson<T>(json);
        }
    }
}