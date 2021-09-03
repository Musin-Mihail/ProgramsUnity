using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using System.IO;
public class DahuaTest : MonoBehaviour
{
    void Start()
    {
        IsCamera("203.86.195.91", 37777);
    }
    void IsCamera(string server, int port)
    {
        // TcpClient tcpClient = new TcpClient();
        try
        {
            byte[] message =
            {
                0xa0, 0x01, 0x00, 0x60, 0x00, 0x00, 0x00, 0x00, 
                0xc4, 0xa3, 0xaf, 0x48, 0x99, 0x56, 0xb6, 0xb4, 
                0xc4, 0xa3, 0xaf, 0x48, 0x99, 0x56, 0xb6, 0xb4, 
                0x05, 0x02, 0x00, 0x01, 0x00, 0x00, 0xa1, 0xaa
            };
            byte[] message2 =
            {
                0xb0, 0x00, 0x00, 0x58, 0x00, 0x00, 0x00, 0x00, 
                0x00, 0x08, 0x08, 0x08, 0x42, 0x00, 0x00, 0x00, 
                0x24, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 
                0x06, 0x00, 0xf9, 0x00, 0x01, 0x08, 0x64, 0x02
            };
            // tcpClient.Connect(server, port);
            // NetworkStream networkStream = tcpClient.GetStream();
            // networkStream.Write(message, 0, message.Length);
            // byte[] response = new byte[16];
            // networkStream.Read(response, 0, response.Length);
            // tcpClient.Close();
            // networkStream.Close();
            // if(response[3] == 0x10 && response[7] == response[11])
            // {
            //     Debug.Log("Есть связь");
            // }
            // else
            // {
            //     Debug.Log("Нет связи");
            // }
            File.WriteAllBytes("message", message);
            File.WriteAllBytes("message2", message2);
        }
        catch (Exception)
        {
            Debug.Log("Ошибка");
        }
    }
}
