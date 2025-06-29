using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using EventPlanner.Core.Messages;
using EventPlanner.Data.Models;
using EventPlanner.Data.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Core.ViewModels;

public partial class AddViewModel : ObservableObject
{
    IDatabase _db;
    private readonly string[] _randomColors = { "PastelBLue", "PastelLightYellow", "PastelRed", "PastelLightRed" };
    private string _newColor = string.Empty;
    private readonly Random _random = new Random();

    public AddViewModel(IDatabase db)
    {
        this._db = db;
        Date = null;
    }

    [NotifyCanExecuteChangedFor(nameof(AddCommand))]
    [ObservableProperty]
    private string _title = string.Empty;

    [ObservableProperty]
    private string _description = string.Empty;

    [ObservableProperty]
    private DateTime? _date;

    [ObservableProperty]
    ObservableCollection<string> _categories = new ObservableCollection<string>()
    {
        "Birthdays", "Holidays & Festivities", "Appointments", "Personal Milestones", "Reminders & Tasks"
    };

    [ObservableProperty]
    private string _selectedCategory = "Select Category";

    private bool CanAdd => Title != "";

    [RelayCommand(CanExecute = nameof(CanAdd))]
    void Add()
    {
        var lastEventColor = _db.GetEvents().LastOrDefault()?.ColorKey ?? "";

        do
        {
            _newColor = _randomColors[_random.Next(_randomColors.Length)];
        } while (_newColor == lastEventColor);

        Event e = new Event(Title, Date ?? DateTime.Today, Categories.IndexOf(SelectedCategory) + 1)
        {
            Description = Description,
            ColorKey = _newColor,
            Category = _db.GetCategoryByTitle(SelectedCategory)
        };
        var result = _db.AddEvent(e);
        System.Diagnostics.Debug.WriteLine(result);
        if (result)
        {
            WeakReferenceMessenger.Default.Send(new AddEventMessage(e));
            this.Title = "";
            this.Description = "";
            this.Date = null;
            this.SelectedCategory = "Select Category";
        }
    }
    [RelayCommand]
    void ShowDatePicker()
    {
        Date = DateTime.Today; // or open programmatically if needed
    }

}
