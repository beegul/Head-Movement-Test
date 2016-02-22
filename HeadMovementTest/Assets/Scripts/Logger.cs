using UnityEngine;
using System;
using System.IO;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class Log
{
    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    DirectoryInfo folder = Directory.CreateDirectory(@"C:\\Particpant Logs");
    public string path = @"C:\\Particpant Logs\\Participant.csv";
    public void writetofile(string data)
    {
        File.AppendAllText(path, data + Environment.NewLine);
    }   
}
public class Logger : MonoBehaviour
{
    int participant = 1;
    Log logger = new Log();
    void Start ()
    {
        DontDestroyOnLoad(this);
        if (GameObject.Find("TestManager").GetComponent<Python>().sensor_connected == true)
        {
            string folder = Path.GetDirectoryName(logger.path);
            string filename = Path.GetFileNameWithoutExtension(logger.path);
            string extension = Path.GetExtension(logger.path);
            int number = 1;
            if (File.Exists(logger.path))
            {
                Match regex = Regex.Match(logger.path, @"(.+) \((\d+)\)\.\w+");
                if (regex.Success)
                {
                    filename = regex.Groups[1].Value;
                    number = int.Parse(regex.Groups[2].Value);
                }
                do
                {
                    participant++;
                    number++;
                    logger.path = Path.Combine(folder, string.Format("{0} {1}{2}", filename, number, extension));
                }
                while (File.Exists(logger.path));
            }
            logger.writetofile("Participant Number: " + participant.ToString() + ",Test started on: " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"));//data coloumns.
        }
    }
    void OnLevelWasLoaded()
    {
        if (GameObject.Find("TestManager").GetComponent<TestManager>().in_task == true)
        {
            logger.writetofile("Current Scene: " + SceneManager.GetActiveScene().name + ",Task started at: " + DateTime.UtcNow.ToString("hh:mm:ss tt"));
            logger.writetofile("Time, Heading, Roll, Pitch, Sys_cal, Gyro_cal, Accel_cal, Mag_cal");
        }
    }
	void Update ()
    {
        if (GameObject.Find("TestManager").GetComponent<Python>().stream_data == true)//checks to see if the bool in the python script is true, if so it writes the data from the "myString" in the python script to the log file.
        {
            logger.writetofile(Time.timeSinceLevelLoad.ToString() + GameObject.Find("TestManager").GetComponent<Python>().python_output);
        }
    }
}
