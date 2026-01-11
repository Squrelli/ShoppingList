using ShopingList2.Models;

namespace ShopingList2.Views;

public partial class ProductView : ContentView
{
    public event Action<Product> DeleteRequested;
    public event Action SaveRequested;
    public event Action StatusChanged;

    public ProductView()
    {
        InitializeComponent();
        DeleteRequested ??= (_) => { };
        SaveRequested ??= () => { };
        StatusChanged ??= () => { };
    }

    private void OnDecreaseClicked(object sender, EventArgs e)
    {
        if (BindingContext is Product p && p.Amount > 0) { p.Amount--; SaveRequested?.Invoke(); }
    }

    private void OnIncreaseClicked(object sender, EventArgs e)
    {
        if (BindingContext is Product p) { p.Amount++; SaveRequested?.Invoke(); }
    }

    private void OnDeleteClicked(object sender, EventArgs e)
    {
        if (BindingContext is Product p) DeleteRequested?.Invoke(p);
    }
    private void OnCheckboxChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender is CheckBox cb && cb.IsFocused)
        {
            SaveRequested?.Invoke();
            StatusChanged?.Invoke();
        }
    }
}