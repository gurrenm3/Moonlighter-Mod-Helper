
namespace Moonlighter_Mod_Helper.Extensions
{
    public static class RecipeManagerExt
    {
        public static RecipeManager.RecipeDatabase GetRecipeDatabase(this RecipeManager recipeManager)
        {
            int plusLevel = GameManager.Instance.GetCurrentGamePlusLevel();
            return recipeManager.GetRecipeDatabase(plusLevel);
        }

        public static RecipeManager.RecipeDatabase GetRecipeDatabase(this RecipeManager recipeManager, int plusLevel)
        {
            return RecipeManager.Instance.database[plusLevel];
        }
    }
}
