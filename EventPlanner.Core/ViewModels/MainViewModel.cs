﻿using CommunityToolkit.Mvvm.ComponentModel;
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

    [RelayCommand]
    void Load()
    {
        this.Title = "Test";
        this.Description = "TEtstsetsetsetsfdasf";
        this.Date = new DateTime(2025,5,23);
        do
        {
            _backgroundColor = _randomColors[new Random().Next(_randomColors.Length)];
        }
        while (_backgroundColor == _events[_events.Count - 1].ColorKey);
        _events.Add(new Event(Title, Date)
        {
            Description = Description,
            ColorKey = _backgroundColor

        });
        EventsToday = new ObservableCollection<Event>(_events.Where(e => e.Date == DateTime.Today));
        EventsUpcoming = new ObservableCollection<Event>(_events.Where(e => e.Date > DateTime.Today && e.Date <= DateTime.Today.AddDays(7)));
    }
}
