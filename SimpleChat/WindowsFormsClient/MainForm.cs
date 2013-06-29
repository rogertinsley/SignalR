using System;
using System.Windows.Forms;
using Microsoft.AspNet.SignalR.Client.Hubs;

namespace WindowsFormsClient
{
    public partial class MainForm : Form
    {
        private IHubProxy chat;

        public MainForm()
        {
            InitializeComponent();
        }

        private void bnConnect_Click(object sender, EventArgs e)
        {
            var hubConnection = new HubConnection("http://localhost:50188");
            chat = hubConnection.CreateHubProxy("chat");
            chat.On<string>("newMessage", msg => messages.Invoke(new Action(() => messages.Items.Add(msg))));
            hubConnection.Start().Wait();
        }

        private void bnSend_Click(object sender, EventArgs e)
        {
            chat.Invoke<string>("sendMessage", tbMessage.Text);
        }
    }
}
