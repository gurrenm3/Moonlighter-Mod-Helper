using UnityEngine;

namespace Moonlighter_Mod_Helper.Extensions
{
    public static class ConsumableExt
    {
        public static void Init<T>(this Consumable consumable, ConsumableItemMaster master) where T : MonoBehaviour
        {
            consumable.consumableMaster = master;
            consumable.gameObject.AddComponent<T>();
            consumable._stack = consumable.GetComponent<ItemStack>();
        }
    }
}
