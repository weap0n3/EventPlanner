﻿namespace EventPlanner.Gui
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //Application.Current.UserAppTheme = AppTheme.Light;

        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}