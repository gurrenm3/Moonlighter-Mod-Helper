using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Moonlighter_Mod_Helper.Extensions
{
    public static class SpriteExt
    {
        public static void SetTexture(this Sprite sprite, Texture2D newTexture)
        {
            var bytes = newTexture.EncodeToPNG();
            sprite.texture.LoadImage(bytes);
        }
    }
}
