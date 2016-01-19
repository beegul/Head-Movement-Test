using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TestManager : MonoBehaviour
{
    private bool isrunning;

    public int VR_first;
    public int VR_second;
    public int VR_third;

    public int NVR_first;
    public int NVR_second;
    public int NVR_third;

    public int NVRW_first;
    public int NVRW_second;
    public int NVRW_third;

    bool pause = false;//toggles true or false to stop the program at its current task.
    IEnumerator VR_Phase()//this automates the tests for me.
    {
        isrunning = true;
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(11);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(VR_first);
        pause = true;
        while (pause == true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                pause = false;
            }
            yield return null;
        }
        SceneManager.LoadScene(15);

        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(VR_second);
        pause = true;
        while (pause == true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                pause = false;
            }
            yield return null;
        }
        SceneManager.LoadScene(15);

        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(VR_third);
        pause = true;
        while (pause == true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                pause = false;
            }
            yield return null;
        }
        SceneManager.LoadScene(14);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(13);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(NVR_first);
        pause = true;
        while (pause == true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                pause = false;
            }
            yield return null;
        }
        SceneManager.LoadScene(13);

        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(NVR_second);
        pause = true;
        while (pause == true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                pause = false;
            }
            yield return null;
        }
        SceneManager.LoadScene(13);

        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(NVR_third);
        pause = true;
        while (pause == true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                pause = false;
            }
            yield return null;
        }
        SceneManager.LoadScene(12);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(12);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(NVRW_first);
        pause = true;
        while (pause == true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                pause = false;
            }
            yield return null;
        }
        SceneManager.LoadScene(12);

        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(NVRW_second);
        pause = true;
        while (pause == true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                pause = false;
            }
            yield return null;
        }
        SceneManager.LoadScene(12);

        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(NVRW_third);
        pause = true;
        while (pause == true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                pause = false;
            }
            yield return null;
        }
        SceneManager.LoadScene(10);
    }
	void Start ()
    {
        DontDestroyOnLoad(this);//makes this script/object persist through scene changes.
    }	
	void Update ()
    {
        if (!isrunning)
        {
            StartCoroutine(VR_Phase());
        }
    }
}
