using Lab2.DataAccess;
using System;
using Microsoft.EntityFrameworkCore;

namespace Lab2.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var db = new RecipeDbContext();

            db.Database.EnsureCreated();


            //var cbook = new Cookbook
            //{
            //    Title = "Vegetarian",

            //};
            //var cbook2 = new Cookbook
            //{
            //    Title = "Healthy",

            //};
            //db.Cookbooks.Add(cbook);
            //db.Cookbooks.Add(cbook2);

            var vegetarian2 = db.Cookbooks.FirstOrDefault(v => v.Title == "Vegetarian");
            var healthy2 = db.Cookbooks.FirstOrDefault(v => v.Title == "Healthy");
            //var rcp = new Recipe
            //{
            //    Category = "Breakfast",
            //    DishName = "Cheese omelette",
            //    PrepTime = 10,
            //    Cookbooks = vegetarian2,
            //    Ingredients = new List<Ingredient>
            //    {
            //        new Ingredient
            //        {
            //            Name = "Egg",
            //            Amount = 2
            //        },
            //        new Ingredient
            //        {
            //            Name = "Olive oil",
            //            Amount = 0.5,
            //            Units = "tbsp"
            //        },
            //        new Ingredient
            //        {
            //            Name = "Butter",
            //            Amount = 1,
            //            Units = "tbsp"
            //        },
            //        new Ingredient
            //        {
            //            Name = "cheddar",
            //            Amount = 15,
            //            Units = "g"
            //        }
            //    }

            //};
            //var rcp2 = new Recipe
            //{
            //    Category = "Breakfast",
            //    DishName = "Fried bread",
            //    PrepTime = 7,
            //    Cookbooks = vegetarian2,
            //    Ingredients = new List<Ingredient>
            //    {
            //        new Ingredient
            //        {
            //            Name = "White bread",
            //            Amount = 2,
            //            Units = "slices"
            //        },
            //        new Ingredient
            //        {
            //            Name = "Sunflower oil",
            //            Amount = 2,
            //            Units = "tbsp"
            //        }

            //    }

            //};
            //var rcp3 = new Recipe
            //{
            //    Category = "Dinner",
            //    DishName = "Sweet potato & peanut curry",
            //    PrepTime = 60,
            //    Cookbooks = healthy2,
            //    Ingredients = new List<Ingredient>
            //    {
            //        new Ingredient
            //        {
            //            Name = "Coconut oil",
            //            Amount = 1,
            //            Units = "tbsp"
            //        },
            //        new Ingredient
            //        {
            //            Name = "Garlic cloves",
            //            Amount = 2
            //        },
            //        new Ingredient
            //        {
            //            Name = "Ginger",
            //            Amount = 1
            //        },
            //        new Ingredient
            //        {
            //            Name = "Thai red curry paste",
            //            Amount = 3,
            //            Units = "tbsp"
            //        },
            //        new Ingredient
            //        {
            //            Name = "Smooth peanut butter",
            //            Amount = 1,
            //            Units = "tbsp"
            //        },
            //        new Ingredient
            //        {
            //            Name = "Sweet potato",
            //            Amount = 500,
            //            Units = "g"
            //        },
            //        new Ingredient
            //        {
            //            Name = "Coconut milk",
            //            Amount = 400,
            //            Units = "ml"
            //        },
            //        new Ingredient
            //        {
            //            Name = "Spinach",
            //            Amount = 200,
            //            Units = "g"
            //        },
            //        new Ingredient
            //        {
            //            Name = "Lime",
            //            Amount = 1
            //        }
            //    }

            //};

            //db.Recipes.Add(rcp);
            //db.Recipes.Add(rcp2);
            //db.Recipes.Add(rcp3);

            //izvades
            var results = db.Recipes
                .Where(r => r.PrepTime <= 30);
            System.Console.WriteLine("Prep time under 30 minutes:");
            foreach (var recipe in results)
            {
                System.Console.WriteLine(recipe.DishName);
            }

            //var record = db.Recipes.FirstOrDefault(r => r.Category == "Breakfast");
            //System.Console.WriteLine("\nBreakfast: \n" + record.DishName);


            //var recordFull = db.Recipes.Include(r => r.Ingredients).FirstOrDefault(r => r.Category == "Dinner");

            ////recordFull.DishName = "Sweet potato & peanut curry";
            //System.Console.WriteLine("\nDinner: \n" + recordFull.DishName);

            //var recordFaulty = db.Ingredients.FirstOrDefault(i => i.Name == "cheddar");
            //recordFaulty.Name = "Cheese";
            //System.Console.WriteLine("\nDinner: \n" + recordFaulty.Name);

            //recordFull.Ingredients.RemoveAt(recordFull.Ingredients.Count - 1);

            //var resutltsAll = db.Recipes;
            //foreach (var recipe in resutltsAll)
            //{
            //    db.Recipes.Remove(recipe);
            //}
            db.SaveChanges();
        }
    }
}  
