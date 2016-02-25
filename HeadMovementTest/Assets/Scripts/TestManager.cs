using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.VR;
using XInputDotNetPure;

public class TestManager : MonoBehaviour
{
    private List<List<int>> condition_list = new List<List<int>>();//list of lists.

    private List<int> vr_list = new List<int>();
    private List<int> nvr_list = new List<int>();
    private List<int> nvrw_list = new List<int>();
    private List<string> coroutine_list = new List<string>();//stores the names of the coroutines in the order that they have been randomly put into.

    private bool pause = false;
    private bool automize;
    public bool in_task = false;

    public GameObject VRTask1;
    public GameObject VRTask2;
    public GameObject VRTask3;
    public GameObject KeepHeadsetOn;
    public GameObject RemoveHeadset;
    public GameObject VRTask1Instructions;
    public GameObject VRTask2Instructions;
    public GameObject VRTask3Instructions;

    private GameObject task1_instructions;
    private GameObject task2_instructions;
    private GameObject task3_instructions;

    Vector3 vr_testpos = new Vector3(0, 2.57f, -3.54f);//position and rotation of tranistion scenes within vr.
    Quaternion vr_testrot = Quaternion.Euler(0, -180, 0);

    public string current_task;

    //gamepad variables.
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    void randomize_test()//randomises the condition/task order every time the application starts.
    {
        int element = 0;
        while(condition_list.Count != 3)//this generates a random number between 1 and 4 each loop, and fills that space in the list with the corresponding test condition.
        {
            element = 0;
            int condition_rand = Random.Range(1, 4);
            if (condition_rand == 1 && !condition_list.Contains(vr_list))//vr.
            {
                condition_list.Add(vr_list);
                while (vr_list.Count != 3)//then populates the current list with a random order between 1-4, 4-7 or 7-10 corresponding to the scene order in scenemanger.
                {
                    int vr_rand = Random.Range(1, 4);
                    if (!vr_list.Contains(vr_rand))
                    {
                        vr_list.Add(vr_rand);
                        element++;
                    }
                }
                coroutine_list.Add("vr");//sets the coroutine name to be added to the list and ran in order they are assigned to the list.
            }
            if (condition_rand == 2 && !condition_list.Contains(nvr_list))//nvr.
            {
                element = 0;
                condition_list.Add(nvr_list);
                while (nvr_list.Count != 3)
                {
                    int nvr_rand = Random.Range(4, 7);
                    if (!nvr_list.Contains(nvr_rand))
                    {
                        nvr_list.Add(nvr_rand);
                        element++;
                    }
                }
                coroutine_list.Add("nvr");
            }
            if (condition_rand == 3 && !condition_list.Contains(nvrw_list))//nvrw.
            {
                element = 0;
                condition_list.Add(nvrw_list);
                while (nvrw_list.Count != 3)
                {
                    int nvrw_rand = Random.Range(7, 10);
                    if (!nvrw_list.Contains(nvrw_rand))
                    {
                        nvrw_list.Add(nvrw_rand);
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

        SceneManager.LoadScene(11);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(1);
        for (int i = 0; i < 3; i ++)
        {
            if (i > 0 && i != 3)//asks the user to keep the vr headset on.
            {
                yield return new WaitForSeconds(1);
                current_task = "Keep Headset On";
                GameObject keep_on = Instantiate(KeepHeadsetOn, vr_testpos, vr_testrot) as GameObject;
                yield return new WaitForSeconds(5);
                Destroy(keep_on);
            }
            if(vr_list[i].Equals(1))
            {
                yield return new WaitForSeconds(1);
                current_task = "VR Task 1 Instructions";
                task1_instructions = Instantiate(VRTask1Instructions, VRTask1Instructions.transform.position, VRTask1Instructions.transform.rotation) as GameObject;
                yield return new WaitForSeconds(1);
            }
            if(vr_list[i].Equals(2))
            {
                yield return new WaitForSeconds(1);
                current_task = "VR Task 2 Instructions";
                task2_instructions = Instantiate(VRTask2Instructions, VRTask2Instructions.transform.position, VRTask2Instructions.transform.rotation) as GameObject;
                yield return new WaitForSeconds(1);
            }
            if(vr_list[i].Equals(3))
            {
                yield return new WaitForSeconds(1);
                current_task = "VR Task 3 Instructions";
                task3_instructions = Instantiate(VRTask3Instructions, VRTask3Instructions.transform.position, VRTask3Instructions.transform.rotation) as GameObject;
                yield return new WaitForSeconds(1);
            }
            pause = true;
            while (pause == true)
            {
                if (Input.GetKeyDown(KeyCode.Space) || state.Triggers.Left == 1 || state.Triggers.Right == 1)
                {
                    in_task = true;
                    GameObject.Find("TestManager").GetComponent<Python>().stream_data = true;
                    pause = false;
                }
                yield return null;
                in_task = false;
            }
            pause = true;
            yield return new WaitForSeconds(1);//allows the triggers to re-init to zero.
            if (vr_list[i] == 1)
            {
                current_task = "VR Task 1";
                Destroy(task1_instructions);
                GameObject task1_prefab = Instantiate(VRTask1, VRTask1.transform.position, VRTask1.transform.rotation) as GameObject;
                while (pause == true)
                {
                    if (Input.GetMouseButtonDown(0) || state.Triggers.Left == 1 || state.Triggers.Right == 1)//stops the test if the left mouse button is pressed.
                    {
                        GameObject.Find("TestManager").GetComponent<Python>().stream_data = false;//when the scene ends, stream_data is set to false. this stop the data stream in the python script and stopp the data being logged in the logging script.
                        Destroy(task1_prefab);//when the image is found, remove the test from the VR screen.
                        pause = false;
                    }
                    yield return null;
                    in_task = false;
                }
            }
            if(vr_list[i] == 2)
            {
                current_task = "VR Task 2";
                Destroy(task2_instructions);
                GameObject task2_prefab = Instantiate(VRTask2, VRTask2.transform.position, VRTask2.transform.rotation) as GameObject;
                while (pause == true)
                {
                    if (GameObject.Find("Circle").GetComponent<LoPresti>().load_next)
                    {
                        GameObject.Find("TestManager").GetComponent<Python>().stream_data = false;
                        Destroy(task2_prefab);
                        pause = false;
                    }
                    yield return null;
                }

            }
            if (vr_list[i] == 3)
            {
                current_task = "VR Task 3";
                Destroy(task3_instructions);
                GameObject task3_prefab = Instantiate(VRTask3, VRTask3.transform.position, VRTask3.transform.rotation) as GameObject;
                while (pause == true)
                {
                    if(GameObject.Find("Circle").GetComponent<LoPresti>().load_next)
                    {
                        GameObject.Find("TestManager").GetComponent<Python>().stream_data = false;
                        Destroy(task3_prefab);
                        pause = false;
                    }
                    yield return null;
                }
            }
        }
        yield return new WaitForSeconds(1);
        current_task = "Remove Headset";
        GameObject remove_headset = Instantiate(RemoveHeadset, vr_testpos, vr_testrot) as GameObject;
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
            current_task = "End of Test";
            SceneManager.LoadScene(10);
        }

    }
    IEnumerator nvr()
    {
        automize = true;
        yield return new WaitForSeconds(0);//needs this to load the nvr instructions for some reason?

        for (int i = 0; i < nvr_list.Count; i++)
        {
            if (nvr_list[i] == 4)
            {
                current_task = "NVR Task 1 Instructions";
                SceneManager.LoadScene(17);
                yield return new WaitForSeconds(1);
            }
            if (nvr_list[i] == 5)
            {
                current_task = "NVR Task 2 Instructions";
                SceneManager.LoadScene(19);
                yield return new WaitForSeconds(1);
            }
            if (nvr_list[i] == 6)
            {
                current_task = "NVR Task 3 Instructions";
                SceneManager.LoadScene(21);
                yield return new WaitForSeconds(1);
            }
            pause = true;
            while (pause == true)
            {
                if (Input.GetKeyDown(KeyCode.Space) || state.Triggers.Left == 1 || state.Triggers.Right == 1)
                {
                    in_task = true;
                    SceneManager.LoadScene(nvr_list[i]);
                    GameObject.Find("TestManager").GetComponent<Python>().stream_data = true;
                    pause = false;
                }
                yield return null;
                in_task = false;
            }
            pause = true;
            yield return new WaitForSeconds(1);
            if (nvr_list[i] == 4)
            {
                while (pause == true)
                {
                    if (Input.GetMouseButtonDown(0) || state.Triggers.Left == 1 || state.Triggers.Right == 1)//stops the test if the left mouse button is pressed.
                    {
                        GameObject.Find("TestManager").GetComponent<Python>().stream_data = false;
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
                        GameObject.Find("TestManager").GetComponent<Python>().stream_data = false;
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
            current_task = "End of Test";
            SceneManager.LoadScene(10);
        }
    }
    IEnumerator nvrw()
    {
        automize = true;

        SceneManager.LoadScene(13);
        yield return new WaitForSeconds(5);

        for (int i = 0; i < nvrw_list.Count; i++)
        {
            if (nvrw_list[i] == 7)
            {
                current_task = "NVRW Task 1 Instructions";
                SceneManager.LoadScene(17);
                yield return new WaitForSeconds(1);
            }
            if (nvrw_list[i] == 8)
            {
                current_task = "NVRW Task 2 Instructions";
                SceneManager.LoadScene(19);
                yield return new WaitForSeconds(1);
            }
            if (nvrw_list[i] == 9)
            {
                current_task = "NVRW Task 3 Instructions";
                SceneManager.LoadScene(21);
                yield return new WaitForSeconds(1);
            }
            pause = true;
            while (pause == true)
            {
                if (Input.GetKeyDown(KeyCode.Space) || state.Triggers.Left == 1 || state.Triggers.Right == 1)
                {
                    in_task = true;
                    SceneManager.LoadScene(nvrw_list[i]);
                    GameObject.Find("TestManager").GetComponent<Python>().stream_data = true;
                    pause = false;
                }
                yield return null;
                in_task = false;
            }
            pause = true;
            yield return new WaitForSeconds(1);
            if (nvrw_list[i] == 7)
            {
                while (pause == true)
                {
                    if (Input.GetMouseButtonDown(0) || state.Triggers.Left == 1 || state.Triggers.Right == 1)//stops the test if the left mouse button is pressed.
                    {
                        GameObject.Find("TestManager").GetComponent<Python>().stream_data = false;
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
                        GameObject.Find("TestManager").GetComponent<Python>().stream_data = false;
                        pause = false;
                    }
                    yield return null;
                }
            }
        }
        SceneManager.LoadScene(22);//tells the user to take of the weighted headband when the nvrw tests are done.
        yield return new WaitForSeconds(5);

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
            current_task = "End of Test";
            SceneManager.LoadScene(10);
        }
    }
    void Start()
    {
        DontDestroyOnLoad(this);
        randomize_test();
    }
    void Update()
    {
        prevState = state;
        state = GamePad.GetState(playerIndex);

        if (!automize)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(coroutine_list[0]);
            }
          
            if (state.Triggers.Left == 1 || state.Triggers.Right == 1)
            {
                StartCoroutine(coroutine_list[0]);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}

