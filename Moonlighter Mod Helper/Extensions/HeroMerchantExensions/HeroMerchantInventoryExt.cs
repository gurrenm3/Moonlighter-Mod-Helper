using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}