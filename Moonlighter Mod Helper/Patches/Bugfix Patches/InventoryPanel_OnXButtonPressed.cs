using HarmonyLib;
using Moonlighter_Mod_Helper.Api;
using UnityEngine;
using System.Collections.Generic;
using System.Reflection.Emit;
using DG.Tweening;

namespace Moonlighter_Mod_Helper.Patches
{
    [HarmonyPatch(typeof(InventoryPanel), nameof(InventoryPanel.OnXButtonPressed))]
    internal class InventoryPanel_OnXButtonPressed
    {
		[HarmonyPrefix]
		internal static bool Prefix(InventoryPanel __instance)
		{
			return false;
		}

		[HarmonyPostfix]
		internal static void Postfix(InventoryPanel __instance, ref bool __result)
		{
			__result = true;
			if (!__instance.IsActive() || !__instance.receiveInput || __instance.blockInput)
			{
				__result = false;
				return;
			}

			if (!InventoryPanel.currentSelectedSlot)
				return;


			InventorySlotGUI component = InventoryPanel.currentSelectedSlot.GetComponent<InventorySlotGUI>();
			if (!component.itemStack || !component.itemStack.Consumable)
				return;


			Consumable consumable = component.itemStack.Consumable;
			if (!consumable.SendConsumeEvent())
			{
				component.PlayWrongAnimation();
				return;
			}



			component.PlaySelectedAnimation(Constants.GetFloat("inventorySlotPressPunchValue"));
			BagInventorySlotGUI component2 = InventoryPanel.currentSelectedSlot.GetComponent<BagInventorySlotGUI>();
			if (component2)
			{
				if (component2.itemStack.Quantity == 0)
				{
					HeroMerchant.Instance.heroMerchantInventory.SetItem(null, component2.slotIndex);
				}
				else
				{
					HeroMerchant.Instance.heroMerchantInventory.RefreshSlotAt(component2.slotIndex);
				}
			}
			else
			{
				EquipmentInventorySlotGUI component3 = InventoryPanel.currentSelectedSlot.GetComponent<EquipmentInventorySlotGUI>();
				if (component3)
				{
					if (component3.type == HeroMerchantInventory.EquipmentSlot.Potion)
					{
						if (component3.itemStack.Quantity > 0)
						{
							component3.UpdateItem();
							HUDManager.Instance.SetPotionQuantity(component3.itemStack.Quantity);
						}
						else
						{
							HeroMerchant.Instance.heroMerchantInventory.SetEquippedItemByType(null, HeroMerchantInventory.EquipmentSlot.Potion);
							HUDManager.Instance.SetEmptyConsumableIcon();
						}
					}
				}
				else
				{
					ChestSlotGUI component4 = InventoryPanel.currentSelectedSlot.GetComponent<ChestSlotGUI>();
					if (component4)
					{
						component4.UpdateItem();
					}
				}
			}



			ConsumableItemMaster consumableItemMaster = component.item as ConsumableItemMaster;
			bool isGuidenceEffect = (consumableItemMaster is null) ? false : consumableItemMaster.consumableEffectName.Contains("Guidance");
			if (isGuidenceEffect)
			{
				DOVirtual.DelayedCall(0.1f, delegate
				{
					GUIManager.Instance.DisableCurrentPanel();
				}, true);
			}
		}
	}
}
