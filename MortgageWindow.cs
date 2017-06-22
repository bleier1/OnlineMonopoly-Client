// The MortgageWindow that allows the player to mortgage and unmortgage properties.
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
    public partial class MortgageWindow : Form
    {
        // Default constructor.
        public MortgageWindow()
        {
            InitializeComponent();
        }
        // Constructor that initializes a client.
        public MortgageWindow(Client a_client)
        {
            m_client = a_client;
            // Populate the list boxes with properties.
            InitializeComponent();
            ListUnmortgagedProperties();
            ListMortgagedProperties();
        }
        // Lists unmortgaged properties in the unmortgagedProperties ListBox.
        private void ListUnmortgagedProperties()
        {
            // Get the client's property lists.
            Hashtable propertyList = m_client.PropertyList;
            Hashtable buildingsList = m_client.PropertiesWithBuildings;
            // Cycle through.
            foreach (DictionaryEntry de in propertyList)
            {
                // Is this propery owned by the player, and does it have no buildings on it?
                if ((bool)de.Value == true && (int)buildingsList[de.Key] == 0)
                {
                    // Add it to the list.
                    unmortgagedProperties.Items.Add(de.Key);
                }
            }
        }
        // Lists mortgaged properties in the mortgagedProperties ListBox.
        private void ListMortgagedProperties()
        {
            // Get the client's mortgaged property list.
            List<string> mortgagedList = m_client.MortgagedPropeties;
            // Cycle through.
            foreach (string property in mortgagedList)
            {
                // Add it to the list.
                mortgagedProperties.Items.Add(property);
            }
        }
        /**/
        /*
        mortgageButton_Click()

        NAME

                mortgageButton_Click() - performs actions upon hitting the Mortgage button

        SYNOPSIS

                private void mortgageButton_Click(object sender, EventArgs e)

        DESCRIPTION

                This function is performed whenever the Mortgage button is clicked on. First, it
                makes sure a property in the unmortgagedProperties ListBox is selected before doing
                anything. Otherwise, it will mortgage the property and send a Mortgage command to
                the server with the property's name as the message, then removes it from the property
                list and add it to the unmortgagedProperties list.

        RETURNS

                Nothing!

        AUTHOR

                Bryan Leier

        DATE

                4:13pm 5/6/2017

        */
        /**/
        private void mortgageButton_Click(object sender, EventArgs e)
        {
            // Make sure a property is selected before doing anything.
            if (unmortgagedProperties.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a property to mortgage.", "Property Not Selected");
                return;
            }
            // Otherwise, okay! Go ahead and do it.
            string propertyName = unmortgagedProperties.SelectedItem.ToString();
            m_client.SendCommand("mortgage", propertyName);
            // Remove the property from the client's property list and add it to the mortgage property list.
            Hashtable newPropList = m_client.PropertyList;
            newPropList[propertyName] = false;
            List<string> newMortgageList = m_client.MortgagedPropeties;
            newMortgageList.Add(propertyName);
            m_client.PropertyList = newPropList;
            m_client.MortgagedPropeties = newMortgageList;
            // Close the window.
            this.Close();
        }
        // Unmortgages a property upon clicking the Unmortgage Button, but only if a property is selected.
        private void unmortgageButton_Click(object sender, EventArgs e)
        {
            // Make sure a property is selected before doing anything.
            if (mortgagedProperties.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a property to unmortgage.", "Property Not Selected");
                return;
            }
            // Otherwise, it's alright!
            m_client.SendCommand("unmortgage", mortgagedProperties.SelectedItem.ToString());
            // We can't do anything further here until we get the OK from the server.
            // Close the window.
            this.Close();
        }

        // The client.
        private Client m_client;
    }
}
