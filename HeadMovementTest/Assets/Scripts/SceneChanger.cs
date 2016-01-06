using UnityEngine;
using UnityEngine.SceneManagement;

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
            SceneManager.LoadScene("Test 1");
        }
        if (Input.GetKeyDown("2"))
        {
            SceneManager.LoadScene("Test 2");
        }
        if (Input.GetKeyDown("3"))
        {
            SceneManager.LoadScene("Test 3");
        }
    }
}