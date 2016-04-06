using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VR;

public class Geri : MonoBehaviour
{
    public float Minimum = 0.0f;//The minimum opacity (invisible).
    public float Maximum = 1.0f;//The maximum opacity
    private float Duration = 60.0f;//The fixed duration that the target image will reach full clarity.

    private float StartTime = 0.0f;
    private float time = 0.0f;
    public SpriteRenderer Target;

    private GameObject target;//The same target as the one we have assigned to the SpriteRenderer, but this GameObject will be used to set it to a random position when the task starts.

    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "NVR Task 1" || SceneManager.GetActiveScene().name == "NVRW Task 1")//Disables VR if the current scene in Non-VR or Non-VR Weighted.
        {
            VRSettings.enabled = false;
        }       
    }

    void Start ()
    {
        target = GameObject.Find("Target");//Assigns our target to our GameObject.
        StartTime = Time.time;

        Target.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);

        if (SceneManager.GetActiveScene().name == "NVR Task 1" || SceneManager.GetActiveScene().name == "NVRW Task 1")//Disables VR if the current scene in Non-VR or Non-VR Weighted.
        {
            if(SceneManager.GetActiveScene().name == "NVR Task 1")//Sets the name of the current task the loaded scene.
            {
                GameObject.Find("TestManager").GetComponent<TestManager>().CurrentTask = "NVR Task 1";
            }
            if (SceneManager.GetActiveScene().name == "NVRW Task 1")
            {
                GameObject.Find("TestManager").GetComponent<TestManager>().CurrentTask = "NVRW Task 1";
            }
            target.transform.position = new Vector3(Random.Range(-25.0f, 25.0f), Random.Range(-15.0f, 15.0f), -1.0f);//Sets the position of the target to a random position within the current viewport.
            VRSettings.enabled = false;
        }
        if (GameObject.Find("TestManager").GetComponent<TestManager>().CurrentTask == "VR Task 1")//Enables VR and sets position of the target within the VR scene.
        {
            transform.localPosition = new Vector3(Random.Range(-1.1f, 1.1f), Random.Range(2.0f, 3.0f), -3.5f);
            VRSettings.enabled = true;
        }
    }	
	void Update ()
    {
        time = (Time.time - StartTime) / Duration;
        Target.color = new Color(1.0f, 1.0f, 1.0f, Mathf.SmoothStep(Minimum, Maximum, time));//Increases the visibility of the sprite over the duration of the "Minimum" to "Maximum" opacity level. 
	}
}