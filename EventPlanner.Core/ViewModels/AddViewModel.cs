using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Core.ViewModels;

public partial class AddViewModel : ObservableObject
{
    [NotifyCanExecuteChangedFor(nameof(AddCommand))]
    [ObservableProperty]
    private string _title=string.Empty;

    [ObservableProperty]
    private string _description;

    [ObservableProperty]
    private DateTime _date = DateTime.Today;


    private bool CanAdd => Title != "";

    [RelayCommand(CanExecute = nameof(CanAdd))]
    void Add()
    {
        
        System.Diagnostics.Debug.WriteLine(Title);
        System.Diagnostics.Debug.WriteLine(Description);
        System.Diagnostics.Debug.WriteLine(Date);
    }
}
