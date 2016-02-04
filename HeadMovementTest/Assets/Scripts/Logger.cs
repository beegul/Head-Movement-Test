using UnityEngine;
using System;
using System.IO;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class Log
{
    public string path = "C:\\Users\\Jack\\Documents\\GitHub\\MSc\\HeadMovementTest\\Participant.csv";//this is the first file that will be wrote to.
    public void writetofile(string data)
    {
        File.AppendAllText(path, data + Environment.NewLine);
    }   
}
public class Logger : MonoBehaviour
{
    int participant = 1;

    float time;
    Log logger = new Log();

    void Start ()
    {
        DontDestroyOnLoad(this);
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
        logger.writetofile("participant,test started,sceneloaded,scenetime");//data coloumns.
        logger.writetofile(participant.ToString() + "," + "=\"" + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "\"");//participant number and time of test start.
    }
    void OnLevelWasLoaded()
    {
        if(SceneManager.GetActiveScene().name != "StartScreen")
        {
            logger.writetofile(",," + SceneManager.GetActiveScene().name + "," + "=\"" + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "\"");
        }
    }
	void Update ()
    {
        time = Time.time;
        //logger.writetofile(time.ToString()); write the current time to the log file every update loop.
	}
}
