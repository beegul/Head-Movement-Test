using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VR;


public class SceneChanger : MonoBehaviour
{
    public int firsttask;
    public int secondtask;
    public int thirdtask;
    public int fourthtask;
    public int fifthtask;
    public int sixthtask;
    public int seventhtask;
    public int eigthtask;
    public int ninthtask;

    string VRTask1 = "VR Task 1";
    string NVRTask1 = "NVR Task 1";
    string NVRWTask1 = "NVRW Task 1";

    string VRTask2 = "VR Task 2";
    string NVRTask2 = "NVR Task 2";
    string NVRWTask2 = "NVRW Task 2";

    string VRTask3 = "VR Task 3";
    string NVRTask3 = "NVR Task 3";
    string NVRWTask3 = "NVRW Task 3";

    void Update ()
    {
        //Quit App.
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        //No Headset Tasks.
        if (Input.GetKeyDown("1"))
        {
            VRSettings.enabled = false;//disables the VR for non-VR scenes.
            SceneManager.LoadScene(NVRTask1);
        }
        if (Input.GetKeyDown("2"))
        {
            VRSettings.enabled = false;
            SceneManager.LoadScene(NVRTask2);
        }
        if (Input.GetKeyDown("3"))
        {
            VRSettings.enabled = false;
            SceneManager.LoadScene(NVRTask3);
        }

        //VR Headset Tasks.
        if (Input.GetKeyDown("4"))
        {
            VRSettings.enabled = true;//enables the VR for VR scenes.
            SceneManager.LoadScene(VRTask1);
        }
        if (Input.GetKeyDown("5"))
        {
            VRSettings.enabled = true;
            SceneManager.LoadScene(VRTask2);
        }
        if (Input.GetKeyDown("6"))
        {
            VRSettings.enabled = true;
            SceneManager.LoadScene(VRTask3);
        }

        //Weighted Headset Tasks.
        if (Input.GetKeyDown("7"))
        {
            VRSettings.enabled = false;
            SceneManager.LoadScene(NVRWTask1);
        }
        if (Input.GetKeyDown("8"))
        {
            VRSettings.enabled = false;
            SceneManager.LoadScene(NVRWTask2);
        }
        if (Input.GetKeyDown("9"))
        {
            VRSettings.enabled = false;
            SceneManager.LoadScene(NVRWTask3);
        }

    }
}