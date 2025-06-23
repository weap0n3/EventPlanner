using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EventPlanner.Data.Models;
using EventPlanner.Data.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventPlanner.Core.ViewModels;
using EventPlanner.Core.Messages;
using CommunityToolkit.Mvvm.Messaging;

namespace EventPlanner.Core.ViewModels;

public partial class AllEventsViewModel : ObservableObject
{
    IDatabase _db;
    public AllEventsViewModel(IDatabase db)
    {
        this._db = db;

        WeakReferenceMessenger.Default.Register<AddEventMessage>(this, (r, m) =>
        {
            Events.Add(m.Value);
        });

        WeakReferenceMessenger.Default.Register<DeleteEventMessage>(this, (r, m) =>
        {
            var itemToRemove = Events.FirstOrDefault(ev => ev.Id == m.Value.Id);
            if (itemToRemove != null)
            {
                Events.Remove(itemToRemove);
            }
        });
        WeakReferenceMessenger.Default.Register<UpdateEventMessage>(this, (r, m) =>
        {
            Events = new ObservableCollection<Event>(_db.GetEvents());
        });
        WeakReferenceMessenger.Default.Register<DetailsOpenMessage>(this, (r, m) =>
        {
            ShowDetails = true;
        });
    }

    [ObservableProperty]
    private ObservableCollection<Event> _events = new ObservableCollection<Event>();

    [ObservableProperty]
    private string _title;

    [ObservableProperty]
    private string _description;

    [ObservableProperty]
    private DateTime _date;

    [ObservableProperty]
    private Event? _selectedEvent = null;

    partial void OnSelectedEventChanged(Event? value)
    {
        EditedTitle = SelectedEvent?.Title?? "";
        EditedDescription = SelectedEvent?.Description ?? "";
        EditedDate = SelectedEvent?.Date ?? DateTime.Today;
    }

    [ObservableProperty]
    private bool _showDetails = false;

    [ObservableProperty]
    private string _editedTitle;

    [ObservableProperty]
    private string _editedDescription;

    [ObservableProperty]
    private DateTime _editedDate;

    [RelayCommand]
    void Edit()
    {
        var oldEvent = _events.FirstOrDefault(e => e.Id == SelectedEvent.Id);
        var newEvent = new Event(EditedTitle, EditedDate, SelectedEvent.CategoryTitle)
        {
            Title = EditedTitle,
            Date = EditedDate,
            Description = EditedDescription
        };
        var result = _db.UpdateEvent(oldEvent, newEvent);
        if (result)
        {
            WeakReferenceMessenger.Default.Send(new UpdateEventMessage("Updated"));
        }
        ShowDetails = false;
    }

    private bool IsLoaded = false;

    [RelayCommand]
    void Load()
    {
        if (!IsLoaded)
        {
            Events = new ObservableCollection<Event>(_db.GetEvents());
            IsLoaded = !IsLoaded;
        }
    }
}
