using UnityEngine;
using UnityEngine.VR;
using System.Collections;

public class EnableVR : MonoBehaviour
{
    void Awake()
    {
        VRSettings.enabled = true;
    }
}
