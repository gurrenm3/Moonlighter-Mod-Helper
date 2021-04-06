namespace Moonlighter_Mod_Helper.Extensions
{
    public static class RecipeIngredientExt
    {
        public static void Init(this RecipeIngredient recipeIngredient)
        {
            recipeIngredient.Init(GameManager.Instance.GetCurrentGamePlusLevel());
        }
    }
}
