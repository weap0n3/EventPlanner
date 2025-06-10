using CommunityToolkit.Maui.Behaviors;
using EventPlanner.Core.ViewModels;
using EventPlanner.Data.Models;
using Syncfusion.Maui.Scheduler;
using System.Collections.ObjectModel;

namespace EventPlanner.Gui.Pages;

public partial class CalendarPage : ContentPage
{
	public CalendarPage(CalendarViewModel viewModel)
	{
		InitializeComponent();
        this.BindingContext = viewModel;

        SfScheduler scheduler = new SfScheduler
        {
            View = SchedulerView.Month,
            MinimumDateTime = new DateTime(2025, 1, 1), 
            MaximumDateTime = new DateTime(2025, 12, 31)
        };
        var mapping = new SchedulerAppointmentMapping
        {
            StartTime = "Date",
            EndTime = "Date",
            Subject = "Title"
        };

        scheduler.AppointmentMapping = mapping;


        scheduler.AppointmentsSource = viewModel.CalendarEvents;
        this.Content = scheduler;
    }


}
