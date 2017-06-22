// The PropertiesWindow that lets the player see what properties they own.
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
    public partial class PropertiesWindow : Form
    {
        public PropertiesWindow()
        {
            InitializeComponent();
            m_client = new Client();
        }
        // A constructor that initializes the client that opens the window.
        public PropertiesWindow(Client a_client)
        {
            InitializeComponent();
            m_client = a_client;
            // Highlight what the player owns.
            HighlightPropertiesOwned();
        }
        // Highlights the properties that the player owns.
        private void HighlightPropertiesOwned()
        {
            // Get the hashtable of properties owned and the table with building amounts in the client.
            Hashtable list = m_client.PropertyList;
            // This is gonna be a long list... if the property in the table is true, highlight the text.
            if ((bool)list["Mediterranean Avenue"] == true) mediterraneanText.ForeColor = Color.Purple;
            if ((bool)list["Baltic Avenue"] == true) balticText.ForeColor = Color.Purple;
            if ((bool)list["Reading Railroad"] == true) readingText.ForeColor = SystemColors.ControlText;
            if ((bool)list["Oriental Avenue"] == true) orientalText.ForeColor = Color.LightBlue;
            if ((bool)list["Vermont Avenue"] == true) vermontText.ForeColor = Color.LightBlue;
            if ((bool)list["Connecticut Avenue"] == true) connecticutText.ForeColor = Color.LightBlue;
            if ((bool)list["St. Charles Place"] == true) stCharlesText.ForeColor = Color.HotPink;
            if ((bool)list["Electric Company"] == true) electricText.ForeColor = SystemColors.ControlText;
            if ((bool)list["States Avenue"] == true) statesText.ForeColor = Color.HotPink;
            if ((bool)list["Virginia Avenue"] == true) virginiaText.ForeColor = Color.HotPink;
            if ((bool)list["Pennsylvania Railroad"] == true) pennsylvaniaRailroadText.ForeColor = SystemColors.ControlText;
            if ((bool)list["St. James Place"] == true) stJamesText.ForeColor = Color.Orange;
            if ((bool)list["Tennessee Avenue"] == true) tennesseeText.ForeColor = Color.Orange;
            if ((bool)list["New York Avenue"] == true) newYorkText.ForeColor = Color.Orange;
            if ((bool)list["Kentucky Avenue"] == true) kentuckyText.ForeColor = Color.Red;
            if ((bool)list["Indiana Avenue"] == true) indianaText.ForeColor = Color.Red;
            if ((bool)list["Illinois Avenue"] == true) illinoisText.ForeColor = Color.Red;
            if ((bool)list["B. & O. Railroad"] == true) boText.ForeColor = SystemColors.ControlText;
            if ((bool)list["Atlantic Avenue"] == true) atlanticText.ForeColor = Color.Gold;
            if ((bool)list["Ventnor Avenue"] == true) ventnorText.ForeColor = Color.Gold;
            if ((bool)list["Water Works"] == true) waterText.ForeColor = SystemColors.ControlText;
            if ((bool)list["Marvin Gardens"] == true) marvinText.ForeColor = Color.Gold;
            if ((bool)list["Pacific Avenue"] == true) pacificText.ForeColor = Color.ForestGreen;
            if ((bool)list["North Carolina Avenue"] == true) northCarolinaText.ForeColor = Color.ForestGreen;
            if ((bool)list["Pennsylvania Avenue"] == true) pennsylvaniaText.ForeColor = Color.ForestGreen;
            if ((bool)list["Short Line"] == true) shortLineText.ForeColor = SystemColors.ControlText;
            if ((bool)list["Park Place"] == true) parkText.ForeColor = Color.Blue;
            if ((bool)list["Boardwalk"] == true) boardwalkText.ForeColor = Color.Blue;
        }

        // The client.
        private Client m_client;
    }
}
