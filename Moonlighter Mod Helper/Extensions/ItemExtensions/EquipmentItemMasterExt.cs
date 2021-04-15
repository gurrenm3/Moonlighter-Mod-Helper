using static EquipmentItemMaster;

namespace Moonlighter_Mod_Helper.Extensions
{
    public static class EquipmentItemMasterExt
    {
        /*public static EquipmentItemMaster Init(this EquipmentItemMaster equipmentItemMaster, ItemMaster parent)
        {
            parent.ConvertTo(ref equipmentItemMaster);
            equipmentItemMaster.stats = equipmentItemMaster.stats is null ? new StatsModificator() : equipmentItemMaster.stats;
            return equipmentItemMaster;
        }*/

        public static T Init<T>(this T equipmentItemMaster, ItemMaster parent) where T : EquipmentItemMaster
        {
            parent.DuplicateAs(ref equipmentItemMaster);
            equipmentItemMaster.stats = equipmentItemMaster.stats is null ? new StatsModificator() : equipmentItemMaster.stats;
            return equipmentItemMaster;
        }
    }
}