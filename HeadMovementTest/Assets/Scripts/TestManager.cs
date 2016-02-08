using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class TestManager : MonoBehaviour
{
    public List<List<int>> condition_list = new List<List<int>>();//list of lists.

    public List<int> vr_list = new List<int>();
    public List<int> nvr_list = new List<int>();
    public List<int> nvrw_list = new List<int>();

    public List<string> coroutine_list = new List<string>();//stores the names of the coroutines in the order that they have been randomly put into.

    private bool pause = false;
    private bool automize;

    void randomize_test()//randomises the condition/task order every time the application starts.
    {
        int element = 0;
        while(condition_list.Count != 3)//this generates a random number between 1 and 4 each loop, and fills that space in the list with the corresponding test condition. 
        {
            element = 0;
            int condition_rand = Random.Range(1, 4);
            if (condition_rand == 1 && !condition_list.Contains(vr_list))//vr condition
            {
                condition_list.Add(vr_list);
                //Debug.Log("vr");
                while (vr_list.Count != 3)//then populates the current list with a random order between 1-4, 4-7 or 7-10 corresponding to the scene order in scenemanger.
                {
                    int vr_rand = Random.Range(1, 4);
                    if (!vr_list.Contains(vr_rand))
                    {
                        vr_list.Add(vr_rand);
                        //Debug.Log(vr_list[element].ToString());
                        element++;
                    }
                }
                coroutine_list.Add("vr");//sets the coroutine name to be added to the list and ran in order they are assigned to the list.
            }
            if (condition_rand == 2 && !condition_list.Contains(nvr_list))//nvr condition
            {
                element = 0;
                condition_list.Add(nvr_list);
                //Debug.Log("nvr");
                while (nvr_list.Count != 3)
                {
                    int nvr_rand = Random.Range(4, 7);
                    if (!nvr_list.Contains(nvr_rand))
                    {
                        nvr_list.Add(nvr_rand);
                        //Debug.Log(nvr_list[element].ToString());
                        element++;
                    }
                }
                coroutine_list.Add("nvr");
            }
            if (condition_rand == 3 && !condition_list.Contains(nvrw_list))//nvrw condition
            {
                element = 0;
                condition_list.Add(nvrw_list);
                //Debug.Log("nvrw");
                while (nvrw_list.Count != 3)
                {
                    int nvrw_rand = Random.Range(7, 10);
                    if (!nvrw_list.Contains(nvrw_rand))
                    {
                        nvrw_list.Add(nvrw_rand);
                        //Debug.Log(nvrw_list[element].ToString());
                        element++;
                    }
                }
                coroutine_list.Add("nvrw");
            }
        }
    }
    IEnumerator vr()
    {
        automize = true;
        Debug.Log("running vr coroutine");
        SceneManager.LoadScene(11);
        yield return new WaitForSeconds(5);

        for(int i = 0; i < 3; i ++)
        {
            if (i > 0 && i != 2)//asks the user to keep the vr headset on as there is another vr test on its way.
            {
                SceneManager.LoadScene(15);
                yield return new WaitForSeconds(5);
            }

            if (vr_list[i] == 1)
            {                
                SceneManager.LoadScene(16);
            }
            if (vr_list[i] == 2)
            {
                SceneManager.LoadScene(18);
            }
            if (vr_list[i] == 3)
            {
                SceneManager.LoadScene(20);
            }
            pause = true;

            while(pause == true)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SceneManager.LoadScene(vr_list[i]);
                    pause = false;
                }
                yield return null;
            }

            pause = true;

            if (vr_list[i] == 1)//checks if the current scene is the search task.
            {
                while (pause == true)
                {
                    if (Input.GetMouseButtonDown(0))//stops the test if the left mouse button is pressed.
                    {
                        pause = false;
                    }
                    yield return null;
                }
            }
            else
            {
                while(pause == true)
                {
                    if (GameObject.Find("Circle").GetComponent<LoPresti>().load_next)
                    {
                        pause = false;
                    }
                    yield return null;
                }
            }
        }
        SceneManager.LoadScene(14);//tells the user to take of the vr head when the vr tests are done.
        yield return new WaitForSeconds(5);

        if(coroutine_list[0] == "vr")
        {
            StopCoroutine(coroutine_list[0]);
            StartCoroutine(coroutine_list[1]);
        }
        if (coroutine_list[1] == "vr")
        {
            StopCoroutine(coroutine_list[1]);
            StartCoroutine(coroutine_list[2]);
        }
        if (coroutine_list[2] == "vr")
        {
            StopCoroutine(coroutine_list[2]);
            SceneManager.LoadScene(10);
        }

    }
    IEnumerator nvr()
    {
        automize = true;
        Debug.Log("running nvr coroutine");
        SceneManager.LoadScene(12);
        yield return new WaitForSeconds(5);

        for (int i = 0; i < nvr_list.Count; i++)
        {
            if (nvr_list[i] == 4)
            {
                SceneManager.LoadScene(17);
            }
            if (nvr_list[i] == 5)
            {
                SceneManager.LoadScene(19);
            }
            if (nvr_list[i] == 6)
            {
                SceneManager.LoadScene(21);
            }
            pause = true;

            while (pause == true)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SceneManager.LoadScene(nvr_list[i]);
                    pause = false;
                }
                yield return null;
            }

            pause = true;

            if (nvr_list[i] == 4)//checks if the current scene is the search task.
            {
                while (pause == true)
                {
                    if (Input.GetMouseButtonDown(0))//stops the test if the left mouse button is pressed.
                    {
                        pause = false;
                    }
                    yield return null;
                }
            }
            else
            {
                while (pause == true)
                {
                    if (GameObject.Find("Circle").GetComponent<LoPresti>().load_next)
                    {
                        pause = false;
                    }
                    yield return null;
                }
            }
        }
        if (coroutine_list[0] == "nvr")//checks to see if this is the current coroutine, and if it is, load the next coroutine when this one has finished.
        {
            StopCoroutine(coroutine_list[0]);
            StartCoroutine(coroutine_list[1]);
        }
        if (coroutine_list[1] == "nvr")
        {
            StopCoroutine(coroutine_list[1]);
            StartCoroutine(coroutine_list[2]);
        }
        if (coroutine_list[2] == "nvr")
        {
            StopCoroutine(coroutine_list[2]);
            SceneManager.LoadScene(10);
        }
    }
    IEnumerator nvrw()
    {
        automize = true;
        Debug.Log("running nvrw coroutine");
        SceneManager.LoadScene(13);
        yield return new WaitForSeconds(5);

        for (int i = 0; i < nvrw_list.Count; i++)
        {
            if (nvrw_list[i] == 7)
            {
                SceneManager.LoadScene(17);
            }
            if (nvrw_list[i] == 8)
            {
                SceneManager.LoadScene(19);
            }
            if (nvrw_list[i] == 9)
            {
                SceneManager.LoadScene(21);
            }
            pause = true;

            while (pause == true)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SceneManager.LoadScene(nvrw_list[i]);
                    pause = false;
                }
                yield return null;
            }

            pause = true;

            if (nvrw_list[i] == 7)//checks if the current scene is the search task.
            {
                while (pause == true)
                {
                    if (Input.GetMouseButtonDown(0))//stops the test if the left mouse button is pressed.
                    {
                        pause = false;
                    }
                    yield return null;
                }
            }
            else
            {
                while (pause == true)
                {
                    if (GameObject.Find("Circle").GetComponent<LoPresti>().load_next)
                    {
                        pause = false;
                    }
                    yield return null;
                }
            }
        }
        if (coroutine_list[0] == "nvrw")
        {
            StopCoroutine(coroutine_list[0]);
            StartCoroutine(coroutine_list[1]);
        }
        if (coroutine_list[1] == "nvrw")
        {
            StopCoroutine(coroutine_list[1]);
            StartCoroutine(coroutine_list[2]);
        }
        if (coroutine_list[2] == "nvrw")
        {
            StopCoroutine(coroutine_list[2]);
            SceneManager.LoadScene(10);
        }
    }
    void Start()
    {
        DontDestroyOnLoad(this);
        randomize_test();

        //debug to show current order of coroutines.
        Debug.Log("test order:");
        for (int i = 0; i < coroutine_list.Count; i++)
        {
            Debug.Log(coroutine_list[i]);
        }

        //System.Diagnostics.Process.Start("sensor.py");//runs python script when program starts. this isnt they way we need to do it though, but it starts in from the program atleast!
    }
    void Update()
    {
        if(!automize)
        {
            if(Input.GetKeyDown(KeyCode.Space))//starts the coroutines when the pace button is pressed on the start screen.
            {
                StartCoroutine(coroutine_list[0]);
            }
        }
    }
}

