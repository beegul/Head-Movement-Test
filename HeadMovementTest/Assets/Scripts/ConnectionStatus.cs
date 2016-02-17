using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VR;
using System.Collections;

public class ConnectionStatus : MonoBehaviour
{
    public Text AccelerometerStatus;
    public Text VRStatus;
    public Text COM3Status;

	void Start()
    {
        AccelerometerStatus.enabled = true;
        VRStatus.enabled = true;
        COM3Status.enabled = true;
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
        if(GameObject.Find("TestManager").GetComponent<Python>().com3_connected == true)
        {
            COM3Status.enabled = false;
        }
        if (GameObject.Find("TestManager").GetComponent<Python>().com3_connected == false)
        {
            COM3Status.enabled = true;
        }
    }
}
