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

public class PortScanning : MonoBehaviour
{
    public Text _leftRanges;
    public Text _collectedAddresses;
    public Text _numberThreads;
    public Text _checkedPorts;
    public Text _portsFound;
    List<string> IPList = new List<string>();
    List<string> GoodIP = new List<string>();
    List<string> IPRangeList = new List<string>();
    int countThread;
    int PortCount;
    int q2 = 0;
    void Awake() 
    {
        Directory.CreateDirectory("IP");
        var AllRange = File.ReadAllLines("IPRangeList.txt");
        IPRangeList = AllRange.ToList();
        FirstRangeIP();
    }
    void Start()
    {
        StartCoroutine(Ping2());
        StartCoroutine(PortScan2());
    }
    void Update() 
    {
        if(countThread == 0 && IPList.Count == 0)
        {
            RangeIP();
        }
        _numberThreads.text = countThread.ToString();
        _collectedAddresses.text = IPList.Count.ToString();
        
    }
    void FirstRangeIP() 
    {
        var numbers2 = IPRangeList[0].Split('.').Select(Int32.Parse).ToList();
        for (int i = numbers2[0]; i <= numbers2[4]; i++)
        {
            if (i == numbers2[4])
            {
                for (int i2 = numbers2[1]; i2 <= numbers2[5]; i2++)
                {
                    if (i2 == numbers2[5])
                    {
                        for (int i3 = numbers2[2]; i3 <= numbers2[6]; i3++)
                        {
                            if (i3 == numbers2[6])
                            {
                                for (int i4 = numbers2[3]; i4 <= numbers2[7]; i4++)
                                {
                                    IPList.Add(i + "." + i2 + "." + i3 + "." + i4);
                                }
                            }
                            else
                            {
                                for (int i4 = numbers2[3]; i4 <= 255; i4++)
                                {
                                    IPList.Add(i + "." + i2 + "." + i3 + "." + i4);
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int i3 = numbers2[2]; i3 <= 255; i3++)
                        {
                            for (int i4 = numbers2[3]; i4 <= 255; i4++)
                            {
                                IPList.Add(i + "." + i2 + "." + i3 + "." + i4);
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i2 = numbers2[1]; i2 <= 255; i2++)
                {
                    for (int i3 = numbers2[2]; i3 <= 255; i3++)
                    {
                        for (int i4 = numbers2[3]; i4 <= 255; i4++)
                        {
                            IPList.Add(i + "." + i2 + "." + i3 + "." + i4);
                        }
                    }
                }
            }
        }
        _leftRanges.text = IPRangeList.Count.ToString();
    }
    void RangeIP()
    {
        var numbers = IPRangeList[1].Split('.').Select(Int32.Parse).ToList();
        for (int i = numbers[0]; i <= numbers[4]; i++)
        {
            if (i == numbers[4])
            {
                for (int i2 = numbers[1]; i2 <= numbers[5]; i2++)
                {
                    if (i2 == numbers[5])
                    {
                        for (int i3 = numbers[2]; i3 <= numbers[6]; i3++)
                        {
                            if (i3 == numbers[6])
                            {
                                for (int i4 = numbers[3]; i4 <= numbers[7]; i4++)
                                {
                                    IPList.Add(i + "." + i2 + "." + i3 + "." + i4);
                                }
                            }
                            else
                            {
                                for (int i4 = numbers[3]; i4 <= 255; i4++)
                                {
                                    IPList.Add(i + "." + i2 + "." + i3 + "." + i4);
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int i3 = numbers[2]; i3 <= 255; i3++)
                        {
                            for (int i4 = numbers[3]; i4 <= 255; i4++)
                            {
                                IPList.Add(i + "." + i2 + "." + i3 + "." + i4);
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i2 = numbers[1]; i2 <= 255; i2++)
                {
                    for (int i3 = numbers[2]; i3 <= 255; i3++)
                    {
                        for (int i4 = numbers[3]; i4 <= 255; i4++)
                        {
                            IPList.Add(i + "." + i2 + "." + i3 + "." + i4);
                        }
                    }
                }
            }
        }
        IPRangeList.RemoveAt(0);
        File.WriteAllLines("IPRangeList.txt", IPRangeList);
        _leftRanges.text = IPRangeList.Count.ToString();
    }
    IEnumerator Ping2()
    {
        while(true)
        {
            if(IPList.Count > 0 && countThread < 100)
            {
                countThread++;
                var _thread = new Thread(() =>
                {
                    Ping(IPList[0]);
                });
                _thread.Start();
                StartCoroutine(stopThread(_thread, 2));
            }
            yield return new WaitForSeconds(0.02f);
        }
    }
    IEnumerator stopThread(Thread _thread, int _second)
    {
        yield return new WaitForSeconds(_second);
        _thread.Abort();
        countThread--;
    }
    IEnumerator stopThread2(Thread _thread, int _second)
    {
        yield return new WaitForSeconds(_second);
        _thread.Abort();
        q2++;
        _checkedPorts.text = q2.ToString();
        _portsFound.text = PortCount.ToString();
    }
    void Ping(string IP)
    {
        IPList.RemoveAt(0);
        System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
        PingReply pingReply = ping.Send(IP);
        if (pingReply.Status != IPStatus.TimedOut)
        {
            GoodIP.Add(IP);
        }
    }
    IEnumerator PortScan2()
    {
        while(true)
        {
            if(GoodIP.Count>0)
            {
                var _thread = new Thread(() =>
                {
                    PortScan(GoodIP[0], 37777);
                });
                _thread.Start();
                StartCoroutine(stopThread2(_thread, 10));
            }
            yield return new WaitForSeconds(0.02f);
        }
    }
    void PortScan(string IP, int _port)
    {
        GoodIP.RemoveAt(0);
        Socket s = new Socket(AddressFamily.InterNetwork,
        SocketType.Stream,
        ProtocolType.Tcp);
        try
        {
            s.Connect(IP, _port);
            s.Close();
            File.Create($"IP\\{_port}_" + IP + ".txt");
            PortCount++;
            
        }
        catch (Exception)
        {
            s.Close();
        }
    }
}