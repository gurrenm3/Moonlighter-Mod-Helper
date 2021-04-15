using System.Linq;
using static HeroMerchantInventory;

namespace Moonlighter_Mod_Helper.Extensions
{
    public static class HeroMerchantInventoryExt
    {
        public static void RefreshAllSlots(this HeroMerchantInventory heroMerchantInventory)
        {
            for (int i = 0; i < heroMerchantInventory.slots.Length; i++)
            {
                heroMerchantInventory.RefreshSlotAt(i);
            }
        }

        public static void EquipItem(this HeroMerchantInventory __this, ItemStack weapon, EquipmentSlot slot)
        {
            __this.SetEquippedItemByType(weapon, slot);
        }
    }
}