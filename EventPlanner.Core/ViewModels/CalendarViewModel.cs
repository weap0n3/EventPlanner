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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Core.ViewModels;

public partial class CalendarViewModel : ObservableObject
{
    EventService _eventService;

    public CalendarViewModel(EventService eventService)
    {
        _eventService = eventService;
        CalendarEvents = new ObservableCollection<Event>(_eventService.GetAll());
        WeakReferenceMessenger.Default.Register<AddEventMessage>(this, (r, m) => Load());
        WeakReferenceMessenger.Default.Register<DeleteEventMessage>(this, (r, m) => Load());
        WeakReferenceMessenger.Default.Register<UpdateEventMessage>(this, (r, m) => Load());
    }

    [ObservableProperty]
    ObservableCollection<Event> _calendarEvents;

    [RelayCommand]
    void Load()
    {
        var events = _eventService.GetAll();

        CalendarEvents.Clear();

        foreach (var ev in events)
        {
            CalendarEvents.Add(ev);
        }
    }

}
