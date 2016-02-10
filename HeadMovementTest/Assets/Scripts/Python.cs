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
    string myString;

    void Start ()
    {
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
	void Update ()
    {
        myStreamReader = myProcess.StandardOutput;
        myString = myStreamReader.ReadLine();

        if (myString != null || myString != " ")
        {
            UnityEngine.Debug.Log(myString);
        }
    }
    void OnApplicationQuit()
    {
        myProcess.Close();
    }
}
