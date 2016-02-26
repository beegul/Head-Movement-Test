using UnityEngine;
using UnityEngine.VR;

public class DisableVR : MonoBehaviour
{
	void Awake()
    {
        VRSettings.enabled = false;//Disables VR.
    }
}
