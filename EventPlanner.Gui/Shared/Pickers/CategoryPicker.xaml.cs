using CommunityToolkit.Maui.Views;
using EventPlanner.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EventPlanner.Gui.Shared.Pickers;

public partial class CategoryPicker : ContentView
{
    public static readonly BindableProperty CategoriesProperty =
        BindableProperty.Create(nameof(Categories), typeof(IEnumerable<string>), typeof(CategoryPicker), default(IEnumerable<string>));

    public IEnumerable<string> Categories
    {
        get => (IEnumerable<string>)GetValue(CategoriesProperty);
        set => SetValue(CategoriesProperty, value);
    }


    public static readonly BindableProperty SelectedCategoryProperty =
    BindableProperty.Create(nameof(SelectedCategory), typeof(string), typeof(CategoryPicker), null);

    public string SelectedCategory
    {
        get => (string)GetValue(SelectedCategoryProperty);
        set => SetValue(SelectedCategoryProperty, value);
    }

    private void OkButton_Clicked(object sender, EventArgs e)
    {
        var column = picker.Columns?.FirstOrDefault();
        if (column?.ItemsSource is IList<string> items && column.SelectedIndex >= 0)
        {
            SelectedCategory = items[column.SelectedIndex];
        }

        picker.IsOpen = false;
    }

    public void OpenPicker()
    {
        var column = picker.Columns.FirstOrDefault();

        if (column?.ItemsSource is IList<string> items)
        {
            System.Diagnostics.Debug.WriteLine($"Items count: {items.Count}");

            if (items.Count > 0)
            {
                var firstValue = items[0];
                System.Diagnostics.Debug.WriteLine($"First value: {firstValue}");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Items list is empty");
            }
        }
        else
        {
            System.Diagnostics.Debug.WriteLine("ItemsSource is null or not a list of strings");
        }

        picker.IsOpen = true;
    }


    public CategoryPicker()
	{
		InitializeComponent();
	}
    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        System.Diagnostics.Debug.WriteLine("CategoryPicker - BindingContext changed");
        System.Diagnostics.Debug.WriteLine($"Categories null? {Categories == null}");
        System.Diagnostics.Debug.WriteLine($"Count: {Categories?.Count()}");
    }

}