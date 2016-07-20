using UnityEngine;
using System.Collections;

public class CampfireAudio : MonoBehaviour
{
    int LeftIndex;
    int RightIndex;

    AudioSource Audio;

    void Start ()
    {
        LeftIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
        RightIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);

        Audio = GetComponent<AudioSource>();
        Audio.Play();
    }
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space) || SteamVR_Controller.Input(LeftIndex).GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) || SteamVR_Controller.Input(RightIndex).GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Audio.Stop();
        }
    }
}
