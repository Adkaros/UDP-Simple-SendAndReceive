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
    public class UDPSend : SingletonBehaviour<UDPSend>
    {
        // 10.1.10.255 // All
        // 127.0.0.1   // Self
        public string ModelViewerIP = "10.1.10.25";
        public int ModelViewerPort = 5000;

        private IPEndPoint ModelViewerRemoteEndPoint;
        private UdpClient ModelViewerClient;

        public void Start()
        {
            ModelViewerRemoteEndPoint = new IPEndPoint(IPAddress.Parse(ModelViewerIP), ModelViewerPort);
            ModelViewerClient = new UdpClient();
        }

        public void SendString(string _message)
        {
            try
            {
                if (_message != "")
                {
                    byte[] data = Encoding.UTF8.GetBytes(_message);
                    ModelViewerClient.Send(data, data.Length, ModelViewerRemoteEndPoint);
                }
            }
            catch (Exception err)
            {
                print(err.ToString());
            }
        }
    }
}
