using CommunityToolkit.Maui.Behaviors;
using EventPlanner.Core.ViewModels;

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
    }
}
