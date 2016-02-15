using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VR;
using System.Collections;

public class ConnectionStatus : MonoBehaviour
{
    public Text AccelerometerStatus;
    public Text VRStatus;

	void Start()
    {
        DontDestroyOnLoad(this);
        AccelerometerStatus.enabled = true;
        VRStatus.enabled = true;
    }
	void Update ()
    {
        if(GameObject.Find("TestManager").GetComponent<Python>().sensor_connected == true)
        {
            AccelerometerStatus.enabled = false;
        }
        if(GameObject.Find("TestManager").GetComponent<Python>().sensor_connected == false)
        {
            AccelerometerStatus.enabled = true;
        }

        if (VRSettings.loadedDevice != VRDeviceType.None)
        {
            VRStatus.enabled = false;
        }
        if (VRSettings.loadedDevice == VRDeviceType.None)
        {
            VRStatus.enabled = true;
        }
    }
}
