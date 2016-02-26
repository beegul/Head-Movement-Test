using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VR;

public class ConnectionStatus : MonoBehaviour
{
    public Text AccelerometerStatus;
    public Text VRStatus;
    public Text COM3Status;

	void Start()
    {
        //Set to true to display before connection status has been confirmed. "Guilty until proven innocent" in this case!
        AccelerometerStatus.enabled = true;
        VRStatus.enabled = true;
        COM3Status.enabled = true;
    }
	void Update ()
    {
        //Enables or Disables the Accelerometer connection status. Boolean data is received from the "Python" script which checks the connection.
        if(GameObject.Find("TestManager").GetComponent<Python>().SensorConnected == true)
        {
            AccelerometerStatus.enabled = false;
        }
        if(GameObject.Find("TestManager").GetComponent<Python>().SensorConnected == false)
        {
            AccelerometerStatus.enabled = true;
        }
        //Enables or Disables the VR Headset connection status. This script checks the connection to the headset directly.
        if (VRSettings.loadedDevice != VRDeviceType.None)
        {
            VRStatus.enabled = false;
        }
        if (VRSettings.loadedDevice == VRDeviceType.None)
        {
            VRStatus.enabled = true;
        }
        //Enables or Disables the COM3 connection status. Boolean data is received from the "Python" script which checks the connection.
        if (GameObject.Find("TestManager").GetComponent<Python>().COMConnected == true)
        {
            COM3Status.enabled = false;
        }
        if (GameObject.Find("TestManager").GetComponent<Python>().COMConnected == false)
        {
            COM3Status.enabled = true;
        }
    }
}
