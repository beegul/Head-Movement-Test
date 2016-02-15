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

    public bool sensor_connected;//this is used to display the "sensor not connected" text from ConnectionStatus.cs
    public bool stream_data;//this is enabled and disables in the TestManager.cs script, allowing data to only be read when the partcipants are completing the tasks.

    void Start ()
    {
        stream_data = false;

        string python = "python";
        //string python_script = @"Python/sensor.py";
        string python_script = Application.persistentDataPath + "\\sensor.py";
        //UnityEngine.Debug.Log(python_script);

        myProcessStartInfo = new ProcessStartInfo(python);
        myProcessStartInfo.UseShellExecute = false;
        myProcessStartInfo.RedirectStandardOutput = true;
        myProcessStartInfo.CreateNoWindow = true;

        myProcessStartInfo.Arguments = python_script;

        myProcess = new Process();
        myProcess.StartInfo = myProcessStartInfo;

        myProcess.Start();

        myStreamReader = myProcess.StandardOutput;
        myString = myStreamReader.ReadLine();
        if (String.IsNullOrEmpty(myString) == true)
        {
            sensor_connected = false;//sets this to false, which displays the text in ConnectionStatus.cs
        }
        else
        {
            sensor_connected = true;//need to trigger outside this function, it is only being called when stream_data is set to ture in the test manager script.
        }
    }

    void check_connection()
    {
        //UnityEngine.Debug.Log("Checking connection");
        if(String.IsNullOrEmpty(myString) == true)
        {
            sensor_connected = false;
        }
    }
    void get_data()
    {
        myStreamReader = myProcess.StandardOutput;
        myString = myStreamReader.ReadLine();//we are always logging acclerometer data.
    }
	void Update ()
    {
        check_connection();
        if(stream_data == true)
        {
            //UnityEngine.Debug.Log("Data stream true, logging data");
            get_data();
        }
        //else
        //{
            //UnityEngine.Debug.Log("Data stream not true, not logging data");
        //}
    }
    void OnApplicationQuit()
    {
        myProcess.Close();
    }
}
