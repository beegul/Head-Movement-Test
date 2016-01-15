using UnityEngine;
using System.Collections;
using UnityEngine.VR;

public class DisableVR : MonoBehaviour
{
	void Awake ()
    {
        VRSettings.enabled = false;
    }
}
