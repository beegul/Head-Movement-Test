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

        p.StartInfo.RedirectStandardError = true;
        p.StartInfo.RedirectStandardOutput = true;
        p.StartInfo.CreateNoWindow = true;

        p.StartInfo.WorkingDirectory = @"C:\Python\Scripts";
        p.StartInfo.UseShellExecute = false;

        p.Start();

        UnityEngine.Debug.Log(p.StandardOutput.ReadToEnd());
        UnityEngine.Debug.Log("Python Started");
        p.WaitForExit();
        p.Close();
    }
    void Start ()
    {
        testing();
    }

}
