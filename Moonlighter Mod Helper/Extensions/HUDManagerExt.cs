using UnityEngine;

namespace Moonlighter_Mod_Helper.Extensions
{
    public static class HUDManagerExt
    {
        public static void SetPotionConsumableIcon(this HUDManager hudManager, Sprite consumableIcon)
        {
            ItemStack equippedItemByType = HeroMerchant.Instance.heroMerchantInventory.GetEquippedItemByType(HeroMerchantInventory.EquipmentSlot.Potion);
            if (equippedItemByType)
            {
                hudManager.currentPotionIcon.overrideSprite = consumableIcon;
                hudManager.potionQuantityLabel.text = equippedItemByType.Quantity.ToString("0.");
            }
        }
    }
}
