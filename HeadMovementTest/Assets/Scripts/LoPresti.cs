using UnityEngine;
using UnityEngine.VR;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoPresti : MonoBehaviour
{
    //The GameObjects that we assign our targets to.
    private GameObject Circle;
    private GameObject PlusX;
    private GameObject PlusY;
    private GameObject MinusX;
    private GameObject MinusY;
    private GameObject MinusX_PlusY;
    private GameObject MinusX_MinusY;
    private GameObject PlusX_PlusY;
    private GameObject PlusX_MinusY;
    private GameObject Centre;
   
    //The bools that are triggered to start each movement coroutine.
    private bool MoveToRight = true;//As out first movement is to the right, this bool is set to true to make sure we move to the right first.
    private bool MoveToLeft = false;
    private bool MoveToTop = false;
    private bool MoveToBottom = false;
    private bool MoveToTopLeft = false;
    private bool MoveToBottomRight = false;
    private bool MoveToTopRight = false;
    private bool MoveToBottomLeft = false;

    private float Speed = 0.0f;//Sets the movement speed of the circle for Task 2.
    private float CloseEnough = 0.00001f;//To ensure that the circle meets its target, we keep moving it until it is this close to the target.

    public bool LoadNext = false;//Public bool to tell other scripts that this task has finished.
    private bool Finished = false;//Internal bool to say when the task has finished.

    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "NVR Task 2" || SceneManager.GetActiveScene().name == "NVR Task 3" || SceneManager.GetActiveScene().name == "NVRW Task 2" || SceneManager.GetActiveScene().name == "NVRW Task 3")//Disables VR if the current scene in Non-VR or Non-VR Weighted.
        {
            VRSettings.enabled = false;
        }
    }

    void Start ()
    {
        //Assigns out GameObjects to the corresponding scene object.
        Circle = GameObject.Find("Circle");
        PlusX = GameObject.Find("+X_Waypoint");
        PlusY = GameObject.Find("+Y_Waypoint");
        MinusX = GameObject.Find("-X_Waypoint");
        MinusY = GameObject.Find("-Y_Waypoint");
        MinusX_PlusY = GameObject.Find("-X+Y_Waypoint");
        MinusX_MinusY = GameObject.Find("-X-Y_Waypoint");
        PlusX_PlusY = GameObject.Find("+X+Y_Waypoint");
        PlusX_MinusY = GameObject.Find("+X-Y_Waypoint");
        Centre = GameObject.Find("Centre");

        if (SceneManager.GetActiveScene().name == "NVR Task 2" || SceneManager.GetActiveScene().name == "NVR Task 3" || SceneManager.GetActiveScene().name == "NVRW Task 2" || SceneManager.GetActiveScene().name == "NVRW Task 3")//Disables VR if the current scene in Non-VR or Non-VR Weighted.
        {
            if(SceneManager.GetActiveScene().name == "NVR Task 2")//Sets the name of the current task the loaded scene.
            {
                Speed = 7.0f;
                GameObject.Find("TestManager").GetComponent<TestManager>().CurrentTask = "NVR Task 2";
            }
            if(SceneManager.GetActiveScene().name == "NVR Task 3")
            {
                GameObject.Find("TestManager").GetComponent<TestManager>().CurrentTask = "NVR Task 3";
            }
            if(SceneManager.GetActiveScene().name == "NVRW Task 2")
            {
                Speed = 7.0f;
                GameObject.Find("TestManager").GetComponent<TestManager>().CurrentTask = "NVRW Task 2";
            }
            if(SceneManager.GetActiveScene().name == "NVRW Task 3")
            {
                GameObject.Find("TestManager").GetComponent<TestManager>().CurrentTask = "NVRW Task 3";
            }
            VRSettings.enabled = false;
        }
        if (GameObject.Find("TestManager").GetComponent<TestManager>().CurrentTask == "VR Task 2")//Enables VR if we are in a VR task.
        {
            Speed = 0.185f;
            VRSettings.enabled = true;
        }
    }
    IEnumerator centre()
    {
        yield return new WaitForSeconds(2.0f);//Ensures that the target pauses before it moves to its next destination.
        if (SceneManager.GetActiveScene().name == "NVR Task 2" || GameObject.Find("TestManager").GetComponent<TestManager>().CurrentTask == "VR Task 2" || SceneManager.GetActiveScene().name == "NVRW Task 2")
        {
            while (Vector3.Distance(Circle.transform.position, Centre.transform.position) > CloseEnough)//If we are in Task 2 for either of the 3 conditions, move the target toward it target over time with the given speed.
            {
                Circle.transform.position = Vector3.MoveTowards(Circle.transform.position, Centre.transform.position, Speed * Time.deltaTime);
                yield return null;
            }
        }
        if (SceneManager.GetActiveScene().name == "NVR Task 3" || GameObject.Find("TestManager").GetComponent<TestManager>().CurrentTask == "VR Task 3" || SceneManager.GetActiveScene().name == "NVRW Task 3")
        {
            Circle.transform.position = Centre.transform.position;//If we are in Task 3 for either of the 3 conditions, move the target toward it target over time with the given speed.
        }
        if (Circle.transform.position == Centre.transform.position && Finished == true)//If the target has reached the centre and all of the waypoints have been moved to, notify the other scripts that this task has been completed.
        {
            yield return new WaitForSeconds(2.0f);
            LoadNext = true;
        }
        yield return new WaitForSeconds(2.0f);
    }
    IEnumerator right()
    {
        yield return new WaitForSeconds(2.0f);
        if(SceneManager.GetActiveScene().name == "NVR Task 2" || GameObject.Find("TestManager").GetComponent<TestManager>().CurrentTask == "VR Task 2" || SceneManager.GetActiveScene().name == "NVRW Task 2")
        {
            while(Vector3.Distance(Circle.transform.position, PlusX.transform.position) > CloseEnough)
            {
                Circle.transform.position = Vector3.MoveTowards(Circle.transform.position, PlusX.transform.position, Speed * Time.deltaTime);
                yield return null;
            }
        }
        if (SceneManager.GetActiveScene().name == "NVR Task 3" || GameObject.Find("TestManager").GetComponent<TestManager>().CurrentTask == "VR Task 3" || SceneManager.GetActiveScene().name == "NVRW Task 3")
        {
            Circle.transform.position = PlusX.transform.position;
        }
        if (Circle.transform.position == PlusX.transform.position)//If the target has reached this waypoint, move back towards the centre and set the bool of the targets next destination as true to it will move towards there when it has reached the centre waypoint.
        {
            yield return StartCoroutine(centre());
            MoveToLeft = true;
        }
    }
    IEnumerator left()
    {
        yield return new WaitForSeconds(2.0f);
        if (SceneManager.GetActiveScene().name == "NVR Task 2" || GameObject.Find("TestManager").GetComponent<TestManager>().CurrentTask == "VR Task 2" || SceneManager.GetActiveScene().name == "NVRW Task 2")
        {
            while(Vector3.Distance(Circle.transform.position, MinusX.transform.position) > CloseEnough)
            {
                Circle.transform.position = Vector3.MoveTowards(Circle.transform.position, MinusX.transform.position, Speed * Time.deltaTime);
                yield return null;
            }
        }
        if (SceneManager.GetActiveScene().name == "NVR Task 3" || GameObject.Find("TestManager").GetComponent<TestManager>().CurrentTask == "VR Task 3" || SceneManager.GetActiveScene().name == "NVRW Task 3")
        {
            Circle.transform.position = MinusX.transform.position;
        }
        if (Circle.transform.position == MinusX.transform.position)
        {
            yield return StartCoroutine(centre());  
            MoveToTop = true;
        }
    }
    IEnumerator top()
    {
        yield return new WaitForSeconds(2.0f);
        if (SceneManager.GetActiveScene().name == "NVR Task 2" || GameObject.Find("TestManager").GetComponent<TestManager>().CurrentTask == "VR Task 2" || SceneManager.GetActiveScene().name == "NVRW Task 2")
        {
            while(Vector3.Distance(Circle.transform.position, PlusY.transform.position) > CloseEnough)
            {
                Circle.transform.position = Vector3.MoveTowards(Circle.transform.position, PlusY.transform.position, Speed * Time.deltaTime);
                yield return null;
            }
        }
        if (SceneManager.GetActiveScene().name == "NVR Task 3" || GameObject.Find("TestManager").GetComponent<TestManager>().CurrentTask == "VR Task 3" || SceneManager.GetActiveScene().name == "NVRW Task 3")
        {
            Circle.transform.position = PlusY.transform.position;
        }
        if(Circle.transform.position == PlusY.transform.position)
        {
            yield return StartCoroutine(centre());
            MoveToBottom = true;
        }
    }
    IEnumerator bottom()
    {
        yield return new WaitForSeconds(2.0f);
        if (SceneManager.GetActiveScene().name == "NVR Task 2" || GameObject.Find("TestManager").GetComponent<TestManager>().CurrentTask == "VR Task 2" || SceneManager.GetActiveScene().name == "NVRW Task 2")
        {
            while(Vector3.Distance(Circle.transform.position, MinusY.transform.position) > CloseEnough)
            {
                Circle.transform.position = Vector3.MoveTowards(Circle.transform.position, MinusY.transform.position, Speed * Time.deltaTime);
                yield return null;
            }
        }
        if (SceneManager.GetActiveScene().name == "NVR Task 3" || GameObject.Find("TestManager").GetComponent<TestManager>().CurrentTask == "VR Task 3" || SceneManager.GetActiveScene().name == "NVRW Task 3")
        {
            Circle.transform.position = MinusY.transform.position;
        }
        if (Circle.transform.position == MinusY.transform.position)
        {
            yield return StartCoroutine(centre());
            MoveToTopLeft = true;
        }
    }
    IEnumerator top_left()
    {
        yield return new WaitForSeconds(2.0f);
        if (SceneManager.GetActiveScene().name == "NVR Task 2" || GameObject.Find("TestManager").GetComponent<TestManager>().CurrentTask == "VR Task 2" || SceneManager.GetActiveScene().name == "NVRW Task 2")
        {
            while(Vector3.Distance(Circle.transform.position, MinusX_PlusY.transform.position) > CloseEnough)
            {
                Circle.transform.position = Vector3.MoveTowards(Circle.transform.position, MinusX_PlusY.transform.position, Speed * Time.deltaTime);
                yield return null;
            }
        }
        if (SceneManager.GetActiveScene().name == "NVR Task 3" || GameObject.Find("TestManager").GetComponent<TestManager>().CurrentTask == "VR Task 3" || SceneManager.GetActiveScene().name == "NVRW Task 3")
        {
            Circle.transform.position = MinusX_PlusY.transform.position;
        }
        if(Circle.transform.position == MinusX_PlusY.transform.position)
        {
            yield return StartCoroutine(centre());
            MoveToBottomRight = true;
        }
    }
    IEnumerator bottom_right()
    {
        yield return new WaitForSeconds(2.0f);
        if (SceneManager.GetActiveScene().name == "NVR Task 2" || GameObject.Find("TestManager").GetComponent<TestManager>().CurrentTask == "VR Task 2" || SceneManager.GetActiveScene().name == "NVRW Task 2")
        {
            while(Vector3.Distance(Circle.transform.position, PlusX_MinusY.transform.position) > CloseEnough)
            {
                Circle.transform.position = Vector3.MoveTowards(Circle.transform.position, PlusX_MinusY.transform.position, Speed * Time.deltaTime);
                yield return null;
            }
        }
        if (SceneManager.GetActiveScene().name == "NVR Task 3" || GameObject.Find("TestManager").GetComponent<TestManager>().CurrentTask == "VR Task 3" || SceneManager.GetActiveScene().name == "NVRW Task 3")
        {
            Circle.transform.position = PlusX_MinusY.transform.position;
        }
        if (Circle.transform.position == PlusX_MinusY.transform.position)
        {
            yield return StartCoroutine(centre());
            MoveToTopRight = true;
        }
    }
    IEnumerator top_right()
    {
        yield return new WaitForSeconds(2.0f);
        if (SceneManager.GetActiveScene().name == "NVR Task 2" || GameObject.Find("TestManager").GetComponent<TestManager>().CurrentTask == "VR Task 2" || SceneManager.GetActiveScene().name == "NVRW Task 2")
        {
            while(Vector3.Distance(Circle.transform.position, PlusX_PlusY.transform.position) > CloseEnough)
            {
                Circle.transform.position = Vector3.MoveTowards(Circle.transform.position, PlusX_PlusY.transform.position, Speed * Time.deltaTime);
                yield return null;
            }
        }
        if (SceneManager.GetActiveScene().name == "NVR Task 3" || GameObject.Find("TestManager").GetComponent<TestManager>().CurrentTask == "VR Task 3" || SceneManager.GetActiveScene().name == "NVRW Task 3")
        {
            Circle.transform.position = PlusX_PlusY.transform.position;
        }
        if(Circle.transform.position == PlusX_PlusY.transform.position)
        {
            yield return StartCoroutine(centre());
            MoveToBottomLeft = true;
        } 
    }
    IEnumerator bottom_left()
    {
        yield return new WaitForSeconds(2.0f);
        if (SceneManager.GetActiveScene().name == "NVR Task 2" || GameObject.Find("TestManager").GetComponent<TestManager>().CurrentTask == "VR Task 2" || SceneManager.GetActiveScene().name == "NVRW Task 2")
        {
            while(Vector3.Distance(Circle.transform.position, MinusX_MinusY.transform.position) > CloseEnough)
            {
                Circle.transform.position = Vector3.MoveTowards(Circle.transform.position, MinusX_MinusY.transform.position, Speed * Time.deltaTime);
                yield return null;
            }
        }
        if (SceneManager.GetActiveScene().name == "NVR Task 3" || GameObject.Find("TestManager").GetComponent<TestManager>().CurrentTask == "VR Task 3" || SceneManager.GetActiveScene().name == "NVRW Task 3")
        {
            Circle.transform.position = MinusX_MinusY.transform.position;
        }
        if(Circle.transform.position == MinusX_MinusY.transform.position)
        {
            Finished = true;//This is the last waypoint that the target will move to, so when it has reached this point the task has finished.
            yield return StartCoroutine(centre());
        }
    }
    void FixedUpdate()//A fixed update loop is used here to ensure that the target moves to each waypoint smoothly and is not effected by any fluctuations.
    {
        //The order of the coroutines to be executed for this task.
        if (MoveToRight == true)
        {
            StartCoroutine(right());
            MoveToRight = false;//When the coroutine has been started, the bool is set to false to ensure that the next waypoint can be moved to without any issue.
        }
        if (MoveToLeft == true)
        {
            StartCoroutine(left());
            MoveToLeft = false;
        }
        if (MoveToTop == true)
        {
            StartCoroutine(top());
            MoveToTop = false;
        }
        if (MoveToBottom == true)
        {
            StartCoroutine(bottom());
            MoveToBottom = false;
        }
        if (MoveToTopLeft == true)
        {
            StartCoroutine(top_left());
            MoveToTopLeft = false;
        }
        if (MoveToBottomRight == true)
        {
            StartCoroutine(bottom_right());
            MoveToBottomRight = false;
        }
        if (MoveToTopRight == true)
        {
            StartCoroutine(top_right());
            MoveToTopRight = false;
        }
        if (MoveToBottomLeft == true)
        {
            StartCoroutine(bottom_left());
            MoveToBottomLeft = false;
        }
    }
}

