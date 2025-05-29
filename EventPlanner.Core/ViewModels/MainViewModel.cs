using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using EventPlanner.Core.Messages;
using EventPlanner.Data.Models;
using EventPlanner.Data.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Core.ViewModels;
public partial class MainViewModel : ObservableObject
{
    IDatabase _db;

    private readonly EventColorService _colorService;

    public MainViewModel(IDatabase db, EventColorService colorService)
    {
        this._db = db;
        this._colorService = colorService;
        WeakReferenceMessenger.Default.Register<AddEventMessage>(this, (r, m) =>
        {
            System.Diagnostics.Debug.WriteLine(r);
            System.Diagnostics.Debug.WriteLine(m.Value);
            _events.Add(_colorService.AddColor(m.Value));
            UpdateCollections();
        });
    }

    private List<Event> _events = new List<Event>();

    [ObservableProperty]
    private ObservableCollection<Event> _eventsToday;

    [ObservableProperty]
    private ObservableCollection<Event> _eventsUpcoming;

    [ObservableProperty]
    private Event? _selectedEvent = null;

    partial void OnSelectedEventChanged(Event? value)
    {
        ShowDetails = true;
    }

    [ObservableProperty]
    private bool _showDetails = false;


    [ObservableProperty]
    private string _title;

    [ObservableProperty]
    private string _description;

    [ObservableProperty]
    private DateTime _date;

    private bool IsLoaded = false;

    [RelayCommand]
    void Toggle()
    {
        ShowDetails = !ShowDetails;
    }

    [RelayCommand]
    void DeleteEvent(Event eventToDelete)
    {
        var result = _db.DeleteEvent(eventToDelete);
        if (result)
        {
            _events.Remove(eventToDelete);
            UpdateCollections();
        }
    }

    [RelayCommand]
    void Load()
    {
        if (!IsLoaded)
        {
            var events = _db.GetEvents();
            
            foreach (var ev in events)
            {
                _events.Add(_colorService.AddColor(ev));
            }
            UpdateCollections();
            IsLoaded = !IsLoaded;
        }
    }
    private void UpdateCollections()
    {
        EventsToday = new ObservableCollection<Event>(_events.Where(e => e.Date == DateTime.Today));
        EventsUpcoming = new ObservableCollection<Event>(_events.Where(e => e.Date > DateTime.Today && e.Date <= DateTime.Today.AddDays(7)));
    }
}
