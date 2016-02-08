using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class Python : MonoBehaviour
{
    public static void testing()
    {
        Process p = new Process();
        p.StartInfo.FileName = "python";
        p.StartInfo.Arguments = "sensor.py";

        p.StartInfo.WorkingDirectory = @"C:\Python\Scripts";
        p.StartInfo.UseShellExecute = false;

        p.Start();

        UnityEngine.Debug.Log(p.StandardOutput.ReadToEnd());
        p.WaitForExit();
        p.Close();
    }
    // Use this for initialization
    void Start ()
    {
        testing();
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
