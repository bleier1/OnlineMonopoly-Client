// The MainWindow that the client will primarily play the game in.
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
    public partial class MainWindow : Form
    {
        // The constructor that initializes the client who will be interacting with the main window.
        public MainWindow(Client a_client)
        {
            InitializeComponent();
            // Get the client.
            m_client = a_client;
            // Change the client's main window to this.
            m_client.MainWindow = this;
        }
        // Property to add text to the chat log.
        public string ChatLog
        {
            set
            {
                this.chatLog.AppendText(value);
            }
        }
        // Property to set the first die box.
        public string FirstDieBox
        {
            set
            {
                this.firstDieBox.Text = value;
            }
        }
        // Property to set the second die box.
        public string SecondDieBox
        {
            set
            {
                this.secondDieBox.Text = value;
            }
        }
        // Property to set the Start Game button.
        public bool StartButton
        {
            set
            {
                this.startButton.Enabled = value;
            }
        }
        // Property to set the Roll button.
        public bool RollButton
        {
            set
            {
                this.rollButton.Enabled = value;
            }
        }
        // Property to set the Done button.
        public bool DoneButton
        {
            set
            {
                this.doneButton.Enabled = value;
            }
        }
        // Property to set the Properties button.
        public bool PropertiesButton
        {
            set
            {
                this.propertiesButton.Enabled = value;
            }
        }
        // Property to set the Mortgage button.
        public bool MortgageButton
        {
            set
            {
                this.mortgageButton.Enabled = value;
            }
        }
        // Property to set the Trade button.
        public bool TradeButton
        {
            set
            {
                this.tradeButton.Enabled = value;
            }
        }
        // Property to set the Buildings button.
        public bool BuildingsButton
        {
            set
            {
                this.buildingsButton.Enabled = value;
            }
        }
        // Property to set the Bankruptcy button.
        public bool BankruptcyButton
        {
            set
            {
                this.bankruptcyButton.Enabled = value;
            }
        }
        // Property to update the position label.
        public string PositionList
        {
            set
            {
                this.positionLabel.Text = value;
            }
        }
        // Property to update the funds label.
        public string FundsDisplay
        {
            set
            {
                this.fundsLabel.Text = value;
            }
        }
        // Sends a chat message whenever the enter key is hit in the chatInput TextBox.
        private void chatInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Get the message to send, then clear the input box.
                string chatMessage = m_client.Name + ": " + chatInput.Text;
                chatInput.Text = "";
                // Send this message to all the connected players in the chat log.
                m_client.SendCommand("chat", chatMessage);
            }
        }
        // Sends a Roll command to the server when the Roll button is clicked.
        private void rollButton_Click(object sender, EventArgs e)
        {
            // Send a Roll command to the server.
            m_client.SendCommand("roll", "");
        }
        // Sends a Start command to the server when the Start Game button is clicked.
        private void startButton_Click(object sender, EventArgs e)
        {
            // Send a Start command to the server.
            m_client.SendCommand("start", "");
        }
        // Sends a Done command to the server when the Done button is clicked.
        private void doneButton_Click(object sender, EventArgs e)
        {
            // It is no longer this client's turn.
            m_client.IsOurTurn = false;
            // Nullify buttons.
            this.doneButton.Enabled = false;
            this.tradeButton.Enabled = false;
            this.buildingsButton.Enabled = false;
            // Send a Done command to the server.
            m_client.SendCommand("done", "");
        }
        // Opens the PropertiesWindow when the Properties button is clicked.
        private void propertiesButton_Click(object sender, EventArgs e)
        {
            // Open the properties window.
            PropertiesWindow propertyList = new PropertiesWindow(m_client);
            // Invoke the window so we can still receive main window updates without crashing.
            this.Invoke(new Action(() =>
            {
                propertyList.ShowDialog();
            }));
        }
        // Opens the MortgageWindow when the Mortgage button is clicked.
        private void mortgageButton_Click(object sender, EventArgs e)
        {
            // Open the mortgage window.
            MortgageWindow mortgageWindow = new MortgageWindow(m_client);
            // Invoke the window so we can still receive main window updates without crashing.
            this.Invoke(new Action(() =>
            {
                mortgageWindow.ShowDialog();
            }));
        }
        // Opens the TradeWindow when the Trade button is clicked.
        private void tradeButton_Click(object sender, EventArgs e)
        {
            // First, request an updated list of current players.
            m_client.SendCommand("getNames", "");
            // Open the trade window.
            TradeWindow tradeWindow = new TradeWindow(m_client);
            // Invoke the window so we can still receive main window updates without crashing.
            this.Invoke(new Action(() =>
            {
                tradeWindow.ShowDialog();
            }));
        }
        // Opens the BuildingsWindow when the Buildings button is clicked.
        private void buildingsButton_Click(object sender, EventArgs e)
        {
            // First, request an updated list of buildings that can have building actions performed
            // on them.
            m_client.SendCommand("getBuildingProperties", "");
            // Sleep for a little.
            System.Threading.Thread.Sleep(700);
            // Open the buildings window.
            BuildingsWindow buildingsWindow = new BuildingsWindow(m_client);
            // Invoke the window so we can still receive main window updates without crashing.
            this.Invoke(new Action(() =>
            {
                buildingsWindow.ShowDialog();
            }));
        }
        /**/
        /*
        bankruptcyButton_Click()

        NAME

                bankruptcyButton_Click() - performs actions upon hitting the Bankruptcy button

        SYNOPSIS

                private void bankruptcyButton_Click(object sender, EventArgs e)

        DESCRIPTION

                This function is performed whenever the Bankruptcy button is clicked on. It fetches the
                funds the player currently has through looking at the fundUpdate label, then makes a decision
                based on that: if their current Funds are greater than 0, the player is no longer bankrupt
                and will have buttons changed accordingly. Otherwise, they are still bankrupt and will be
                asked if they are sure if they want to declare bankruptcy. If so, a Bankrupt command is sent
                to the Server and all buttons are disabled and their funds are changed to bankrupt. They are
                now out of the game.

        RETURNS

                Nothing!

        AUTHOR

                Bryan Leier

        DATE

                10:16pm 5/31/2017

        */
        /**/
        private void bankruptcyButton_Click(object sender, EventArgs e)
        {
            // When clicking this button, there can only be two outcomes: the player getting back in after selling
            // enough, or the player declaring bankruptcy. We can determine what their intent is by looking at the
            // funds they have.
            // Look at the funds this player has through the fundsLabel.
            string[] labelText = fundsLabel.Text.Split('$');
            // Convert to an integer.
            int currentFunds = Int32.Parse(labelText[1]);
            if (currentFunds > 0)
            {
                // The player has escaped bankruptcy! Enable buttons once more.
                // Make buttons clickable.
                if (m_client.IsOurTurn)
                {
                    // Can we still roll?
                    if (m_client.CanStillRoll)
                    {
                        // Make the roll button clickable.
                        this.rollButton.Enabled = true;
                    }
                    // Otherwise, make the done button clickable.
                    else this.doneButton.Enabled = true;
                }
                // Otherwise, just make the Buildings button unclickable.
                else
                {
                    this.buildingsButton.Enabled = false;
                }
                // Don't forget to disable the Bankruptcy button.
                this.bankruptcyButton.Enabled = false;
            }
            else
            {
                // The player is either knowingly declaring bankruptcy or does not know they are. Make sure
                // they know what they're doing.
                this.Invoke(new Action(() =>
                {
                    DialogResult bankruptcyResult = MessageBox.Show("You still have negative funds. Do you want to declare bankruptcy?", "Still Bankrupt",
                        MessageBoxButtons.YesNo);
                    if (bankruptcyResult == DialogResult.Yes)
                    {
                        // The player is declaring bankruptcy. Send a Bankrupt command to the server. This player is now out of the game.
                        m_client.SendCommand("bankrupt", "");
                        // Also, null all of the buttons.
                        this.rollButton.Enabled = false;
                        this.propertiesButton.Enabled = false;
                        this.mortgageButton.Enabled = false;
                        this.tradeButton.Enabled = false;
                        this.buildingsButton.Enabled = false;
                        this.doneButton.Enabled = false;
                        this.bankruptcyButton.Enabled = false;
                        // Change the label with funds.
                        fundsLabel.Text = "Bankrupt!";
                    }
                }));
            }
        }

        // The client.
        private Client m_client;
    }
}
