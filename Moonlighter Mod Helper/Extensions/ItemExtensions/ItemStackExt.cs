using Moonlighter.Weapons;
using System;
using static HeroMerchantInventory;

namespace Moonlighter_Mod_Helper.Extensions
{
    public static class ItemStackExt
    {
        public static bool Equip(this ItemStack itemStack, EquipmentSlot slot)
        {
            return HeroMerchant.Instance.heroMerchantInventory.SetEquippedItemByType(itemStack, slot);
        }

        public static bool TryAddToInventory(this ItemStack itemStack)
        {
            var inventory = HeroMerchant.Instance?.heroMerchantInventory;
            return !inventory ? false : inventory.TryAddItem(itemStack);
        }

        public static bool TryRemoveFromInventory(this ItemStack itemStack)
        {
            var inventory = HeroMerchant.Instance?.heroMerchantInventory;
            return !inventory ? false : inventory.TryRemoveItem(itemStack);
        }

        public static Equipment AddEquipment(this ItemStack itemStack, EquipmentItemMaster equipmentItemMaster)
        {
            equipmentItemMaster.stats = equipmentItemMaster.stats is null ? new StatsModificator() : equipmentItemMaster.stats;

            itemStack._equipment = itemStack.gameObject.GetOrAddComponent<Equipment>();
            itemStack._equipment.Init(equipmentItemMaster);
            Equipment equipment = itemStack._equipment;
            equipment.onEquipmentEnchanted = (Action)Delegate.Remove(equipment.onEquipmentEnchanted, new Action(itemStack.SetIdleMaterial));
            Equipment equipment2 = itemStack._equipment;
            equipment2.onEquipmentEnchanted = (Action)Delegate.Combine(equipment2.onEquipmentEnchanted, new Action(itemStack.SetIdleMaterial));
            return itemStack._equipment;
        }

        public static Amulet AddAmulet(this ItemStack itemStack, AmuletEquipmentMaster amuletEquipmentMaster)
        {
            itemStack._amulet = itemStack.gameObject.GetOrAddComponent<Amulet>();
            itemStack._amulet.Init(amuletEquipmentMaster);
            return itemStack._amulet;
        }

        public static Ability AddAbility(this ItemStack itemStack, AbilityItemMaster abilityItemMaster)
        {
            itemStack._ability = itemStack.gameObject.GetOrAddComponent<Ability>();
            itemStack._ability.Init(abilityItemMaster);
            return itemStack._ability;
        }

        public static DecorativeItem AddDecorative(this ItemStack itemStack, DecorativeItemMaster decorativeItemMaster)
        {
            itemStack._decorative = itemStack.gameObject.GetOrAddComponent<DecorativeItem>();
            itemStack._decorative.Init(decorativeItemMaster);
            return itemStack._decorative;
        }

        public static FamiliarItem AddFamiliar(this ItemStack itemStack, FamiliarItemMaster familiarItemMaster)
        {
            itemStack._familiar = itemStack.gameObject.GetOrAddComponent<FamiliarItem>();
            itemStack._familiar.Init(familiarItemMaster);
            return itemStack._familiar;
        }

        public static AutoConsumable AddAutoConsumable(this ItemStack itemStack)
        {
            itemStack._autoConsumable = itemStack.gameObject.GetOrAddComponent<AutoConsumable>();
            return itemStack._autoConsumable;
        }

        public static Consumable AddConsumable(this ItemStack itemStack, ConsumableItemMaster consumableItemMaster)
        {
            itemStack._consumable = itemStack.gameObject.GetOrAddComponent<Consumable>();
            itemStack._consumable.Init(consumableItemMaster);
            return itemStack._consumable;
        }

        public static void SetDestroyedOnRunEnd(this ItemStack itemStack)
        {
            itemStack.GenerateEffect(ItemEffectManager.Instance.trickEffectPrefab.gameObject);
        }

        public static TrickWeapon AddTrickWeapon(this ItemStack itemStack, TrickWeaponMaster trickWeaponMaster, bool setDestroyedOnRun = true)
        {
            itemStack._trickWeapon = itemStack.gameObject.AddComponent<TrickWeapon>();
            itemStack._trickWeapon.Init(trickWeaponMaster);
            itemStack.SetDestroyedOnRunEnd();
            return itemStack._trickWeapon;
        }

        public static void AddWeapon(this ItemStack itemStack, WeaponEquipmentMaster weaponEquipmentMaster)
        {
            itemStack._weapon = itemStack.gameObject.AddComponent<Weapon>();
            itemStack._weapon.Init(weaponEquipmentMaster);
            
            
            if (weaponEquipmentMaster is GlovesWeaponMaster glovesWeaponMaster)
                itemStack.gameObject.AddComponent<SecondaryAttackWeapon>().Init(glovesWeaponMaster);


            if (weaponEquipmentMaster is BigSwordWeaponMaster bigSwordWeaponMaster)
                itemStack.gameObject.AddComponent<SecondaryAttackWeapon>().Init(bigSwordWeaponMaster);


            if (weaponEquipmentMaster is SpearWeaponMaster spearWeaponMaster)
                itemStack.gameObject.AddComponent<SecondaryAttackWeapon>().Init(spearWeaponMaster);


            if (weaponEquipmentMaster is ShortSwordWeaponMaster shortSwordWeaponMaster)
                itemStack.gameObject.AddComponent<ShortSword>().Init(shortSwordWeaponMaster);

            
            if (weaponEquipmentMaster is BowWeaponMaster bowWeaponMaster)
            {
                itemStack._bow = itemStack.gameObject.GetOrAddComponent<Bow>();
                itemStack._bow.Init(bowWeaponMaster);
            }
        }
    }
}
