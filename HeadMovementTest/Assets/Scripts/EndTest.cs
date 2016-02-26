using UnityEngine;

public class EndTest : MonoBehaviour
{
    void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();//When the Escape key is pressed, quit the application.
        }
    }
}