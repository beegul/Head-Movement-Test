using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConnectionStatus : MonoBehaviour
{
    public Text ConnectionStatusText;

	void Start ()
    {
        DontDestroyOnLoad(this);
        ConnectionStatusText.enabled = false;
    }
	

	void Update ()
    {
        if(GameObject.Find("TestManager").GetComponent<Python>().sensor_connected == true)
        {
            ConnectionStatusText.enabled = false;
        }
        else
        {
            ConnectionStatusText.enabled = true;
        }
	
	}
}
