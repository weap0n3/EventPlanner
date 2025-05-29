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

namespace EventPlanner.Core.ViewModels;

public partial class AllEventsViewModel : ObservableObject
{
    IDatabase _db;
    private readonly EventColorService _colorService;
    public AllEventsViewModel(IDatabase db, EventColorService _colorService)
    {
        this._db = db;
        this._colorService = _colorService;
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
                Events.Add(_colorService.AddColor(ev));
            }
            IsLoaded = !IsLoaded;
        }
    }
}
