using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

public class Client : MonoBehaviour
{
    private Socket socket;
    private byte[] buffer = new byte[1024];
    // Start is called before the first frame update
    void Start()
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.Connect("127.0.0.1", 6666);
        Debug.Log("连接成功");
        Send();
        StartReceive();
    }

    void StartReceive()
    {
        socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceiveCallback, null);
    }

    void ReceiveCallback(IAsyncResult iar)
    {
        int len = socket.EndReceive(iar);
        if (len == 0)
        {
            return;
        }
        string str = Encoding.UTF8.GetString(buffer, 0, len);
        Debug.Log(str);
        StartReceive();
    }
    void Send()
    {
        socket.Send(Encoding.UTF8.GetBytes("你好"));
    }
}
