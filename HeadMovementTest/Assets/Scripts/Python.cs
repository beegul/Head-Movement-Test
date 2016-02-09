using UnityEngine;
using UnityEditor;
using System.Text;
using System.Collections;
using System;
using System.IO;
using System.Diagnostics;
public class Python : MonoBehaviour
{   
    Process myProcess;
    StreamReader myStreamReader;
    // Use this for initialization
    void Start ()
    {
        UnityEngine.Debug.Log("python started");
        // full path of python interpreter
        string python = "python"; //TODO somehow make it check that python is installed and prompt?

        // python app to call
        string myPythonApp = @"C:\Python\Scripts\sensor.py"; //TODO make it local to this project

        // dummy parameters to send Python script
        //int x = 2;
        //int y = 5;

        // Create new process start info
        ProcessStartInfo myProcessStartInfo = new ProcessStartInfo(python);

        // make sure we can read the output from stdout
        myProcessStartInfo.UseShellExecute = false;
        myProcessStartInfo.RedirectStandardError = true;
        myProcessStartInfo.RedirectStandardOutput = true;
        myProcessStartInfo.CreateNoWindow = false;

        //myProcessStartInfo.Arguments = "-c \"print('hello')\"";
        myProcessStartInfo.Arguments = myPythonApp;

        myProcess = new Process();
        // assign start information to the process
        myProcess.StartInfo = myProcessStartInfo;

        UnityEngine.Debug.Log("Starting Python script");
        myProcess.Start();
        myStreamReader = myProcess.StandardOutput;

    }

    void Update ()
    {
        //UnityEngine.Debug.Log("Getting line from Python script");

        //TODO repeat until no more lines
        int count = 0;
        //UnityEngine.Debug.Log("myStreamReader: " + myStreamReader);
        //UnityEngine.Debug.Log("peek: " + myStreamReader.Peek());
        string line;
        while ((line = myStreamReader.ReadLine()) != null) //TODO borken myStreamReader.Peek() > -1
        {
            string myString = myStreamReader.ReadLine();
            UnityEngine.Debug.Log(count++ + "  STDOUT: " + myString);
            //myString = myStreamReader.ReadLine();
        }
    }

    void OnApplicationQuit() //TODO not be called??
    {
        UnityEngine.Debug.Log("Closing Python");
        myProcess.Close();
        UnityEngine.Debug.Log("Closing Python (SUCCCES)");

    }
}