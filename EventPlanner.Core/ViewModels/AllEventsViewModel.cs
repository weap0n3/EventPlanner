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

namespace EventPlanner.Core.ViewModels;

public partial class AllEventsViewModel : ObservableObject
{
    IDatabase _db;
    public AllEventsViewModel(IDatabase db)
    {
        this._db = db;
    }

    [ObservableProperty]
    private ObservableCollection<Event> _events;

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
                _events.Add(ev);
            }
            IsLoaded = !IsLoaded;
        }
    }
}
