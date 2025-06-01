using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Views;
using EventPlanner.Core.ViewModels;
using EventPlanner.Data.Models;
using EventPlanner.Gui.Popups;

namespace EventPlanner.Gui.Pages;

public partial class AllEvents : ContentPage
{
    private readonly AllEventsViewModel _allEventsViewModel;
	public AllEvents(AllEventsViewModel viewModel)
	{
		InitializeComponent();
        this._allEventsViewModel = viewModel;
		this.BindingContext = _allEventsViewModel;

        var behavior = new EventToCommandBehavior
        {
            EventName = nameof(ContentPage.Appearing),
            Command = viewModel.LoadCommand
        };
        Behaviors.Add(behavior);
    }
    private void OnDetailsTapped(object sender, EventArgs e)
    {
        if ((sender as Image)?.BindingContext is Event selectedEvent)
        {
            var popup = new EditDeletePopup(selectedEvent, _allEventsViewModel._eventService); 

            this.ShowPopupAsync(popup); 
        }
    }
}