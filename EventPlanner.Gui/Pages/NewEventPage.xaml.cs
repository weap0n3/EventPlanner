using EventPlanner.Core.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Syncfusion.Maui.Picker;
using System.Collections.ObjectModel;

namespace EventPlanner.Gui.Pages;

public partial class NewEventPage : ContentPage
{
    AddViewModel _vm;

	public NewEventPage(AddViewModel viewModel)
	{
		InitializeComponent();
        _vm = viewModel;
		this.BindingContext = _vm;

    }
    private void Button_Clicked(object sender, System.EventArgs e)
    {
        var column = picker.Columns?.FirstOrDefault();
        if (column != null)
        {
            column.SelectedIndex = 0;
        }
        this.picker.IsOpen = true;
    }

    private void OkButton_Clicked(object sender, System.EventArgs e)
    {
        var column = this.picker.Columns?.FirstOrDefault();
        if (column?.ItemsSource is IList<string> items && column.SelectedIndex >= 0)
        {
            _vm.SelectedCategory = items[column.SelectedIndex];
        }
        this.picker.IsOpen = false;
    }
}