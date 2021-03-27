using Moonlighter_Mod_Helper.Extensions;

namespace Moonlighter_Mod_Helper.Api.Items
{
    public class Potion : BagItem, IConsumable, IEquippable
    {
        public EquipmentItemMaster.EquipmentSlot EquipmentSlot { get; } = EquipmentItemMaster.EquipmentSlot.Potion;

        public Potion()
        {
            InventoryIcon = ItemDatabase.GetSpriteByItemName("HPPotionI");
            ItemName = "A Custom Potion";
            ItemDescription = "This is a custom potion. What it does is a mystery...";
        }

        public virtual void CanConsume(ConsumableChecker consumableChecker)
        {
            System.Console.WriteLine("==== Potion Base Class CanConsume() called ====");
            //consumableChecker.canConsume = GameManager.Instance.IsDungeonSceneLoaded();
        }

        public virtual void Consume()
        {

        }

        public virtual void OnEquipped()
        {
            HUDManager.Instance.SetPotionConsumableIcon(InventoryIcon);
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
    }
}
