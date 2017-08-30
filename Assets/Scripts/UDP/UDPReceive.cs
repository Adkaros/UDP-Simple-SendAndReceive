using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;

namespace UDP
{
    public class UDPReceive : SingletonBehaviour<UDPReceive>
    {
        public delegate void DDataReceived();
        public event DDataReceived DataReceived;

        public int port = 5000;

        private Thread receiveThread;
        private UdpClient client;

        private bool send = false;
        private bool threadOpen = true;

        private string currentPacket = "";
        private string previousPacket = "";

        public void Awake()
        {
            Application.runInBackground = true;
        }

        public void Start()
        {
            port = 5000;
            Debug.Log("Port : " + port);

            receiveThread = new Thread(new ThreadStart(ReceiveData));
            receiveThread.IsBackground = true;
            receiveThread.Start();
        }

        // receive thread
        private void ReceiveData()
        {
            client = new UdpClient(port);
            while (threadOpen)
            {
                try
                {
                    IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, port);
                    byte[] data = client.Receive(ref anyIP);

                    // call receve callback
                    send = true;
                }
                catch (Exception e)
                {
                    print(e.ToString());
                }
            }
        }

        void Update()
        {
            if (send)
            {
                if (DataReceived != null)
                    DataReceived();
            }
        }

        void OnApplicationQuit()
        {
            threadOpen = false;
            if (receiveThread != null)
                receiveThread.Abort();

            if (client != null)
                client.Close();
        }

        public void Reset()
        {
            threadOpen = false;
            if (receiveThread != null)
                receiveThread.Abort();

            if (client != null)
                client.Close();
        }
    }
}