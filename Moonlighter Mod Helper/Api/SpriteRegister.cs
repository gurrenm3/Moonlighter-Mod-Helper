using System.Collections.Generic;
using UnityEngine;

namespace Moonlighter_Mod_Helper.Api
{
    public class SpriteRegister
    {
        public static Dictionary<string, Sprite> spriteRegister = new Dictionary<string, Sprite>();

        public static Sprite GetOrAddToRegister(string itemName, Sprite sprite)
        {
            if (IsInRegister(itemName))
                return GetFromRegister(itemName);

            AddToRegister(itemName, sprite);
            return sprite;
        }

        public static void AddToRegister(string itemName, Sprite newSprite)
        {
            Main.LogMessage($"Registered Sprite for {itemName}");
            spriteRegister.Add(itemName, newSprite);
        }

        public static void RemoveFromRegister(string itemName)
        {
            spriteRegister.Remove(itemName);
        }

        public static Sprite GetFromRegister(string itemName, bool ignoreWarnings = false)
        {
            var hasKey = spriteRegister.TryGetValue(itemName, out Sprite sprite);
            if (!hasKey && !ignoreWarnings)
                Main.LogWarning($"SpriteRegister failed to find sprite by the name of {itemName}");
            
            return sprite;
        }

        public static bool IsInRegister(string itemName)
        {
            return spriteRegister.ContainsKey(itemName);
        }

        public static bool IsInRegister(Sprite sprite)
        {
            var result = spriteRegister.Values.FirstOrDefault(item => item == sprite);
            return result;
        }
    }
}
