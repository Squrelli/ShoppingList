using ShopingList2.Models;

namespace ShopingList2.Views;

public partial class CategoryView : ContentView
{
    public event Action RequestRefresh;
    public event Action RequestSave;

    public CategoryView()
    {
        InitializeComponent();
        RequestRefresh ??= () => { };
        RequestSave ??= () => { };
    }

    public void LoadCategory(Category category)
    {
        BindingContext = category;

        CategoryHeader.Text = $"{(category.IsExpanded ? "+" : "-")} {category.Name.ToUpper()}";

        ProductsContainer.Children.Clear();
        ProductsContainer.IsVisible = category.IsExpanded;

        if (category.IsExpanded)
        {
            foreach (var product in category.Products.OrderBy(p => p.IsPurchased))
            {
                var productView = new ProductView { BindingContext = product };

                productView.DeleteRequested += (p) =>
                {
                    category.Products.Remove(p);
                    RequestRefresh?.Invoke();
                };

                productView.SaveRequested += () => RequestSave?.Invoke();

                productView.StatusChanged += async () =>
                {
                    RequestRefresh?.Invoke();
                };

                ProductsContainer.Children.Add(productView);
            }
        }
    }

    private void OnHeaderClicked(object sender, EventArgs e)
    {
        if (BindingContext is Category category)
        {
            category.IsExpanded = !category.IsExpanded;
            RequestRefresh?.Invoke();
        }
    }
}