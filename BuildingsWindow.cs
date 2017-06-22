// The Building window, which lets the player build or sell buildings on eligible properties.
using System;
using System.Collections;
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
    public partial class BuildingsWindow : Form
    {
        // Default constructor.
        public BuildingsWindow()
        {
            InitializeComponent();
        }
        // Constructor that lets the client that is opening the window be transferred to it.
        public BuildingsWindow(Client a_client)
        {
            // Initialize the client with the client that opened the window.
            m_client = a_client;
            // Get the names of eligible properties and add them to the box.
            InitializeComponent();
            GetProperties();
        }
        // Populates the propertiesBox ListBox with eligible properties to build on.
        private void GetProperties()
        {
            // Get the client's updated player list.
            List<string> propertyNames = new List<string>();
            propertyNames = m_client.BuildingProperties;
            // Add these names to the ComboBox.
            foreach (string name in propertyNames)
            {
                propertiesBox.Items.Insert(0, name);
            }
        }
        /**/
        /*
        propertiesBox_SelectedIndexChanged()

        NAME

                propertiesBox_SelectedIndexChanged() - gets the building information of the property selected

        SYNOPSIS

                private void propertiesBox_SelectedIndexChanged(object sender, EventArgs e)

        DESCRIPTION

                This function is performed whenever the property selected in the propertiesBox ListBox is
                changed. The client sends a GetBuildingInfo command to the server to get the information
                it needs about the building amount and costs. It also makes sure to enable and disable
                buttons that should or should not be clicked.

        RETURNS

                Nothing!

        AUTHOR

                Bryan Leier

        DATE

                1:38pm 5/31/2017

        */
        /**/
        private void propertiesBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // First, reset the building amount and cost of the current property selected.
            m_client.PropertyBuildingAmount = 0;
            m_client.PropertyBuildingCost = 0;
            // Disable buttons.
            this.buyButton.Enabled = false;
            this.sellButton.Enabled = false;
            // Send a request to get building info.
            m_client.SendCommand("getBuildingInfo", propertiesBox.GetItemText(propertiesBox.SelectedItem));
            // Sleep for a little bit while information is fetched.
            System.Threading.Thread.Sleep(750);
            // If this building amount is less than 5, highlight Buy.
            if (m_client.PropertyBuildingAmount < 5) this.buyButton.Enabled = true;
            // If it is greater than 0, highlight Sell.
            if (m_client.PropertyBuildingAmount > 0) this.sellButton.Enabled = true;
        }
        /**/
        /*
        buyButton_Click()

        NAME

                buyButton_Click() - buys a building when the button is clicked

        SYNOPSIS

                private void buyButton_Click(object sender, EventArgs e)

        DESCRIPTION

                This function is performed whenever the Buy button is clicked on. This means the
                player wants to buy a building on the space. First, it assembles a message saying
                what kind of buildings the property currently has on it along with the price of buying
                a building for it. Then it asks the player if they want to buy a building for this space.
                If they do, it sends a BuyBuilding command to the server and closes the window.

        RETURNS

                Nothing!

        AUTHOR

                Bryan Leier

        DATE

                2:24pm 5/31/2017

        */
        /**/
        private void buyButton_Click(object sender, EventArgs e)
        {
            // Show a message.
            string message = propertiesBox.GetItemText(propertiesBox.SelectedItem) + " currently has ";
            // Change the message depending on the building amount.
            switch (m_client.PropertyBuildingAmount)
            {
                case 0:
                    message = message + "no buildings ";
                    break;
                case 5:
                    message = message + "a hotel ";
                    break;
                default:
                    message = message + m_client.PropertyBuildingAmount.ToString() + " houses ";
                    break;
            }
            message = message + "on it." + Environment.NewLine + "Buying a building for this property costs $" + m_client.PropertyBuildingCost.ToString() + ".";
            message = message + Environment.NewLine + "Would you like to buy a building for this space?";
            DialogResult buyBuildingResult = MessageBox.Show(message, "Buy Building", MessageBoxButtons.YesNo);
            if (buyBuildingResult == DialogResult.Yes)
            {
                // Send a BuyBuilding command.
                m_client.SendCommand("buyBuilding", propertiesBox.GetItemText(propertiesBox.SelectedItem));
                // Update the propertiesWithBuildings hashtable accordingly.
                m_client.PropertiesWithBuildings[propertiesBox.GetItemText(propertiesBox.SelectedItem)] =
                    (int)m_client.PropertiesWithBuildings[propertiesBox.GetItemText(propertiesBox.SelectedItem)] + 1;
                // Close the window.
                this.Close();
            }
        }
        /**/
        /*
        sellButton_Click()

        NAME

                sellButton_Click() - buys a building when the button is clicked

        SYNOPSIS

                private void sellButton_Click(object sender, EventArgs e)

        DESCRIPTION

                This function is performed whenever the Sell button is clicked on. This means the
                player wants to sell a building on the space. First, it assembles a message saying
                what kind of buildings the property currently has on it along with the money that will
                be gained from selling. Then it asks the player if they want to sell a building in this
                space. If they do, it sends a SellBuilding command to the server and closes the window.

        RETURNS

                Nothing!

        AUTHOR

                Bryan Leier

        DATE

                2:24pm 5/31/2017

        */
        /**/
        private void sellButton_Click(object sender, EventArgs e)
        {
            // Show a message.
            string message = propertiesBox.GetItemText(propertiesBox.SelectedItem) + " currently has ";
            // Change the message depending on the building amount.
            switch (m_client.PropertyBuildingAmount)
            {
                case 0:
                    message = message + "no buildings ";
                    break;
                case 1:
                    message = message + m_client.PropertyBuildingAmount.ToString() + " house ";
                    break;
                case 5:
                    message = message + "a hotel ";
                    break;
                default:
                    message = message + m_client.PropertyBuildingAmount.ToString() + " houses ";
                    break;
            }
            message = message + "on it." + Environment.NewLine + "Selling a building this property will give you $" + (m_client.PropertyBuildingCost/2).ToString() + ".";
            message = message + Environment.NewLine + "Would you like to sell a building on this space?";
            DialogResult sellBuildingResult = MessageBox.Show(message, "Sell Building", MessageBoxButtons.YesNo);
            if (sellBuildingResult == DialogResult.Yes)
            {
                // Send a SellBuilding command.
                m_client.SendCommand("sellBuilding", propertiesBox.GetItemText(propertiesBox.SelectedItem));
                // Update the propertiesWithBuildings hashtable accordingly.
                m_client.PropertiesWithBuildings[propertiesBox.GetItemText(propertiesBox.SelectedItem)] =
                    (int)m_client.PropertiesWithBuildings[propertiesBox.GetItemText(propertiesBox.SelectedItem)] - 1;
                // Close the window.
                this.Close();
            }
        }

        // The client who wants to do building actions.
        private Client m_client;
    }
}
