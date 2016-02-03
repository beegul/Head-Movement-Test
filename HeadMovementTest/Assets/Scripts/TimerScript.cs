using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerScript : MonoBehaviour
{
    public Text timerText;
    private float startime;

	// Use this for initialization
	void Start ()
    {
        startime = 10;//Time.time;
	}
	
	// Update is called once per frame
	void Update ()
    {
            float t = startime - Time.timeSinceLevelLoad;// by having time since level loaded, the timer resets each scene. 
            string seconds = (t % 60).ToString("f0");
            timerText.text = "Starting in: " + seconds;
	}
}
