using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Views;
using EventPlanner.Core.ViewModels;
using EventPlanner.Data.Models;
using EventPlanner.Gui.Popups;

namespace EventPlanner.Gui
{
    public partial class MainPage : ContentPage
    {
        private readonly MainViewModel _mainViewModel;
        public MainPage(MainViewModel mainViewModel)
        {
            InitializeComponent();
            this._mainViewModel = mainViewModel;
            this.BindingContext = _mainViewModel;

            var behavior = new EventToCommandBehavior
            {
                EventName = nameof(ContentPage.Appearing),
                Command = mainViewModel.LoadCommand
            };
            Behaviors.Add(behavior);
        }
        private void OnDetailsTapped(object sender, EventArgs e)
        {
            if ((sender as Image)?.BindingContext is Event selectedEvent)
            {
                var popup = new EditDeletePopup(selectedEvent, _mainViewModel._eventService); 
                
                this.ShowPopupAsync(popup); 
            }
        }

    }
}