using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Views;
using EventPlanner.Core.ViewModels;
using EventPlanner.Gui.Popups;

namespace EventPlanner.Gui
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel mainViewModel)
        {
            InitializeComponent();
            this.BindingContext = mainViewModel;

            var behavior = new EventToCommandBehavior
            {
                EventName = nameof(ContentPage.Appearing),
                Command = mainViewModel.LoadCommand
            };
            Behaviors.Add(behavior);
        }
        private void OnDetailsTapped(object sender, EventArgs e)
        {
            DisplayPopup();
        }

        public void DisplayPopup()
        {
            var popup = new EditDeletePopup();

            this.ShowPopup(popup);
        }
    }
}