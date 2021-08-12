using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Collections;
using System.Threading;
using System.Net;

namespace FestCommunicationServer
{

    class Program
    {
        public static Hashtable clientsList = new Hashtable();
        public static IPAddress localAddr = IPAddress.Parse("192.168.29.247");
        public static int ifcount=0;
        public static int bcount = 0;

        static void Main(string[] args)
        {
            TcpListener serverSocket = new TcpListener(localAddr,6677);
            TcpClient clientSocket = default(TcpClient);
            int counter = 0;

            serverSocket.Start();
            Console.WriteLine("Chat Server Started ....");
            counter = 0;
            while ((true))
            {
                counter += 1;
                clientSocket = serverSocket.AcceptTcpClient();

                byte[] bytesFrom = new byte[clientSocket.ReceiveBufferSize];
                string dataFromClient = null;

                NetworkStream networkStream = clientSocket.GetStream();
                networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));

                clientsList.Add(clientSocket, clientSocket);

                broadcast(dataFromClient + " Joined ", dataFromClient, false);

                Console.WriteLine(dataFromClient + " Joined chat room ");
                handleClinet client = new handleClinet();
                client.startClient(clientSocket, dataFromClient, clientsList);
                System.Diagnostics.Debug.WriteLine("usr add "+counter);
            }

            clientSocket.Close();
            serverSocket.Stop();
            Console.WriteLine("exit");
            Console.ReadLine();
        }

        public static void broadcast(string msg, string uName, bool flag)
        {
            
            foreach (DictionaryEntry Item in clientsList)
            {
                TcpClient broadcastSocket;
                broadcastSocket = (TcpClient)Item.Value;
               
                    NetworkStream broadcastStream = broadcastSocket.GetStream();
                    Byte[] broadcastBytes = null;

                    if (flag == true && broadcastSocket.Connected)
                    {
                        broadcastBytes = Encoding.ASCII.GetBytes(uName + " says : " + msg); 
                   
                      
                    }
                    else if(broadcastSocket.Connected)
                    {
                        broadcastBytes = Encoding.ASCII.GetBytes(msg);
                    }

                    broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
               
               
                broadcastStream.Flush();
                
            }
        }  //end broadcast function
    }//end Main class


    public class handleClinet
    {
        TcpClient clientSocket;
        string clNo;
        Hashtable clientsList;

        public void startClient(TcpClient inClientSocket, string clineNo, Hashtable cList)
        {
            this.clientSocket = inClientSocket;
            this.clNo = clineNo;
            this.clientsList = cList;
             if(inClientSocket.Connected)
            {
                Thread ctThread = new Thread(doChat);
                System.Diagnostics.Debug.WriteLine("New Thread started");
                ctThread.Start();
            }
              
            
            
        }

        private void doChat()
        {
            int requestCount = 0;
            byte[] bytesFrom;
            string dataFromClient = null;
            Byte[] sendBytes = null;
            string serverResponse = null;
            string rCount = null;
            requestCount = 0;

            while ((true))
            {
               if(clientSocket.Connected)
                {
                    try
                    {
                        requestCount = requestCount + 1;
                        bytesFrom = new byte[clientSocket.ReceiveBufferSize];
                        if(bytesFrom!=null)
                        {
                            NetworkStream networkStream = clientSocket.GetStream();
                            networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                            dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                            dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
                            Console.WriteLine("From client - " + clNo + " : " + dataFromClient);
                            rCount = Convert.ToString(requestCount);
                            Program.broadcast(dataFromClient, clNo, true);
                            bytesFrom = null;
                        }
                       
                     

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        clientsList.Remove(clientSocket);
                        break;
                    }
                }
                  
                
                    
                
                
            }//end while
        }//end doChat
       
    } //end class handleClinet



}
