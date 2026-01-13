using System.Collections.ObjectModel;
using System.Text.Json;
using ShopingList2.Models;

namespace ShopingList2.Services
{
    public static class StorageService
    {
        private static string FilePath => Path.Combine(FileSystem.AppDataDirectory, "shopping_list.json");

        public static void SaveDataToFile(List<Category> categories)
        {
            var json = JsonSerializer.Serialize(categories);
            File.WriteAllText(FilePath, json);
        }

        public static List<Category> LoadData()
        {
            if (!File.Exists(FilePath)) return GiveDefaultCategories();
            try
            {
                var json = File.ReadAllText(FilePath);
                return JsonSerializer.Deserialize<List<Category>>(json) ?? GiveDefaultCategories();
            }
            catch { return GiveDefaultCategories(); }
        }

        private static List<Category> GiveDefaultCategories()
        {
            return new List<Category>
            {
                new Category { Name = "Nabiał" },
                new Category { Name = "Warzywa" }
            };
        }
    }
}