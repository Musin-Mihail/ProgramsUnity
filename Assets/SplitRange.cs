using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Linq;
using System.IO;
public class SplitRange : MonoBehaviour
{
    void Start()
    {
        string oldIp = "";
        string newIp = "";
        string[] AllRange = File.ReadAllLines("DBIP.txt");
        foreach (string item in AllRange)
        {
            List<string> numbers2 = item.Split(',').ToList();
            List<int> startIp = IPAddress.Parse(numbers2[0]).ToString().Split('.').Select(Int32.Parse).ToList();
            List<int> endIp = IPAddress.Parse(numbers2[1]).ToString().Split('.').Select(Int32.Parse).ToList();
            oldIp = IPAddress.Parse(numbers2[0]).ToString();
            if(startIp[1] != endIp[1])
            {
                int count = endIp[1] - startIp[1];
                for (int i = 0; i < count + 1; i++)
                {
                    if(i == count)
                    {
                        newIp = startIp[0] + "." + (startIp[1]+i) + "." + endIp[2] + "." + endIp[3];
                        File.AppendAllText(numbers2[3] + ".txt", oldIp + "." + newIp + "\n");
                        oldIp = startIp[0] + "." + (startIp[1]+i+1) + "." + 0 + "." + 0;
                    }
                    else
                    {
                        newIp = startIp[0] + "." + (startIp[1]+i) + "." + 255 + "." + 255;
                        File.AppendAllText(numbers2[3] + ".txt", oldIp + "." + newIp + "\n");
                        oldIp = startIp[0] + "." + (startIp[1]+i+1) + "." + 0 + "." + 0;
                    }
                }
            }
            else
            {
                File.AppendAllText(numbers2[3] + ".txt", IPAddress.Parse(numbers2[0]).ToString() + "." + IPAddress.Parse(numbers2[1]).ToString() + "\n");
            }            
        }
    }
}