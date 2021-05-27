using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using SocketGameProtocol;

public class ClientManager : BaseManager
{
    private Socket socket;

    //:base()����BaseManager�Ļ��๹�캯��
    public ClientManager(GameFace face) : base(face) { }
    private Message message;
    public override void OnInit()
    {
        base.OnInit();
        InitSocket();
        message = new Message();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        message = null;
        CloseSocket();
    }
    /// <summary>
    /// ��ʼ��socket
    /// </summary>
    private void InitSocket()
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            socket.Connect("127.0.0.1", 6666);
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
        }
    }

    private void CloseSocket()
    {
        if(socket.Connected&&socket!=null)
        {
            socket.Close();
        }
    }

    private void StartReceive()
    {
        socket.BeginReceive(message.Buffer, message.StartIndex, message.RemainningSize, SocketFlags.None,ReceiveCallback, null);
    }
    private void ReceiveCallback(IAsyncResult iar)
    {
        try
        {
            if (socket == null || socket.Connected == face) return;
            int len = socket.EndReceive(iar);
            if(len==0)
            {
                CloseSocket();
                return;
            }
            message.ReadBuffer(len,HandleResponse);
            StartReceive();

        }
        catch
        {

        }
    }

    private void HandleResponse(MainPack pack)
    {
        face.HandleResponse(pack);
    }

    public void send(MainPack pack)
    {
        socket.Send(Message.PackData(pack));
    }
}
