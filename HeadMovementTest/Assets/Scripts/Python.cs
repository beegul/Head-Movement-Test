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
    string port = "COM3";//port accelerometer will connect to.

    TextAsset sensor;
    void Start ()
    {
        stream_data = false;
                
        string python = "python";

        sensor = (TextAsset)Resources.Load("sensor", typeof(TextAsset));//loads in the text file containing the python code.
        string python_script = "-c \"" + sensor.text + "\"";//adds the python compile keys.

        python_info = new ProcessStartInfo(python);
        python_info.UseShellExecute = false;
        python_info.RedirectStandardOutput = true;
        python_info.CreateNoWindow = true;
        python_info.Arguments = python_script;

        python_process = new Process();
        python_process.StartInfo = python_info;
        python_process.Start();

        python_streamreader = python_process.StandardOutput;
        python_output = python_streamreader.ReadLine();

        ports = SerialPort.GetPortNames();

        if (String.IsNullOrEmpty(python_output) == true)//initial check to see if data is being streamed from the sensor.
        {
            sensor_connected = false;
        }
        else
        {
            sensor_connected = true;
        }

        if (Array.IndexOf(ports, port) < 0)
        {
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
        python_output = python_streamreader.ReadLine();//we are always logging data.
    }
	void Update ()
    {
        check_connection();
        if(stream_data == true)
        {
            get_data();
        }
    }
    void OnApplicationQuit()
    {
        python_process.Close();
    }
}
