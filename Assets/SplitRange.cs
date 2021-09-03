using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Linq;
using System.IO;
public class SplitRange : MonoBehaviour
{
    List<string> ip100 = new List<string>();
    List<string> ip1000 = new List<string>();
    List<string> ip10000 = new List<string>();
    List<string> ip60000 = new List<string>();
    List<string> ip66666 = new List<string>();
    void Start()
    {
        string[] AllRange = File.ReadAllLines("IPRangeList.txt");
        foreach (var item in AllRange)
        {
            Split(item);
        }
        File.WriteAllLines("ip100.txt", ip100);
        File.WriteAllLines("ip1000.txt", ip1000);
        File.WriteAllLines("ip10000.txt", ip10000);
        File.WriteAllLines("ip60000.txt", ip60000);
        File.WriteAllLines("ip66666.txt", ip66666);

        // сортировка базы по странам
        // string oldIp = "";
        // string newIp = "";
        // string[] AllRange = File.ReadAllLines("DBIP.txt");
        // foreach (string item in AllRange)
        // {
        //     List<string> numbers2 = item.Split(',').ToList();
        //     List<int> startIp = IPAddress.Parse(numbers2[0]).ToString().Split('.').Select(Int32.Parse).ToList();
        //     List<int> endIp = IPAddress.Parse(numbers2[1]).ToString().Split('.').Select(Int32.Parse).ToList();
        //     oldIp = IPAddress.Parse(numbers2[0]).ToString();
        //     if(startIp[1] != endIp[1])
        //     {
        //         int count = endIp[1] - startIp[1];
        //         for (int i = 0; i < count + 1; i++)
        //         {
        //             if(i == count)
        //             {
        //                 newIp = startIp[0] + "." + (startIp[1]+i) + "." + endIp[2] + "." + endIp[3];
        //                 File.AppendAllText("IPRangeList.txt", oldIp + "." + newIp + "\n");
        //                 oldIp = startIp[0] + "." + (startIp[1]+i+1) + "." + 0 + "." + 0;
        //             }
        //             else
        //             {
        //                 newIp = startIp[0] + "." + (startIp[1]+i) + "." + 255 + "." + 255;
        //                 File.AppendAllText("IPRangeList.txt", oldIp + "." + newIp + "\n");
        //                 oldIp = startIp[0] + "." + (startIp[1]+i+1) + "." + 0 + "." + 0;
        //             }
        //         }
        //     }
        //     else
        //     {
        //         File.AppendAllText("IPRangeList.txt", IPAddress.Parse(numbers2[0]).ToString() + "." + IPAddress.Parse(numbers2[1]).ToString() + "\n");
        //     }            
        // }
    }
    void Split(string IPstring)
    {
        List<int> IP = IPstring.Split('.').Select(Int32.Parse).ToList();
        int countIP = 0;
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
                                    countIP++;
                                }
                            }
                            else
                            {
                                for (int i4 = IP[3]; i4 <= 255; i4++)
                                {
                                    countIP++;
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
                                countIP++;
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
                            countIP++;
                        }
                    }
                }
            }
        }
        if(countIP < 100)
        {
            ip100.Add(IPstring);
        }
        else if(countIP < 1000)
        {
            ip1000.Add(IPstring);
        }
        else if(countIP < 10000)
        {
            ip10000.Add(IPstring);
        }
        else if(countIP < 60000)
        {
            ip60000.Add(IPstring);
        }
        else
        {
            ip66666.Add(IPstring);
        }
    }
}