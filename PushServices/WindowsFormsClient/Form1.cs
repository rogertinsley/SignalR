using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.AspNet.SignalR.Client.Hubs;

namespace WindowsFormsClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private readonly BindingSource bindingSource1 = new BindingSource();
        private IHubProxy hubProxy;

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = bindingSource1;

            var hubConnection = new HubConnection("http://localhost:8080");
            hubProxy = hubConnection.CreateHubProxy("monitor");

            hubProxy.On<IEnumerable<Artist>>("refresh", action => GetData());

            hubConnection.Start().Wait();
            GetData();
        }

        private void GetData()
        {
            try
            {
                const string query = "SELECT Artists.ArtistId, Artists.Name FROM Artists";
                string connectionString = ConfigurationManager.ConnectionStrings["winformConnectionstring"].ConnectionString;

                // Create a new data adapter based on the specified query.
                using (var dataAdapter = new SqlDataAdapter(query, connectionString))
                {
                    // Populate a new data table and bind it to the BindingSou rce.
                    using (var dataTable = new DataTable {Locale = System.Globalization.CultureInfo.InvariantCulture})
                    {
                        dataAdapter.Fill(dataTable);
                        bindingSource1.DataSource = dataTable;

                        // Resize the DataGridView columns to fit the newly loaded content.
                        dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
        }
    }
}
