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
        string[] elements = new [] { "a", "b", "c", "d", "e", "f", "g", "h" };
        foreach (var item in elements)
        {
            Debug.Log(item);
        }
        List<string> cauntry = new List<string>();
        List<string> SplitRange = new List<string>();
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

                // Debug.Log(startIp[0] + "." + startIp[1] + "." + startIp[2] + "." + startIp[3]);
                // Debug.Log(endIp[0] + "." + endIp[1] + "." + endIp[2] + "." + endIp[3]);

                oldIp = IPAddress.Parse(numbers2[0]).ToString();
                // if(startIp[0] != endIp[0])
                // {
                //     Debug.Log(startIp[0] + "." + startIp[1] + "." + startIp[2] + "." + startIp[3]);
                //     Debug.Log(endIp[0] + "." + endIp[1] + "." + endIp[2] + "." + endIp[3]);
                //     Debug.Log(endIp[0] - startIp[0]);
                // }
                if(startIp[1] != endIp[1])
                {
                    Debug.Log(startIp[0] + "." + startIp[1] + "." + startIp[2] + "." + startIp[3]);
                    Debug.Log(endIp[0] + "." + endIp[1] + "." + endIp[2] + "." + endIp[3]);
                    int count = endIp[1] - startIp[1];
                    Debug.Log(count);
                    for (int i = 0; i < count + 1; i++)
                    {
                        if(i == count)
                        {
                            newIp = startIp[0] + "." + (startIp[1]+i) + "." + endIp[2] + "." + endIp[3];
                            SplitRange.Add(oldIp + " - " + newIp + ";" + numbers2[4]);
                            oldIp = startIp[0] + "." + (startIp[1]+i+1) + "." + 0 + "." + 0;
                        }
                        else
                        {
                            newIp = startIp[0] + "." + (startIp[1]+i) + "." + 255 + "." + 255;
                            SplitRange.Add(oldIp + " - " + newIp + ";" + numbers2[4]);
                            oldIp = startIp[0] + "." + (startIp[1]+i+1) + "." + 0 + "." + 0;
                        }
                    }
                }
                else
                {
                    SplitRange.Add(IPAddress.Parse(numbers2[0]).ToString() + ";" + IPAddress.Parse(numbers2[1]).ToString() + ";" + numbers2[4]);
                }            
                // if(startIp[2] != endIp[2])
                // {
                //     int count = endIp[2] - startIp[2];
                //     Debug.Log(count);
                //     for (int i = 0; i < count+1; i++)
                //     {
                //         newIp = startIp[0] + "." + startIp[1] + "." + (startIp[2] + i) + "." + 255;
                //         SplitRange.Add(oldIp + " - " + newIp);
                //         oldIp = startIp[0] + "." + startIp[1] + "." + (startIp[2] + i) + "." + 0;
                //     }
                // }
                // if(startIp[3] != endIp[3])
                // {
                    // Debug.Log(endIp[3] - startIp[3]);
                // }
            }
        }
        File.WriteAllLines("FR.txt", SplitRange);
    }
}