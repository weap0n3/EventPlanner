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
    private void Button_Clicked(object sender, EventArgs e)
    {
        CategoryPickerControl.OpenPicker();
    }
}