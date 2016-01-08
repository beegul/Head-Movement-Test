using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.VR;

public class LoPresti : MonoBehaviour
{
    //Declarations of waypoints.
    private GameObject Circle;//Moving target.
    private GameObject plus_X;
    private GameObject plus_Y;
    private GameObject minus_X;
    private GameObject minus_Y;
    private GameObject minus_X_plus_Y;
    private GameObject minus_X_minus_Y;
    private GameObject plus_X_plus_Y;
    private GameObject plus_X_minus_Y;
    private GameObject Centre;

    //Declarations of movement bools.
    private bool move_to_right = true;
    private bool move_to_left = false;
    private bool move_to_top = false;
    private bool move_to_bottom = false;
    private bool move_to_top_left = false;
    private bool move_to_bottom_right = false;
    private bool move_to_top_right = false;
    private bool move_to_bottom_left = false;

    private LineRenderer line;
    public Material material;

    public float speed;//sets the movement speed of the circle.

    private bool run = false;

    void Awake()
    {
        //QualitySettings.vSyncCount = 0;//disable vsync.
        Application.targetFrameRate = 60;//lock fps.
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

        line = GetComponent<LineRenderer>();
        line.SetVertexCount(2);
        line.material = material;
        line.SetWidth(0.1f, 0.1f);

        if (SceneManager.GetActiveScene().name == "Test 2" || SceneManager.GetActiveScene().name == "Test 3")//disables VR.
        {
            VRSettings.enabled = false;
        }
        if (SceneManager.GetActiveScene().name == "Test 2VR" || SceneManager.GetActiveScene().name == "Test 3VR")//enables VR.
        {
            VRSettings.enabled = true;
        }
    }
    IEnumerator centre()
    {
        yield return new WaitForSeconds(2.0f);
        if (Circle.transform.position != Centre.transform.position)
        {
            if(SceneManager.GetActiveScene().name == "Test 2" || SceneManager.GetActiveScene().name == "Test 2VR")//checks the current scenes name.
            {
                Circle.transform.position = Vector3.MoveTowards(Circle.transform.position, Centre.transform.position, speed * Time.deltaTime);
            }
            if(SceneManager.GetActiveScene().name == "Test 3" || SceneManager.GetActiveScene().name == "Test 3VR")
            {
                Circle.transform.position = Centre.transform.position;
            }
        }
        yield return new WaitForSeconds(2.0f);
    }
    IEnumerator right()
    {
        yield return new WaitForSeconds(2.0f);
        if (SceneManager.GetActiveScene().name == "Test 2" || SceneManager.GetActiveScene().name == "Test 2VR")
        {
            Circle.transform.position = Vector3.MoveTowards(Circle.transform.position, plus_X.transform.position, speed * Time.deltaTime);
        }
        if (SceneManager.GetActiveScene().name == "Test 3" || SceneManager.GetActiveScene().name == "Test 3VR")
        {
            Circle.transform.position = plus_X.transform.position;
        }

        if (Circle.transform.position == plus_X.transform.position)
        {
            yield return new WaitForSeconds(2.0f);

            move_to_right = false;
            yield return StartCoroutine(centre());//executes this coroutine, when it has completed, continue the rest of this code.
            move_to_left = true;
        }
    }
    IEnumerator left()
    {
        yield return new WaitForSeconds(2.0f);
        if (SceneManager.GetActiveScene().name == "Test 2" || SceneManager.GetActiveScene().name == "Test 2VR")
        {
            Circle.transform.position = Vector3.MoveTowards(Circle.transform.position, minus_X.transform.position, speed * Time.deltaTime);
        }
        if (SceneManager.GetActiveScene().name == "Test 3" || SceneManager.GetActiveScene().name == "Test 3VR")
        {
            Circle.transform.position = minus_X.transform.position;
        }
        if (Circle.transform.position == minus_X.transform.position)
        {
            yield return new WaitForSeconds(2.0f);

            move_to_left = false;
            yield return StartCoroutine(centre());        
            move_to_top = true;
        }
    }
    IEnumerator top()
    {
        yield return new WaitForSeconds(2.0f);
        if (SceneManager.GetActiveScene().name == "Test 2" || SceneManager.GetActiveScene().name == "Test 2VR")
        {
            Circle.transform.position = Vector3.MoveTowards(Circle.transform.position, plus_Y.transform.position, speed * Time.deltaTime);
        }
        if (SceneManager.GetActiveScene().name == "Test 3" || SceneManager.GetActiveScene().name == "Test 3VR")
        {
            Circle.transform.position = plus_Y.transform.position;
        }
        if(Circle.transform.position == plus_Y.transform.position)
        {
            yield return new WaitForSeconds(2.0f);

            move_to_top = false;
            yield return StartCoroutine(centre());
            move_to_bottom = true;
        }
    }
    IEnumerator bottom()
    {
        yield return new WaitForSeconds(2.0f);
        if (SceneManager.GetActiveScene().name == "Test 2" || SceneManager.GetActiveScene().name == "Test 2VR")
        {
            Circle.transform.position = Vector3.MoveTowards(Circle.transform.position, minus_Y.transform.position, speed * Time.deltaTime);
        }
        if (SceneManager.GetActiveScene().name == "Test 3" || SceneManager.GetActiveScene().name == "Test 3VR")
        {
            Circle.transform.position = minus_Y.transform.position;
        }
        if (Circle.transform.position == minus_Y.transform.position)
        {
            yield return new WaitForSeconds(2.0f);

            move_to_bottom = false;
            yield return StartCoroutine(centre());
            move_to_top_left = true;
        }
    }
    IEnumerator top_left()
    {
        yield return new WaitForSeconds(2.0f);
        if (SceneManager.GetActiveScene().name == "Test 2" || SceneManager.GetActiveScene().name == "Test 2VR")
        {
            Circle.transform.position = Vector3.MoveTowards(Circle.transform.position, minus_X_plus_Y.transform.position, speed * Time.deltaTime);
        }
        if (SceneManager.GetActiveScene().name == "Test 3" || SceneManager.GetActiveScene().name == "Test 3VR")
        {
            Circle.transform.position = minus_X_plus_Y.transform.position;
        }
        if(Circle.transform.position == minus_X_plus_Y.transform.position)
        {
            yield return new WaitForSeconds(2.0f);

            move_to_top_left = false;
            yield return StartCoroutine(centre());
            move_to_bottom_right = true;
        }
    }
    IEnumerator bottom_right()
    {
        yield return new WaitForSeconds(2.0f);
        if (SceneManager.GetActiveScene().name == "Test 2" || SceneManager.GetActiveScene().name == "Test 2VR")
        {
            Circle.transform.position = Vector3.MoveTowards(Circle.transform.position, plus_X_minus_Y.transform.position, speed * Time.deltaTime);
        }
        if (SceneManager.GetActiveScene().name == "Test 3" || SceneManager.GetActiveScene().name == "Test 3VR")
        {
            Circle.transform.position = plus_X_minus_Y.transform.position;
        }
        if (Circle.transform.position == plus_X_minus_Y.transform.position)
        {
            yield return new WaitForSeconds(2.0f);

            move_to_bottom_right = false;
            yield return StartCoroutine(centre());
            move_to_top_right = true;
        }
    }
    IEnumerator top_right()
    {
        yield return new WaitForSeconds(2.0f);
        if (SceneManager.GetActiveScene().name == "Test 2" || SceneManager.GetActiveScene().name == "Test 2VR")
        {
            Circle.transform.position = Vector3.MoveTowards(Circle.transform.position, plus_X_plus_Y.transform.position, speed * Time.deltaTime);
        }
        if (SceneManager.GetActiveScene().name == "Test 3" || SceneManager.GetActiveScene().name == "Test 3VR")
        {
            Circle.transform.position = plus_X_plus_Y.transform.position;
        }
        if(Circle.transform.position == plus_X_plus_Y.transform.position)
        {
            yield return new WaitForSeconds(2.0f);

            move_to_top_right = false;
            yield return StartCoroutine(centre());
            move_to_bottom_left = true;
        } 
    }
    IEnumerator bottom_left()
    {
        yield return new WaitForSeconds(2.0f);
        if (SceneManager.GetActiveScene().name == "Test 2" || SceneManager.GetActiveScene().name == "Test 2VR")
        {
            Circle.transform.position = Vector3.MoveTowards(Circle.transform.position, minus_X_minus_Y.transform.position, speed * Time.deltaTime);
        }
        if (SceneManager.GetActiveScene().name == "Test 3" || SceneManager.GetActiveScene().name == "Test 3VR")
        {
            Circle.transform.position = minus_X_minus_Y.transform.position;
        }
        if(Circle.transform.position == minus_X_minus_Y.transform.position)
        {
            yield return new WaitForSeconds(2.0f);

            move_to_bottom_left = false;
            yield return StartCoroutine(centre());
            move_to_right = true;
        }
    }
    public void display_line(Vector3 destination)//function that displays the line between the centre and the passed in vector 3.
    {
        line.enabled = true;
        line.SetPosition(0, Centre.transform.position);//start
        line.SetPosition(1, destination);//finish
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            run = true;
        }
        if(run == true)
        {
            if (move_to_right == true)
            {
                StartCoroutine(right());
                if (SceneManager.GetActiveScene().name == "Test 2" || SceneManager.GetActiveScene().name == "Test 2VR")
                {
                    display_line(plus_X.transform.position);
                }
            }
            if (move_to_left == true)
            {
                StartCoroutine(left());
                if (SceneManager.GetActiveScene().name == "Test 2" || SceneManager.GetActiveScene().name == "Test 2VR")
                {
                    display_line(minus_X.transform.position);
                }
            }
            if (move_to_top == true)
            {
                StartCoroutine(top());
                if (SceneManager.GetActiveScene().name == "Test 2" || SceneManager.GetActiveScene().name == "Test 2VR")
                {
                    display_line(plus_Y.transform.position);
                }
            }
            if (move_to_bottom == true)
            {
                StartCoroutine(bottom());
                if (SceneManager.GetActiveScene().name == "Test 2" || SceneManager.GetActiveScene().name == "Test 2VR")
                {
                    display_line(minus_Y.transform.position);
                }
            }
            if (move_to_top_left)
            {
                StartCoroutine(top_left());
                if (SceneManager.GetActiveScene().name == "Test 2" || SceneManager.GetActiveScene().name == "Test 2VR")
                {
                    display_line(minus_X_plus_Y.transform.position);
                }
            }
            if (move_to_bottom_right)
            {
                StartCoroutine(bottom_right());
                if (SceneManager.GetActiveScene().name == "Test 2" || SceneManager.GetActiveScene().name == "Test 2VR")
                {
                    display_line(plus_X_minus_Y.transform.position);
                }
            }
            if (move_to_top_right)
            {
                StartCoroutine(top_right());
                if (SceneManager.GetActiveScene().name == "Test 2" || SceneManager.GetActiveScene().name == "Test 2VR")
                {
                    display_line(plus_X_plus_Y.transform.position);
                }
            }
            if (move_to_bottom_left)
            {
                StartCoroutine(bottom_left());
                if (SceneManager.GetActiveScene().name == "Test 2" || SceneManager.GetActiveScene().name == "Test 2VR")
                {
                    display_line(minus_X_minus_Y.transform.position);
                }
            }
        }
    }
}