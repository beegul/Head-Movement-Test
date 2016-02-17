using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.IO.Ports;
using System.Diagnostics;
using UnityEngine.UI;

public class Python : MonoBehaviour
{
    ProcessStartInfo python_info;
    Process python_process;
    StreamReader python_streamreader;
    public string python_output;

    public bool sensor_connected;//this is used to display the "sensor not connected" text from ConnectionStatus.cs
    public bool stream_data;//this is enabled and disables in the TestManager.cs script, allowing data to only be read when the partcipants are completing the tasks.
    public bool com3_connected;

    string[] ports;//array of ports;
    string port = "COM3";//port acceleromter will connect to.

    TextAsset sensor;
    void Start ()
    {
        stream_data = false;
         
        string python = "python";

        sensor = (TextAsset)Resources.Load("sensor", typeof(TextAsset));//loads in the text file containing the python code.
        string python_script = "-c \"" + sensor.text + "\"";//adds the python compile keys.

        python_info = new ProcessStartInfo(python);
        python_info.UseShellExecute = false;//false
        python_info.RedirectStandardOutput = true;//true
        python_info.CreateNoWindow = true;//true
        python_info.Arguments = python_script;
        python_process = new Process();
        python_process.StartInfo = python_info;
        python_process.Start();
        python_streamreader = python_process.StandardOutput;
        python_output = python_streamreader.ReadLine();

        ports = SerialPort.GetPortNames();

        if (String.IsNullOrEmpty(python_output) == true)//intial check to see if data is being streamed from the sensor.
        {
            //UnityEngine.Debug.Log("No Python data stream.");
            sensor_connected = false;//sets this to false, which displays the text in ConnectionStatus.cs
        }
        else
        {
            sensor_connected = true;
        }

        if (Array.IndexOf(ports, port) < 0)
        {
            UnityEngine.Debug.Log("COM3 not connected");
            sensor_connected = false;
            com3_connected = false;
        }
        else
        {
            com3_connected = true;
        }
    }
    void check_connection()
    {
        ports = SerialPort.GetPortNames();
        if (String.IsNullOrEmpty(python_output) == true)
        {
            sensor_connected = false;
        }
        else
        {
            sensor_connected = true;
        }
        if(Array.IndexOf(ports, port) < 0)
        {
            //UnityEngine.Debug.Log("COM3 not connected");
            sensor_connected = false;
            com3_connected = false;
        }
        else
        {
            com3_connected = true;
        }
    }
    void get_data()
    {
        python_streamreader = python_process.StandardOutput;
        python_output = python_streamreader.ReadLine();//we are always logging acclerometer data.
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
        python_process.Close();
    }
}
