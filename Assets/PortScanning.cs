using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
public class PortScanning : MonoBehaviour
{
    public Text _leftRanges;
    public Text _collectedAddresses;
    public Text _numberThreads;
    public Text _portsFound;
    public Text _status;
    public Text _statusInternet;
    List<string> IPList = new List<string>();
    List<string> GoodIP = new List<string>();
    List<string> IPRangeList = new List<string>();
    List<string> foundPort8000 = new List<string>();
    List<string> foundPort34567 = new List<string>();
    List<string> foundPort37777 = new List<string>();
    int countThread;
    int PortCount;
    bool checkInternet = true;
    public Material _internet;
    List<int> PortList = new List<int>();
    int maxThreads = 100;
    void Start()
    {
        PortList.Add(37777);
        PortList.Add(8000);
        PortList.Add(34567);
        Directory.CreateDirectory("IP37777");
        Directory.CreateDirectory("IP8000");
        Directory.CreateDirectory("IP34567");
        string[] AllRange = File.ReadAllLines("IPRangeList.txt");
        IPRangeList = AllRange.ToList();
        FirstRangeIP();
        StartCoroutine(StartThread());
        StartCoroutine(WriteText());
        StartCoroutine(CheckInternet());
        StartCoroutine(CheckList());
    }
    IEnumerator WriteText()
    {
        while (true)
        {
            _collectedAddresses.text = IPList.Count.ToString();
            _portsFound.text = PortCount.ToString();
            _numberThreads.text = countThread.ToString();
            yield return new WaitForSeconds(1);
        }
    }
    IEnumerator StartThread()
    {
        while(true)
        {
            if(checkInternet)
            {
                _status.text = "Статус: В работе";
                if(GoodIP.Count > 0)
                {
                    for (int i = 0; i < GoodIP.Count-1; i++)
                    {
                        string IP = GoodIP[i];
                        GoodIP.RemoveAt(i);
                        foreach (var port in PortList)
                        {
                            countThread++;
                            var _thread = new Thread(() =>
                            {
                                PortScan(IP, port);
                            });
                            _thread.Start();
                            StartCoroutine(stopThread(_thread, 2));
                        }
                    }
                }
                if(IPList.Count <= 0)
                {
                    RangeIP();
                }
                else
                {
                    while(countThread < maxThreads && IPList.Count > 0)
                    {
                        string IP = IPList[0];
                        IPList.RemoveAt(0);
                        countThread++;
                        var _thread = new Thread(() =>
                        {
                            Ping(IP);
                        });
                        _thread.Start();
                        StartCoroutine(stopThread(_thread, 10));
                        yield return new WaitForSeconds(0.01f);
                    }
                }
                _status.text = "Статус: Ожидание";
            }
            else
            {
                _status.text = "Статус: Нет интернета";
            }
            yield return new WaitForSeconds(5);
        }
    }
    IEnumerator CheckInternet()
    {
        while(true)
        {
            try
            {
                System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
                PingReply pingReply = ping.Send("91.109.200.200");
                _statusInternet.text = pingReply.Status.ToString();
                if (pingReply.Status == IPStatus.Success)
                {
                    _internet.color = Color.green;
                    checkInternet = true;
                }
                else
                {
                    _internet.color = Color.red;
                    checkInternet = false;
                }
            }
            catch(Exception)
            {
                _statusInternet.text = "Ошибка соединения";
                _internet.color = Color.red;
                checkInternet = false;
            }
            yield return new WaitForSeconds(10);
        }
    }
    void FirstRangeIP() 
    {
        List<int> IP = IPRangeList[0].Split('.').Select(Int32.Parse).ToList();
        SplitRange(IP);
        _leftRanges.text = IPRangeList.Count.ToString();
    }
    void RangeIP()
    {
        var IP = IPRangeList[1].Split('.').Select(Int32.Parse).ToList();
        SplitRange(IP);
        IPRangeList.RemoveAt(0);
        File.WriteAllLines("IPRangeList.txt", IPRangeList);
        _leftRanges.text = IPRangeList.Count.ToString();
    }
    void SplitRange(List<int> IP)
    {
        for (int i = IP[0]; i <= IP[4]; i++)
        {
            if (i == IP[4])
            {
                for (int i2 = IP[1]; i2 <= IP[5]; i2++)
                {
                    if (i2 == IP[5])
                    {
                        for (int i3 = IP[2]; i3 <= IP[6]; i3++)
                        {
                            if (i3 == IP[6])
                            {
                                for (int i4 = IP[3]; i4 <= IP[7]; i4++)
                                {
                                    IPList.Add(i + "." + i2 + "." + i3 + "." + i4);
                                }
                            }
                            else
                            {
                                for (int i4 = IP[3]; i4 <= 255; i4++)
                                {
                                    IPList.Add(i + "." + i2 + "." + i3 + "." + i4);
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int i3 = IP[2]; i3 <= 255; i3++)
                        {
                            for (int i4 = IP[3]; i4 <= 255; i4++)
                            {
                                IPList.Add(i + "." + i2 + "." + i3 + "." + i4);
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i2 = IP[1]; i2 <= 255; i2++)
                {
                    for (int i3 = IP[2]; i3 <= 255; i3++)
                    {
                        for (int i4 = IP[3]; i4 <= 255; i4++)
                        {
                            IPList.Add(i + "." + i2 + "." + i3 + "." + i4);
                        }
                    }
                }
            }
        }
    }
    void Ping(string IP)
    {
        try
        {
            System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
            PingReply pingReply = ping.Send(IP);
            var textPing = pingReply.Status.ToString();
            if (pingReply.Status == IPStatus.Success)
            {
                GoodIP.Add(IP);
            }
        }
        catch(Exception)
        {
        }
    }
    void PortScan(string IP, int port)
    {
        try
        {
            Socket socket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            socket.Connect(IP, port);
            // File.Create($"IP" + port + "\\" + IP);
            if(port == 8000)
            {
                foundPort8000.Add(IP);
            }
            else if(port == 37777)
            {
                foundPort37777.Add(IP);
            }
            else if(port == 34567)
            {
                foundPort34567.Add(IP);
            }
            PortCount++;
        }
        catch (Exception)
        {
        }
    }
    IEnumerator stopThread(Thread _thread, int _second)
    {
        yield return new WaitForSeconds(_second);
        _thread.Abort();
        countThread--;
    }
    IEnumerator CheckList()
    {
        while (true)
        {
            if(foundPort8000.Count > 0)
            {
                File.AppendAllLines("Port_8000.txt",foundPort8000);
                foundPort8000.Clear();
            }
            if(foundPort37777.Count > 0)
            {
                File.AppendAllLines("Port_37777.txt",foundPort37777);
                foundPort37777.Clear();
            }
            if(foundPort34567.Count > 0)
            {
                File.AppendAllLines("Port_34567.txt",foundPort34567);
                foundPort34567.Clear();
            }
            yield return new WaitForSeconds(5);
        }
    }
}