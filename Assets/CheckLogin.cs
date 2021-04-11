using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

public class CheckLogin : MonoBehaviour
{
    List<string> _login;
    List<string> _password1;
    TcpClient _Client;
    void Start()
    {
        _login = new List<string>();
        _password1 = new List<string>();
        _login.Add("admin");
        _login.Add("666666");
        _login.Add("888888");
        _login.Add("default");
        _login.Add("system");
        _login.Add("user");
        _password1.Add("admin");
        _password1.Add("admin123");
        _password1.Add("666666");
        _password1.Add("888888");
        StartCoroutine(CheckPort());
    }
    IEnumerator CheckPort()
    {
        var allfiles = Directory.GetFileSystemEntries(@"c:\Test\", "*.*", SearchOption.AllDirectories);
        // var allfiles2 = Directory.GetFiles(@"c:\Test\", "*.*", SearchOption.AllDirectories);
        // Debug.Log(allfiles.);
        // Debug.Log(allfiles2[5]);
        // _Client = new TcpClient();
        // _Client.Connect(Host, Port);
        // while(true)
        // {

            // foreach (var item in _login)
            // {
            //     foreach (var item2 in _password1)
            //     {
            //         Debug.Log("Login: " + item + " Password: " + item2);
            //     }
            // }
            yield return new WaitForSeconds(5);
        // }
    }
}
