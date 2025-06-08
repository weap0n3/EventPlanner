using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using EventPlanner.Core.Messages;
using EventPlanner.Data.Models;
using EventPlanner.Data.Services;

namespace EventPlanner.Gui.Popups;

public partial class EditDeletePopup : Popup
{
    private readonly Event _event;
    IDatabase _db;

    public EditDeletePopup(Event ev, IDatabase db)
    {
        InitializeComponent();
        _event = ev;
        this._db = db;
    }

    private void OnDeleteClicked(object sender, EventArgs e)
    {
        var result = _db.DeleteEvent(_event);
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