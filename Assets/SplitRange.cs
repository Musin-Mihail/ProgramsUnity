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
        // List<string> cauntryName = new List<string>();
        // List<string> SplitRange = new List<string>();
        string oldIp = "";
        string newIp = "";
        string[] AllRange = File.ReadAllLines("DBIP.txt");
        foreach (string item in AllRange)
        {
            List<string> numbers2 = item.Split(',').ToList();
            if (numbers2[2] == "FR")
            {
                List<int> startIp = IPAddress.Parse(numbers2[0]).ToString().Split('.').Select(Int32.Parse).ToList();
                List<int> endIp = IPAddress.Parse(numbers2[1]).ToString().Split('.').Select(Int32.Parse).ToList();
                oldIp = IPAddress.Parse(numbers2[0]).ToString();
                if(startIp[1] != endIp[1])
                {
                    // Debug.Log(startIp[0] + "." + startIp[1] + "." + startIp[2] + "." + startIp[3]);
                    // Debug.Log(endIp[0] + "." + endIp[1] + "." + endIp[2] + "." + endIp[3]);
                    int count = endIp[1] - startIp[1];
                    // Debug.Log(count);
                    for (int i = 0; i < count + 1; i++)
                    {
                        if(i == count)
                        {
                            newIp = startIp[0] + "." + (startIp[1]+i) + "." + endIp[2] + "." + endIp[3];
                            File.AppendAllText(numbers2[3] + ".txt", oldIp + "." + newIp + "\n");
                            // SplitRange.Add(oldIp + " - " + newIp + ";" + numbers2[4]);
                            oldIp = startIp[0] + "." + (startIp[1]+i+1) + "." + 0 + "." + 0;
                        }
                        else
                        {
                            newIp = startIp[0] + "." + (startIp[1]+i) + "." + 255 + "." + 255;
                            File.AppendAllText(numbers2[3] + ".txt", oldIp + "." + newIp + "\n");
                            // SplitRange.Add(oldIp + " - " + newIp + ";" + numbers2[4]);
                            oldIp = startIp[0] + "." + (startIp[1]+i+1) + "." + 0 + "." + 0;
                        }
                    }
                }
                else
                {
                    File.AppendAllText(numbers2[3] + ".txt", IPAddress.Parse(numbers2[0]).ToString() + "." + IPAddress.Parse(numbers2[1]).ToString() + "\n");
                    // SplitRange.Add(IPAddress.Parse(numbers2[0]).ToString() + ";" + IPAddress.Parse(numbers2[1]).ToString() + ";" + numbers2[4]);
                }            
            }
        }
        // File.WriteAllLines("FR.txt", SplitRange);
    }
}