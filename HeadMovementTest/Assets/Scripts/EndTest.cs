using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTest : MonoBehaviour
{
    void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}