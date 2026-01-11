using ShopingList2.Models;
using ShopingList2.Services;
using ShopingList2.Views;

namespace ShopingList2.Views;


public partial class MainPage : ContentPage
{
    public List<Category> Categories { get; set; }

    public MainPage()
    {
        InitializeComponent();
        Categories = StorageService.LoadData();
        UpdateCategoryPicker();
        RefreshUI();
    }

    private void UpdateCategoryPicker() =>
        CategoryPicker.ItemsSource = Categories.Select(c => c.Name).ToList();

    public void RefreshUI()
    {
        if (CategoriesStack == null) return;
        CategoriesStack.Children.Clear();

        foreach (var category in Categories)
        {
            var categoryView = new CategoryView();

            categoryView.LoadCategory(category);

            categoryView.RequestRefresh += () => SaveAndRefresh();
            categoryView.RequestSave += () => StorageService.SaveDataToFile(Categories);

            CategoriesStack.Children.Add(categoryView);
        }
    }

    private void OnAddProductClicked(object sender, EventArgs e)
    {
        string catName = !string.IsNullOrWhiteSpace(NewCategoryEntry.Text) ? NewCategoryEntry.Text : CategoryPicker.SelectedItem?.ToString();
        if (string.IsNullOrEmpty(catName) || string.IsNullOrWhiteSpace(ProductNameEntry.Text)) return;

        var category = Categories.FirstOrDefault(c => c.Name.Equals(catName, StringComparison.OrdinalIgnoreCase));
        if (category == null) { category = new Category { Name = catName }; Categories.Add(category); UpdateCategoryPicker(); }

        double.TryParse(ProductAmountEntry.Text, out double amt);
        category.Products.Add(new Product { Name = ProductNameEntry.Text, Amount = amt, Unit = ProductUnitEntry.Text });

        ProductNameEntry.Text = ProductAmountEntry.Text = NewCategoryEntry.Text = string.Empty;
        SaveAndRefresh();
    }

    private void SaveAndRefresh() { StorageService.SaveDataToFile(Categories); RefreshUI(); }
}