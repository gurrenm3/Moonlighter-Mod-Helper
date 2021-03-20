using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moonlighter_Mod_Helper.Extensions
{
    public static class HeroMerchantStatsExt
    {
        public static float GetMaxHealth(this HeroMerchantStats heroMerchantStats)
        {
            return heroMerchantStats.currentHealth.maxValue.GetDecrypted();
        }

        public static void SetMaxHealth(this HeroMerchantStats heroMerchantStats, float newValue)
        {
            heroMerchantStats.currentHealth.maxValue.SetNewValue(newValue);
        }
    }
}