using Moonlighter_Mod_Helper.Extensions;
using System;
using System.Collections;
using UnityEngine;

namespace Moonlighter_Mod_Helper.Api.Items
{
    public abstract class Potion : CraftableItem, IConsumable
    {
        public ConsumableItemMaster consumableItemMaster;

        public override RecipeCollectionType RecipeCollectionType { get; set; } = RecipeCollectionType.Witch;
        public override string CollectionName { get; set; } = "potions";

        public abstract void CanConsume(ConsumableChecker consumableChecker);
        public abstract void Consume();

        public Potion()
        {
            
        }

        public float GetBonusFromNGP()
        {
            GameSlot currentGameSlot = GameManager.Instance.currentGameSlot;
            if (currentGameSlot.gameType != GameSlot.GameType.NewGamePlus)
                return 0f;

            string floatName = currentGameSlot.wasDlcCompleted ? "ngpPotionsHealFactor" : "ngpPostDLCPotionsHealFactor";
            return Constants.GetFloat(floatName);
        }

        public void PlayPotionSound()
        {
            HeroMerchant.Instance.heroMerchantController.PlayPotionSound();
        }

        public void DestroyEffects()
        {
            HeroMerchant.Instance.DestroyEffects();
        }

        public new ConsumableItemMaster GetItemMaster()
        {
            if (consumableItemMaster != null)
                return consumableItemMaster;

            if (ItemRegister.TryGetItem<ConsumableItemMaster>(Name, out var result))
            {
                consumableItemMaster = result;
                return consumableItemMaster;
            }

            consumableItemMaster = new ConsumableItemMaster();
            consumableItemMaster = ItemDatabase.GetItemByName("HP Potion I").DuplicateAs<ConsumableItemMaster>();
            consumableItemMaster.SetItemName(NameKey, Name);
            consumableItemMaster.SetItemDescription(DescriptionKey, Description);
            consumableItemMaster.worldSpriteName = SpriteKey;

            consumableItemMaster.stats = new StatsModificator();
            consumableItemMaster.equipmentSlot = EquipmentItemMaster.EquipmentSlot.Potion;


            ItemRegister.AddItemMaster(consumableItemMaster);

            return consumableItemMaster;
        }



        public ItemStack CreateItemStack<T>() where T : Potion, new()
        {
            return CreateItemStack<T>(consumableItemMaster);
        }

        public ItemStack CreateItemStack<T>(EquipmentItemMaster itemMaster) where T : Potion, new()
        {
            var itemStack = CreateItemStack<T, EquipmentItemMaster>(itemMaster);
            return itemStack;
        }
    }
}
