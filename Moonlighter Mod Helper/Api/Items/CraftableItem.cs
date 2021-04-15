using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moonlighter_Mod_Helper.Extensions;

namespace Moonlighter_Mod_Helper.Api.Items
{
    public abstract class CraftableItem : Item
    {
        internal static event EventHandler LoadCustomRecipes;

        public List<Recipe> Recipes
        {
            get { return recipes; }
            set { recipes = value; }
        }
        private List<Recipe> recipes = new List<Recipe>();

        public abstract RecipeCollectionType RecipeCollectionType { get; set; }
        public abstract string CollectionName { get; set; }

        public abstract void CreateRecipes();


        public CraftableItem()
        {
            LoadCustomRecipes += CraftableItem_LoadCustomRecipes;
        }

        public virtual void AddRecipesToDatabase()
        {
            if (!AreRecipesCreated())
            {
                Main.LogWarning($"Warning! Can't add recipe for \"{Name}\" because no recipes were created");
                return;
            }

            foreach (var recipe in Recipes)
            {
                recipe.AddToRecipeDatabase(RecipeCollectionType, CollectionName);
            }
        }

        public virtual bool ShouldAddRecipesToDatabase()
        {
            return AreRecipesCreated();
        }

        private bool AreRecipesCreated()
        {
            return Recipes != null && Recipes.Count > 0;
        }

        internal static void InitCustomRecipes()
        {
            LoadCustomRecipes?.Invoke(null, EventArgs.Empty);
        }

        private void CraftableItem_LoadCustomRecipes(object sender, EventArgs e)
        {
            CreateRecipes();
            AddRecipesToDatabase();
        }
    }
}