using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moonlighter_Mod_Helper.Api;

namespace Moonlighter_Mod_Helper.Extensions
{
    public static class RecipeExt
    {
        public static void SetUnlocked(this Recipe recipe)
        {
            recipe._unlocked = Constants.GetInt("ingredientsDiscoveredToUnlock");
            recipe.unlockedAtStart = true;
        }

        public static void SetVisible(this Recipe recipe)
        {
            recipe.SetUnlocked();
            recipe._seen = true;
        }

        public static void AddIngredient(this Recipe recipe, RecipeIngredient recipeIngredient)
        {
            recipe.ingredients.Add(recipeIngredient);
        }

        public static void AddIngredient(this Recipe recipe, string itemName, int quantity, int plusLevel = 0)
        {
            var recipeIngredient = new RecipeIngredient(itemName, quantity);
            recipeIngredient.Init(plusLevel);
            recipe.AddIngredient(recipeIngredient);
        }

        public static Recipe Init(this Recipe recipe, string itemName)
        {
            var itemMaster = ItemDatabase.GetItemByName(itemName);
            return itemMaster is null ? null : recipe.Init(itemMaster);
        }

        public static Recipe Init(this Recipe recipe, ItemMaster itemMaster)
        {
            recipe.craftedItemName = itemMaster.name;
            recipe.craftedItemNameKey = itemMaster.nameKey;
            recipe.plusLevel = itemMaster.plusLevel;

            if (recipe.ingredients is null)
                recipe.ingredients = new List<RecipeIngredient>();

            recipe.Init(recipe.plusLevel);
            return recipe;
        }

        public static void AddToRecipeDatabase(this Recipe recipe, RecipeCollectionType collectionType, string collectionName = "")
        {
            int plusLevel = GameManager.Instance.GetCurrentGamePlusLevel();
            recipe.AddToRecipeDatabase(collectionType, plusLevel, collectionName);
        }

        public static void AddToRecipeDatabase(this Recipe recipe, RecipeCollectionType collectionType, int plusLevel, string collectionName = "")
        {
            var database = RecipeManager.Instance.GetRecipeDatabase(plusLevel);
            switch (collectionType)
            {
                case RecipeCollectionType.All:
                    recipe.AddToRecipeDatabase(database._allBlacksmithCollections, collectionName);
                    recipe.AddToRecipeDatabase(database._allWitchCollections, collectionName);
                    break;
                case RecipeCollectionType.Blacksmith:
                    recipe.AddToRecipeDatabase(database._allBlacksmithCollections, collectionName);
                    break;
                case RecipeCollectionType.Witch:
                    recipe.AddToRecipeDatabase(database._allWitchCollections, collectionName);
                    break;
                default:
                    break;
            }

            bool containsRecipe = database._recipeByItemName.TryGetValue(recipe.craftedItemName, out List<Recipe> recipes);
            if (containsRecipe)
                database._recipeByItemName[recipe.craftedItemName].Add(recipe);
            else
                database._recipeByItemName.Add(recipe.craftedItemName, new List<Recipe>() { recipe });

        }

        private static void AddToRecipeDatabase(this Recipe recipe, List<RecipeCollection> recipeCollections, string collectionName)
        {
            foreach (var recipeCollection in recipeCollections)
            {
                if (!string.IsNullOrEmpty(collectionName) && recipeCollection.collectionName != collectionName)
                    continue;

                bool hasRecipe = recipeCollection.recipes.FirstOrDefault(item => ReferenceEquals(item, recipe)) != null;
                if (hasRecipe)
                    continue;

                recipeCollection.recipes.Add(recipe);
            }
        }
    }
}
