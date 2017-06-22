// The Client class, which is how the player connects to the game's server and plays the game.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Text.RegularExpressions;

namespace MonopolyClient
{
    public class Client
    {
    	// Default constructor.
    	public Client()
    	{
    		// Initialize the client's socket.
    		m_clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    		// Initialize the property lists.
    		InitializePropertyList();
    		m_mortgagedProperties = new List<string>();
    		m_requestedPlayerProperties = new List<string>();
            m_buildingProperties = new List<string>();
    		// Initialize the list of non-bankrupt players in the game.
    		m_playerList = new List<string>();
            // Initialize funds amounts.
            m_funds = 0;
            m_requestedPlayerFunds = 0;
            // Initialize property building counts.
            m_propertyBuildingAmount = 0;
            m_propertyBuildingCost = 0;

        }
        // Constructor that sets the client's name.
        public Client(string a_name)
        {
            // Initialize the client's socket.
            m_clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // Initialize the property list.
    		InitializePropertyList();
    		m_mortgagedProperties = new List<string>();
    		m_requestedPlayerProperties = new List<string>();
            m_buildingProperties = new List<string>();
            // Initialize the list of non-bankrupt players in the game.
            m_playerList = new List<string>();
            // Initialize funds amounts.
            m_funds = 0;
            m_requestedPlayerFunds = 0;
            // Initialize property building counts.
            m_propertyBuildingAmount = 0;
            m_propertyBuildingCost = 0;
        }
        // Property to get and set the client's name.
        public string Name
        {
            get
            {
                return m_name;
            }
            set
            {
                m_name = value;
            }
        }
        // Property to get the client's socket.
        public Socket Socket
        {
        	get
        	{
        		return m_clientSocket;
        	}
        }
        // Properties to get and set the client's turn state.
        public bool IsOurTurn
        {
            get
            {
                return m_isOurTurn;
            }
        	set
        	{
        		m_isOurTurn = value;
        	}
        }
        // Property to get the state of whether or not the client can still roll.
        public bool CanStillRoll
        {
            get
            {
                return m_canStillRoll;
            }
        }
        // Property to get the property list.
        public Hashtable PropertyList
        {
        	get
        	{
        		return m_propertiesList;
        	}
        	set
        	{
        		m_propertiesList = value;
        	}
        }
        // Property to get the property list with the amount of buildings they have.
        public Hashtable PropertiesWithBuildings
        {
            get
            {
                return m_propertiesWithBuildings;
            }
            set
            {
                m_propertiesWithBuildings = value;
            }
        }
        // Property to get the mortgaged property list.
        public List<string> MortgagedPropeties
        {
        	get
        	{
        		return m_mortgagedProperties;
        	}
        	set
        	{
        		m_mortgagedProperties = value;
        	}
        }
        // Property to get the names of players.
        public List<string> PlayerNames
        {
        	get
        	{
        		return m_playerList;
        	}
        }
        // Property to get another player's properties in a trade.
        public List<string> RequestedPlayerProperties
        {
        	get
        	{
        		return m_requestedPlayerProperties;
        	}
        }
        // Property to get this player's funds.
        public int Funds
        {
            get
            {
                return m_funds;
            }
        }
        // Property to get a requested player's funds.
        public int RequestedPlayerFunds
        {
            get
            {
                return m_requestedPlayerFunds;
            }
        }
        // Property to get eligible properties for building actions.
        public List<string> BuildingProperties
        {
            get
            {
                return m_buildingProperties;
            }
        }
        // Property to get and set a property's building amount.
        public int PropertyBuildingAmount
        {
            get
            {
                return m_propertyBuildingAmount;
            }
            set
            {
                m_propertyBuildingAmount = value;
            }
        }
        // Property to get and set a property's building cost.
        public int PropertyBuildingCost
        {
            get
            {
                return m_propertyBuildingCost;
            }
            set
            {
                m_propertyBuildingCost = value;
            }
        }
        // Property to get the main window.
        public MainWindow MainWindow
        {
            set
            {
                m_mainWindow = value;
            }
        }
        /**/
        /*
        Connect()

        NAME

                Connect() - lets the client connect to the server

        SYNOPSIS

                public bool Connect(string a_ipAddress)

                a_ipAddress -> the IP address of the server to connect to

        DESCRIPTION

                This function is performed when a client tries connecting to a game server.
                First, it tries connect to the IP address. If it cannot, it shows a message
                saying so and returns false. Otherwise, the connection was a success and it
                sends a Join command to the server. It receives the Join response from the server
                and converts it into a PlayerCommand struct. If the command is Yes, it calls
                BeginReceive and returns true. Otherwise, it returns false after showing a
                MessageBox that shows why the connection could not be made.

        RETURNS

                A boolean that determines whether or not the connection was a success.

        AUTHOR

                Bryan Leier

        DATE

                5:14pm 4/8/2017

        */
        /**/
        public bool Connect(string a_ipAddress)
    	{
            // Connect to the server.
    		try
	    	{
	    		m_clientSocket.Connect(IPAddress.Parse(a_ipAddress), 100);
	    	}
	    	catch (SocketException)
            {
                MessageBox.Show("Failed connecting to the server.", "Connection Failure");
                return false;
    		}
    		// A connection was made! Send a join command to the server.
    		SendCommand("join", "");
    		//Receive the response from the server on whether or not connecting was a success.
    		int joinBytes = m_clientSocket.Receive(m_buffer, 0, m_buffer.Length, SocketFlags.None);
    		byte[] joinBuffer = new byte[joinBytes];
    		// Transfer data, get the response from the server.
    		Array.Copy(m_buffer, joinBuffer, joinBytes);
    		PlayerCommand response = GetStruct(joinBuffer);
    		if (response.Command == Command.Yes)
    		{
    			// We are connected! Start receiving and return true.
    			m_clientSocket.BeginReceive(m_buffer, 0, m_buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), m_clientSocket);
    			return true;
    		}
    		// Otherwise, we weren't so lucky.
    		MessageBox.Show(response.Message, "Unable to Connect");
    		return false;
    	}
        /**/
        /*
        SendCommand()

        NAME

                SendCommand() - lets the client connect to the server

        SYNOPSIS

                public void SendCommand(string a_command, string a_message)

                a_command -> the command to send to the server
                a_message -> the message that may be sent to the server, depending on the command

        DESCRIPTION

                This function sends a command that the client wants to send to the server. It sets
                up a PlayerCommand, then uses a switch statement to look at the string command that was
                passed into the function. It performs the appropriate actions for each command and replaces
                the blank message that was initialized in the PlayerCommand if necessary. Then it sends the
                command to the server.

        RETURNS

                Nothing!

        AUTHOR

                Bryan Leier

        DATE

                6:56pm 4/9/2017

        */
        /**/
    	public void SendCommand(string a_command, string a_message)
    	{
    		// Set up a new player command.
    		PlayerCommand command = new PlayerCommand();
    		command.Name = m_name;
            command.Message = "";
    		// Set up a switch statement.
    		switch(a_command)
    		{
    			case "roll":
    				command.Command = Command.Roll;
    				break;
    			case "chat":
    				command.Command = Command.Chat;
    				command.Message = a_message;
    				break;
    			case "join":
    				command.Command = Command.Join;
                    break;
                case "start":
                	command.Command = Command.Start;
                    break;
                case "done":
                    command.Command = Command.Done;
                    break;
                case "fundUpdate":
                	command.Command = Command.FundUpdate;
                    break;
                case "buyProperty":
                	command.Command = Command.BuyProperty;
                    break;
                case "mortgage":
                	command.Command = Command.Mortgage;
                	command.Message = a_message;
                	break;
                case "unmortgage":
                	command.Command = Command.Unmortgage;
                	command.Message = a_message;
                	break;
                case "jail":
                	command.Command = Command.Jail;
                	break;
                case "card":
                	command.Command = Command.Card;
                	command.Message = a_message;
                    break;
                case "getNames":
                	command.Command = Command.GetNames;
                	break;
                case "getProperties":
                	command.Command = Command.GetProperties;
                	command.Message = a_message;
                    break;
                case "getFunds":
                    command.Command = Command.GetFunds;
                    command.Message = a_message;
                    break;
                case "tradeRequest":
                    command.Command = Command.TradeRequest;
                    command.Message = a_message;
                    break;
                case "acceptTrade":
                    command.Command = Command.AcceptTrade;
                    command.Message = a_message;
                    break;
                case "declineTrade":
                    command.Command = Command.DeclineTrade;
                    command.Message = a_message;
                    break;
                case "getBuildingProperties":
                    command.Command = Command.GetBuildingProperties;
                    break;
                case "getBuildingInfo":
                    command.Command = Command.GetBuildingInfo;
                    command.Message = a_message;
                    break;
                case "buyBuilding":
                    command.Command = Command.BuyBuilding;
                    command.Message = a_message;
                    break;
                case "sellBuilding":
                    command.Command = Command.SellBuilding;
                    command.Message = a_message;
                    break;
                case "bankrupt":
                    command.Command = Command.Bankrupt;
                    break;
    			default:
    				command.Command = Command.None;
    				break;
    		}
    		// Convert the playerCommand struct to a byte array.
    		byte[] dataToSend = GetBytes(command);
    		// Send it over.
    		try
    		{
    			m_clientSocket.Send(dataToSend);
    		}
    		catch(SocketException)
    		{
    			MessageBox.Show("Connection to server lost.", "Connection Lost");
    		}
    	}
        // "Refreshes" the socket in case of a disconnect while trying to connect to the server.
        public void RefreshSocket()
        {
            // Make a new socket.
            m_clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        // Command enumeration. These Commands are used to communicate what actions should be performed between
        // the clients and the server. The Commands serve the following functions:
        // Yes: Indicates a success
        // No: Indicates a failure
        // Join: Join the game
        // Roll: Roll the dice
        // Chat: Write a message to the chat
        // Done: Finish a turn
        // Start: Starts the game
        // NoStart: Indicates a game cannot be started yet
        // TurnStart: Indicates the start of a turn
        // PositionUpdate: Updates player's positions on the board
        // FundUpdate: Updates the amount of funds a player has
        // ShowMessageBox: Shows a message to the client in a MessageBox
        // RentBox: Shows a message about rent to a client
        // BuyProperty: Buys a property
        // Mortgage: Mortgages a property
        // Unmortgage: Unmortgages a property
        // Tax: Indicates tax must be paid
        // Jail: Sends someone to jail
        // Card: Sends information about a card
        // GetNames: Gets the names of players in the game
        // GetProperties: Gets a list of properties
        // GetFunds: Gets funds for players
        // TradeRequest: Sends a trade request
        // AcceptTrade: Accepts a trade
        // DeclineTrade: Declines a trade
        // GetBuildingProperties: Gets properties to build on
        // GetBuildngInfo: Gets information about building on a property (cost, amount of buildings)
        // BuyBuilding: Buys a building
        // SellBuilding: Sells a building
        // BankruptWarning: Warns a player that they are bankrupt
        // Bankrupt: Bankrupts a player
        // Winner: Indicates who won the game
        // None: Default command
        public enum Command
        {
            Yes, No, Join, Roll, Chat, Done, Start, NoStart, TurnStart, PositionUpdate, FundUpdate, ShowMessageBox,
            RentBox, BuyProperty, Mortgage, Unmortgage, Tax, Jail, Card, GetNames, GetProperties, GetFunds,
            TradeRequest, AcceptTrade, DeclineTrade, GetBuildingProperties, GetBuildingInfo, BuyBuilding, 
            SellBuilding, BankruptWarning, Bankrupt, Winner, None
        }
        // The PlayerCommand structure that is sent between the client and servers as an easy method of communication.
        // It contains a Name, a Command, and a Message. It uses Marshaling to properly allocate data for ease of
        // byte conversion later.
        public struct PlayerCommand
        {
            public string Name
            {
                get
                {
                    return s_name;
                }
                set
                {
                    s_name = value;
                }
            }
            public Command Command
            {
                get
                {
                    return s_command;
                }
                set
                {
                    s_command = value;
                }
            }
            public string Message
            {
                get
                {
                    return s_message;
                }
                set
                {
                    s_message = value;
                }
            }
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 25)]
            private string s_name;
            private Command s_command;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 510)]
            private string s_message;
        }
        /**/
        /*
        GetBytes()

        NAME

                GetBytes() - converts a PlayerCommand struct into a byte array

        SYNOPSIS

                private byte[] GetBytes(PlayerCommand a_command)

        DESCRIPTION

                This function uses marshaling to convert a PlayerCommand structure to a byte
                array.

        RETURNS

                A byte array containing the structure that it converted.

        AUTHOR

                Vincent McNabb

        DATE

                6:21pm 4/13/2017

        */
        /**/
        private byte[] GetBytes(PlayerCommand a_command)
        {
            // Get the size of the struct.
            int size = Marshal.SizeOf(a_command);
            byte[] data = new byte[size];

            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(a_command, ptr, false);
            Marshal.Copy(ptr, data, 0, size);
            Marshal.FreeHGlobal(ptr);
            return data;
        }
        /**/
        /*
        GetStruct()

        NAME

                GetStruct() - converts a byte array into a struct

        SYNOPSIS

                private PlayerCommand GetStruct(byte[] a_bytes)

        DESCRIPTION

                This function uses marshaling to convert a byte array into a PlayerCommand
                struct.

        RETURNS

                A PlayerCommand structure containing data from the byte array it converted.

        AUTHOR

                Vincent McNabb

        DATE

                6:24pm 4/13/2017

        */
        /**/
        private PlayerCommand GetStruct(byte[] a_bytes)
        {
            PlayerCommand structure = new PlayerCommand();

            int size = Marshal.SizeOf(structure);
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.Copy(a_bytes, 0, ptr, size);
            structure = (PlayerCommand)Marshal.PtrToStructure(ptr, structure.GetType());
            Marshal.FreeHGlobal(ptr);

            return structure;
        }
        // Initializes the list of properties in the propertiesList hashtable.
        private void InitializePropertyList()
        {
            m_propertiesList = new Hashtable();
            m_propertiesWithBuildings = new Hashtable();
            // Add all the properties to the properiesList hashtable, with a boolean that determines whether or not the player owns it.
            m_propertiesList.Add("Mediterranean Avenue", false);
            m_propertiesList.Add("Baltic Avenue", false);
            m_propertiesList.Add("Reading Railroad", false);
            m_propertiesList.Add("Oriental Avenue", false);
            m_propertiesList.Add("Vermont Avenue", false);
            m_propertiesList.Add("Connecticut Avenue", false);
            m_propertiesList.Add("St. Charles Place", false);
            m_propertiesList.Add("Electric Company", false);
            m_propertiesList.Add("States Avenue", false);
            m_propertiesList.Add("Virginia Avenue", false);
            m_propertiesList.Add("Pennsylvania Railroad", false);
            m_propertiesList.Add("St. James Place", false);
            m_propertiesList.Add("Tennessee Avenue", false);
            m_propertiesList.Add("New York Avenue", false);
            m_propertiesList.Add("Kentucky Avenue", false);
            m_propertiesList.Add("Indiana Avenue", false);
            m_propertiesList.Add("Illinois Avenue", false);
            m_propertiesList.Add("B. & O. Railroad", false);
            m_propertiesList.Add("Atlantic Avenue", false);
            m_propertiesList.Add("Ventnor Avenue", false);
            m_propertiesList.Add("Water Works", false);
            m_propertiesList.Add("Marvin Gardens", false);
            m_propertiesList.Add("Pacific Avenue", false);
            m_propertiesList.Add("North Carolina Avenue", false);
            m_propertiesList.Add("Pennsylvania Avenue", false);
            m_propertiesList.Add("Short Line", false);
            m_propertiesList.Add("Park Place", false);
            m_propertiesList.Add("Boardwalk", false);
            // Do the same for the propertiesWithBuildings hashtable, but with integers.
            m_propertiesWithBuildings.Add("Mediterranean Avenue", 0);
            m_propertiesWithBuildings.Add("Baltic Avenue", 0);
            m_propertiesWithBuildings.Add("Reading Railroad", 0);
            m_propertiesWithBuildings.Add("Oriental Avenue", 0);
            m_propertiesWithBuildings.Add("Vermont Avenue", 0);
            m_propertiesWithBuildings.Add("Connecticut Avenue", 0);
            m_propertiesWithBuildings.Add("St. Charles Place", 0);
            m_propertiesWithBuildings.Add("Electric Company", 0);
            m_propertiesWithBuildings.Add("States Avenue", 0);
            m_propertiesWithBuildings.Add("Virginia Avenue", 0);
            m_propertiesWithBuildings.Add("Pennsylvania Railroad", 0);
            m_propertiesWithBuildings.Add("St. James Place", 0);
            m_propertiesWithBuildings.Add("Tennessee Avenue", 0);
            m_propertiesWithBuildings.Add("New York Avenue", 0);
            m_propertiesWithBuildings.Add("Kentucky Avenue", 0);
            m_propertiesWithBuildings.Add("Indiana Avenue", 0);
            m_propertiesWithBuildings.Add("Illinois Avenue", 0);
            m_propertiesWithBuildings.Add("B. & O. Railroad", 0);
            m_propertiesWithBuildings.Add("Atlantic Avenue", 0);
            m_propertiesWithBuildings.Add("Ventnor Avenue", 0);
            m_propertiesWithBuildings.Add("Water Works", 0);
            m_propertiesWithBuildings.Add("Marvin Gardens", 0);
            m_propertiesWithBuildings.Add("Pacific Avenue", 0);
            m_propertiesWithBuildings.Add("North Carolina Avenue", 0);
            m_propertiesWithBuildings.Add("Pennsylvania Avenue", 0);
            m_propertiesWithBuildings.Add("Short Line", 0);
            m_propertiesWithBuildings.Add("Park Place", 0);
            m_propertiesWithBuildings.Add("Boardwalk", 0);
        }
        /**/
        /*
        ReceiveCallback()

        NAME

                ReceiveCallback() - receives information sent from the server

        SYNOPSIS

                private void ReceiveCallback(IAsyncResult AR)

                AR -> the status of an asynchronous operation

        DESCRIPTION

                This function receives an array of bytes sent by the server. It transforms the
                bytes received into the PlayerCommand structure using GetStruct and proceeds to
                perform actions based on the Command that was sent through the structure. This is
                achieved through another large switch statement. After performing the appropriate
                actions, the client will start accepting information from the server once more.

        RETURNS

                Nothing!

        AUTHOR

                Bryan Leier

        DATE

                6:34pm 4/8/2017

        */
        /**/
    	private void ReceiveCallback(IAsyncResult AR)
    	{
    		// Get the socket sending info.
    		Socket socket = (Socket)AR.AsyncState;
    		try
    		{
    			int bytesReceived = socket.EndReceive(AR);
    			byte[] receivedBuffer = new byte[bytesReceived];
    			// Copy data over.
    			Array.Copy(m_buffer, receivedBuffer, bytesReceived);
    			// Convert the receivedBuffer to a PlayerCommand struct.
	    		PlayerCommand response = GetStruct(receivedBuffer);
	    		// There's a lot of these switch statements in my program... necessary, though.
	    		switch(response.Command)
	    		{
	    			case Command.Chat:
                        // Get the message, then display it in the text box.
                        m_mainWindow.ChatLog = response.Message + Environment.NewLine;
                        break;
                    case Command.Join:
                    	// This will only be returned by the server if the client is unable to connect.
                    	// Disconnect.
                    	m_clientSocket.Disconnect(true);
                        break;
                    case Command.Roll:
                    	// Get the die rolls and display them.
                    	DisplayRolls(response.Message);
                        break;
                    case Command.Start:
                    	// Turn off the Start Game button.
                    	m_mainWindow.StartButton = false;
                    	// Turn on the Properties button.
                    	m_mainWindow.PropertiesButton = true;
                    	// Turn on the Mortgage button.
                    	m_mainWindow.MortgageButton = true;
                        break;
                    case Command.NoStart:
                    	// Display that there needs to be more than one player to start.
                    	MessageBox.Show("There must be more than one player connected to start the game.", "Unable to Start");
                    	break;
                    case Command.TurnStart:
                    	// Start this player's turn by making the appropriate buttons visible. Also make the boolean determining
                    	// whether or not this is the player's turn true.
                    	m_mainWindow.RollButton = true;
                        m_mainWindow.TradeButton = true;
                        m_mainWindow.BuildingsButton = true;
                    	m_isOurTurn = true;
                    	m_canStillRoll = true;
                    	break;
                    case Command.PositionUpdate:
                    	// Update the position list text.
                    	m_mainWindow.PositionList = response.Message;
                        break;
                    case Command.FundUpdate:
                    	// Update funds text.
                    	string newFunds = "Funds: $" + response.Message;
                    	m_mainWindow.FundsDisplay = newFunds;
                        break;
                    case Command.ShowMessageBox:
                    	// Show what was received in a message box.
                    	MessageBox.Show(response.Message, response.Name);
                        break;
                    case Command.RentBox:
                    	// Display a dialog box in regards to a rent being paid.
                    	ShowRentDialog(response.Message, response.Name);
                        SendCommand("fundUpdate", "");
                        break;
                    case Command.BuyProperty:
                    	// Ask if the player wants to buy a property.
                    	ShowBuyPropertyDialog(response.Message, response.Name);
                    	break;
                    case Command.Mortgage:
                        // This is only received if the property is unable to be mortgaged.
                        m_mainWindow.Invoke(new Action(() =>
                        {
                            MessageBox.Show("You are not able to mortgage this property.", "Cannot Mortgage");
                            // Undo everything the mortgage window did.
                            m_propertiesList[response.Message] = true;
                            m_mortgagedProperties.Remove(response.Message);
                        }));
                        break;
                    case Command.Unmortgage:
                    	// Look at the name, as this determines whether or not the unmortgaging was a success.
                    	if (response.Name == "yes")
                    	{
                    		// Remove the property from the mortgage list, add it to the property list, and request a fund update.
                    		m_mortgagedProperties.Remove(response.Message);
                    		m_propertiesList[response.Message] = true;
                    		SendCommand("fundUpdate", "");
                    	}
                    	else
                    	{
                    		// Show that there is not enough funds to unmortgage the property.
                    		m_mainWindow.Invoke(new Action(() =>
                    		{
                    			MessageBox.Show("You do not have enough money to unmortgage that property.", "Not Enough Money");
                    		}));
                    	}
                        break;
                    case Command.Tax:
                    	// A tax needed to be paid.
                    	string taxMessage;
                    	// If it was an income tax, display as such. Otherwise, display the luxury tax.s
                    	if (response.Name == "income tax")
                    	{
                    		taxMessage = "Pay an Income Tax of $200.";
                    	}
                    	else taxMessage = "Pay a Luxury Tax of $75.";
                    	m_mainWindow.Invoke(new Action(() =>
                    	{
                    		MessageBox.Show(taxMessage, "Pay Tax");
                    	}));
                    	// Request a fund update to get the proper remaining amount of funds the player has left.
                    	SendCommand("fundUpdate", "");
                        break;
                    case Command.Jail:
                    	// The player was sent to jail.
                    	m_mainWindow.Invoke(new Action(() =>
                    	{
                    		MessageBox.Show("Go directly to Jail! Do not pass Go. Do not collect $200.", "Jail");
                    	}));
                    	// You can only end your turn now.
                    	m_mainWindow.DoneButton = true;
                    	m_mainWindow.RollButton = false;
                    	// Send a command back for a position update.
                    	SendCommand("jail", "");
                    	break;
                    case Command.Card:
                        // Show the info about the card just drawn.
                        m_mainWindow.Invoke(new Action(() =>
                        {
                    		MessageBox.Show(response.Message, "Card Drawn");
                            // Send necessary info back to the server to perform the action associated with the card.
                            SendCommand("card", response.Name);
                    	}));
						break;
					case Command.GetNames:
						// Update the names list.
						UpdatePlayerList(response.Message);
                        break;
                    case Command.GetProperties:
                    	// Add the properties received to the request player properties list.
                    	AddRequestedProperties(response.Message);
                        break;
                    case Command.GetFunds:
                        // Separate the funds appropriately.
                        ParsePlayerFunds(response.Message);
                        break;
                    case Command.TradeRequest:
                        // Read the trade request.
                        ReadTradeRequest(response.Name, response.Message);
                        break;
                    case Command.AcceptTrade:
                        // Perform actions upon an accepted trade.
                        AcceptTradeActions(response.Message);
                        break;
                    case Command.DeclineTrade:
                        // Indicate there was a decline.
                        m_mainWindow.Invoke(new Action(() =>
                        {
                            MessageBox.Show("The other player declined your offer.", "Trade Declined");
                        }));
                        break;
                    case Command.GetBuildingProperties:
                        // Update the building properties list.
                        UpdateBuildingPropertiesList(response.Message);
                        break;
                    case Command.GetBuildingInfo:
                        // Update the building info.
                        string[] numbers = response.Message.Split(',');
                        m_propertyBuildingAmount = Int32.Parse(numbers[0]);
                        m_propertyBuildingCost = Int32.Parse(numbers[1]);
                        break;
                    case Command.BuyBuilding:
                        // A building cannot be bought due to insufficient funds.
                        m_mainWindow.Invoke(new Action(() =>
                        {
                            MessageBox.Show("You do not have enough money to buy a building for this property.", "Buy Building");
                            m_propertiesWithBuildings[response.Message] = (int)m_propertiesWithBuildings[response.Message] - 1;
                        }));
                        break;
                    case Command.BankruptWarning:
                        // Undergo bankrupt actions.
                        BankruptActions();
                        break;
                    case Command.Winner:
                        // Are you the winner?
                        if (m_name == response.Name)
                        {
                            m_mainWindow.Invoke(new Action(() =>
                            {
                                m_mainWindow.RollButton = false;
                                m_mainWindow.PropertiesButton = false;
                                m_mainWindow.MortgageButton = false;
                                m_mainWindow.TradeButton = false;
                                m_mainWindow.BuildingsButton = false;
                                m_mainWindow.DoneButton = false;
                                MessageBox.Show("You are the only player not bankrupt. You win!", "Congratulations!");
                            }));
                        }
                        else
                        {
                            string winMessage = response.Name + " is the only player not bankrupt. They win!";
                            m_mainWindow.Invoke(new Action(() =>
                            {
                                MessageBox.Show(winMessage, "Game Over");
                            }));
                        }
                        break;
                    default:
                        break;
	    		}
	    		// Begin receiving if the socket is still connected.
	    		if (m_clientSocket.Connected) socket.BeginReceive(m_buffer, 0, m_buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
    		}
    		catch
    		{
    			MessageBox.Show("Connection error.", "Connection Error");
    		}
    	}
        /**/
        /*
        ReceiveCallback()

        NAME

                ReceiveCallback() - receives information sent from the server

        SYNOPSIS

                private void DisplayRolls(string a_buffer)

                a_buffer -> the buffer that contains the die rolls

        DESCRIPTION

                This function receives an array of bytes containg the die roll that was just
                performed in the game. It reads the die roll by splitting the buffer into a
                string array, then displays each of them in the respective text boxes for each
                die roll. Then it looks at the double count that was also sent: if it's less
                than 1 or greater than 2, the Roll button must be nullified. Then the Done
                button is enabled since the player no longer needs to roll.

        RETURNS

                Nothing!

        AUTHOR

                Bryan Leier

        DATE

                8:37pm 4/9/2017

        */
        /**/
    	private void DisplayRolls(string a_buffer)
    	{
    		// Split the string up. This is easy since the server sends these as comma separated values.
    		string[] values = a_buffer.Split(',');
    		// Now display it.
    		m_mainWindow.FirstDieBox = values[0];
    		m_mainWindow.SecondDieBox = values[1];
    		// Look at the double count. If it's less than 1 or greater than 2, nullify the roll button.
    		int doubleCount = Int32.Parse(values[2]);
    		if (doubleCount < 1 || doubleCount > 2)
    		{
    			m_mainWindow.RollButton = false;
    			m_canStillRoll = false;
    			// They can end their turn now that their rolls are done. But only if they're actually going.
    			if (m_isOurTurn) m_mainWindow.DoneButton = true;
    		}
    	}
        /**/
        /*
        ShowRentDialog()

        NAME

                ShowRentDialog() - shows the rent dialog received from the server

        SYNOPSIS

                private void ShowRentDialog(string a_message, string a_windowName)

                a_message -> the message to display to the client
                a_windowName -> the name of the window to display

        DESCRIPTION

                This function shows a rent dialog to the player. First, it nulls boxes
                if it is currently the player's turn so nothing can be interfered with
                while the rent box is displayed and read. Then it shows the message.
                After the window is closed, the buttons are reenabled if it is the
                player's turn.

        RETURNS

                Nothing!

        AUTHOR

                Bryan Leier

        DATE

                8:46pm 4/9/2017

        */
        /**/
    	private void ShowRentDialog(string a_message, string a_windowName)
    	{
    		// If it's our turn, null some boxes.
            if (m_isOurTurn)
        	{
                // Can we still roll?
          		if (m_canStillRoll)
               	{
                    // Null the roll button.
                    m_mainWindow.RollButton = false;
                }
                // Otherwise, the done button must be nulled.
                else m_mainWindow.DoneButton = false;
            }
            // Show the messages received in a message box.
            m_mainWindow.Invoke(new Action(() =>
            {
                MessageBox.Show(a_message, a_windowName);
                // Make buttons clickable.
                if (m_isOurTurn)
                {
                    // Can we still roll?
                    if (m_canStillRoll)
                    {
                        // Make the roll button clickable.
                        m_mainWindow.RollButton = true;
                    }
                    // Otherwise, make the done button clickable.
                    else m_mainWindow.DoneButton = true;
                }
            }));
    	}
        /**/
        /*
        ShowRentDialog()

        NAME

                ShowRentDialog() - shows the rent dialog received from the server

        SYNOPSIS

                private void ShowRentDialog(string a_message, string a_windowName)

                a_message -> the message to display to the client
                a_windowName -> the name of the window to display

        DESCRIPTION

                This function shows a rent dialog to the player. First, it nulls boxes
                if it is currently the player's turn so nothing can be interfered with
                while the rent box is displayed and read. Then it shows the message.
                After the window is closed, the buttons are reenabled if it is the
                player's turn.

        RETURNS

                Nothing!

        AUTHOR

                Bryan Leier

        DATE

                8:46pm 4/9/2017

        */
        /**/
    	private void ShowBuyPropertyDialog(string a_message, string a_name)
    	{
    		// You can only buy a property on your turn, so... null some boxes.
    		// Can we still roll?
    		if (m_canStillRoll)
    		{
    			// Null the roll button.
    			m_mainWindow.RollButton = false;
    		}
    		// Otherwise, null the done button.
    		else m_mainWindow.DoneButton = false;
            // Show the message received in a dialogbox.
            // Invoking the MessageBox like this allows the client to not crash after receiving a chat message while making their decision.
            m_mainWindow.Invoke(new Action(() =>
            {
                DialogResult buyPropertyResult = MessageBox.Show(a_message, "Buy Property", MessageBoxButtons.YesNo);
                // If the player wants to buy the property, then go ahead and buy it!
                if (buyPropertyResult == DialogResult.Yes)
                {
                    // Go ahead and send a BuyProperty command back to the server.
                    SendCommand("buyProperty", "");
                    // Add the property to the property list.
                    m_propertiesList[a_name] = true;
                }
                // Make buttons clickable.
                // Can we still roll?
                if (m_canStillRoll)
                {
                    // Make the roll button clickable.
                    m_mainWindow.RollButton = true;
                }
                // Otherwise, make the done button clickable.
                else m_mainWindow.DoneButton = true;
            }));
    	}
        // Fills the list of players still playing the game.
    	private void UpdatePlayerList(string a_message)
    	{
    		// First, clear the list.
    		m_playerList.Clear();
            // Separate the message received.
            string[] nameList = a_message.Split(',');
            // Make sure the blank name at the end of the array is not added:
            for (int i = nameList.Length-2; i >= 0; i--)
            {
            	// Add to the list.
                m_playerList.Add(nameList[i]);
            }
    	}
        // Fills the list of a requested player's properties.
        private void AddRequestedProperties(string a_propertyList)
        {
            // First, clear the list.
            m_requestedPlayerProperties.Clear();
            // Parse the properties.
            string[] parsedProperties = a_propertyList.Split(',');
            // Now add them.
            for (int i = 0; i < parsedProperties.Length - 1; i++)
            {
                m_requestedPlayerProperties.Add(parsedProperties[i]);
            }
        }
        // Parses the funds received from the server for trades.
        private void ParsePlayerFunds(string a_message)
        {
            // First, separate the funds.
            string[] fundsArray = a_message.Split(',');
            // Now place them appropriately into m_funds and m_requestedPlayerFunds.
            m_funds = Int32.Parse(fundsArray[0]);
            m_requestedPlayerFunds = Int32.Parse(fundsArray[1]);
        }
        /**/
        /*
        ReadTradeRequest()

        NAME

                ReadTradeRequest() - shows the rent dialog received from the server

        SYNOPSIS

                private void ReadTradeRequest(string a_requesterName, string a_message)

                a_requesterName -> the name of the person requesting the trade
                a_message -> the information about the trade to display

        DESCRIPTION

                This function reads a trade request sent from another player. First, it parses
                the trade info sent into a string array (sent through this format:"property,money,
                requestedPlay,property,money"). Then it starts building a message saying that a
                player wants to trade, what this player will receive, and what they must exchange
                to get what they receive. Then it asks them the question in a MessageBox. If
                they click Yes, it sends an AcceptTrade command to the server after modifying
                the initial information to have the original requester within the array.
                Otherwise, it sends a DeclineTrade command.

        RETURNS

                Nothing!

        AUTHOR

                Bryan Leier

        DATE

                9:14pm 5/30/2017

        */
        /**/
        private void ReadTradeRequest(string a_requesterName, string a_message)
        {
            // Parse the trade info.
            string[] tradeInfo = a_message.Split(',');
            // Start building the message to be displayed.
            string displayMessage = a_requesterName + " wants to trade!" + Environment.NewLine + Environment.NewLine + "You will receive:" + Environment.NewLine;
            // Add anything to this message that is not null.
            // If it's all null, list so.
            if (tradeInfo[0] == "null" && tradeInfo[1] == "null") displayMessage = displayMessage + "Nothing!" + Environment.NewLine;
            if (tradeInfo[0] != "null") displayMessage = displayMessage + tradeInfo[0] + Environment.NewLine;
            if (tradeInfo[1] != "null") displayMessage = displayMessage + "$" + tradeInfo[1] + Environment.NewLine;
            displayMessage = displayMessage + Environment.NewLine + "In exchange for:" + Environment.NewLine;
            if (tradeInfo[3] == "null" && tradeInfo[4] == "null") displayMessage = displayMessage + "Nothing!" +Environment.NewLine;
            if (tradeInfo[3] != "null") displayMessage = displayMessage + tradeInfo[3] + Environment.NewLine;
            if (tradeInfo[4] != "null") displayMessage = displayMessage + "$" + tradeInfo[4] + Environment.NewLine;
            displayMessage = displayMessage + Environment.NewLine + "Will you accept this trade?";
            // Now open the window.
            m_mainWindow.Invoke(new Action(() =>
            {
                DialogResult tradeRequestResult = MessageBox.Show(displayMessage, "Trade Request", MessageBoxButtons.YesNo);
                // Start initializing a PlayerCommand.
                // If the player wants to trade, then go aheand start the trading operations.
                if (tradeRequestResult == DialogResult.Yes)
                {
                    // Send an AcceptTrade command.
                    string newTradeInfo = tradeInfo[0] + "," + tradeInfo[1] + "," + a_requesterName + "," + tradeInfo[3] + "," + tradeInfo[4];
                    SendCommand("acceptTrade", newTradeInfo);
                }
                else
                {
                    // Send a DeclineTrade command.
                    SendCommand("declineTrade", a_requesterName);
                }
            }));
        }
        /**/
        /*
        AcceptTradeActions()

        NAME

                AcceptTradeActions() - perform actions upon reading an AcceptTrade command

        SYNOPSIS

                private void AcceptTradeActions(string a_properties)

                a_properties -> properties to change in the client's propertiesList hashtable

        DESCRIPTION

                This function does actions upon reading an AcceptTrade command from the server.
                First, it does actions to let the GUI know that properties have new owners.
                This is done by receiving the comma separated two properties within a_properties.
                The first property will always be the property to remove and the second to add.
                If any of these are null, these "highlight" operations are not performed. Afterwards,
                a message will pop up indicating the trade was successful, and a FundUpdate will be
                requested.

        RETURNS

                Nothing!

        AUTHOR

                Bryan Leier

        DATE

                11:01pm 5/30/2017

        */
        /**/
        private void AcceptTradeActions(string a_properties)
        {
            // Perform property switches.
            string[] propertyList = a_properties.Split(',');
            // Dull out the first property, and highlight the second.
            if (propertyList[0] != "null") m_propertiesList[propertyList[0]] = false;
            if (propertyList[1] != "null") m_propertiesList[propertyList[1]] = true;
            // Indicate the trade was a success.
            m_mainWindow.Invoke(new Action(() =>
            {
                MessageBox.Show("The trade was successful!");
                // Request a fund update.
                SendCommand("fundUpdate", "");
            }));
        }
        // Updates the list of properties able to be built on.
        private void UpdateBuildingPropertiesList(string a_properties)
        {
            // First, clear the list.
            m_buildingProperties.Clear();
            // Separate the names and add them.
            string[] propertiesReceived = a_properties.Split(',');
            for (int i = 0; i < propertiesReceived.Length - 1; i++)
            {
                m_buildingProperties.Add(propertiesReceived[i]);
            }
        }
        /**/
        /*
        BankruptActions()

        NAME

                BankruptActions() - shows the rent dialog received from the server

        SYNOPSIS

                private void BankruptActions()

        DESCRIPTION

                This function performs actions upon a player receiving the BankruptWarning command.
                If it's the player's turn, the Roll and Done buttons are nullified so the player can
                correct their Bankrupt state. Otherwise, the Buildings button becomes clickable so
                they can potentially sell off buildings. The Bankruptcy button becoms enabled and a
                message is displayed that tells the player they are bankrupt and must perform appropriate
                actions.

        RETURNS

                Nothing!

        AUTHOR

                Bryan Leier

        DATE

                7:07pm 5/31/2017

        */
        /**/
        private void BankruptActions()
        {
            // If it's our turn, null some buttons.
            if (m_isOurTurn)
            {
                // Can we still roll?
                if (m_canStillRoll)
                {
                    // Null the roll button.
                    m_mainWindow.RollButton = false;
                }
                // Otherwise, the done button must be nulled.
                else m_mainWindow.DoneButton = false;
            }
            // Otherwise, make the Buildings button clickable.
            else
            {
                m_mainWindow.BuildingsButton = true;
            }
            // Enable the Bankruptcy button.
            m_mainWindow.BankruptcyButton = true;
            // Show a message indicating this player is currently in the red.
            m_mainWindow.Invoke(new Action(() =>
            {
                MessageBox.Show("You are currently bankrupt! Sell some assets to come back. If you cannot (or choose not to), declare bankruptcy by clicking on the Bankruptcy button.",
                    "Bankrupt Warning");
            }));
        }

    	// The client's socket.
    	private Socket m_clientSocket;
    	// The client's name.
    	private string m_name;
    	// The buffer that the client receives data on.
    	private byte[] m_buffer = new byte[1024];
    	// Main window form.
    	private MainWindow m_mainWindow;
    	// Determines whether or not it is currently this player's turn.
    	private bool m_isOurTurn = false;
    	// Determines whether or not the player can still roll.
    	private bool m_canStillRoll = false;
        // The list of properties in the game.
        private Hashtable m_propertiesList;
        // The list of properties, along with the amount of buildings they have.
        private Hashtable m_propertiesWithBuildings;
        // The list of properties that are mortgaged.
        private List<string> m_mortgagedProperties;
        // A list of players in the game.
        private List<string> m_playerList;
        // A list of properties from a requested player in a trade.
        private List<string> m_requestedPlayerProperties;
        // The amount of funds this client currently has.
        private int m_funds;
        // The amount of funds a requested player in a trade has.
        private int m_requestedPlayerFunds;
        // The properties that are able to have buildings built on them.
        private List<string> m_buildingProperties;
        // The amount of buildings the current property has.
        private int m_propertyBuildingAmount;
        // The price of buildings on the current property.
        private int m_propertyBuildingCost;
    }
}
