using UnityEngine;

namespace Moonlighter_Mod_Helper.Api
{
    public interface IEquippable
    {
        EquipmentItemMaster.EquipmentSlot EquipmentSlot { get; }

        Sprite InventoryIcon { get; set; }
        string ItemName { get; set; }
        string ItemDescription { get; set; }

        void OnEquipped();
    }
}