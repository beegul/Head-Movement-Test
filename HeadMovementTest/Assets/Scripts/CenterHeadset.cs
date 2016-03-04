using UnityEngine;
using System.Collections;

public class CenterHeadset : MonoBehaviour
{
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            OVRManager.display.RecenterPose();
        }        
    }
}
