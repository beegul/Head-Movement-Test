using UnityEngine;
using System;
using System.IO;
using System.Collections;
using UnityEngine.SceneManagement;

public class Log
{
    public string path = "C:\\Users\\Jack\\Documents\\GitHub\\MSc\\HeadMovementTest\\test.txt";//test path to write to.
    public void writetofile(string data)
    {
        File.AppendAllText(path, data + Environment.NewLine);
    }
}
public class Logger : MonoBehaviour
{
    public int participant;

    float time;
    Log logger = new Log();

    void Start ()
    {
        //File.WriteAllText(logger.path, String.Empty);//clears the current file if it already contain a string value (testing only, will create new file for each test. probably.)
        logger.writetofile("participant:   " + participant.ToString());
        logger.writetofile("test started: " + DateTime.Now.ToString());
        logger.writetofile("scene loaded: " + SceneManager.GetActiveScene().name);
    }
	void Update ()
    {
        time = Time.time;
        logger.writetofile(time.ToString());
	}
}
