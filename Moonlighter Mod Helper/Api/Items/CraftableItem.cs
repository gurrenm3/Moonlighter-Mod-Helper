using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moonlighter_Mod_Helper.Extensions;

namespace Moonlighter_Mod_Helper.Api.Items
{
    public abstract class CraftableItem : Item
    {
        public List<Recipe> Recipes
        {
            get { return recipes; }
            set { recipes = value; }
        }
        private static List<Recipe> recipes;

        public abstract RecipeCollectionType RecipeCollectionType { get; set; }
        public abstract string CollectionName { get; set; }

        public abstract void CreateRecipes();


        public CraftableItem()
        {
            if (ShouldAddRecipesToDatabase())
            {
                CreateRecipes();
                AddRecipesToDatabase();
            }
        }

        public virtual void AddRecipesToDatabase()
        {
            if (!AreRecipesCreated())
            {
                Main.Log(BepInEx.Logging.LogLevel.Warning, $"Warning! Can't add recipe for \"{Name}\" because no recipes were created");
                return;
            }

            foreach (var recipe in recipes)
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
            return recipes is null || recipes.Count == 0;
        }
    }
}