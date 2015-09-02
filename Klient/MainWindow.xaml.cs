using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;
using System.Threading;
//using System.Windows.Threading;
using System.IO;

namespace Klient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Client NewClient;
        public MainWindow()
        {
            NewClient = new Client("127.0.0.1", 9001);
            InitializeComponent();
            Thread clientReaderThread = new Thread(ClientReader);
            clientReaderThread.Start();
        }

        private void txtBox_input_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                NewClient.ClientWriter(txtBox_input.Text);
                txtBox_input.Text = "";


            }
        }

        public void ClientReader()
        {
            while (true)
            {
                string input = DataManager(NewClient.ClientReader());
                Dispatcher.Invoke(new Action(() => txtBox_output.Text = input));
            }            
        }
        public string DataManager(string input)
        {
            if(input == "gedeost")
            { return "Leif er awesome!"; }
            else return input;
        }
    }
}
