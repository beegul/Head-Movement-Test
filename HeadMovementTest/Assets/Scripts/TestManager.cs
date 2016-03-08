using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;//This allows Xbox 360 Controller support.

public class TestManager : MonoBehaviour
{
    private List<List<int>> ConditionList = new List<List<int>>();//The List of Lists, contains the full order of the experiment.
    private List<int> VRList = new List<int>();//A list that contains the random order for our VR tasks.
    private List<int> NVRList = new List<int>();//A list that contains the random order for our Non-VR tasks.
    private List<int> NVRWList = new List<int>();//A list that contains the random order for our Non-VR Weighted tasks.
    private List<string> CoroutineList = new List<string>();//Stores the names of the coroutines in the order that they have been randomly put into.

    public bool InTask = false;//This is set to true when we go from a transition screen into a participation task.
    private bool Pause = false;//When true, this pauses the automation of the test.
    private bool Automise = false;

    //GameObjects of the tasks and instuctions for VR and intructions for Non-VR.
    public GameObject VRTask1;
    public GameObject VRTask2;
    public GameObject VRTask3;
    public GameObject KeepHeadsetOn;
    public GameObject RemoveHeadset;
    public GameObject LostConnectionVR;
    public GameObject VRTask1Instructions;
    public GameObject VRTask2Instructions;
    public GameObject VRTask3Instructions;
    private GameObject Task1Instructions;
    private GameObject Task2Instructions;
    private GameObject Task3Instructions;

    private Vector3 VRPosition = new Vector3(0, 2.485f, -3.133f);//Position and Rotation of the tranistion scenes within the VR tasks.
    private Quaternion VRRotation = Quaternion.Euler(0, -180, 0);

    public string CurrentTask;//This is assigned to the task that the participant is being shown, this is then logged to thier data file.

    //Varaibles used in order to allow Xbox 360 Controller support.
    private PlayerIndex Player;
    private GamePadState CurrentState;
    private GamePadState PreviousState;

    private int RandomCondition = 0;
    private int RandomVRTask = 0;
    private int RandomNVRTask = 0;
    private int RandomNVRWTask = 0;

    void RandomiseTest()//Randomises the condition/task order every time the application starts.
    {
        while(ConditionList.Count != 3)//This generates a random number between 1 and 4 each loop, and fills that space in the list with the corresponding test condition.
        {
            RandomCondition = Random.Range(1, 4);
            if (RandomCondition == 1 && !ConditionList.Contains(VRList))//VR Tasks.
            {
                ConditionList.Add(VRList);
                while (VRList.Count != 3)//Populates the current list with a random order between 1-4, 4-7 or 7-10 corresponding to the scene order in scenemanger.
                {
                    RandomVRTask = Random.Range(1, 4);
                    if (!VRList.Contains(RandomVRTask))//Checks to see if the task corresponding to the generated number has already been added to the task list.
                    {
                        VRList.Add(RandomVRTask);
                    }
                }
                CoroutineList.Add("VR");//sets the coroutine name to be added to the list and ran in order they are assigned to the list.
            }
            if (RandomCondition == 2 && !ConditionList.Contains(NVRList))//Non-VR Tasks.
            {
                ConditionList.Add(NVRList);
                while (NVRList.Count != 3)
                {
                    RandomNVRTask = Random.Range(4, 7);
                    if (!NVRList.Contains(RandomNVRTask))
                    {
                        NVRList.Add(RandomNVRTask);
                    }
                }
                CoroutineList.Add("NVR");
            }
            if (RandomCondition == 3 && !ConditionList.Contains(NVRWList))//Non-VR Weighted Tasks.
            {
                ConditionList.Add(NVRWList);
                while (NVRWList.Count != 3)
                {
                    RandomNVRWTask = Random.Range(7, 10);
                    if (!NVRWList.Contains(RandomNVRWTask))
                    {
                        NVRWList.Add(RandomNVRWTask);
                    }
                }
                CoroutineList.Add("NVRW");
            }
        }
    }
    IEnumerator VR()
    {
        Automise = true;
        yield return new WaitForSeconds(1);//These are used to keep the pace of the experiment uniform, ensures that the participant has a regular pause between each screen as to not feel rushsed.
        SceneManager.LoadScene("Pre VR");
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(1);
        for (int i = 0; i < 3; i ++)//As we have three conditions, this loops runs three times, loading each element within our condition.
        {
            if (i > 0 && i != 3)//This loads the scene which asks the user to keep thier VR headset on.
            {
                yield return new WaitForSeconds(1);
                GameObject keep_on = Instantiate(KeepHeadsetOn, VRPosition, VRRotation) as GameObject;//Creates a temporary GameObject to enable a clone of a prefab to be loaded into the same scene.
                yield return new WaitForSeconds(5);
                Destroy(keep_on);//Destroys the clone.
            }
            if(VRList[i] == 1)//Displays the instructions for each of the three VR tasks by applying a prefab clone into the same scene. VRList[i].Equals(1)
            {
                yield return new WaitForSeconds(1);
                Task1Instructions = Instantiate(VRTask1Instructions, VRTask1Instructions.transform.position, VRTask1Instructions.transform.rotation) as GameObject;
                yield return new WaitForSeconds(1);
            }
            if(VRList[i] == 2)
            {
                yield return new WaitForSeconds(1);
                Task2Instructions = Instantiate(VRTask2Instructions, VRTask2Instructions.transform.position, VRTask2Instructions.transform.rotation) as GameObject;
                yield return new WaitForSeconds(1);
            }
            if(VRList[i] == 3)
            {
                yield return new WaitForSeconds(1);
                Task3Instructions = Instantiate(VRTask3Instructions, VRTask3Instructions.transform.position, VRTask3Instructions.transform.rotation) as GameObject;
                yield return new WaitForSeconds(1);
            }
            Pause = true;
            while (Pause == true)
            {
                if (Input.GetKeyDown(KeyCode.Space) || CurrentState.Triggers.Left == 1 || CurrentState.Triggers.Right == 1)//Sets the current task name to be logged to the data file.
                {
                    if(VRList[i] == 1)
                    {
                        CurrentTask = "VR Task 1";
                    }
                    if (VRList[i] == 2)
                    {
                        CurrentTask = "VR Task 2";
                    }
                    if (VRList[i] == 3)
                    {
                        CurrentTask = "VR Task 3";
                    }
                    InTask = true;
                    GameObject.Find("TestManager").GetComponent<Python>().StreamData = true;//Asks for data to be streamed to the Logging script.
                    Pause = false;
                }
                yield return null;
                InTask = false;
            }
            Pause = true;
            yield return new WaitForSeconds(1);
            if (VRList[i] == 1)
            {
                Destroy(Task1Instructions);
                GameObject task1_prefab = Instantiate(VRTask1, VRTask1.transform.position, VRTask1.transform.rotation) as GameObject;//A Temporary prefab of the current VR task.
                while (Pause == true)
                {
                    if (Input.GetMouseButtonDown(0) || CurrentState.Triggers.Left == 1 || CurrentState.Triggers.Right == 1)
                    {
                        GameObject.Find("TestManager").GetComponent<Python>().StreamData = false;//When the scene ends, StreamData is set to false. This stops the data stream in the Python script and stops the data being logged in the Logging script.
                        GameObject.Find("Logger").GetComponent<Logger>().Log_Data = false;
                        Destroy(task1_prefab);//Destroys the current task prefab, removing it from the scene.
                        Pause = false;
                    }
                    yield return null;
                    InTask = false;
                }
            }
            if(VRList[i] == 2)
            {
                Destroy(Task2Instructions);
                GameObject task2_prefab = Instantiate(VRTask2, VRTask2.transform.position, VRTask2.transform.rotation) as GameObject;
                while (Pause == true)
                {
                    if (GameObject.Find("Circle").GetComponent<LoPresti>().LoadNext)
                    {
                        GameObject.Find("TestManager").GetComponent<Python>().StreamData = false;
                        GameObject.Find("Logger").GetComponent<Logger>().Log_Data = false;
                        Destroy(task2_prefab);
                        Pause = false;
                    }
                    yield return null;
                }

            }
            if (VRList[i] == 3)
            {
                Destroy(Task3Instructions);
                GameObject task3_prefab = Instantiate(VRTask3, VRTask3.transform.position, VRTask3.transform.rotation) as GameObject;
                while (Pause == true)
                {
                    if(GameObject.Find("Circle").GetComponent<LoPresti>().LoadNext)
                    {
                        GameObject.Find("TestManager").GetComponent<Python>().StreamData = false;
                        GameObject.Find("Logger").GetComponent<Logger>().Log_Data = false;
                        Destroy(task3_prefab);
                        Pause = false;
                    }
                    yield return null;
                }
            }
        }
        yield return new WaitForSeconds(1);
        CurrentTask = "Remove Headset";
        GameObject remove_headset = Instantiate(RemoveHeadset, VRPosition, VRRotation) as GameObject;
        yield return new WaitForSeconds(5);

        if(CoroutineList[0] == "VR")//Checks to see if this is the current coroutine. If it is, load the next coroutine when this one has finished.
        {
            StopCoroutine(CoroutineList[0]);
            StartCoroutine(CoroutineList[1]);
        }
        if (CoroutineList[1] == "VR")
        {
            StopCoroutine(CoroutineList[1]);
            StartCoroutine(CoroutineList[2]);
        }
        if (CoroutineList[2] == "VR")//If this condition is the final condition to be shown, display the end of experiment image on the screen.
        {
            StopCoroutine(CoroutineList[2]);
            SceneManager.LoadScene("End Screen");
        }
    }
    IEnumerator NVR()
    {
        Automise = true;
        yield return new WaitForSeconds(1);
        for (int i = 0; i < NVRList.Count; i++)
        {
            if (NVRList[i] == 4)
            {
                yield return new WaitForSeconds(1);
                SceneManager.LoadScene("Task 1 Instructions");
                yield return new WaitForSeconds(1);
            }
            if (NVRList[i] == 5)
            {
                yield return new WaitForSeconds(1);
                SceneManager.LoadScene("Task 2 Instructions");
                yield return new WaitForSeconds(1);
            }
            if (NVRList[i] == 6)
            {
                yield return new WaitForSeconds(1);
                SceneManager.LoadScene("Task 3 Instructions");
                yield return new WaitForSeconds(1);
            }
            Pause = true;
            while (Pause == true)
            {
                if (Input.GetKeyDown(KeyCode.Space) || CurrentState.Triggers.Left == 1 || CurrentState.Triggers.Right == 1)
                {
                    yield return new WaitForSeconds(1);
                    InTask = true;
                    SceneManager.LoadScene(NVRList[i]);
                    GameObject.Find("TestManager").GetComponent<Python>().StreamData = true;
                    Pause = false;
                }
                yield return null;
                InTask = false;
            }
            Pause = true;
            yield return new WaitForSeconds(1);
            if (NVRList[i] == 4)
            {
                while (Pause == true)
                {
                    if (Input.GetMouseButtonDown(0) || CurrentState.Triggers.Left == 1 || CurrentState.Triggers.Right == 1)
                    {
                        GameObject.Find("TestManager").GetComponent<Python>().StreamData = false;
                        GameObject.Find("Logger").GetComponent<Logger>().Log_Data = false;
                        Pause = false;
                    }
                    yield return null;
                }
            }
            else
            {
                while (Pause == true)
                {
                    if (GameObject.Find("Circle").GetComponent<LoPresti>().LoadNext)
                    {
                        GameObject.Find("TestManager").GetComponent<Python>().StreamData = false;
                        GameObject.Find("Logger").GetComponent<Logger>().Log_Data = false;
                        Pause = false;
                    }
                    yield return null;
                }
            }
        }
        if (CoroutineList[0] == "NVR")
        {
            StopCoroutine(CoroutineList[0]);
            StartCoroutine(CoroutineList[1]);
        }
        if (CoroutineList[1] == "NVR")
        {
            StopCoroutine(CoroutineList[1]);
            StartCoroutine(CoroutineList[2]);
        }
        if (CoroutineList[2] == "NVR")
        {
            StopCoroutine(CoroutineList[2]);
            SceneManager.LoadScene("End Screen");
        }
    }
    IEnumerator NVRW()
    {
        Automise = true;
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Pre NVRW");
        yield return new WaitForSeconds(5);

        for (int i = 0; i < NVRWList.Count; i++)
        {
            if (NVRWList[i] == 7)
            {
                yield return new WaitForSeconds(1);
                SceneManager.LoadScene("Task 1 Instructions");
            }
            if (NVRWList[i] == 8)
            {
                yield return new WaitForSeconds(1);
                SceneManager.LoadScene("Task 2 Instructions");
            }
            if (NVRWList[i] == 9)
            {
                yield return new WaitForSeconds(1);
                SceneManager.LoadScene("Task 3 Instructions");
            }
            Pause = true;
            while (Pause == true)
            {
                if (Input.GetKeyDown(KeyCode.Space) || CurrentState.Triggers.Left == 1 || CurrentState.Triggers.Right == 1)
                {
                    yield return new WaitForSeconds(1);
                    InTask = true;
                    SceneManager.LoadScene(NVRWList[i]);
                    GameObject.Find("TestManager").GetComponent<Python>().StreamData = true;
                    Pause = false;
                }
                yield return null;
                InTask = false;
            }
            Pause = true;
            yield return new WaitForSeconds(1);
            if (NVRWList[i] == 7)
            {
                while (Pause == true)
                {
                    if (Input.GetMouseButtonDown(0) || CurrentState.Triggers.Left == 1 || CurrentState.Triggers.Right == 1)//stops the test if the left mouse button is pressed.
                    {
                        GameObject.Find("TestManager").GetComponent<Python>().StreamData = false;
                        GameObject.Find("Logger").GetComponent<Logger>().Log_Data = false;
                        Pause = false;
                    }
                    yield return null;
                }
            }
            else
            {
                while (Pause == true)
                {
                    if (GameObject.Find("Circle").GetComponent<LoPresti>().LoadNext)
                    {
                        GameObject.Find("TestManager").GetComponent<Python>().StreamData = false;
                        GameObject.Find("Logger").GetComponent<Logger>().Log_Data = false;
                        Pause = false;
                    }
                    yield return null;
                }
            }
        }
        SceneManager.LoadScene("Post NVRW");
        yield return new WaitForSeconds(5);

        if (CoroutineList[0] == "NVRW")
        {
            StopCoroutine(CoroutineList[0]);
            StartCoroutine(CoroutineList[1]);
        }
        if (CoroutineList[1] == "NVRW")
        {
            StopCoroutine(CoroutineList[1]);
            StartCoroutine(CoroutineList[2]);
        }
        if (CoroutineList[2] == "NVRW")
        {
            StopCoroutine(CoroutineList[2]);
            SceneManager.LoadScene("End Screen");
        }
    }
    void CheckConnection()
    {
        if (GameObject.Find("TestManager").GetComponent<Python>().SensorConnected == false)
        {
            if(CurrentTask == "VR Task 1" || CurrentTask == "VR Task 2" || CurrentTask == "VR Task 3")//Checks if the current scene is VR, if so, display the VR version of the "Connection Lost, Please Restart" screen.
            {
                GameObject.Find("TestManager").GetComponent<Python>().StreamData = false;
                GameObject.Find("Logger").GetComponent<Logger>().Log_Data = false;
                StopAllCoroutines();
                GameObject task = GameObject.FindGameObjectWithTag("Task");//Each prefab has been tagged, they are then deleted when the connection has been lost so they dont overlap the "Connection Lost, Please Restart" screen.
                GameObject instruction = GameObject.FindGameObjectWithTag("VRInstruction");
                Destroy(task);
                Destroy(instruction);
                GameObject lostconnection_prefab = Instantiate(LostConnectionVR, VRPosition, VRRotation) as GameObject;//Displays to the participant that the connection with the sensor has been lost, and asks them to restart the test.
            }
            if (CurrentTask == "NVR Task 1" || CurrentTask == "NVR Task 2" || CurrentTask == "NVR Task 3" || CurrentTask == "NVRW Task 1" || CurrentTask == "NVRW Task 2" || CurrentTask == "NVRW Task 3")
            {
                GameObject.Find("TestManager").GetComponent<Python>().StreamData = false;
                GameObject.Find("Logger").GetComponent<Logger>().Log_Data = false;
                StopAllCoroutines();
                SceneManager.LoadScene("Lost Connection");
            }
        }
    }
    void Start()
    {
        DontDestroyOnLoad(this);
        RandomiseTest();
    }
    void Update()
    {
        CheckConnection();
        PreviousState = CurrentState;
        CurrentState = GamePad.GetState(Player);

        if (!Automise)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(CoroutineList[0]);
            }          
            if (CurrentState.Triggers.Left == 1 || CurrentState.Triggers.Right == 1)
            {
                StartCoroutine(CoroutineList[0]);
            }
        }
    }
}

