﻿using Microsoft.AspNet.SignalR.Client.Hubs;
using Microsoft.Phone.Controls;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using WP8Client.ViewModels;

namespace WP8Client
{
    public partial class MainPage : PhoneApplicationPage
    {
        private IHubProxy chat;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            DataContext = App.ViewModel;
            var hubConnection = new HubConnection("http://192.168.2.128:50188");
            chat = hubConnection.CreateHubProxy("chat");

            chat.On<string>("newMessage", msg => Dispatcher.BeginInvoke(() => App.ViewModel.Items.Add(new ItemViewModel { LineOne = msg })));

            hubConnection.Error += ex => Dispatcher.BeginInvoke(() =>
                {
                    var aggEx = (AggregateException)ex;
                    App.ViewModel.Items.Add(new ItemViewModel { LineOne = aggEx.InnerExceptions[0].Message });
                });

            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            hubConnection.Start().ContinueWith(task =>
                {
                    var ex = task.Exception.InnerExceptions[0];
                    App.ViewModel.Items.Add(new ItemViewModel { LineOne = ex.Message });
                },
                CancellationToken.None,
                TaskContinuationOptions.OnlyOnFaulted,
                scheduler);
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        // Handle selection changed on LongListSelector
        private void MainLongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected item is null (no selection) do nothing
            if (MainLongListSelector.SelectedItem == null)
                return;

            // Navigate to the new page
            NavigationService.Navigate(new Uri("/DetailsPage.xaml?selectedItem=" + (MainLongListSelector.SelectedItem as ItemViewModel).ID, UriKind.Relative));

            // Reset selected item to null (no selection)
            MainLongListSelector.SelectedItem = null;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            chat.Invoke<string>("sendMessage", message.Text);
        }
    }
}