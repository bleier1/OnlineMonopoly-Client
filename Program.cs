// The main Program file that the program starts from.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonopolyClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // set up a client
            Client client = new Client();
            // open the form
            Application.Run(new ConnectToServer(client));
        }
    }
}
