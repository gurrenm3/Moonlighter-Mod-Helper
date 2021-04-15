using HarmonyLib;
using Moonlighter_Mod_Helper.Api.Items;

namespace Moonlighter_Mod_Helper.Patches
{
    [HarmonyPatch(typeof(RecipeManager), nameof(RecipeManager.Init))]
    internal class RecipeManager_Init
    {
        private static bool areRecipesLoaded;

        [HarmonyPrefix]
        internal static bool Prefix(RecipeManager __instance)
        {
            return true;
        }

        [HarmonyPostfix]
        internal static void Postfix(RecipeManager __instance)
        {
            if (areRecipesLoaded)
                return;

            System.Console.WriteLine("trying to load custom recipes");
            //CraftableItem.InitCustomRecipes();
            areRecipesLoaded = true;
            Main.LogMessage("Custom Recipes loaded");

            System.Console.WriteLine("done+++");
        }
    }
}
