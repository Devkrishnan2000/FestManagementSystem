using FestManagementApp.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using UserControl = System.Windows.Controls.UserControl;

namespace FestManagementApp.UserControls
{
    /// <summary>
    /// Interaction logic for ChatClient.xaml
    /// </summary>
    public partial class ChatClient : UserControl
    {
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

        public ChatClient()
        {
            InitializeComponent();
            this.Loaded += ChatClient_Loaded;
            try
            {
                readData = "Welcome to the Group chat!";
                chat = new ObservableCollection<ChatViewModel>();
                msg();
                clientSocket.Connect("192.168.29.247", 6677);
                serverStream = clientSocket.GetStream();

                byte[] outStream = System.Text.Encoding.ASCII.GetBytes(Properties.Settings.Default.Chatname + "$");
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();

                Thread ctThread = new Thread(getMessage);
                ctThread.Start();
            }
            catch(Exception)
            {

            }
        }

        private void ChatClient_Loaded(object sender, RoutedEventArgs e)
        {
            Window window =  Window.GetWindow(this);
            window.Closing += Window_Closing;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            flag = false;
            clientSocket.Close();
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
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(new MethodInvoker(msg));
                
            }
            else
            {
                if(readData.Contains("Joined"))
                {
                    //usr join
                }
                else if(readData.Contains(" says"))
                {
                    if(readData.Contains(Properties.Settings.Default.Chatname))
                    {
                       
                        string usr = readData.Substring(0, readData.IndexOf(' '));
                        string[] usrinfo = usr.Split('+');
                        string[] msg = readData.Split(':');
                        DateTime curdate = DateTime.Now;

                        chat.Add(new ChatViewModel { Usrname = usrinfo[0], Message = msg[1], Reciver="no",Time=curdate.ToString("hh:mm tt"),Rank=usrinfo[1] });
                        chatbox.ItemsSource = chat;
                    }
                    else
                    {
                       
                        string usr = readData.Substring(0, readData.IndexOf(' '));
                        string[] usrinfo = usr.Split('+');
                        string[] msg = readData.Split(':');
                        DateTime curdate = DateTime.Now;
                        chat.Add(new ChatViewModel { Usrname = usrinfo[0], Message = msg[1], Reciver = "yes", Time = curdate.ToString("hh:mm tt"), Rank = usrinfo[1] });
                        chatbox.ItemsSource = chat;
                    }
                   
                }
                
             
            }
                
               
        }



        private void sendbtn_Click(object sender, RoutedEventArgs e)
        {
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(Inputtext.Text + "$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
            Inputtext.Clear();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            flag = false;
            clientSocket.Close();
        }
        
    }
}
