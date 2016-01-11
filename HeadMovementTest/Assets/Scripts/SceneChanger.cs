using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VR;


public class SceneChanger : MonoBehaviour
{
    void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown("1"))
        {
            VRSettings.enabled = false;//disables the VR for non-VR scenes.
            SceneManager.LoadScene("Test 1");
        }
        if (Input.GetKeyDown("2"))
        {
            VRSettings.enabled = false;
            SceneManager.LoadScene("Test 2");
        }
        if (Input.GetKeyDown("3"))
        {
            VRSettings.enabled = false;
            SceneManager.LoadScene("Test 3");
        }

        //VR Scenes
        if (Input.GetKeyDown("4"))
        {
            VRSettings.enabled = true;//enables the VR for VR scenes.
            SceneManager.LoadScene("Test 1VR");
        }
        if (Input.GetKeyDown("5"))
        {
            VRSettings.enabled = true;
            SceneManager.LoadScene("Test 2VR");
        }
        if (Input.GetKeyDown("6"))
        {
            VRSettings.enabled = true;
            SceneManager.LoadScene("Test 3VR");
        }

    }
}