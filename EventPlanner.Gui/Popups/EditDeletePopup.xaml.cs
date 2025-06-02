using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using EventPlanner.Core.Messages;
using EventPlanner.Data.Models;
using EventPlanner.Data.Services;

namespace EventPlanner.Gui.Popups;

public partial class EditDeletePopup : Popup
{
    private readonly Event _event;
    private EventService _eventService;

    public EditDeletePopup(Event ev, EventService eventService)
    {
        InitializeComponent();
        _event = ev;
        this._eventService = eventService;
    }

    private void OnDeleteClicked(object sender, EventArgs e)
    {
        var result = _eventService.Delete(_event);
        if (result)
        {
            WeakReferenceMessenger.Default.Send(new DeleteEventMessage(_event));
        }
        Close();
    }
    private void OnEditClicked(object sender, EventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new DetailsOpenMessage("open"));
        Close();
    }
}