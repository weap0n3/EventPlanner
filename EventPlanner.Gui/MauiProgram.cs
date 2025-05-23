using CommunityToolkit.Maui;
using EventPlanner.Core.ViewModels;
using EventPlanner.Gui.Pages;
using Microsoft.Extensions.Logging;

namespace EventPlanner.Gui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
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
                });
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainViewModel>();

            builder.Services.AddSingleton<NewEventPage>();
            builder.Services.AddSingleton<AddViewModel>();

            var path = FileSystem.AppDataDirectory;
            System.Diagnostics.Debug.WriteLine("Path " + path);
            string file = Path.Combine(path, "contacts.db");

            if (File.Exists(file))
            {
                System.Diagnostics.Debug.WriteLine("File exists");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("File does not exist");
            }

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
