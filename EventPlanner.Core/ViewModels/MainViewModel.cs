using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EventPlanner.Data.Models;
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
    private List<Event> _events = new List<Event>()
    {
        new Event("Gello",new DateTime(2025, 5, 19))
    };

    private string[] _randomColors = { "#D8E2DC", "#FFD7BA", "#FEC5BB", "#FAE1DD" };


    [ObservableProperty]
    private string _eventBackgroundColor;

    [ObservableProperty]
    private ObservableCollection<Event> _eventsToday = new();

    [ObservableProperty]
    private ObservableCollection<Event> _eventsUpcoming;

    [ObservableProperty]
    private string _title;

    [ObservableProperty]
    private string _description;

    [ObservableProperty]
    private DateTime _date;

    [RelayCommand]
    void Load()
    {
        this.Title = "Test";
        this.Description = "TEtstsetsetsetsfdasf";
        this.Date = new DateTime(2025,5,23);
        this.EventBackgroundColor = _randomColors[new Random().Next(0, 3)];
        System.Diagnostics.Debug.WriteLine(EventBackgroundColor);
        _events.Add(new Event(Title, Date)
        {
            Description = Description,
            EventBackgroundColor = this.EventBackgroundColor

        });
        EventsToday = new ObservableCollection<Event>(_events.Where(e => e.Date == DateTime.Today));
        EventsUpcoming = new ObservableCollection<Event>(_events.Where(e => e.Date > DateTime.Today && e.Date <= DateTime.Today.AddDays(7)));
    }
}
