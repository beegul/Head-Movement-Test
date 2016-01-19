using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTest : MonoBehaviour
{
    void Update ()
    {
        //Quit App.
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}