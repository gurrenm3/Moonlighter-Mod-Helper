using System.Collections.Generic;
using UnityEngine;

namespace Moonlighter_Mod_Helper.Api
{
    public class SpriteRegister
    {
        public static Dictionary<string, Sprite> spriteRegister = new Dictionary<string, Sprite>();

        public static void AddToRegister(string itemName, Sprite newSprite)
        {
            Main.LogMessage($"Registered Sprite for {itemName}");
            spriteRegister.Add(itemName, newSprite);
        }

        public static void RemoveFromRegister(string itemName)
        {
            spriteRegister.Remove(itemName);
        }

        public static Sprite GetFromRegister(string itemName)
        {
            var hasKey = spriteRegister.TryGetValue(itemName, out Sprite sprite);
            return hasKey ? sprite : null;
        }
    }
}
