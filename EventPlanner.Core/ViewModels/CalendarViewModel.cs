using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using EventPlanner.Core.Messages;
using EventPlanner.Data.Models;
using EventPlanner.Data.Services;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Scheduler;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace EventPlanner.Core.ViewModels;

public partial class CalendarViewModel : ObservableObject
{
    IDatabase _db;

    public CalendarViewModel(IDatabase db)
    {
        this._db = db;
        CalendarEvents = new ObservableCollection<Event>(_db.GetEvents());
        WeakReferenceMessenger.Default.Register<AddEventMessage>(this, (r, m) => Load());
        WeakReferenceMessenger.Default.Register<DeleteEventMessage>(this, (r, m) => Load());
        WeakReferenceMessenger.Default.Register<UpdateEventMessage>(this, (r, m) => Load());
    }

    [ObservableProperty]
    ObservableCollection<Event> _calendarEvents;

    [RelayCommand]
    void Load()
    {
        var events = _db.GetEvents();

        CalendarEvents.Clear();

        foreach (var ev in events)
        {
            CalendarEvents.Add(ev);
        }
    }

}
