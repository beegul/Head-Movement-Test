using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Diagnostics;
using UnityEngine.UI;

public class Python : MonoBehaviour
{
    ProcessStartInfo myProcessStartInfo;
    Process myProcess;
    StreamReader myStreamReader;
    public string myString;

    public bool sensor_connected = false;
    public bool stream_data;//will enable/disable the data stream to the log file. allowing only data during the tests to be logged.

    void Start ()
    {
        stream_data = false;

        string python = "python";
        string python_script = @"Python/sensor.py";

        myProcessStartInfo = new ProcessStartInfo(python);
        myProcessStartInfo.UseShellExecute = false;
        myProcessStartInfo.RedirectStandardOutput = true;
        myProcessStartInfo.CreateNoWindow = true;

        myProcessStartInfo.Arguments = python_script;

        myProcess = new Process();
        myProcess.StartInfo = myProcessStartInfo;

        myProcess.Start();
    }
    void get_data()
    {
        myStreamReader = myProcess.StandardOutput;
        myString = myStreamReader.ReadLine();

        if (String.IsNullOrEmpty(myString) == true)
        {
            sensor_connected = false;
            //myString = "If the sensor was connected, this would be logging data";
            //UnityEngine.Debug.Log("Data not recived from sensor, please check connection");
        }
        else
        {
            sensor_connected = true;
            UnityEngine.Debug.Log(myString);
        }
    }
	void Update ()
    {
        if(stream_data == true)
        {
            UnityEngine.Debug.Log("Data stream true, logging data");
            get_data();
        }
        else
        {
            UnityEngine.Debug.Log("Data stream not true, not logging data");
        }
    }
    void OnApplicationQuit()
    {
        myProcess.Close();
    }
}
