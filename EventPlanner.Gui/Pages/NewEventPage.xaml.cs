using EventPlanner.Core.ViewModels;

namespace EventPlanner.Gui.Pages;

public partial class NewEventPage : ContentPage
{
	public NewEventPage(AddViewModel viewModel)
	{
		InitializeComponent();

		this.BindingContext = viewModel;

    }
    private void Button_Clicked(object sender, System.EventArgs e)
    {
        this.picker.IsOpen = true;
    }
}