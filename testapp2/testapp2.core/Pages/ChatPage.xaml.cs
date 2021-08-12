using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using testapp2.Classes;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace testapp2.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatPage : ContentPage
    {
        public string metadata;
        private ObservableCollection<ChatViewModel> chat;
        public ObservableCollection<ChatViewModel> Chat
        {
            get { return chat; }
            set { chat = value; }
        }
        System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
        NetworkStream serverStream = default(NetworkStream);
        string readData = null;
        public bool flag = true;

        public ChatPage()
        {
          InitializeComponent();
            string username = Preferences.Get("UserName", "Shon");
            string eventname = Preferences.Get("EventName", "ename");
            metadata = username + "+" + eventname;
            readData = "Welcome to the Group chat!";
            chat = new ObservableCollection<ChatViewModel>();
           msg();
          clientSocket.Connect("192.168.29.247", 6677);
            serverStream = clientSocket.GetStream();

        byte[] outStream = System.Text.Encoding.ASCII.GetBytes(metadata + "$");
        serverStream.Write(outStream, 0, outStream.Length);
          serverStream.Flush();

        Thread ctThread = new Thread(getMessage);
        ctThread.Start();

        }
        private void getMessage()
        {
            while (flag == true)
            {
                try
                {
                    serverStream = clientSocket.GetStream();
                    int buffSize = 0;
                    byte[] inStream = new byte[clientSocket.ReceiveBufferSize];
                    buffSize = clientSocket.ReceiveBufferSize;
                    serverStream.Read(inStream, 0, buffSize);
                    System.Diagnostics.Debug.WriteLine(buffSize);
                    string returndata = System.Text.Encoding.ASCII.GetString(inStream);
                    string refineddata = returndata.Substring(0, returndata.IndexOf('\0'));
                    readData = "" + refineddata;
                    msg();
                }
                catch (Exception)
                {

                }

            }

        }

        private void msg()
        {
            
            if(Dispatcher==null)
            {
                System.Diagnostics.Debug.WriteLine("this called");
                Dispatcher.BeginInvokeOnMainThread(new Action(msg));
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("else working");
                if (readData.Contains("Joined"))
                {
                    
                }
                else if (readData.Contains(" says"))
                {
                    if (readData.Contains(metadata))
                    {
                        string usr = readData.Substring(0, readData.IndexOf(' '));
                        string[] usrinfo = usr.Split('+');
                        string[] msg = readData.Split(':');
                        DateTime curdate = DateTime.Now;
                        System.Diagnostics.Debug.WriteLine(usrinfo[0] + '\n' + msg[1] + '\n' + curdate.ToString("hh:mm: tt") + '\n' + usrinfo[1]); 
                        chat.Add(new ChatViewModel { Usrname = usrinfo[0], Message = msg[1], Reciver = "no",Time = curdate.ToString("hh:mm tt"), Rank = usrinfo[1] });
                        ChatList.ItemsSource = chat;
                                           

                    }
                    else
                    {
                        string usr = readData.Substring(0, readData.IndexOf(' '));
                        string[] usrinfo = usr.Split('+');
                        string[] msg = readData.Split(':');
                        DateTime curdate = DateTime.Now;
                        chat.Add(new ChatViewModel { Usrname = usrinfo[0], Message = msg[1], Reciver = "yes", Time = curdate.ToString("hh:mm tt"), Rank = usrinfo[1] });
                        ChatList.ItemsSource = chat;
                    }

                }
            }

            

                // chatbox.Items.Add(readData);
            
            // msgtext.Text = msgtext.Text + Environment.NewLine + " >> " + readData;

        }

        private void sendbtn_Clicked(object sender, EventArgs e)
        {
           byte[] outStream = System.Text.Encoding.ASCII.GetBytes(inputtext.Text + "$");
          serverStream.Write(outStream, 0, outStream.Length);
           serverStream.Flush();
            inputtext.Text = "";
        }
    }
}