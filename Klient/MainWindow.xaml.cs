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
        List<Item> itemlist = new List<Item>();
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
                NewClient.ClientWriter("Bud: " + cbox_Items.SelectedItem + txtBox_Bid.Text);
                txtBox_Bid.Text = "";


            }
        }

        public void ClientReader()
        {
            while (true)
            {
                DataManager(NewClient.ClientReader());
                // Dispatcher.Invoke(new Action(() => txtBox_output.Text = input));
            }
        }
        public void DataManager(string input) //
        {
            string[] inputarray = input.Split(' ');


            if (inputarray[0] == "/s") //offer accepted as highest bid
            {
                Dispatcher.Invoke(new Action(() => cbox_Items.IsEnabled = false)); //disable itemlist
                Dispatcher.Invoke(new Action(() => txtBox_Bid.IsEnabled = true)); //enable bidding
                txtbox_estimate.Text = itemlist[int.Parse(inputarray[1])].Estimate.ToString(); //estimated price
                txtBox_HighestBid.Text = itemlist[int.Parse(inputarray[1])].CurrentBid.ToString(); //Highest offer

            }

            if (inputarray[0] == "/oa") //offer accepted as highest bid
            {
                Dispatcher.Invoke(new Action(() => txtBox_HighestBid.Text = inputarray[1]));
                Dispatcher.Invoke(new Action(() => checkbox_Status.IsChecked = true));
            }

            if (inputarray[0] == "/od") //offer denied as highest bid
            {
                Dispatcher.Invoke(new Action(() => txtBox_HighestBid.Text = inputarray[3]));
                Dispatcher.Invoke(new Action(() => checkbox_Status.IsChecked = false));
            }

            if (inputarray[0] == "/la") //list auktion
            {
                int counter = 0, pris = 0, id = 0;
                string navn = "", vurdering = "";
                foreach (string a in inputarray)
                {
                    if (a == "/la") //tilføj ikke "/la" til itemlisten.
                    {

                    }

                    if (counter == 1)
                    {
                        id = int.Parse(a);
                        counter++;
                    }
                    else if (counter == 2)
                    {
                        vurdering = a;
                        counter++;
                    }
                    else if (counter == 3)
                    {
                        pris = int.Parse(a);
                        counter++;
                    }
                    else if (counter > 3)
                    {
                        if (a.Contains("/n"))
                        {
                            itemlist.Add(new Item(navn, vurdering, pris, id));
                            navn = "";
                            counter = 1;
                        }
                        else
                        {
                            navn += a + " ";
                            counter++;
                        }
                    }
                    else
                        counter++;

                    //gem vores data itemliste
                    cbox_Items.Items.Clear();
                    foreach (Item i in itemlist)
                    {
                        cbox_Items.Items.Add(i.Id + ": " + i.Name);
                    }

                }
            }


            {

            }

        }

        private void cbox_ItemSubscribed_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbox_ItemSubscribed.IsEnabled == true)
                {
                    string[] selectedID = cbox_Items.SelectedItem.ToString().Split(':');
                    foreach (Item i in itemlist)
                    {
                        if (i.Id == int.Parse(selectedID[0]))
                        {
                            NewClient.ClientWriter("/s " + i.Id);
                        }
                    }
                }
                else
                {
                    NewClient.ClientWriter("/us 0");
                }


            }
            catch
            {
                label_SubscribeWarning.Visibility = Visibility.Visible;
            }
        }


    }
}
