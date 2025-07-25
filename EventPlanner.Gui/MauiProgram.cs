﻿using CommunityToolkit.Maui;
using EventPlanner.Core.ViewModels;
using EventPlanner.Data.Services;
using EventPlanner.Gui.Pages;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;

namespace EventPlanner.Gui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.ConfigureSyncfusionCore();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Raleway-VariableFont.ttf", "Raleway");
                    fonts.AddFont("Inter-VariableFont.ttf", "Inter");
                    fonts.AddFont("Raleway-Bold.ttf", "Raleway-Bold");
                    fonts.AddFont("segoeuithis.ttf", "Segoe UI");
                });
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainViewModel>();

            builder.Services.AddSingleton<NewEventPage>();
            builder.Services.AddSingleton<AddViewModel>();

            builder.Services.AddSingleton<AllEvents>();
            builder.Services.AddSingleton<AllEventsViewModel>();

            builder.Services.AddSingleton<EventService>();

            builder.Services.AddSingleton<CalendarPage>();
            builder.Services.AddSingleton<CalendarViewModel>();

            var path = FileSystem.AppDataDirectory;
            System.Diagnostics.Debug.WriteLine("Path " + path);
            string file = Path.Combine(path, "eventplanner.db");

            if (File.Exists(file))
            {
                System.Diagnostics.Debug.WriteLine("File exists");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("File does not exist");
            }

            builder.Services.AddSingleton<IDatabase>(new DBRepository(file));

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
