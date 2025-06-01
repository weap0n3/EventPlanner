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
    public EventService _eventService;
    public AllEventsViewModel(IDatabase db, EventService _colorService)
    {
        this._db = db;
        this._eventService = _colorService;

        WeakReferenceMessenger.Default.Register<AddEventMessage>(this, (r, m) =>
        {
            Events.Add(_eventService.AddColor(m.Value));
        });

        WeakReferenceMessenger.Default.Register<DeleteEventMessage>(this, (r, m) =>
        {
            Events.Remove(m.Value);
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

    private bool IsLoaded = false;

    [RelayCommand]
    void Load()
    {
        if (!IsLoaded)
        {
            System.Diagnostics.Debug.WriteLine("LOaded");
            var events = _db.GetEvents();
            foreach (var ev in events)
            {
                Events.Add(_eventService.AddColor(ev));
            }
            IsLoaded = !IsLoaded;
        }
    }
}
