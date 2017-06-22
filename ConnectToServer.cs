// The ConnectToServer window, which lets the client connect to a server to play the game.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonopolyClient
{
    public partial class ConnectToServer : Form
    {
        // Constructor that takes in a client.
        public ConnectToServer(Client a_client)
        {
            InitializeComponent();
            m_client = a_client;
        }
        /**/
        /*
        connectButton_Click()

        NAME

                connectButton_Click() - connects to the server upon clicking the button

        SYNOPSIS

                private void connectButton_Click(object sender, EventArgs e)

        DESCRIPTION

                This function is performed when the Connect button is clicked. It performs
                necessary name checks to make sure the name is "valid" (no commas, actually
                entered a name, less than 21 characters). Then it creates the client and
                attempts to connect to the server with the Client's Connect function. If
                it was a success, it displays a message saying so and opens the main game
                window.

        RETURNS

                Nothing!

        AUTHOR

                Bryan Leier

        DATE

                7:21pm 4/8/2017

        */
        /**/
        private void connectButton_Click(object sender, EventArgs e)
        {
            // Check if the user entered anything in the text box, entered too much, or entered commas.
            if (nameBox.Text.Length == 0)
            {
                MessageBox.Show("Please enter a name to connect to the server with.", "Name");
                return;
            }
            if (nameBox.Text.Length > 21)
            {
                MessageBox.Show("Please enter a name with 20 characters or less.", "Name");
                return;
            }
            if (nameBox.Text.Contains(','))
            {
                MessageBox.Show("Please enter a name without a comma.", "Name");
                return;
            }
            // Create a client.
            m_client.Name = nameBox.Text;
            this.Text = "Connecting...";
            // Connect to the server. Was it a success?
            if (m_client.Connect(ipAddressBox.Text))
            {
                // Show a message upon successful connection.
                this.Text = "Success";
                MessageBox.Show("Connection successful!");
                // Set the client's name to the name they connected with.
                // Open the main window, then close this window.
                this.Hide();
                MainWindow main = new MainWindow(m_client);
                main.ShowDialog();
                this.Close();
            }
            else
            {
                this.Text = "Connect To Server";
                m_client.RefreshSocket();
            }
        }

        // The client.
        private Client m_client;
    }
}
