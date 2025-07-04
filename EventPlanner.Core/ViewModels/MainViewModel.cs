﻿using CommunityToolkit.Mvvm.ComponentModel;
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
    public IDatabase _db;
    public MainViewModel(IDatabase db)
    {
        _db = db;
        WeakReferenceMessenger.Default.Register<AddEventMessage>(this, (r, m) =>
        {
            System.Diagnostics.Debug.WriteLine(r);
            System.Diagnostics.Debug.WriteLine(m.Value);
            _events.Add(m.Value);
            UpdateCollections();
        });
        WeakReferenceMessenger.Default.Register<DeleteEventMessage>(this, (r, m) =>
        {
            System.Diagnostics.Debug.WriteLine(r);
            System.Diagnostics.Debug.WriteLine(m.Value);
            var itemToRemove = _events.FirstOrDefault(ev => ev.Id == m.Value.Id);
            if (itemToRemove != null)
            {
                _events.Remove(itemToRemove);
                UpdateCollections();
            }
        });
        WeakReferenceMessenger.Default.Register<UpdateEventMessage>(this, (r, m) =>
        {
            _events = _db.GetEvents();
            UpdateCollections();
        });
        WeakReferenceMessenger.Default.Register<DetailsOpenMessage>(this, (r, m) =>
        {
            if(SelectedEvent != null)
            {
                ShowDetails = true;
            }
        });
        WeakReferenceMessenger.Default.Register<DetailsCloseMessage>(this, (r, m) =>
        {
            ShowDetails = false;

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
        EditedTitle = SelectedEvent?.Title ?? "";
        EditedDescription = SelectedEvent?.Description ?? "";
        EditedDate = SelectedEvent?.Date ?? DateTime.Today;
    }

    [ObservableProperty]
    private bool _showDetails = false;

    [ObservableProperty]
    private string _title;

    [ObservableProperty]
    private string _description;

    [ObservableProperty]
    private DateTime _date;

    [ObservableProperty]
    private string _editedTitle;

    [ObservableProperty]
    private string _editedDescription;

    [ObservableProperty]
    private DateTime _editedDate;

    private bool IsLoaded = false;

    [RelayCommand]
    void Load()
    {
        if (!IsLoaded)
        {
            _events = _db.GetEvents();
            UpdateCollections();
            IsLoaded = !IsLoaded;
        }
    }

    [RelayCommand]
    void Edit()
    {
        var oldEvent = _events.FirstOrDefault(e => e.Id == SelectedEvent.Id);
        var newEvent = new Event(EditedTitle, EditedDate, SelectedEvent.CategoryId)
        {
            Title = EditedTitle,
            Date = EditedDate,
            Description = EditedDescription
        };
        var result = _db.UpdateEvent(oldEvent, newEvent);
        if (result)
        {
            WeakReferenceMessenger.Default.Send(new UpdateEventMessage("Updated"));
            WeakReferenceMessenger.Default.Send(new DetailsCloseMessage("Close"));
            SelectedEvent = null;
        }
    }

    private void UpdateCollections()
    {
        EventsToday = new ObservableCollection<Event>(_events.Where(e => e.Date == DateTime.Today));
        EventsUpcoming = new ObservableCollection<Event>(_events.Where(e => e.Date > DateTime.Today && e.Date <= DateTime.Today.AddDays(7)));
    }
}
