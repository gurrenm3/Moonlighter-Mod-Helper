using HarmonyLib;
using Moonlighter_Mod_Helper.Api;
using Moonlighter_Mod_Helper.Extensions;
using Moonlighter_Mod_Helper.Api.Items;
using System;
using Moonlighter.Weapons;

namespace Moonlighter_Mod_Helper.Patches
{
    [HarmonyPatch(typeof(ItemStack), nameof(ItemStack.Init))]
    internal class ItemStack_Init
    {
        [HarmonyPrefix]
        internal static bool Prefix(ItemStack __instance, ref ItemMaster itemMaster, int howMany = 1)
        {
			if (ItemRegister.TryGetItem<ConsumableItemMaster>(itemMaster.name, out var consumable))
				itemMaster = consumable;

			if (ItemRegister.TryGetItem<WeaponEquipmentMaster>(itemMaster.name, out var weapon))
				itemMaster = weapon;

			if (ItemRegister.TryGetItem<EquipmentItemMaster>(itemMaster.name, out var equip))
				itemMaster = equip;

			if (ItemRegister.TryGetItem<ItemMaster>(itemMaster.name, out var item))
				itemMaster = item;

			return true;
        }

        [HarmonyPostfix]
        internal static void Postfix(ItemStack __instance, ItemMaster itemMaster, int howMany = 1)
		{
			/*__instance.master = itemMaster;
			__instance.Quantity = howMany;
			__instance._initialLives = __instance._lives;
			__instance.name = string.Format("ItemStack({0}{1})", itemMaster.name, (itemMaster.plusLevel <= 0) ? string.Empty : string.Format(" +{0}", itemMaster.plusLevel));
			__instance.itemImage.sprite = ItemDatabase.GetSpriteByItemName(__instance.master.worldSpriteName);
			if (__instance._pixelatorFlashMaterial == null)
			{
				__instance._pixelatorFlashMaterial = __instance.itemImage.material;
			}

			// use custom item if it exists
			*//*EquipmentItemMaster equipmentItemMaster = ItemRegister.GetEquip(itemMaster.name);
			if (equipmentItemMaster is null)*//*
				var equipmentItemMaster = __instance.master as EquipmentItemMaster;

			if (equipmentItemMaster != null)
			{
				__instance._equipment = __instance.gameObject.GetOrAddComponent<Equipment>();
				__instance._equipment.Init(equipmentItemMaster);
				Equipment equipment = __instance._equipment;
				equipment.onEquipmentEnchanted = (Action)Delegate.Remove(equipment.onEquipmentEnchanted, new Action(__instance.SetIdleMaterial));
				Equipment equipment2 = __instance._equipment;
				equipment2.onEquipmentEnchanted = (Action)Delegate.Combine(equipment2.onEquipmentEnchanted, new Action(__instance.SetIdleMaterial));
			}
			AmuletEquipmentMaster amuletEquipmentMaster = __instance.master as AmuletEquipmentMaster;
			if (amuletEquipmentMaster != null)
			{
				__instance._amulet = __instance.gameObject.GetOrAddComponent<Amulet>();
				__instance._amulet.Init(amuletEquipmentMaster);
			}
			AbilityItemMaster abilityItemMaster = __instance.master as AbilityItemMaster;
			if (abilityItemMaster != null)
			{
				__instance._ability = __instance.gameObject.GetOrAddComponent<Ability>();
				__instance._ability.Init(abilityItemMaster);
			}
			WeaponEquipmentMaster weaponEquipmentMaster = __instance.master as WeaponEquipmentMaster;
			if (weaponEquipmentMaster != null)
			{
				TrickWeaponMaster trickWeaponMaster = weaponEquipmentMaster as TrickWeaponMaster;
				if (trickWeaponMaster != null)
				{
					__instance._trickWeapon = __instance.gameObject.AddComponent<TrickWeapon>();
					__instance._trickWeapon.Init(trickWeaponMaster);
				}
				else
				{
					__instance._weapon = __instance.gameObject.AddComponent<Weapon>();
					__instance._weapon.Init(weaponEquipmentMaster);
					BowWeaponMaster bowWeaponMaster = weaponEquipmentMaster as BowWeaponMaster;
					if (bowWeaponMaster != null)
					{
						__instance._bow = __instance.gameObject.GetOrAddComponent<Bow>();
						__instance._bow.Init(bowWeaponMaster);
					}
					GlovesWeaponMaster glovesWeaponMaster = weaponEquipmentMaster as GlovesWeaponMaster;
					BigSwordWeaponMaster bigSwordWeaponMaster = weaponEquipmentMaster as BigSwordWeaponMaster;
					SpearWeaponMaster spearWeaponMaster = weaponEquipmentMaster as SpearWeaponMaster;
					if (glovesWeaponMaster != null)
					{
						__instance.gameObject.AddComponent<SecondaryAttackWeapon>().Init(glovesWeaponMaster);
					}
					if (bigSwordWeaponMaster != null)
					{
						__instance.gameObject.AddComponent<SecondaryAttackWeapon>().Init(bigSwordWeaponMaster);
					}
					if (spearWeaponMaster != null)
					{
						__instance.gameObject.AddComponent<SecondaryAttackWeapon>().Init(spearWeaponMaster);
					}
					ShortSwordWeaponMaster shortSwordWeaponMaster = weaponEquipmentMaster as ShortSwordWeaponMaster;
					if (shortSwordWeaponMaster != null)
					{
						__instance.gameObject.AddComponent<ShortSword>().Init(shortSwordWeaponMaster);
					}
				}
			}
			DecorativeItemMaster decorativeItemMaster = __instance.master as DecorativeItemMaster;
			if (decorativeItemMaster != null)
			{
				__instance._decorative = __instance.gameObject.GetOrAddComponent<DecorativeItem>();
				__instance._decorative.Init(decorativeItemMaster);
			}
			FamiliarItemMaster familiarItemMaster = __instance.master as FamiliarItemMaster;
			if (familiarItemMaster != null)
			{
				__instance._familiar = __instance.gameObject.GetOrAddComponent<FamiliarItem>();
				__instance._familiar.Init(familiarItemMaster);
			}
			AutoConsumableItemMaster autoConsumableItemMaster = __instance.master as AutoConsumableItemMaster;
			if (autoConsumableItemMaster != null)
			{
				__instance._autoConsumable = __instance.gameObject.GetOrAddComponent<AutoConsumable>();
			}


			*//*ConsumableItemMaster consumableItemMaster = ItemRegister.GetConsumable(itemMaster.name);
			if (consumableItemMaster is null)*//*
				var consumableItemMaster = __instance.master as ConsumableItemMaster;


			if (consumableItemMaster != null)
			{
				__instance._consumable = __instance.gameObject.GetOrAddComponent<Consumable>();
				__instance._consumable.Init(consumableItemMaster);
			}
			__instance.SetIdleMaterial();
			if (__instance.master.isDestroyedOnRunEnded)
			{
				__instance.GenerateEffect(ItemEffectManager.Instance.trickEffectPrefab.gameObject);
			}*/


			// Check if this item has a custom type. If so add the custom type
			var hasCustomItem = ItemRegister.Instance.CustomItemTypes.TryGetValue(itemMaster.name, out var type);
			if (hasCustomItem)
			{
				__instance.gameObject.AddComponent(type);
			}
		}
    }
}
