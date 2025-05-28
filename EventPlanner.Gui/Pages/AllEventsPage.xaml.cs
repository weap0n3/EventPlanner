using CommunityToolkit.Maui.Behaviors;
using EventPlanner.Core.ViewModels;

namespace EventPlanner.Gui.Pages;

public partial class AllEvents : ContentPage
{
	public AllEvents(AllEventsViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;

        var behavior = new EventToCommandBehavior
        {
            EventName = nameof(ContentPage.Appearing),
            Command = viewModel.LoadCommand
        };
        Behaviors.Add(behavior);
    }
}