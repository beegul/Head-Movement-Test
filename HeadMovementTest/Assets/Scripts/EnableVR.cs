using UnityEngine;
using UnityEngine.VR;

public class EnableVR : MonoBehaviour
{
    void Awake()
    {
        VRSettings.enabled = true;//Enables VR.
    }
}
