using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EventPlanner.Data.Models;
using EventPlanner.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Core.ViewModels;

public partial class AddViewModel : ObservableObject
{
    IDatabase _db;

    public AddViewModel(IDatabase db)
    {
        this._db = db;
    }

    [NotifyCanExecuteChangedFor(nameof(AddCommand))]
    [ObservableProperty]
    private string _title = string.Empty;

    [ObservableProperty]
    private string _description;

    [ObservableProperty]
    private DateTime _date = DateTime.Today;


    private bool CanAdd => Title != "";

    [RelayCommand(CanExecute = nameof(CanAdd))]
    void Add()
    {
        Event e = new Event(Title, Date)
        {
            Description = Description,
            ColorKey = ""
        };
        var result = _db.AddEvent(e);
        if (result)
        {
            this.Title = "";
            this.Description = "";
            this.Date = DateTime.Today;
        }

    }
}
