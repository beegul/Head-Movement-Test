using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Text.RegularExpressions;

public class Log
{
    private DirectoryInfo DataFolder = Directory.CreateDirectory(@"C:\\Particpant Logs");//Sets where the folder that will contain the partcipant logs are.
    public string Path = @"C:\\Particpant Logs\\Participant.csv";//Sets the name for the first log that will be saved when the test is run.
    public void WriteToFile(string data)//Appends any new data onto the current file, this stop new data overwriting previous information.
    {
        File.AppendAllText(Path, data + Environment.NewLine);
    }   
}
public class Logger : MonoBehaviour
{
    private Log MyLogger = new Log();

    public bool Log_Data = false;

    private int Participant = 1;//The current number of partcipants this log has created, it is incremented the more a log file is succesfully created.
    private int Number;
    private float Task_Time = 0.0f;

    //This is where the information about the file we are saving data to will be assgined.
    private string Folder;
    private string Filename;
    private string Extension;

    void Start ()
    {
        DontDestroyOnLoad(this);//Makes sure that this code persits through scene changes so that it can always log participant data.
        if (GameObject.Find("TestManager").GetComponent<Python>().SensorConnected == true)//Checks if the acceleromter has been connected. If it has not, then the participant file is not created. Stops any useless files being created.
        {
            Folder = Path.GetDirectoryName(MyLogger.Path);
            Filename = Path.GetFileNameWithoutExtension(MyLogger.Path);
            Extension = Path.GetExtension(MyLogger.Path);
            Number = 1;
            if (File.Exists(MyLogger.Path))//Checks to see if a file with the same name already exists within our participant directory. If it does, it increments the name by 1. e.g. Particiant1 exists... create Participant 2 instead.
            {
                Match regex = Regex.Match(MyLogger.Path, @"(.+) \((\d+)\)\.\w+");
                if (regex.Success)
                {
                    Filename = regex.Groups[1].Value;
                    Number = int.Parse(regex.Groups[2].Value);
                }
                do//Always nice to get a do, while loop in ;-)
                {
                    Participant++;
                    Number++;
                    MyLogger.Path = Path.Combine(Folder, string.Format("{0} {1}{2}", Filename, Number, Extension));
                }
                while (File.Exists(MyLogger.Path));
            }
            MyLogger.WriteToFile("Participant Number:," + Participant.ToString());//Writes the initial test data at the top of the file, Particpant Number and Start Date/Time.
            MyLogger.WriteToFile("Test started on:," + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"));
        }
    }
	void FixedUpdate()//Logs data at a fixed interval of 0.2ms to enable uniform logging and analysis.
    {
        if (GameObject.Find("TestManager").GetComponent<Python>().StreamData == true && Log_Data == true)//Checks to see if a data stream from the Python script is true and that Log_Data has been triggered in the update loop before data logging begins.
        {
            MyLogger.WriteToFile(GameObject.Find("TestManager").GetComponent<Python>().PythonOutput + Task_Time.ToString("0.00"));//Writes the output of our Python script to our file, rounds the task time to 2 decimal places to make analysis easier.
        }
    }
    void Update()
    {
        if (GameObject.Find("TestManager").GetComponent<TestManager>().InTask == true)//Checks to see if the participant is currently in a task. If so, we log which task they are doing, and when they began the task.
        {
            Task_Time = 0.0f;//Resets the task duration to zero each time a new task loads so we know the exact duration taken for each task.
            MyLogger.WriteToFile("Current Task:," + GameObject.Find("TestManager").GetComponent<TestManager>().CurrentTask);
            MyLogger.WriteToFile("Task Start:," + DateTime.UtcNow.ToString("hh:mm:ss tt"));
            MyLogger.WriteToFile("Heading, Roll, Pitch, Task Time");
            GameObject.Find("TestManager").GetComponent<TestManager>().InTask = false;//After we have logged this start data we set the bool back to false so it is ready for the next task to set it to true.
            Log_Data = true;
        }
        Task_Time += Time.deltaTime;//Increments the current time spent within the current task.

        if (GameObject.Find("TestManager").GetComponent<Python>().SensorConnected == false)//Deletes the current file if connection is lost with the accelerometer.
        {
            File.Delete(MyLogger.Path);
        }
    }
    void OnApplicationQuit()
    {
        if (SceneManager.GetActiveScene().name != "End Screen")//When the escape button is pressed during the test, the current file being wrote to is deleted. Stops any junk files being kept.
        {
            File.Delete(MyLogger.Path);
        }
    }
}
