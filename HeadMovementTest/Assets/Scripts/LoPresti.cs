using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.VR;

public class LoPresti : MonoBehaviour
{
    private GameObject Circle;
    private GameObject plus_X;
    private GameObject plus_Y;
    private GameObject minus_X;
    private GameObject minus_Y;
    private GameObject minus_X_plus_Y;
    private GameObject minus_X_minus_Y;
    private GameObject plus_X_plus_Y;
    private GameObject plus_X_minus_Y;
    private GameObject Centre;
   
    private bool move_to_right = true;
    private bool move_to_left = false;
    private bool move_to_top = false;
    private bool move_to_bottom = false;
    private bool move_to_top_left = false;
    private bool move_to_bottom_right = false;
    private bool move_to_top_right = false;
    private bool move_to_bottom_left = false;

    public float speed;//sets the movement speed of the circle.

    string NVRTask2 = "NVR Task 2";
    string NVRTask3 = "NVR Task 3";

    string NVRWTask2 = "NVRW Task 2";
    string NVRWTask3 = "NVRW Task 3";

    bool finished = false;
    public bool load_next = false;

    float close_enough = 0.00001f;

    void Awake()
    {
        //QualitySettings.vSyncCount = 0;//disable vsync. enable when you are the 165hz monitor
        //Application.targetFrameRate = 60;//lock fps.
    }
    void Start ()
    {
        Circle = GameObject.Find("Circle");
        plus_X = GameObject.Find("+X_Waypoint");
        plus_Y = GameObject.Find("+Y_Waypoint");
        minus_X = GameObject.Find("-X_Waypoint");
        minus_Y = GameObject.Find("-Y_Waypoint");
        minus_X_plus_Y = GameObject.Find("-X+Y_Waypoint");
        minus_X_minus_Y = GameObject.Find("-X-Y_Waypoint");
        plus_X_plus_Y = GameObject.Find("+X+Y_Waypoint");
        plus_X_minus_Y = GameObject.Find("+X-Y_Waypoint");
        Centre = GameObject.Find("Centre");

        if (SceneManager.GetActiveScene().name == NVRTask2 || SceneManager.GetActiveScene().name == NVRTask3 || SceneManager.GetActiveScene().name == NVRWTask2 || SceneManager.GetActiveScene().name == NVRWTask3)//disables VR.
        {
            if(SceneManager.GetActiveScene().name == NVRTask2)
            {
                GameObject.Find("TestManager").GetComponent<TestManager>().current_task = "NVR Task 2";
            }
            if(SceneManager.GetActiveScene().name == NVRTask3)
            {
                GameObject.Find("TestManager").GetComponent<TestManager>().current_task = "NVR Task 3";
            }
            if(SceneManager.GetActiveScene().name == NVRWTask2)
            {
                GameObject.Find("TestManager").GetComponent<TestManager>().current_task = "NVRW Task 2";
            }
            if(SceneManager.GetActiveScene().name == NVRWTask3)
            {
                GameObject.Find("TestManager").GetComponent<TestManager>().current_task = "NVRW Task 3";
            }
            VRSettings.enabled = false;
        }
        if (GameObject.Find("TestManager").GetComponent<TestManager>().current_task == "VR Task 2" || GameObject.Find("TestManager").GetComponent<TestManager>().current_task == "VR Task 3")//enables VR.
        {
            VRSettings.enabled = true;
        }
    }
    IEnumerator centre()
    {
        yield return new WaitForSeconds(2.0f);
        if (SceneManager.GetActiveScene().name == NVRTask2 || GameObject.Find("TestManager").GetComponent<TestManager>().current_task == "VR Task 2" || SceneManager.GetActiveScene().name == NVRWTask2)//checks the current scenes name.  //SceneManager.GetActiveScene().name == NVRTask3
        {
            while (Vector3.Distance(Circle.transform.position, Centre.transform.position) > close_enough)
            {
                Circle.transform.position = Vector3.MoveTowards(Circle.transform.position, Centre.transform.position, speed * Time.deltaTime);
                yield return null;
            }
        }
        if (SceneManager.GetActiveScene().name == NVRTask3 || GameObject.Find("TestManager").GetComponent<TestManager>().current_task == "VR Task 3" || SceneManager.GetActiveScene().name == NVRWTask3)
        {
            Circle.transform.position = Centre.transform.position;
        }
        if (Circle.transform.position == Centre.transform.position && finished == true)
        {
            yield return new WaitForSeconds(2.0f);
            load_next = true;
        }
        yield return new WaitForSeconds(2.0f);
    }
    IEnumerator right()
    {
        yield return new WaitForSeconds(2.0f);
        if(SceneManager.GetActiveScene().name == NVRTask2 || GameObject.Find("TestManager").GetComponent<TestManager>().current_task == "VR Task 2" || SceneManager.GetActiveScene().name == NVRWTask2)
        {
            while(Vector3.Distance(Circle.transform.position, plus_X.transform.position) > close_enough)
            {
                Circle.transform.position = Vector3.MoveTowards(Circle.transform.position, plus_X.transform.position, speed * Time.deltaTime);
                yield return null;
            }
        }
        if (SceneManager.GetActiveScene().name == NVRTask3 || GameObject.Find("TestManager").GetComponent<TestManager>().current_task == "VR Task 3" || SceneManager.GetActiveScene().name == NVRWTask3)
        {
            Circle.transform.position = plus_X.transform.position;
        }

        if (Circle.transform.position == plus_X.transform.position)
        {
            yield return StartCoroutine(centre());
            move_to_left = true;
        }
    }
    IEnumerator left()
    {
        yield return new WaitForSeconds(2.0f);
        if (SceneManager.GetActiveScene().name == NVRTask2 || GameObject.Find("TestManager").GetComponent<TestManager>().current_task == "VR Task 2" || SceneManager.GetActiveScene().name == NVRWTask2)
        {
            while(Vector3.Distance(Circle.transform.position, minus_X.transform.position) > close_enough)
            {
                Circle.transform.position = Vector3.MoveTowards(Circle.transform.position, minus_X.transform.position, speed * Time.deltaTime);
                yield return null;
            }
        }
        if (SceneManager.GetActiveScene().name == NVRTask3 || GameObject.Find("TestManager").GetComponent<TestManager>().current_task == "VR Task 3" || SceneManager.GetActiveScene().name == NVRWTask3)
        {
            Circle.transform.position = minus_X.transform.position;
        }
        if (Circle.transform.position == minus_X.transform.position)
        {
            yield return StartCoroutine(centre());  
            move_to_top = true;
        }
    }
    IEnumerator top()
    {
        yield return new WaitForSeconds(2.0f);
        if (SceneManager.GetActiveScene().name == NVRTask2 || GameObject.Find("TestManager").GetComponent<TestManager>().current_task == "VR Task 2" || SceneManager.GetActiveScene().name == NVRWTask2)
        {
            while(Vector3.Distance(Circle.transform.position, plus_Y.transform.position) > close_enough)
            {
                Circle.transform.position = Vector3.MoveTowards(Circle.transform.position, plus_Y.transform.position, speed * Time.deltaTime);
                yield return null;
            }
        }
        if (SceneManager.GetActiveScene().name == NVRTask3 || GameObject.Find("TestManager").GetComponent<TestManager>().current_task == "VR Task 3" || SceneManager.GetActiveScene().name == NVRWTask3)
        {
            Circle.transform.position = plus_Y.transform.position;
        }
        if(Circle.transform.position == plus_Y.transform.position)
        {
            yield return StartCoroutine(centre());
            move_to_bottom = true;
        }
    }
    IEnumerator bottom()
    {
        yield return new WaitForSeconds(2.0f);
        if (SceneManager.GetActiveScene().name == NVRTask2 || GameObject.Find("TestManager").GetComponent<TestManager>().current_task == "VR Task 2" || SceneManager.GetActiveScene().name == NVRWTask2)
        {
            while(Vector3.Distance(Circle.transform.position, minus_Y.transform.position) > close_enough)
            {
                Circle.transform.position = Vector3.MoveTowards(Circle.transform.position, minus_Y.transform.position, speed * Time.deltaTime);
                yield return null;
            }
        }
        if (SceneManager.GetActiveScene().name == NVRTask3 || GameObject.Find("TestManager").GetComponent<TestManager>().current_task == "VR Task 3" || SceneManager.GetActiveScene().name == NVRWTask3)
        {
            Circle.transform.position = minus_Y.transform.position;
        }
        if (Circle.transform.position == minus_Y.transform.position)
        {
            yield return StartCoroutine(centre());
            move_to_top_left = true;
        }
    }
    IEnumerator top_left()
    {
        yield return new WaitForSeconds(2.0f);
        if(SceneManager.GetActiveScene().name == NVRTask2 || GameObject.Find("TestManager").GetComponent<TestManager>().current_task == "VR Task 2" || SceneManager.GetActiveScene().name == NVRWTask2)
        {
            while(Vector3.Distance(Circle.transform.position, minus_X_plus_Y.transform.position) > close_enough)
            {
                Circle.transform.position = Vector3.MoveTowards(Circle.transform.position, minus_X_plus_Y.transform.position, speed * Time.deltaTime);
                yield return null;
            }
        }
        if (SceneManager.GetActiveScene().name == NVRTask3 || GameObject.Find("TestManager").GetComponent<TestManager>().current_task == "VR Task 3" || SceneManager.GetActiveScene().name == NVRWTask3)
        {
            Circle.transform.position = minus_X_plus_Y.transform.position;
        }
        if(Circle.transform.position == minus_X_plus_Y.transform.position)
        {
            yield return StartCoroutine(centre());
            move_to_bottom_right = true;
        }
    }
    IEnumerator bottom_right()
    {
        yield return new WaitForSeconds(2.0f);
        if(SceneManager.GetActiveScene().name == NVRTask2 || GameObject.Find("TestManager").GetComponent<TestManager>().current_task == "VR Task 2" || SceneManager.GetActiveScene().name == NVRWTask2)
        {
            while(Vector3.Distance(Circle.transform.position, plus_X_minus_Y.transform.position) > close_enough)
            {
                Circle.transform.position = Vector3.MoveTowards(Circle.transform.position, plus_X_minus_Y.transform.position, speed * Time.deltaTime);
                yield return null;
            }
        }
        if (SceneManager.GetActiveScene().name == NVRTask3 || GameObject.Find("TestManager").GetComponent<TestManager>().current_task == "VR Task 3" || SceneManager.GetActiveScene().name == NVRWTask3)
        {
            Circle.transform.position = plus_X_minus_Y.transform.position;
        }
        if (Circle.transform.position == plus_X_minus_Y.transform.position)
        {
            yield return StartCoroutine(centre());
            move_to_top_right = true;
        }
    }
    IEnumerator top_right()
    {
        yield return new WaitForSeconds(2.0f);
        if(SceneManager.GetActiveScene().name == NVRTask2 || GameObject.Find("TestManager").GetComponent<TestManager>().current_task == "VR Task 2" || SceneManager.GetActiveScene().name == NVRWTask2)
        {
            while(Vector3.Distance(Circle.transform.position, plus_X_plus_Y.transform.position) > close_enough)
            {
                Circle.transform.position = Vector3.MoveTowards(Circle.transform.position, plus_X_plus_Y.transform.position, speed * Time.deltaTime);
                yield return null;
            }
        }
        if (SceneManager.GetActiveScene().name == NVRTask3 || GameObject.Find("TestManager").GetComponent<TestManager>().current_task == "VR Task 3" || SceneManager.GetActiveScene().name == NVRWTask3)
        {
            Circle.transform.position = plus_X_plus_Y.transform.position;
        }
        if(Circle.transform.position == plus_X_plus_Y.transform.position)
        {
            yield return StartCoroutine(centre());
            move_to_bottom_left = true;
        } 
    }
    IEnumerator bottom_left()
    {
        yield return new WaitForSeconds(2.0f);
        if(SceneManager.GetActiveScene().name == NVRTask2 || GameObject.Find("TestManager").GetComponent<TestManager>().current_task == "VR Task 2" || SceneManager.GetActiveScene().name == NVRWTask2)
        {
            while(Vector3.Distance(Circle.transform.position, minus_X_minus_Y.transform.position) > close_enough)
            {
                Circle.transform.position = Vector3.MoveTowards(Circle.transform.position, minus_X_minus_Y.transform.position, speed * Time.deltaTime);
                yield return null;
            }
        }
        if (SceneManager.GetActiveScene().name == NVRTask3 || GameObject.Find("TestManager").GetComponent<TestManager>().current_task == "VR Task 3" || SceneManager.GetActiveScene().name == NVRWTask3)
        {
            Circle.transform.position = minus_X_minus_Y.transform.position;
        }
        if(Circle.transform.position == minus_X_minus_Y.transform.position)
        {
            finished = true;
            yield return StartCoroutine(centre());
        }
    }
    void FixedUpdate()
    {
        if (move_to_right == true)
        {
            StartCoroutine(right());
            move_to_right = false;
        }
        if (move_to_left == true)
        {
            StartCoroutine(left());
            move_to_left = false;
        }
        if (move_to_top == true)
        {
            StartCoroutine(top());
            move_to_top = false;
        }
        if (move_to_bottom == true)
        {
            StartCoroutine(bottom());
            move_to_bottom = false;
        }
        if (move_to_top_left == true)
        {
            StartCoroutine(top_left());
            move_to_top_left = false;
        }
        if (move_to_bottom_right == true)
        {
            StartCoroutine(bottom_right());
            move_to_bottom_right = false;

        }
        if (move_to_top_right == true)
        {
            StartCoroutine(top_right());
            move_to_top_right = false;
        }
        if (move_to_bottom_left == true)
        {
            StartCoroutine(bottom_left());
            move_to_bottom_left = false;
        }
    }
}