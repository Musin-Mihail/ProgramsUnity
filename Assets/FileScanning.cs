using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class FileScanning : MonoBehaviour
{
    int _time;
    int _timeTemp = 0;
    string[] allfiles;
    TimeSpan ts;
    public Text _dataUI;
    public Text _alltimeUI;
    public Text _nowTimeUI;
    DateTime _dateTime;
    int _countFile;
    int _check;
    void Start()
    {
        _dateTime = DateTime.Now;
        allfiles = Directory.GetFiles(@"c:\github\Drawing\", "*.*", SearchOption.AllDirectories);
        _countFile = allfiles.Length;
        _time = int.Parse(File.ReadAllText("DrawingTime.txt"));
        _dataUI.text = _dateTime.ToString("g");
        ts = TimeSpan.FromSeconds(_time);
        _alltimeUI.text = ts.Days + " д. " + ts.Hours + " ч. " + ts.Minutes + " м. " + ts.Minutes + " с.";
        ts = TimeSpan.FromSeconds(_timeTemp);
        _nowTimeUI.text = ts.Days + " д. " + ts.Hours + " ч. " + ts.Minutes + " м. " + ts.Minutes + " с.";
        StartCoroutine(Scan());
    }
    IEnumerator Scan()
    {
        while(true)
        {
            yield return new WaitForSeconds (120);
            _check = 0;
            allfiles = Directory.GetFiles(@"c:\github\Drawing\", "*.*", SearchOption.AllDirectories);
            var DT= DateTime.Now;
            if(allfiles.Length != _countFile)
            {
                File.AppendAllText("DrawingLog.txt", Environment.NewLine + DT.ToString("g") + ": Количество файлов изменилось");
                _check = 1;
            }
            foreach (var item in allfiles)
            {
                if(File.GetLastWriteTime(item) > _dateTime)
                {
                    File.AppendAllText("DrawingLog.txt", Environment.NewLine + DT.ToString("g") + ": Файл " + item + " Изменился");
                    _check = 1;
                }
            }
            if(_check == 1)
            {
                Time();
            }
            _countFile = allfiles.Length;
            _dateTime = DateTime.Now;
        }
    }
    void Time()
    {
        _time += 120;
        _timeTemp += 120;
        ts = TimeSpan.FromSeconds(_time);
        _alltimeUI.text = ts.Days + " д. " + ts.Hours + " ч. " + ts.Minutes + " м. " + ts.Minutes + " с.";
        ts = TimeSpan.FromSeconds(_timeTemp);
        _nowTimeUI.text = ts.Days + " д. " + ts.Hours + " ч. " + ts.Minutes + " м. " + ts.Minutes + " с.";
        File.WriteAllText("DrawingTime.txt", _time.ToString());
    }     
}