using UnityEngine;
using System;
using System.IO;
using System.IO.Ports;
using System.Diagnostics;

public class Python : MonoBehaviour
{
    private ProcessStartInfo PythonInfo;
    private Process PythonProcess;
    private StreamReader PythonStreamreader;
    private string PythonExe;
    private string PythonScript;
    public string PythonOutput;

    public bool SensorConnected = false;//This is used to display the "sensor not connected" text from ConnectionStatus.cs
    public bool StreamData = false;//This is enabled and disabled in the TestManager.cs script, allowing data to only be read when the partcipants are completing the tasks.
    public bool COMConnected = false;//This is toggled true and false depending on if the acceleromter is connected to the correct COM port. In this case, it needs to be connected to COM3.

    private string[] Ports;//This array will save all COM connected devices, allowing us to scan through it and find out device.
    private string Port = "COM3";

    private TextAsset Sensor;//This is what we will save our Python script into, as a .txt file.

    void Start ()
    {
        StreamData = false;              
        PythonExe = "python";

        Sensor = (TextAsset)Resources.Load("sensor", typeof(TextAsset));//Loads in the text file containing the python code.
        PythonScript = "-c \"" + Sensor.text + "\"";//Adds the Python compile keys.

        PythonInfo = new ProcessStartInfo(PythonExe);
        PythonInfo.UseShellExecute = false;//Stops the Python shell being opened.
        PythonInfo.RedirectStandardOutput = true;//Redirects the output to Unity so it can be logged.
        PythonInfo.CreateNoWindow = true;//Stop a window being opened to show output.
        PythonInfo.Arguments = PythonScript;//Passes our script to our Python process.

        PythonProcess = new Process();
        PythonProcess.StartInfo = PythonInfo;
        PythonProcess.Start();

        PythonStreamreader = PythonProcess.StandardOutput;
        PythonOutput = PythonStreamreader.ReadLine();

        Ports = SerialPort.GetPortNames();

        if (String.IsNullOrEmpty(PythonOutput) == true)//Initial check to see if data is being streamed from the sensor.
        {
            SensorConnected = false;
        }
        else
        {
            SensorConnected = true;
        }
        if (Array.IndexOf(Ports, Port) < 0)//Initial check to see if our sensor is connected to the correct COM port.
        {
            SensorConnected = false;
            COMConnected = false;
        }
        else
        {
            COMConnected = true;
        }
    }
    void check_connection()//Constantly checks to see of the connection to the sensor is still there.
    {
        Ports = SerialPort.GetPortNames();
        if (String.IsNullOrEmpty(PythonOutput) == true)
        {
            SensorConnected = false;
        }
        else
        {
            SensorConnected = true;
        }
        if(Array.IndexOf(Ports, Port) < 0)
        {
            SensorConnected = false;
            COMConnected = false;
        }
        else
        {
            COMConnected = true;
        }
    }
    void get_data()
    {
        PythonStreamreader = PythonProcess.StandardOutput;//Send the sensor output to the stream reader, then assigns it to our string.
        PythonOutput = PythonStreamreader.ReadLine();
    }
	void Update ()
    {
        check_connection();
        if(StreamData == true)//If the TaskManager is asking for data to be streamed, enable data to be logged from the Accelerometer.
        {
            get_data();
        }
    }
    void OnApplicationQuit()//When the program closes, we stop the Python process. 
    {
        PythonProcess.Close();
    }
}
