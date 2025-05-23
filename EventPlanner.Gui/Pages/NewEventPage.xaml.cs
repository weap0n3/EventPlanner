using EventPlanner.Core.ViewModels;

namespace EventPlanner.Gui.Pages;

public partial class NewEventPage : ContentPage
{
	public NewEventPage(AddViewModel viewModel)
	{
		InitializeComponent();

		this.BindingContext = viewModel;
	}
}