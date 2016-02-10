using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Diagnostics;

public class Python : MonoBehaviour
{
    ProcessStartInfo myProcessStartInfo;
    Process myProcess;
    StreamReader myStreamReader;

    void Start ()
    {
        string python = "python";
        string python_script = @"no_sensor_test.py";

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
        string myString = myStreamReader.ReadLine();

        if(myString != null || myString != " ")
        {
            UnityEngine.Debug.Log(myString);
        }
    }

    void OnApplicationQuit()
    {
        myProcess.Close();
    }
}
