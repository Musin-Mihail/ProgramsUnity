using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convas : MonoBehaviour
{
    public GameObject _convasScanIP;
    public GameObject _canvasMain;
    public GameObject _canvasStatistics;
    bool _convasScanIPBool;
    bool _convasScanFileBool;

    void Start()
    {
        _convasScanIPBool = false;
        _convasScanFileBool = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConvasScanIP()
    {
        if (_convasScanIPBool)
        {
            _convasScanIP.SetActive(false);
            _convasScanIPBool = false;
            _canvasMain.SetActive(true);
        }
        else
        {
            _convasScanIP.SetActive(true);
            _convasScanIPBool = true;
            _canvasMain.SetActive(false);
        }
    }
    public void ConvasScanFile()
    {
        if (_convasScanFileBool)
        {
            _canvasStatistics.SetActive(false);
            _convasScanFileBool = false;
            _canvasMain.SetActive(true);
        }
        else
        {
            _canvasStatistics.SetActive(true);
            _convasScanFileBool = true;
            _canvasMain.SetActive(false);
        }
    }
}
