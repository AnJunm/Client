using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SocketGameProtocol;
using Google.Protobuf;
using System.Linq;

public class Message

{
    private byte[] buffer = new byte[1024];

    private int startindex;

    public byte[] Buffer
    {
        get
        {
            return buffer;
        }
    }

    public int StartIndex
    {
        get
        {
            return startindex;
        }
    }

    public int RemainningSize
    {
        get
        {
            return buffer.Length - startindex;
        }
    }
    public void ReadBuffer(int len, Action<MainPack> HandleRequest)
    {
        startindex += len;
        if (startindex <= 4) return;
        int count;
        while (true)
        {
            count = BitConverter.ToInt32(buffer, 0);
            if (startindex >= (count + 4))
            {
                MainPack pack = (MainPack)MainPack.Descriptor.Parser.ParseFrom(Buffer, 4, count);

                HandleRequest(pack);

                Array.Copy(buffer, count + 4, buffer, 0, startindex - count - 4);

                startindex -= count + 4;
            }
            else
            {
                break;
            }
        }

    }

    public static byte[] PackData(MainPack pack)
    {
        byte[] data = pack.ToByteArray();//包体
                                         //包头
        byte[] head = BitConverter.GetBytes(data.Length);
        return head.Concat(data).ToArray();
    }
}
