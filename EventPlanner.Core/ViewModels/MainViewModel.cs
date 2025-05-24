using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EventPlanner.Data.Models;
using EventPlanner.Data.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Core.ViewModels;

public partial class MainViewModel : ObservableObject
{
    IDatabase _db;

    public MainViewModel(IDatabase db)
    {
        this._db = db;
    }

    private List<Event> _events = new List<Event>()
    {
        new Event("Gello", DateTime.Today)
    };

    private string[] _randomColors = { "PastelBLue", "PastelLightYellow", "PastelRed", "PastelLightRed" };

    [ObservableProperty]
    private ObservableCollection<Event> _eventsToday = new();

    [ObservableProperty]
    private ObservableCollection<Event> _eventsUpcoming;

    private string _backgroundColor;

    [ObservableProperty]
    private string _title;

    [ObservableProperty]
    private string _description;

    [ObservableProperty]
    private DateTime _date;

    private bool IsLoaded = true;

    [RelayCommand]
    void Load()
    {
        if (IsLoaded)
        {
            var events = _db.GetEvents();
            foreach (var ev in events)
            {
                do
                {
                    _backgroundColor = _randomColors[new Random().Next(_randomColors.Length)];
                }
                while (_backgroundColor == _events[_events.Count - 1].ColorKey);
                ev.ColorKey = _backgroundColor;
                _events.Add(ev);
            }
            EventsToday = new ObservableCollection<Event>(_events.Where(e => e.Date == DateTime.Today));
            EventsUpcoming = new ObservableCollection<Event>(_events.Where(e => e.Date > DateTime.Today && e.Date <= DateTime.Today.AddDays(7)));
            IsLoaded = !IsLoaded;
        }
    }
}
